using NodaTime;

namespace Shared.Models;

public interface IAccount
{
	public Guid Id { get; set; }
	public string AcpId { get; set; }
	public ZonedDateTime LastLogin { get; set; }
	public bool Banned { get; set; }
	public string? BannReason { get; set; }
	public short CharacterLimit { get; set; }
	public short PedLimit { get; set; }
	public short AnimalLimit { get; set; }

	public int AllCharacterLimit => CharacterLimit + PedLimit + AnimalLimit;
}