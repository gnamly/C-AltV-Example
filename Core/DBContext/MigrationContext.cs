using Core.DBEntities;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Core.DBContext;

public class MigrationContext : DbContext
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(MigrationContext));

	private DatabaseConfig? DatabaseConfig { get; set; }

	public virtual DbSet<Account> Accounts { get; set; } = default!;
	public virtual DbSet<Character> Characters { get; set; } = default!;

	public virtual DbSet<Vehicle> Vehicles { get; set; } = default!;
	public virtual DbSet<VehicleData> VehicleData { get; set; } = default!;
	public virtual DbSet<VehicleRegistration> VehicleRegistrations { get; set; } = default!;

	public virtual DbSet<Inventory> Inventories { get; set; } = default!;
	public virtual DbSet<ItemData> ItemData { get; set; } = default!;
	public virtual DbSet<Item> Items { get; set; } = default!;
	public virtual DbSet<Equipment> Equipments { get; set; } = default!;
	public virtual DbSet<Toolbar> Toolbars { get; set; } = default!;

	#region Constructors

	public MigrationContext()
	{
	}

	public MigrationContext(DbContextOptions<AccountContext> options) : base(options)
	{
	}

	#endregion

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		DatabaseConfig = DatabaseConfig.ReadConfig() ??
		                 throw new Exception($"Database config can not be used in MigrationContext");
		optionsBuilder.UseNpgsql(DatabaseConfig.GetConnectionString(), o => { o.UseNodaTime(); });
		optionsBuilder.EnableSensitiveDataLogging();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresExtension("uuid-ossp");
		modelBuilder.Entity<Character>()
			.HasMany(e => e.Vehicles)
			.WithMany(e => e.Characters)
			.UsingEntity<VehicleKey>(
				j =>
				{
					j.Property(e => e.IsPrimary);
					j.Property("CharactersId").HasColumnName("character_id");
					j.Property("VehiclesId").HasColumnName("vehicle_id");
				}
			);
	}
}