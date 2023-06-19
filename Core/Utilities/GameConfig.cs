using AltV.Net;
using AltV.Net.Data;
using Newtonsoft.Json;
using JsonException = System.Text.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Utilities;

public struct GameConfigData
{
	public string DiscordLogHook { get; set; }
	public string? SkipLoginId { get; set; }
	public bool UseDebugVue { get; set; }
	public Position CharacterSelectPos { get; set; }
	public float CharacterSelectRot { get; set; }
	public short PlayerMaxCharacterSlots { get; set; }
	public Position CharacterCreatorPos { get; set; }
	public float CharacterCreatorRot { get; set; }
	public Position CharacterNewSpawnPos { get; set; }
	public StreamConfigData StreamConfigData { get; set; }

	/// <summary>
	/// Time in milliseconds
	/// </summary>
	public int TimeBetweenVehicleUpdates { get; set; }

	/// <summary>
	/// Time in milliseconds
	/// </summary>
	public int TimeBetweenVehicleSaves { get; set; }

	//should be set to 60000 for real world time flow
	public int MsPerGameMinute { get; set; }
}

public struct StreamConfigData
{
	/// <summary>
	/// Time in milliseconds
	/// </summary>
	public int TimeBetweenUpdates { get; set; }
}

public class GameConfig
{
	private static GameConfigData? _defaultConfig;

	public static GameConfigData Default
	{
		get
		{
			if (_defaultConfig == null)
			{
				_defaultConfig = ReadConfig();
				return (GameConfigData)_defaultConfig;
			}
			else
			{
				return (GameConfigData)_defaultConfig;
			}
		}
	}

	private GameConfig()
	{
	}

	public static GameConfigData ReadConfig()
	{
		GameConfigData config;

		try
		{
			config = JsonConvert.DeserializeObject<GameConfigData>(File.ReadAllText("GameConfig.json"));
		}
		catch (FileNotFoundException e)
		{
			Alt.Log($"{e}");
			throw;
		}
		catch (FileLoadException e)
		{
			Alt.Log($"{e}");
			throw;
		}
		catch (JsonException e)
		{
			Alt.Log($"{e}");
			throw;
		}

		return config;
	}
}