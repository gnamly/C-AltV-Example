using AltV.Net;
using AltV.Net.Async;
using Core.DBContext;
using Core.Entities;
using Core.Utilities;
using Shared.Events.Systems;
using Shared.Keys;

namespace Core.Systems;

public class PlayerStateController
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(PlayerStateController));

	private readonly int _timeBetweenPings = 4950;
	private readonly int _timeBetweenSaves = 60000;
	private long _nextSaveTime;

	public PlayerStateController()
	{
		Alt.OnClient<RpPlayer>(SystemEvents.PLAYER_TICK, (player) => HandlePlayerTick(player));
		Alt.OnPlayerDisconnect += (player, reason) =>
		{
			//Player is not spawned, so nothing to handle here
			if (((RpPlayer)player).Character == null)
			{
				return;
			}

			_logger.Log($"Saving player state while disconnect because of '{reason}'");
			HandlePlayerTick((RpPlayer)player, true);
		};
	}

	public void InitPlayer(RpPlayer player)
	{
		//TODO init health system for player
		player.Health = player.Character!.Health ?? 200;
	}

	public async void HandlePlayerTick(RpPlayer player, bool skipTimeCheck = false)
	{
		var now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
		if (now < player.nextPingTime && !skipTimeCheck)
		{
			return;
		}

		player.SetSyncedMetaData(PlayerSyncedMeta.PING, player.Ping);
		player.SetSyncedMetaData(PlayerSyncedMeta.POSITION, player.Position);
		player.nextPingTime = now + _timeBetweenPings;

		player.Character!.Position = player.Position;

		long deltaTime = now - player.lastPlayTime; //in milliseconds
		float deltaHours = (deltaTime / 1000f) / 60 / 60;
		player.Character!.Hours += deltaHours;
		player.lastPlayTime = now;
		using var db = new AccountContext();
		if (_nextSaveTime <= now || skipTimeCheck)
		{
			db.Characters.Update(player.Character);
			await db.SaveChangesAsync();
			_nextSaveTime = now + _timeBetweenSaves;
		}


		// Only the driver of the vehicle should be responsible for vehicle updates.
		if (player.Vehicle != null && player.Vehicle.Driver == player)
		{
			((RpVehicle)player.Vehicle).UpdateState();
		}
	}
}