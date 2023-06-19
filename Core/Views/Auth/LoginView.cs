using System.Net;
using System.Net.Http.Json;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Autofac;
using Core.Entities;
using Core.Externals;
using Core.Services;
using Core.Utilities;
using Shared.Events.Systems;
using Shared.Events.Views;
using Shared.Events.Views.Auth;

namespace Core.Views.Auth;

public class LoginView : IScript
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(LoginView));

	private readonly CharacterService _characterService;

	public LoginView()
	{
		_characterService = Core.Container.Resolve<CharacterService>();

		Alt.OnClient<RpPlayer>(SystemEvents.WEBVIEW_READY, StartLogin);
		Alt.OnClient<RpPlayer, string, string, string?>(LoginEvents.LOGIN, Login);
		Alt.OnClient<RpPlayer>(SystemEvents.BEGIN_CONNECTION, OnBeginConnection);
		Alt.OnPlayerConnect += OnPlayerConnect;
		Alt.OnPlayerDisconnect += OnPlayerDisconnect;
	}

	public void OnBeginConnection(RpPlayer player)
	{
		//Sets the clients Webview to use the deployed or local hosted
		var url = GameConfig.Default.UseDebugVue ? "http://localhost:3000" : "http://assets/webviews/index.html";
		player.Emit(SystemEvents.WEBVIEW_INFO, url);
	}

	public void OnPlayerConnect(IPlayer player, string reason)
	{
		if (player is null || !player.Exists)
		{
			throw new ArgumentNullException(nameof(player));
		}

		if (Core.Warmup)
		{
			player.Kick("Server startet, versuche es später erneut!");
			return;
		}
	}

	public void OnPlayerDisconnect(IPlayer basePlayer, string reason)
	{
		var player = (RpPlayer)basePlayer;
		if (player.Character != null)
		{
			_logger.Log(
				$"Disconnect | {player.Character.Name} | ID: ({player.Id}) | Character ID: {player.Character.Id} | Account: {player.Character.AccountId}");
		}
	}

	private void StartLogin(RpPlayer player)
	{
		//Skipping login Process and using a defined login ID (must be a Account ID that can be found in the DB
		if (GameConfig.Default.SkipLoginId != null && Alt.IsDebug)
		{
			_logger.Log($"Skipping Login for {player.Name}");
			//Accepting the ID as "logged in" and loading the account from that ID
			LoadAccount(player, GameConfig.Default.SkipLoginId);
			return;
		}

		_logger.Log($"Starting Login for {player.Name}");
		//Client must authenticate to a Account before we can proceed
		player.Emit(LoginEvents.OPEN);
	}

	//Client tries to Login via username + password and optional MFA
	private async void Login(RpPlayer player, string username, string password, string? mfaCode = null)
	{
		await using var scope = Core.Container.BeginLifetimeScope();
		var authService = scope.Resolve<AuthenticationService>();

		var auth = await authService.Login(username, password, mfaCode);

		if (auth.error)
		{
			player.Emit(LoginEvents.ERROR);
		}

		if (auth.requireMfa)
		{
			player.Emit(LoginEvents.REQUEST_MFA);
		}

		if (auth.user != null)
		{
			LoadAccount(player, auth.user.id);
		}
	}

	public async void LoadAccount(RpPlayer player, string acpId)
	{
		await player.Init(acpId);
		player.Emit(LoginEvents.CLOSE);
		_characterService.StartSelection(player);
	}
}