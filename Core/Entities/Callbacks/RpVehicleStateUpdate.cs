using AltV.Net.Elements.Entities;
using AltV.Net.Elements.Pools;
using Autofac;
using Core.DBContext;
using Core.Services;

namespace Core.Entities;

public class RpVehicleStateUpdate : IBaseObjectCallback<IVehicle>
{
	public void OnBaseObject(IVehicle baseObject)
	{
		var vehicle = (RpVehicle)baseObject;
		vehicle.UpdateState(false);
	}
}