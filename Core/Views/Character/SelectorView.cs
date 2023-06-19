using AltV.Net;
using Autofac;
using Core.Entities;
using Core.Services;
using Core.Utilities;
using Shared.Events.Views.Character;

namespace Core.Views.Character;

public class SelectorView : IScript
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(SelectorView));

	private readonly CharacterService _characterService;

	public SelectorView()
	{
		_characterService = Core.Container.Resolve<CharacterService>();

		Alt.OnClient<RpPlayer, string>(CharacterSelectEvents.Update, UpdatePreview);
		Alt.OnClient<RpPlayer>(CharacterSelectEvents.NEW, CreateCharacter);
		Alt.OnClient<RpPlayer, string>(CharacterSelectEvents.SELECT, SelectCharacter);
	}

	public void UpdatePreview(RpPlayer player, string characterId)
	{
		var character = player.Characters.Find(character => character.Id.ToString() == characterId);
		if (character == null)
		{
			_logger.LogWarning($"Can not find character with ID {characterId} for player {player.Account!.Id}");
			return;
		}

		var index = player.Characters.IndexOf(character);
		_characterService.ShowCharacter(player, player.Characters, index);
	}

	public void CreateCharacter(RpPlayer player)
	{
	}

	public void SelectCharacter(RpPlayer player, string characterId)
	{
		_logger.Log($"Select Character {characterId}");
		var character = player.Characters.Find(character => character.Id.ToString() == characterId);
		if (character == null)
		{
			_logger.LogWarning($"Player {player.Account!.Id} tried to select character that can not be found {characterId}");
			return;
		}

		_characterService.SelectCharacter(player, character);
	}
}