using AltV.Net;
using Core.DBEntities;
using Core.Entities;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Core.DBContext;

public class ItemContext : DbContext
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(AccountContext));

	private DatabaseConfig? DatabaseConfig { get; set; }

	public virtual DbSet<Inventory> Inventories { get; set; } = default!;
	public virtual DbSet<ItemData> ItemData { get; set; } = default!;
	public virtual DbSet<Item> Items { get; set; } = default!;
	public virtual DbSet<Equipment> Equipments { get; set; } = default!;
	public virtual DbSet<Toolbar> Toolbars { get; set; } = default!;

	#region Constructors

	public ItemContext()
	{
	}

	public ItemContext(DbContextOptions<AccountContext> options) : base(options)
	{
	}

	#endregion

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		DatabaseConfig = DatabaseConfig.ReadConfig() ??
		                 throw new Exception($"Database config can not be used in AccountContext");
		optionsBuilder.UseNpgsql(DatabaseConfig.GetConnectionString(), o => o.UseNodaTime());
		optionsBuilder.EnableSensitiveDataLogging();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Equipment>().Navigation(e => e.Hat).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.HeadExtra).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Mask).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Neck).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Body).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Legs).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Feet).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Hands).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Back).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.ArmorWest).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Phone).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Tablet).AutoInclude();
		modelBuilder.Entity<Equipment>().Navigation(e => e.Radio).AutoInclude();
		modelBuilder.Entity<Item>().Navigation(item => item.ItemData).AutoInclude();
		modelBuilder.Entity<Inventory>().Navigation(inv => inv.Items).AutoInclude();
	}
}