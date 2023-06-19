using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DBEntities;

[Table("toolbars")]
public class Toolbar
{
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }

	[Column("primary_weapon_id")]
	public long? PrimaryWeaponId { get; set; }

	public Item? PrimaryWeapon { get; set; }

	[Column("secondary_weapon_id")]
	public long? SecondaryWeaponId { get; set; }

	public Item? SecondaryWeapon { get; set; }

	[Column("secondary_extra_weapon_id")]
	public long? SecondaryExtraWeaponId { get; set; }

	public Item? SecondaryExtraWeapon { get; set; }

	[Column("melee_id")]
	public long? MeleeId { get; set; }

	public Item? Melee { get; set; }

	[Column("melee_extra_id")]
	public long? MeleeExtraId { get; set; }

	public Item? MeleeExtra { get; set; }

	[Column("gadget_one_id")]
	public long? GadgetOneId { get; set; }

	public Item? GadgetOne { get; set; }

	[Column("gadget_two_id")]
	public long? GadgetTwoId { get; set; }

	public Item? GadgetTwo { get; set; }

	[Column("misc_one_id")]
	public long? MiscOneId { get; set; }

	public Item? MiscOne { get; set; }

	[Column("misc_two_id")]
	public long? MiscTwoId { get; set; }

	public Item? MiscTwo { get; set; }
}