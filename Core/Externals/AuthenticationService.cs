using System.Net;
using System.Net.Http.Json;
using Core.Utilities;
using Core.Views.Auth;

namespace Core.Externals;

public class AuthenticationService
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(AuthenticationService));

	public struct LoginResult
	{
		public bool error;
		public bool requireMfa;
		public LoginResponse? user;
	}

	public async Task<LoginResult> Login(string username, string password, string? mfaCode = null)
	{
		var client = new HttpClient();
		var values = new Dictionary<string, string>
		{
			{ "username", username },
			{ "password", password }
		};
		if (mfaCode != null)
		{
			values.Add("mfaCode", mfaCode);
		}

		var content = new FormUrlEncodedContent(values);
		client.DefaultRequestHeaders.Clear();
		client.DefaultRequestHeaders.Add("Authorization", "Bearer oqwihgqwjbevqurewv"); //TODO add Auth Token from config

		var response = await client.PostAsync("http://81.169.223.162:3001/api/auth/verify-login", content); //TODO add url from config

		if (response.StatusCode != HttpStatusCode.OK)
		{
			var msg = await response.Content.ReadAsStringAsync();
			if (response.StatusCode != HttpStatusCode.BadRequest || msg != "Wrong credentials provided")
			{
				_logger.LogError($"Error on Login Validation Request with status {response.StatusCode}: {msg}");
			}

			return new LoginResult
			{
				error = true
			};
		}

		var user = await response.Content.ReadFromJsonAsync<LoginResponse>();
		if (user != null)
		{
			_logger.Log(user.ToString());
			if (user.requireMfa)
			{
				return new LoginResult
				{
					error = false,
					requireMfa = true,
				};
			}

			return new LoginResult
			{
				error = false,
				requireMfa = false,
				user = user,
			};
		}

		return new LoginResult
		{
			error = true
		};
		;
	}
}