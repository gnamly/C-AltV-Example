using System.ComponentModel.DataAnnotations;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Data;
using AltV.Net.Enums;
using Core.DBContext;
using Core.DBEntities;
using Core.Utilities;

namespace Core.Entities;

public class RpVehicle : AsyncVehicle, IAsyncConvertible<RpVehicle>
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(RpVehicle));

	public Vehicle? VehicleEntity { get; private set; }

	public RpVehicle(ICore core, uint model, Position position, Rotation rotation) : base(core, model, position, rotation)
	{
	}

	public RpVehicle(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
	{
	}

	public new RpVehicle ToAsync(IAsyncContext _)
	{
		return this;
	}

	public void SetVehicle(Vehicle vehicle)
	{
		VehicleEntity = vehicle;
		NumberplateText = VehicleEntity.Plate;
		DirtLevel = VehicleEntity.Dirt;
		ApplyDamage(VehicleEntity.Damage);
		//TODO set tuning
	}

	public void UpdateState(bool saveChanges = true)
	{
		if (VehicleEntity == null) return;
		VehicleEntity.Position = Position;
		VehicleEntity.Rotation = Rotation;

		VehicleEntity.Dirt = DirtLevel;
		VehicleEntity.Damage.SetDamage(this);
		if (saveChanges)
		{
			using var db = new VehicleContext();
			db.Update(VehicleEntity);
			db.SaveChanges();
		}
	}

	private void ApplyDamage(VehicleDamage damage)
	{
		foreach (var (vehiclePart, value) in damage.Parts)
		{
			SetPartDamageLevel((byte)vehiclePart, value);
		}

		foreach (var (bulletHole, value) in damage.PartBulletHoles)
		{
			SetPartBulletHoles((byte)bulletHole, value);
		}

		for (byte i = 0; i < damage.Windows.Count; i++)
		{
			SetWindowDamaged(i, damage.Windows[i]);
		}

		foreach (var (bumper, value) in damage.Bumpers)
		{
			SetBumperDamageLevel((byte)bumper, value);
		}

		foreach (var (wheel, health) in damage.Wheels)
		{
			SetWheelHealth((byte)wheel, health);
		}

		for (byte i = 0; i < damage.Lights.Count; i++)
		{
			SetLightDamaged(i, damage.Lights[i]);
		}

		for (byte i = 0; i < damage.SpecialLights.Count; i++)
		{
			SetSpecialLightDamaged(i, damage.SpecialLights[i]);
		}

		BodyHealth = damage.BodyHealth;
		BodyAdditionalHealth = damage.BodyAdditionalHealth;
		EngineHealth = damage.EngineHealth;
		PetrolTankHealth = damage.PetrolTankHealth;
	}
}