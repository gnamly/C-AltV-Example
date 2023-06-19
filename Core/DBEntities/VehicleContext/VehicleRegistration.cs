using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DBEntities;
using NodaTime;

namespace Core.DBEntities;

[Table("vehicle_registrations")]
public class VehicleRegistration
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }

	[Column("vehicle_id")]
	public Guid VehicleId { get; set; }

	public Vehicle Vehicle { get; set; } = null!;

	[Column("plate")]
	public string Plate { get; set; } = "";

	[Column("registered_at", TypeName = "timestamptz")]
	public ZonedDateTime RegisteredAt { get; set; }

	[Column("registered_by_id")]
	public Guid RegisteredById { get; set; }

	public Character RegisteredBy { get; set; } = null!;

	[Column("unregistered_at", TypeName = "timestamptz")]
	public ZonedDateTime? UnregisteredAt { get; set; }
}