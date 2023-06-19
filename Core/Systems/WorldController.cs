using AltV.Net.Enums;
using Core.Entities;
using Core.Utilities;
using NodaTime;
using Shared.Events.Systems;

namespace Core.Systems;

public class WorldController
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(WorldController));

	public void SyncTime(RpPlayer player)
	{
		player.Emit(WorldEvents.UpdateTime, GameConfig.Default.MsPerGameMinute);

		LocalDateTime now = LocalDateTime.FromDateTime(DateTime.Now);
		player.SetDateTime(now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second);
	}

	public void SyncWeather(RpPlayer player)
	{
		player.SetWeather((uint)GetCurrentWeather());
	}

	private WeatherType GetCurrentWeather()
	{
		return WeatherType.Clear;
	}

	private float GetTemperature(int dayOfYear, int hour)
	{
		return 24f;
	}
}