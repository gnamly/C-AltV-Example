namespace Shared.Events.Views.Auth;

public static class LoginEvents
{
	public static string PAGE_NAME => "Login";
	public static string OPEN => $"{PAGE_NAME}:Open";
	public static string CLOSE => $"{PAGE_NAME}:Close";
	public static string LOGIN => $"{PAGE_NAME}:Login";
	public static string REQUEST_MFA => $"{PAGE_NAME}:Request_Mfa";
	public static string ERROR => $"{PAGE_NAME}:Error";
}