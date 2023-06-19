using System.Numerics;
using System.Text.Json;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Data;
using Core.DBContext;
using Core.DBEntities;
using Core.Utilities;
using Shared.Events.Systems;
using Shared.Models;

namespace Core.Entities;

public class RpPlayer : AsyncPlayer, IAsyncConvertible<RpPlayer>
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(RpPlayer));

	public long nextPingTime;
	public long lastPlayTime;

	public string LogName
	{
		get
		{
			if (Character != null)
			{
				return Character.Name;
			}

			return Name;
		}
	}

	public bool IsLoggedIn { get; private set; }
	public bool HasFullySpawned { get; set; }

	public Account? Account { get; private set; }

	public Character? Character { get; set; }

	/// <summary>
	/// Only set during character selection, always empty afterwards
	/// </summary>
	public List<Character> Characters { get; } = new List<Character>();

	public Equipment? Equipment { get; set; }
	public Toolbar? Toolbar { get; set; }

	public bool HasModel { get; private set; }

	public RpPlayer(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
	{
	}

	public new RpPlayer ToAsync(IAsyncContext _)
	{
		return this;
	}

	public async Task Init(string acpId)
	{
		await using var db = new AccountContext();
		Account = db.GetOrCreateAccount(acpId, this);

		await AltAsync.Do(() =>
		{
			Dimension = 0; //TODO set to private dim
			SetPosition(GameConfig.Default.CharacterNewSpawnPos);
		});

		IsLoggedIn = true;

		_logger.Log($"{SocialClubId} is initialized");
	}

	public void SetFrozen(bool value)
	{
		Frozen = value;
	}

	public void SetPosition(Position pos)
	{
		if (!HasModel)
		{
			HasModel = true;
			Spawn(pos, 0);
			Model = Alt.Hash("mp_m_freemode_01");
		}

		Position = pos;
		_logger.Log($"Set {LogName} Position to {pos}");
	}

	public void SyncAppearance()
	{
		if (Character == null || Character.Appearance == null)
		{
			_logger.LogWarning($"{(Character != null ? Character.Name : Name)} is missing appearance or character -> Skipping appearance sync");
			return;
		}

		var appearance = Character.Appearance;
		if (appearance.Sex == 0)
		{
			Model = Alt.Hash("mp_f_freemode_01");
		}
		else
		{
			Model = Alt.Hash("mp_m_freemode_01");
		}

		//Set Face
		ClearBloodDamage();
		SetHeadBlendData(appearance.FaceMother, appearance.FaceFather, 0, appearance.SkinMother, appearance.SkinFather, 0, appearance.FaceMix,
			appearance.SkinMix, 0);

		//Facial Features
		for (byte i = 0; i < appearance.Structure.Length; i++)
		{
			SetFaceFeature(i, appearance.Structure[i]);
		}

		//Overlay Features - Without Colors
		for (byte i = 0; i < appearance.OpacityOverlays.Length; i++)
		{
			var overlay = appearance.OpacityOverlays[i];
			SetHeadOverlay(overlay.Id, overlay.Value, overlay.Opacity);
		}

		if (appearance.HairOverlay != null)
		{
			var decorationsToSync = new HairOverlayData[]
			{
				appearance.HairOverlay!
			};
			// Emit(CharacterEvents.SET_PLAYER_DECORATIONS, decorationsToSync); //TODO fix
		}

		// Hair - Supports DLC
		if (appearance.HairDlc == null || appearance.HairDlc.Value == 0)
		{
			SetClothes(2, appearance.Hair, 0, 0);
		}
		else
		{
			SetDlcClothes(appearance.HairDlc.Value, 2, (byte)appearance.Hair, 0, 0);
		}

		HairColor = appearance.HairColor1;
		HairHighlightColor = appearance.HairColor2;

		// Facial Hair
		SetHeadOverlay(1, appearance.FacialHair, appearance.FacialHairOpacity);
		SetHeadOverlayColor(1, 1, appearance.FacialHairColor1, appearance.FacialHairColor1);

		// Chest Hair
		if (appearance.ChestHair.HasValue)
		{
			SetHeadOverlay(10, appearance.ChestHair.Value, appearance.ChestHairOpacity);
			SetHeadOverlayColor(10, 1, appearance.ChestHairColor1, appearance.ChestHairColor1);
		}

		// Eyebrows
		SetHeadOverlay(2, appearance.Eyebrows, appearance.EyebrowsOpacity);
		SetHeadOverlayColor(2, 1, appearance.EyebrowsColor1, appearance.EyebrowsColor1);

		// Decor
		for (int i = 0; i < appearance.ColorOverlays.Length; i++)
		{
			var overlay = appearance.ColorOverlays[i];
			var color2 = overlay.Color2.HasValue ? overlay.Color2.Value : overlay.Color1;

			SetHeadOverlay(overlay.Id, overlay.Value, overlay.Opacity);
			SetHeadOverlayColor(overlay.Id, 1, overlay.Color1, color2);
		}

		// Eyes
		SetEyeColor(appearance.Eyes);
	}

	public void FadeScreenFromBlack(float time)
	{
		Emit(SystemEvents.SCREEN_FADE_FROM_BLACK, time);
	}

	public void FadeScreenToBlack(float time)
	{
		Emit(SystemEvents.SCREEN_FADE_TO_BLACK, time);
	}
}