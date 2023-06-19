using System.Collections.Specialized;
using System.Net.Http.Json;
using AltV.Net;
using Autofac;
using Core.Utilities;
using CSharpDiscordWebhook.NET.Discord;

namespace Core.Services;

public class DiscordService
{
	//See https://github.com/N4T4NM/CSharpDiscordWebhook for usage of Webhook

	public enum LogCondition
	{
		None = 0,
		Debug,
		NotDebug
	}

	public async void Log(string msg, LogCondition condition = LogCondition.None)
	{
		if ((condition == LogCondition.NotDebug && Alt.IsDebug) || (condition == LogCondition.Debug && !Alt.IsDebug))
		{
			return;
		}

		DiscordWebhook hook = new DiscordWebhook();
		hook.Uri = new Uri(GameConfig.Default.DiscordLogHook);

		var serverName = Alt.GetServerConfig().Get("name").GetString();

		DiscordMessage message = new DiscordMessage();
		message.Content = $"[{serverName}] {msg}";
		message.Username = "Husky";

		await hook.SendAsync(message);
	}
}