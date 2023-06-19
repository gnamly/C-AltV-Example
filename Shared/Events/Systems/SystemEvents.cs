namespace Shared.Events.Systems;

public static class SystemEvents
{
	#region Webview

	public static string WEBVIEW_INFO => "System:Webview:Info";
	public static string WEBVIEW_READY => "System:Webview:Ready";

	#endregion

	public static string BEGIN_CONNECTION => "System:Connection:Begin";

	public static string SCREEN_FADE_FROM_BLACK => "System:Screen:Fade:FromBlack";
	public static string SCREEN_FADE_TO_BLACK => "System:Screen:Fade:ToBlack";

	public static string TICKS_START => "System:Events:TickStart";
	public static string PLAYER_TICK => "System:Events:PlayerTick";
}