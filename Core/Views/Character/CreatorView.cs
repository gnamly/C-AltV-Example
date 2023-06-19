using AltV.Net;
using Core.Entities;
using Core.Utilities;
using Shared.Events.Views.Character;

namespace Core.Views.Character;

public class CreatorView
{
	public CreatorView()
	{
		// Alt.OnClient<CaliPlayer>(CharacterCreatorEvents.DONE, Done);
	}

	public void Show(RpPlayer player)
	{
		player.Visible = false;
		player.SetFrozen(true);
		player.SetPosition(GameConfig.Default.CharacterCreatorPos);
		player.Emit(CharacterCreatorEvents.OPEN, GameConfig.Default.CharacterCreatorPos,
			GameConfig.Default.CharacterCreatorRot);
	}

	private void Done(RpPlayer player)
	{
	}
}