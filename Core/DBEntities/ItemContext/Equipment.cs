using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net;
using Core.DBContext;

namespace Core.DBEntities;

[Table("equipment")]
public class Equipment : IWritable
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[DefaultValue("uuid_generate_v4()")]
	[Key]
	public Guid Id { get; set; }

	public Character Character { get; set; } = null!;

	[Column("hat_id")]
	public long? HatId { get; set; }

	public Item? Hat { get; set; }

	[Column("head_extra_id")]
	public long? HeadExtraId { get; set; }

	public Item? HeadExtra { get; set; }

	[Column("mask_id")]
	public long? MaskId { get; set; }

	public Item? Mask { get; set; }

	[Column("neck_id")]
	public long? NeckId { get; set; }

	public Item? Neck { get; set; }

	[Column("body_id")]
	public long? BodyId { get; set; }

	public Item? Body { get; set; }

	[Column("legs_id")]
	public long? LegsId { get; set; }

	public Item? Legs { get; set; }

	[Column("feet_id")]
	public long? FeetId { get; set; }

	public Item? Feet { get; set; }

	[Column("hands_id")]
	public long? HandsId { get; set; }

	public Item? Hands { get; set; }

	[Column("watch_id")]
	public long? WatchId { get; set; }

	public Item? Watch { get; set; }

	[Column("back_id")]
	public long? BackId { get; set; }

	public Item? Back { get; set; }

	[Column("armor_west_id")]
	public long? ArmorWestId { get; set; }

	public Item? ArmorWest { get; set; }

	[Column("phone_id")]
	public long? PhoneId { get; set; }

	public Item? Phone { get; set; }

	[Column("tablet_id")]
	public long? TabletId { get; set; }

	public Item? Tablet { get; set; }

	[Column("radio_id")]
	public long? RadioId { get; set; }

	public Item? Radio { get; set; }

	public void LoadInventoryContent(ItemContext db)
	{
		if (Hat is { ContentInventory: not null })
			db.Entry(Hat.ContentInventory).Collection(inv => inv.Items).Load();
		if (HeadExtra is { ContentInventory: not null })
			db.Entry(HeadExtra.ContentInventory).Collection(inv => inv.Items).Load();
		if (Mask is { ContentInventory: not null })
			db.Entry(Mask.ContentInventory).Collection(inv => inv.Items).Load();
		if (Neck is { ContentInventory: not null })
			db.Entry(Neck.ContentInventory).Collection(inv => inv.Items).Load();
		if (Body is { ContentInventory: not null })
			db.Entry(Body.ContentInventory).Collection(inv => inv.Items).Load();
		if (Legs is { ContentInventory: not null })
			db.Entry(Legs.ContentInventory).Collection(inv => inv.Items).Load();
		if (Feet is { ContentInventory: not null })
			db.Entry(Feet.ContentInventory).Collection(inv => inv.Items).Load();
		if (Hands is { ContentInventory: not null })
			db.Entry(Hands.ContentInventory).Collection(inv => inv.Items).Load();
		if (Watch is { ContentInventory: not null })
			db.Entry(Watch.ContentInventory).Collection(inv => inv.Items).Load();
		if (Back is { ContentInventory: not null })
			db.Entry(Back.ContentInventory).Collection(inv => inv.Items).Load();
		if (ArmorWest is { ContentInventory: not null })
			db.Entry(ArmorWest.ContentInventory).Collection(inv => inv.Items).Load();
		if (Phone is { ContentInventory: not null })
			db.Entry(Phone.ContentInventory).Collection(inv => inv.Items).Load();
		if (Tablet is { ContentInventory: not null })
			db.Entry(Tablet.ContentInventory).Collection(inv => inv.Items).Load();
		if (Radio is { ContentInventory: not null })
			db.Entry(Radio.ContentInventory).Collection(inv => inv.Items).Load();
	}

	public void OnWrite(IMValueWriter writer)
	{
		writer.BeginObject();
		if (Hat != null)
		{
			writer.Name("Hat");
			writer.Value(Hat);
		}

		if (HeadExtra != null)
		{
			writer.Name("HeadExtra");
			writer.Value(HeadExtra);
		}

		if (Mask != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Mask);
		}

		if (Neck != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Neck);
		}

		if (Body != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Body);
		}

		if (Legs != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Legs);
		}

		if (Feet != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Feet);
		}

		if (Hands != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Hands);
		}

		if (Watch != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Watch);
		}

		if (Back != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Back);
		}

		if (ArmorWest != null)
		{
			writer.Name("HeadExtra");
			writer.Value(ArmorWest);
		}

		if (Phone != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Phone);
		}

		if (Tablet != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Tablet);
		}

		if (Radio != null)
		{
			writer.Name("HeadExtra");
			writer.Value(Radio);
		}

		writer.EndObject();
	}
}