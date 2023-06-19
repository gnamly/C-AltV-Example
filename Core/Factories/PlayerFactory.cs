using AltV.Net;
using AltV.Net.Elements.Entities;
using Core.Entities;

namespace Core.Factories;

public class PlayerFactory : IEntityFactory<IPlayer>
{
	public IPlayer Create(ICore core, IntPtr entityPointer, ushort id)
	{
		return new RpPlayer(core, entityPointer, id);
	}
}