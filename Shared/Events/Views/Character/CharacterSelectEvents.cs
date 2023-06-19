namespace Shared.Events.Views.Character;

public static class CharacterSelectEvents
{
	public static string PAGE_NAME => "CharacterSelector";
	public static string Update => $"{PAGE_NAME}:Update";
	public static string CLOSE => $"{PAGE_NAME}:Close";
	public static string NEW => $"{PAGE_NAME}:New";
	public static string SELECT => $"{PAGE_NAME}:Select";
}