// ReSharper disable InconsistentNaming
namespace Core.Views.Auth;

public class LoginResponse
{
	public string id { get; set; } = "";
	public string username { get; set; } = "";
	public string email { get; set; } = "";
	public bool requireMfa { get; set; } = false;

	public override string ToString()
	{
		string result = $"Response: requires mfa? {requireMfa}";
		if (!requireMfa)
		{
			result += $" userId: {id} username: {username} email: {email}";
		}

		return result;
	}
}