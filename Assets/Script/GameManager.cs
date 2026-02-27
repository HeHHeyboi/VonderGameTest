using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player player;
	public Inventory playerInventory;
	public InventoryUI inventoryUI;
	public CraftManager craftManager;

	void OnEnable()
	{
		inventoryUI.OnSelected.AddListener(SetPlayerSelectedItem);
		craftManager.AddListener(CraftItem);
		craftManager.AddListener(CheckCraftItemReceipe);
		player.PlaceItem.AddListener(PlayerPlaceItem);
	}

	void OnDisable()
	{
		inventoryUI.OnSelected.RemoveListener(SetPlayerSelectedItem);
		craftManager.RemoveListener(CheckCraftItemReceipe);
		craftManager.RemoveListener(CraftItem);
		player.PlaceItem.RemoveListener(PlayerPlaceItem);
	}

	public void PlayerPlaceItem(int index)
	{
		playerInventory.UseItem(index);
	}

	void SetPlayerSelectedItem(Item item, int index)
	{
		player.SetHoldItem(item);
		player.curItemIndex = index;
	}

	void CheckCraftItemReceipe(ItemData craftItem)
	{
		if (craftItem == null)
		{
			return;
		}

		bool canCraft = false;
		foreach (var i in craftItem.craftReceipes)
		{
			canCraft = playerInventory.CheckRequireCraftItem(i);
		}

		craftManager.IsCanCraft(canCraft);
	}

	void CraftItem()
	{
		ItemData data = craftManager.GetCurrentSelectedItem();
		foreach (var receipe in data.craftReceipes)
		{
			playerInventory.CraftItem(receipe);
		}

		Item item = new(data);
		playerInventory.AddItem(item);
	}
}
