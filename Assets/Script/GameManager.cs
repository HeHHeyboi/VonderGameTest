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
		playerInventory.OnInventoryChanged.AddListener(UpdateCraftPanel);
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

	void UpdateCraftPanel()
	{
		ItemData craftItem = craftManager.GetCurrentSelectedItem();
		if (craftItem == null)
		{
			return;
		}
		bool canCraft = false;
		foreach (var i in craftItem.craftReceipes)
		{
			canCraft = playerInventory.CheckRequireCraftItem(i);
		}

		craftManager.SetCanCraft(canCraft);
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

		craftManager.SetCanCraft(canCraft);
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
