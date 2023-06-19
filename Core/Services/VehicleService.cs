using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using Core.DBContext;
using Core.DBEntities;
using Core.Entities;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Shared.Events.Systems;

namespace Core.Services;

public class VehicleService
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(VehicleService));

	public VehicleService()
	{
		Alt.OnClient<RpPlayer, string, Position>(VehicleEvents.CREATE_VEHICLE, CreateUnregisteredVehicle);
	}

	public async void PopulateVehicles()
	{
		using var db = new VehicleContext();

		foreach (var dbVehicle in db.Vehicles.Include(v => v.VehicleData).ToList())
		{
			//Conditions for spawning Vehicles in the world
			//1. The Vehicle has a position and a rotation
			//2. The Vehicle is not in any interior
			//3. The Vehicle is lasted Used with in {config} days, otherwise auto port it to Depositary //TODO
			if (dbVehicle is { Position: not null, Rotation: not null, Interior: null })
			{
				var vehicle = await AltAsync.Do(() =>
					(RpVehicle)Alt.CreateVehicle(Alt.Hash(dbVehicle.VehicleData.Model), dbVehicle.Position.Value, dbVehicle.Rotation.Value));
				vehicle.SetVehicle(dbVehicle);
			}
		}
	}

	/// <summary>
	/// This creates a vehicle data entry for every vehicle that is not already in the db
	/// </summary>
	public void UpdateVehicleData()
	{
		int newData = 0;
		using var db = new VehicleContext();
		foreach (uint hash in Enum.GetValues(typeof(AltV.Net.Enums.VehicleModel)))
		{
			var model = Alt.GetVehicleModelInfo(hash);
			var data = db.VehicleData.FirstOrDefault(data => data.Model == model.Title);
			if (data == null)
			{
				//must be added
				db.Add(new VehicleData
				{
					Model = model.Title,
					BaseMaxFuel = 50,
					Class = model.Type.ToString(),
					Name = char.ToUpper(model.Title[0]) + model.Title.Substring(1),
				});
				newData++;
			}
		}

		db.SaveChanges();
		_logger.Log($"Created {newData} new Vehicle Data in the DB");
	}

	private async void CreateUnregisteredVehicle(RpPlayer player, string model, Position pos)
	{
		var modelHash = Alt.Hash(model);
		var vehicle = await AltAsync.Do(() => (RpVehicle)Alt.CreateVehicle(modelHash, pos, Rotation.Zero));
		await using var db = new VehicleContext();
		var entity = CreateEntity(vehicle);
		if (entity != null)
		{
			db.Add(entity);
			vehicle.SetVehicle(entity);
			vehicle.UpdateState(false);
			await db.SaveChangesAsync();
		}
		else vehicle.Destroy();
	}

	private Vehicle? CreateEntity(RpVehicle vehicle)
	{
		var data = GetData(Alt.GetVehicleModelInfo(vehicle.Model).Title);
		if (data == null)
		{
			_logger.LogError("Tried to create a vehicle entity, but the vehicles model can not be found in the data");
			return null;
		}

		return new Vehicle
		{
			Position = vehicle.Position,
			Rotation = vehicle.Rotation,
			Plate = data.Model, //Initial the plate is the vehicle name
			Dirt = vehicle.DirtLevel,
			VehicleDataId = data.Id,
			Fuel = data.BaseMaxFuel,
		};
	}

	private VehicleData? GetData(string model)
	{
		using var db = new VehicleContext();
		return db.VehicleData.SingleOrDefault(data => data.Model == model);
	}
}