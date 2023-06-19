using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DBEntities;

[Table("vehicle_data")]
public class VehicleData
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public int Id { get; set; }

	[Column("name")]
	public string Name { get; set; } = "";

	[Column("model")]
	public string Model { get; set; } = "";

	[Column("class")]
	public string Class { get; set; } = "";

	[Column("base_max_fuel")]
	public int BaseMaxFuel { get; set; }
}