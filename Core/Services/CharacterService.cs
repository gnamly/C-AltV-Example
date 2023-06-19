using System.Text.Json;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using Autofac;
using Core.DBContext;
using Core.DBEntities;
using Core.DTOs.CharacterSelect;
using Core.Entities;
using Core.Systems;
using Core.Utilities;
using Core.Views;
using Core.Views.Character;
using NodaTime;
using Shared.DTOs.CharacterSelect;
using Shared.Events.Systems;
using Shared.Events.Views.Character;
using Shared.Models;

namespace Core.Services;

//TODO convert to non Singleton and add to DI
public class CharacterService
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(CharacterService));

	private CreatorView _creatorView = new();

	private readonly DiscordService _discordService;
	private readonly PlayerStateController _playerStateController;

	private readonly WorldController _worldController;
	private readonly InventoryService _inventoryService;

	public CharacterService(WorldController worldController, InventoryService inventoryService)
	{
		_worldController = worldController;
		_inventoryService = inventoryService;
		_discordService = Core.Container.Resolve<DiscordService>();
		_playerStateController = Core.Container.Resolve<PlayerStateController>();
	}

	public void StartSelection(RpPlayer player)
	{
		using var db = new AccountContext();
		if (player.Account == null)
		{
			_logger.LogError($"No Account set for player {player.Name}");
			return;
		}

		db.Attach(player.Account);
		db.Entry(player.Account).Collection(a => a.Characters).Load();
		player.Characters.Clear();
		player.Characters.AddRange(player.Account.Characters);

		_logger.Log(
			$"Player {player.Account.Id} has {player.Characters.Count} characters and a total limit of {player.Account.AllCharacterLimit}");
		if (player.Characters.Count == 0)
		{
			_logger.Log($"Start Character Creator for {player.Account.Id}");
			StartCreator(player);
			return;
		}

		if (player.Account!.AllCharacterLimit > 1)
		{
			//Go to Char selection
			_logger.Log($"Going to character selection for {player.Account.Id}");

			//TODO emit to client createSpinner

			ShowCharacter(player, player.Characters, 0);

			//TODO emit to client clear Spinner

			player.SetDateTime(1, 1, 2023, 15, 2, 0);
		}
		else
		{
			//Skip Selection and automatically select the existing character
			_logger.Log($"Direct character selection for {player.Account.Id} with {player.Characters[0].Name}");
			SelectCharacter(player, player.Characters[0]);
		}
	}

	public void ShowCharacter(RpPlayer player, List<Character> characters, int index)
	{
		var pos = new Position(-1354.4071044921875f, -1180.5870361328125f, 4.421841621398926f);
		player.Model = Alt.Hash("mp_m_freemode_01");
		player.Spawn(pos);

		player.Character = characters[index];
		player.SyncAppearance();
		//TODO sync clothing

		player.Visible = true;
		player.SetPosition(pos);
		player.Rotation = new Rotation(0, 0, 2.6257832050323486f);
		player.SetFrozen(true);

		player.FadeScreenFromBlack(2000);

		var selectItems = new List<CharacterSelectWriteableItem>();
		foreach (var character in characters)
		{
			selectItems.Add(new CharacterSelectWriteableItem(character));
		}

		player.Emit(CharacterSelectEvents.Update, selectItems, index, player.Account!.CharacterLimit, player.Account.PedLimit,
			player.Account.AnimalLimit);
	}

	public void StartCreator(RpPlayer player)
	{
		_creatorView.Show(player);
	}

	public async void SelectCharacter(RpPlayer player, Character character)
	{
		player.Character = character;
		player.lastPlayTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

		_logger.Log(
			$"Selected | {player.Character.Name} | ID: ({player.Id}) | Character ID: {player.Character.Id} | Account: {player.Character.AccountId}"
		);

		player.FadeScreenToBlack(200);
		player.Characters.Clear();

		player.Dimension = player.Character.Dimension;
		player.SetFrozen(true);

		await AltAsync.Do(() => player.SetPosition(player.Character.Position ?? GameConfig.Default.CharacterNewSpawnPos));

		// Synchronization
		await AltAsync.Do(() => { player.SyncAppearance(); });
		_worldController.SyncWeather(player);
		_worldController.SyncTime(player);
		_inventoryService.LoadPlayerEquipment(player);
		_inventoryService.SyncInventoryToPlayer(player);
		if (player.Equipment?.ArmorWest != null)
		{
			player.Armor = player.Equipment.ArmorWest.ItemData.ClothData!.Armor;
		}

		_playerStateController.InitPlayer(player);

		//TODO set controllers (commands, blibs, holograms) on character select

		//TODO interior override

		player.Emit(SystemEvents.TICKS_START);

		// Finish Selection
		player.Emit(CharacterSelectEvents.CLOSE);
		player.SetFrozen(false);
		player.Visible = true;
		player.HasFullySpawned = true;
		player.FadeScreenFromBlack(2000);
		_discordService.Log($"Player spawned with Character {player.Character!.PersoId} {player.Character!.Name}");
	}
}