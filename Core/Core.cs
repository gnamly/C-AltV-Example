using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Autofac;
using Core.DBContext;
using Core.Entities;
using Core.Externals;
using Core.Services;
using Core.Systems;
using Core.Utilities;
using PlayerFactory = Core.Factories.PlayerFactory;
using VehicleFactory = Core.Factories.VehicleFactory;

namespace Core;

internal class Core : AsyncResource
{
	private readonly ServiceLogger _logger = new(nameof(Core));

	public static bool Warmup { get; private set; }

	public static IContainer Container { get; private set; } = null!;

	#region Factories

	public override IEntityFactory<IPlayer> GetPlayerFactory()
	{
		return new PlayerFactory();
	}

	public override IEntityFactory<IVehicle> GetVehicleFactory()
	{
		return new VehicleFactory();
	}

	#endregion

	public Core() : base()
	{
		Warmup = true;
		Console.WriteLine("Registering Services to Container");
		var builder = new ContainerBuilder();
		//Register Services
		builder.RegisterType<AuthenticationService>().AsSelf();
		builder.RegisterType<DiscordService>().AsSelf();
		builder.RegisterType<InventoryService>().AsSelf().SingleInstance();
		builder.RegisterType<CharacterService>().AsSelf().SingleInstance();
		builder.RegisterType<VehicleService>().AsSelf().SingleInstance();

		//Register Systems
		builder.RegisterType<PlayerStateController>().AsSelf().SingleInstance();
		builder.RegisterType<WorldController>().AsSelf().SingleInstance();
		Container = builder.Build();
		Console.WriteLine("Container build successful");
	}

	public override void OnStart()
	{
		_logger.Log("Core Started");

		var vehicleService = Container.Resolve<VehicleService>();
		vehicleService.PopulateVehicles();
		vehicleService.UpdateVehicleData();

		Warmup = false;
		_logger.Log("Server Warmup complete. Now accepting connections");
		Container.Resolve<DiscordService>().Log("Server started", DiscordService.LogCondition.NotDebug);
	}

	public override void OnStop()
	{
		var playerCallback = new RpPlayerPlayerTickCallback();
		Alt.ForEachPlayers(playerCallback);
		var vehicleCallback = new RpVehicleStateUpdate();
		Alt.ForEachVehicles(vehicleCallback);
		using var db = new VehicleContext();
		db.SaveChanges();

		_logger.Log("Core Stopped");
	}
}