using Core.DBContext;
using Core.DBEntities;
using Core.Entities;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Shared.Events.Systems;

namespace Core.Services;

public class InventoryService
{
	private readonly ServiceLogger _logger = new ServiceLogger(nameof(CharacterService));
	private readonly ItemContext _db = new ItemContext();

	public void LoadPlayerEquipment(RpPlayer player)
	{
		player.Equipment = GetPlayerEquipment(player);
	}

	public void SyncInventoryToPlayer(RpPlayer player)
	{
		var equipment = player.Equipment;
		if (equipment == null) return;
		equipment.LoadInventoryContent(_db);

		player.Emit(InventoryEvents.SET_PLAYER_INVENTORY, equipment);
	}

	private Equipment? GetPlayerEquipment(RpPlayer player)
	{
		if (player.Character == null) return null;

		return _db.Equipments.SingleOrDefault(equipment => equipment.Character.Id == player.Character.Id);
	}
}