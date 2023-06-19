using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using AltV.Net;
using AltV.Net.Data;
using Newtonsoft.Json;

namespace Core.DBEntities;

[Table(("items"))]
public class Item : IWritable
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public long Id { get; set; }

	[Column("item_data_id")]
	public int ItemDataId { get; set; }

	public ItemData ItemData { get; set; } = null!;

	[Column("inventory_id")]
	[ForeignKey("Inventory")]
	public Guid? InventoryId { get; set; }

	public Inventory? Inventory { get; set; }

	[NotMapped]
	public Position? WorldPosition
	{
		get => DatabaseWorldPosition != null ? JsonConvert.DeserializeObject<Vector3>(DatabaseWorldPosition) : null;
		set => DatabaseWorldPosition = value != null ? JsonConvert.SerializeObject(value) : null;
	}

	[Column("world_position", TypeName = "json")]
	public string? DatabaseWorldPosition { get; set; }

	[Column("content_inventory_id")]
	[ForeignKey("ContentInventory")]
	public Guid? ContentInventoryId { get; set; }

	public Inventory? ContentInventory { get; set; }

	[Column("meta", TypeName = "json")]
	public ItemMeta Meta { get; set; } = new ItemMeta();

	public void OnWrite(IMValueWriter writer)
	{
		writer.BeginObject();
		writer.Name("Id");
		writer.Value(Id);
		writer.Name("ItemData");
		writer.Value(ItemData);
		if (WorldPosition != null)
		{
			writer.Name("WorldPosition");
			writer.Value(WorldPosition.Value);
		}

		if (ContentInventory != null)
		{
			writer.Name("ContentInventory");
			writer.Value(ContentInventory);
		}

		writer.Name("Meta");
		writer.Value(Meta);
		writer.EndObject();
	}
}

public class ItemMeta : IWritable
{
	public void OnWrite(IMValueWriter writer)
	{
		writer.BeginObject();
		writer.EndObject();
	}
}