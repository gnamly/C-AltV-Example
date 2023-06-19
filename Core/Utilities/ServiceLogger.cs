using AltV.Net;

namespace Core.Utilities;

public class ServiceLogger
{
	public static ServiceLogger Global { get; } = new ServiceLogger("global");

	private readonly string _context;

	public ServiceLogger(string context)
	{
		_context = context;
	}

	public void Log(string message)
	{
		Alt.Log($"[{_context}] {message}");
	}

	public void LogInfo(string message)
	{
		Alt.LogInfo($"[{_context}] {message}");
	}

	public void LogWarning(string message)
	{
		Alt.LogWarning($"[{_context}] {message}");
	}

	public void LogError(string message)
	{
		Alt.LogError($"[{_context}] {message}");
	}

	public void LogDebug(string message)
	{
		Alt.LogDebug($"[{_context}] {message}");
	}
}