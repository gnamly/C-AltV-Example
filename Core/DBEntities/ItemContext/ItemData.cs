using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using AltV.Net;

namespace Core.DBEntities;

public enum ItemSize
{
	Small,
	Medium,
	Large
}

public enum EquipmentSlot
{
	Hat,
	HeadExtra,
	Mask,
	Neck,
	Body,
	Legs,
	Feet,
	Hands,
	Watch,
	Back,
	ArmorWest,
	Phone,
	Tablet,
	Radio
}

public enum ToolbarSlot
{
	Primary,
	Secondary,
	SecondaryExtra,
	Melee,
	MeleeExtra,
	Gadget,
	Misc,
}

[Table("item_data")]
public class ItemData : IWritable
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public int Id { get; set; }

	[Column("item_name")]
	public string ItemName { get; set; } = "";

	[Column("dimension", TypeName = "json")]
	public Vector2 Dimension { get; set; }

	[Column("weight")]
	public float Weight { get; set; }

	[Column("size")]
	public ItemSize Size { get; set; }

	[Column("equipment")]
	public EquipmentSlot? EquipmentSlot { get; set; }

	[Column("toolbar")]
	public ToolbarSlot? ToolbarSlot { get; set; }

	[Column("water_use")]
	[DefaultValue(false)]
	public bool CanBeUsedInWater { get; set; }

	[Column("inventory_data", TypeName = "json")]
	public InventoryItemData? InventoryData { get; set; }

	[Column("cloth_data", TypeName = "json")]
	public ClothData? ClothData { get; set; }

	public void OnWrite(IMValueWriter writer)
	{
		writer.BeginObject();
		writer.Name("Id");
		writer.Value(Id);
		writer.Name("Dimension");
		writer.BeginObject();
		writer.Name("X");
		writer.Value(Dimension.X);
		writer.Name("Y");
		writer.Value(Dimension.Y);
		writer.EndObject();
		writer.Name("Weight");
		writer.Value(Weight);
		writer.Name("Size");
		writer.Value((ushort)Size);
		if (EquipmentSlot != null)
		{
			writer.Name("EquipmentSlot");
			writer.Value((ushort)EquipmentSlot);
		}

		if (ToolbarSlot != null)
		{
			writer.Name("ToolbarSlot");
			writer.Value((ushort)ToolbarSlot);
		}

		writer.Name("CanBeUsedInWater");
		writer.Value(CanBeUsedInWater);
		if (InventoryData != null)
		{
			writer.Name("EquipmentSlot");
			writer.Value(InventoryData);
		}

		if (ClothData != null)
		{
			writer.Name("ToolbarSlot");
			writer.Value(ClothData);
		}

		writer.EndObject();
	}
}

public class InventoryItemData : IWritable
{
	public Vector2 Size { get; set; }

	/// <summary>
	/// -1 => limitless
	/// </summary>
	public float MaxWeight { get; set; }

	public ItemSize SizeRestriction { get; set; } = ItemSize.Large;

	public bool CanBeUsedInWater { get; set; } = false;

	public void OnWrite(IMValueWriter writer)
	{
		writer.BeginObject();
		writer.Name("Size");
		writer.BeginObject();
		writer.Name("X");
		writer.Value(Size.X);
		writer.Name("Y");
		writer.Value(Size.Y);
		writer.EndObject();
		writer.Name("MaxWeight");
		writer.Value(MaxWeight);
		writer.Name("SizeRestriction");
		writer.Value((ushort)SizeRestriction);
		writer.Name("CanBeUsedInWater");
		writer.Value(CanBeUsedInWater);
		writer.EndObject();
	}
}

public class ClothData : IWritable
{
	public EquipmentSlot Slot { get; set; }

	[Range(0, 100)]
	public ushort Armor { get; set; } = 0;

	public void OnWrite(IMValueWriter writer)
	{
		writer.BeginObject();
		writer.Name("ClothData");
		writer.Value((ushort)Slot);
		writer.Name("Armor");
		writer.Value(Armor);
		writer.EndObject();
	}
}