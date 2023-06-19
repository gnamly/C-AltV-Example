using Shared.Models;

namespace Shared.DTOs.CharacterSelect;

public class CharacterSelectItem
{
	public string Id { get; set; }
	
	public string Name { get; set; }
	
	public int PersoId { get; set; }
	
	public float Hours { get; set; }
	
	public CharacterInfo Info { get; set; }
	
	public Appearance? Appearance { get; set; }

	public CharacterSelectItem(ICharacter character)
	{
		Id = character.Id.ToString();
		Name = character.Name;
		PersoId = character.PersoId;
		Hours = character.Hours;
		Info = character.Info;
		Appearance = character.Appearance;
	}
}