using AltV.Net;
using Core.DBEntities;
using Shared.DTOs.CharacterSelect;
using Shared.Models;

namespace Core.DTOs.CharacterSelect;

public class CharacterSelectWriteableItem : CharacterSelectItem, IWritable
{
	public CharacterSelectWriteableItem(ICharacter character) : base(character)
	{
	}


	public void OnWrite(IMValueWriter writer)
	{
		writer.BeginObject();
		writer.Name("Id");
		writer.Value(Id);
		writer.Name("Name");
		writer.Value(Name);
		writer.Name("PersoId");
		writer.Value(PersoId);
		writer.Name("Hours");
		writer.Value(Hours);
		Character.WriteInfo(Info, writer);
		if (Appearance != null)
		{
			Character.WriteAppearance(Appearance, writer);
		}

		writer.EndObject();
	}
}