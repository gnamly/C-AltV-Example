using AltV.Net;
using AltV.Net.Elements.Entities;
using Core.Entities;

namespace Core.Factories;

public class VehicleFactory : IEntityFactory<IVehicle>
{
	public IVehicle Create(ICore core, IntPtr entityPointer, ushort id)
	{
		return new RpVehicle(core, entityPointer, id);
	}
}