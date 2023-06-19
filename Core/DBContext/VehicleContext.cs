using Core.DBEntities;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Core.DBContext;

public class VehicleContext : DbContext
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(VehicleContext));

	private DatabaseConfig? DatabaseConfig { get; set; }

	public virtual DbSet<Vehicle> Vehicles { get; set; } = default!;
	public virtual DbSet<VehicleData> VehicleData { get; set; } = default!;
	public virtual DbSet<VehicleRegistration> VehicleRegistrations { get; set; } = default!;

	#region Constructors

	public VehicleContext()
	{
	}

	public VehicleContext(DbContextOptions<VehicleContext> options) : base(options)
	{
	}

	#endregion

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		DatabaseConfig = DatabaseConfig.ReadConfig() ??
		                 throw new Exception($"Database config can not be used in VehicleContext");
		optionsBuilder.UseNpgsql(DatabaseConfig.GetConnectionString(), o => o.UseNodaTime());
		optionsBuilder.EnableSensitiveDataLogging();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
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