using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net;

namespace Core.DBEntities;

public class Inventory : IWritable
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }

	public List<Item> Items { get; set; } = new();

	public Item InventoryItem { get; set; } = null!;

	public void OnWrite(IMValueWriter writer)
	{
		writer.BeginObject();
		writer.Name("Id");
		writer.Value(Id.ToString());
		writer.Name("Items");
		writer.Value(Items);
		writer.EndObject();
	}
}