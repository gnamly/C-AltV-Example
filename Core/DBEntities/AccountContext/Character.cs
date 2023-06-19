using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using AltV.Net;
using Core.DBEntities;
using Newtonsoft.Json;
using Shared.Models;

namespace Core.DBEntities;

[Table("characters")]
public class Character : ICharacter
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }

	[Column("perso_id")]
	[Required]
	public int PersoId { get; set; }

	[Column("account_id")]
	public Guid AccountId { get; set; }

	public Account? Account { get; set; }

	[Column("dimension")]
	public int Dimension { get; set; }

	[NotMapped]
	public Vector3? Position
	{
		get => JsonConvert.DeserializeObject<Vector3>(DatabasePosition);
		set => DatabasePosition = JsonConvert.SerializeObject(value);
	}

	[Column("position", TypeName = "json")]
	public string DatabasePosition { get; set; } = "";

	[Column("first_name")]
	public string FirstName { get; set; }

	[Column("last_name")]
	public string LastName { get; set; }

	[NotMapped] public string Name => $"{FirstName} {LastName}";

	[Column("health")]
	public ushort? Health { get; set; }

	[Column("is_dead")]
	public bool IsDead { get; set; }

	[Column("hours")]
	public float Hours { get; set; }

	[Column("info", TypeName = "json")]
	public CharacterInfo Info { get; set; }

	[Column("appearance", TypeName = "json")]
	public Appearance? Appearance { get; set; }

	public List<Vehicle> Vehicles { get; set; } = new();

	[Column("equipment_id")]
	public Guid EquipmentId { get; set; }

	public Equipment Equipment { get; set; }

	[Column("toolbar_id")]
	public Guid ToolbarId { get; set; }

	public Toolbar Toolbar { get; set; }

	public Character()
	{
		FirstName = "";
		LastName = "";
		Position = Vector3.Zero;
		Health = 199;
		IsDead = false;
		Hours = 0;
		Info = new CharacterInfo();
		Equipment = new Equipment();
		Toolbar = new Toolbar();
	}

	public static void WriteAppearance(Appearance appearance, IMValueWriter writer)
	{
		writer.Name("Appearance");
		writer.BeginObject();
		writer.Name("Sex");
		writer.Value(appearance.Sex);
		writer.Name("FaceFather");
		writer.Value(appearance.FaceFather);
		writer.Name("FaceMother");
		writer.Value(appearance.FaceMother);
		writer.Name("SkinFather");
		writer.Value(appearance.SkinFather);
		writer.Name("SkinMother");
		writer.Value(appearance.SkinMother);
		writer.Name("FaceMix");
		writer.Value(appearance.FaceMix);
		writer.Name("SkinMix");
		writer.Value(appearance.SkinMix);
		writer.Name("Structure");
		writer.BeginArray();
		foreach (var structure in appearance.Structure)
		{
			writer.Value(structure);
		}

		writer.EndArray();
		writer.Name("Hair");
		writer.Value(appearance.Hair);
		if (appearance.HairDlc.HasValue)
		{
			writer.Name("HairDlc");
			writer.Value(appearance.HairDlc.Value);
		}

		writer.Name("HairColor1");
		writer.Value(appearance.HairColor1);
		writer.Name("HairColor2");
		writer.Value(appearance.HairColor2);
		if (appearance.HairOverlay != null)
		{
			writer.Name("HairOverlay");
			writer.BeginObject();
			writer.Name("Overlay");
			writer.Value(appearance.HairOverlay.Overlay);
			writer.Name("Collection");
			writer.Value(appearance.HairOverlay.Collection);
			writer.EndObject();
		}

		writer.Name("FacialHair");
		writer.Value(appearance.FacialHair);
		writer.Name("FacialHairColor1");
		writer.Value(appearance.FacialHairColor1);
		writer.Name("FacialHairOpacity");
		writer.Value(appearance.FacialHairOpacity);
		writer.Name("Eyebrows");
		writer.Value(appearance.Eyebrows);
		writer.Name("EyebrowsColor1");
		writer.Value(appearance.EyebrowsColor1);
		writer.Name("EyebrowsOpacity");
		writer.Value(appearance.EyebrowsOpacity);
		if (appearance.ChestHair.HasValue)
		{
			writer.Name("ChestHair");
			writer.Value(appearance.ChestHair.Value);
		}

		writer.Name("ChestHairColor1");
		writer.Value(appearance.ChestHairColor1);
		writer.Name("ChestHairOpacity");
		writer.Value(appearance.ChestHairOpacity);
		writer.Name("Eyes");
		writer.Value(appearance.Eyes);
		writer.Name("OpacityOverlays");
		writer.BeginArray();
		foreach (var overlay in appearance.OpacityOverlays)
		{
			writer.BeginObject();
			writer.Name("Id");
			writer.Value(overlay.Id);
			writer.Name("Value");
			writer.Value(overlay.Value);
			writer.Name("Opacity");
			writer.Value(overlay.Opacity);
			writer.Name("Collection");
			writer.Value(overlay.Collection);
			writer.Name("Overlay");
			writer.Value(overlay.Overlay);
			writer.EndObject();
		}

		writer.EndArray();
		writer.Name("ColorOverlays");
		writer.BeginArray();
		foreach (var overlay in appearance.ColorOverlays)
		{
			writer.BeginObject();
			writer.Name("Id");
			writer.Value(overlay.Id);
			writer.Name("Value");
			writer.Value(overlay.Value);
			writer.Name("Color1");
			writer.Value(overlay.Color1);
			if (overlay.Color2.HasValue)
			{
				writer.Name("Color2");
				writer.Value(overlay.Color2.Value);
			}

			writer.Name("Opacity");
			writer.Value(overlay.Opacity);
			writer.EndObject();
		}

		writer.EndArray();
		writer.EndObject();
	}

	public static void WriteInfo(CharacterInfo info, IMValueWriter writer)
	{
		writer.Name("Info");
		writer.BeginObject();
		writer.Name("Gender");
		writer.Value(info.Gender);
		// writer.Name("Birth");
		// writer.Value(info.Birth.ToString());
		// writer.Name("Age");
		// writer.BeginObject();
		// writer.Name("Years");
		// writer.Value(info.Age.Years);
		// writer.Name("Months");
		// writer.Value(info.Age.Months);
		// writer.Name("Days");
		// writer.Value(info.Age.Days);
		// writer.EndObject();
		writer.EndObject();
	}
}