using System.Numerics;
using NodaTime;

namespace Shared.Models;

public interface ICharacter
{
	public Guid Id { get; set; }
	public int PersoId { get; set; }
	public Guid AccountId { get; set; }
	public int Dimension { get; set; }
	public Vector3? Position { get; set; }

	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Name => $"{FirstName} {LastName}";

	public ushort? Health { get; set; }
	public bool IsDead { get; set; }

	public float Hours { get; set; }

	public CharacterInfo Info { get; set; }

	public Appearance? Appearance { get; set; }
}

public class CharacterInfo
{
	public string Gender { get; set; } = "";

	public LocalDate Birth { get; set; } = LocalDate.MinIsoValue;

	// public Period Age => Period.Between(Birth, LocalDate.FromDateTime(DateTime.Now), PeriodUnits.YearMonthDay);
}