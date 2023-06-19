using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;
using Shared.Models;

namespace Core.DBEntities;

[Table("accounts")]
public class Account : IAccount
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }

	[Column("acp_id")]
	public string AcpId { get; set; } = "";

	[Column("ips")]
	public string[]? ips { get; set; }

	[Column("hardware")]
	public ulong[]? hardware { get; set; }

	[Column("last_login", TypeName = "timestamptz")]
	public ZonedDateTime LastLogin { get; set; }

	[Column("banned")]
	public bool Banned { get; set; }

	[Column("bann_reason")]
	public string? BannReason { get; set; }

	[Column("character_limit")]
	public short CharacterLimit { get; set; }

	[Column("ped_limit")]
	public short PedLimit { get; set; }

	[Column("animal_limit")]
	public short AnimalLimit { get; set; }

	public List<Character> Characters { get; set; } = new();

	[NotMapped]
	public int AllCharacterLimit => CharacterLimit + PedLimit + AnimalLimit;
}