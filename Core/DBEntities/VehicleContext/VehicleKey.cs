using System.ComponentModel.DataAnnotations.Schema;
using Core.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace Core.DBEntities;

[Table("vehicle_keys")]
public class VehicleKey
{
	// [Column("character_id")]
	// public Guid CharacterId { get; set; }

	// [Column("vehicle_id")]
	// public Guid VehicleId { get; set; }

	// public Character Character { get; set; } = null!;
	// public Vehicle Vehicle { get; set; } = null!;

	[Column("is_primary")]
	public bool IsPrimary { get; set; }
}