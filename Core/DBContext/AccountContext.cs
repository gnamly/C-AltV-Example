using AltV.Net;
using Core.DBEntities;
using Core.Entities;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Core.DBContext;

public class AccountContext : DbContext
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(AccountContext));

	private DatabaseConfig? DatabaseConfig { get; set; }

	public virtual DbSet<Account> Accounts { get; set; } = default!;
	public virtual DbSet<Character> Characters { get; set; } = default!;

	#region Constructors

	public AccountContext()
	{
	}

	public AccountContext(DbContextOptions<AccountContext> options) : base(options)
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

	public Account GetOrCreateAccount(string acpId, RpPlayer player)
	{
		var account = Accounts.SingleOrDefault(a => a.AcpId == acpId);
		if (account == null)
		{
			account = Add(new Account
			{
				AcpId = acpId,
				ips = new[] { player.Ip },
				hardware = new[] { player.HardwareIdHash, player.HardwareIdExHash },
				CharacterLimit = 1,
				LastLogin = SystemClock.Instance.GetCurrentInstant().InUtc(),
			}).Entity;
			SaveChanges();
		}

		return account;
	}
}