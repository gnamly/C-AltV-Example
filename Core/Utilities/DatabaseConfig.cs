using System.Text.Json;
using AltV.Net;

namespace Core.Utilities;

public class DatabaseConfig
{
    public string? Host { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }
    public string? Database { get; set; }
    public int Port { get; set; }

    public static DatabaseConfig? ReadConfig()
    {
        DatabaseConfig? config = null;
        try
        {
            config = JsonSerializer.Deserialize<DatabaseConfig>(File.ReadAllText("DatabaseConfig.json"));
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

    public string GetConnectionString()
    {
        return $"Host={Host};Port={Port};Username={User};Password={Password};Database={Database}";
    }
}