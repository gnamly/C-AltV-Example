using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using AltV.Net.Data;
using AltV.Net.Enums;
using Core.DBEntities;
using Core.Entities;
using Newtonsoft.Json;
using NodaTime;

namespace Core.DBEntities;

[Table("vehicles")]
public class Vehicle
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }

	[Column("plate")]
	public string Plate { get; set; } = "";

	[Column("vehicle_data_id")]
	public int VehicleDataId { get; set; }

	public VehicleData VehicleData { get; set; } = null!;

	[NotMapped]
	public Vector3? Position
	{
		get => JsonConvert.DeserializeObject<Vector3>(DatabasePosition);
		set => DatabasePosition = JsonConvert.SerializeObject(value);
	}

	[Column("position", TypeName = "json")]
	public string DatabasePosition { get; set; } = "";

	[NotMapped]
	public Rotation? Rotation
	{
		get => JsonConvert.DeserializeObject<Rotation>(DatabaseRotation);
		set => DatabaseRotation = JsonConvert.SerializeObject(value);
	}

	[Column("rotation", TypeName = "json")]
	public string DatabaseRotation { get; set; } = "";

	[Column("fuel")]
	public int Fuel { get; set; }

	[Column("interior")]
	public string? Interior { get; set; }

	[Column("lastUsed", TypeName = "timestamptz")]
	public ZonedDateTime LastUsed { get; set; }

	[Column("damage", TypeName = "json")]
	public VehicleDamage Damage { get; set; } = new();

	[Column("tuning", TypeName = "json")]
	public VehicleTuning Tuning { get; set; } = new();

	[Column("dirt")]
	public byte Dirt { get; set; }

	public List<VehicleRegistration> Registrations { get; set; } = new();

	[Column("created_at", TypeName = "timestamptz")]
	public ZonedDateTime CreatedAt { get; set; }

	/// <summary>
	/// All Characters that own a key to this vehicle (primary + copies)
	/// </summary>
	public List<Character> Characters { get; } = new();
}

public class VehicleDamage
{
	public Dictionary<VehiclePart, byte> Parts { get; set; } = new Dictionary<VehiclePart, byte>();
	public Dictionary<VehiclePart, byte> PartBulletHoles { get; set; } = new Dictionary<VehiclePart, byte>();
	public Dictionary<VehicleBumper, byte> Bumpers { get; set; } = new Dictionary<VehicleBumper, byte>();
	public List<bool> Windows { get; set; } = new List<bool>();

	public Dictionary<VehicleWheel, float> Wheels { get; set; } = new Dictionary<VehicleWheel, float>();
	public List<bool> Lights { get; set; } = new List<bool>();
	public List<bool> SpecialLights { get; set; } = new List<bool>();

	public uint BodyHealth { get; set; } = 1000;
	public uint BodyAdditionalHealth { get; set; } = 1000;
	public int EngineHealth { get; set; } = 1000;
	public int PetrolTankHealth { get; set; } = 1000;

	public VehicleDamage()
	{
		foreach (var part in Enum.GetNames<VehiclePart>())
		{
			Parts.Add(Enum.Parse<VehiclePart>(part), 0);
		}

		foreach (var part in Enum.GetNames<VehiclePart>())
		{
			PartBulletHoles.Add(Enum.Parse<VehiclePart>(part), 0);
		}

		foreach (var part in Enum.GetNames<VehicleBumper>())
		{
			Bumpers.Add(Enum.Parse<VehicleBumper>(part), 0);
		}

		foreach (var part in Enum.GetNames<VehicleWheel>())
		{
			Wheels.Add(Enum.Parse<VehicleWheel>(part), 100);
		}

		for (int i = 0; i < 8; i++)
		{
			Windows.Add(false);
		}

		for (int i = 0; i < 8; i++)
		{
			Lights.Add(false);
			SpecialLights.Add(false);
		}
	}

	public void SetDamage(RpVehicle vehicle)
	{
		foreach (var part in Enum.GetValues<VehiclePart>())
		{
			Parts[(VehiclePart)part] = vehicle.GetPartDamageLevel((byte)part);
		}

		foreach (var part in Enum.GetValues<VehiclePart>())
		{
			PartBulletHoles[(VehiclePart)part] = vehicle.GetPartBulletHoles((byte)part);
		}

		for (byte i = 0; i < Windows.Count; i++)
		{
			Windows[i] = vehicle.IsWindowDamaged(i);
		}

		foreach (var part in Enum.GetValues<VehicleBumper>())
		{
			Bumpers[(VehicleBumper)part] = vehicle.GetBumperDamageLevel((byte)part);
		}

		foreach (var part in Enum.GetValues<VehicleWheel>())
		{
			Wheels[(VehicleWheel)part] = vehicle.GetWheelHealth((byte)part);
		}

		for (byte i = 0; i < Lights.Count; i++)
		{
			Lights[i] = vehicle.IsLightDamaged(i);
			SpecialLights[i] = vehicle.IsSpecialLightDamaged(i);
		}

		BodyHealth = vehicle.BodyHealth;
		BodyAdditionalHealth = vehicle.BodyAdditionalHealth;
		EngineHealth = vehicle.EngineHealth;
		PetrolTankHealth = vehicle.PetrolTankHealth;
	}
}

public class VehicleMod
{
	public int Id { get; set; }
	public int Value { get; set; }
}

public class VehicleTuning
{
	public int Modkit { get; set; }
	public List<VehicleMod> Mods { get; set; } = new List<VehicleMod>();
}