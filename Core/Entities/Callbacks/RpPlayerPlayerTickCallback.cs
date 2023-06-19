using AltV.Net.Elements.Entities;
using AltV.Net.Elements.Pools;
using Autofac;
using Core.Systems;

namespace Core.Entities;

public class RpPlayerPlayerTickCallback : IBaseObjectCallback<IPlayer>
{
	public void OnBaseObject(IPlayer baseObject)
	{
		Core.Container.Resolve<PlayerStateController>().HandlePlayerTick((RpPlayer)baseObject);
	}
}