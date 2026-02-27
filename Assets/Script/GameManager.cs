using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player player;
	public Inventory playerInventory;
	public CraftManager craftManager;

	void OnEnable()
	{
		playerInventory.OnSelected.AddListener(SetPlayerSelectedItem);
		playerInventory.UpdateInventory.AddListener(InventoryUpdate);
		craftManager.AddListener(CraftItem);
		craftManager.AddListener(CheckCraftItemReceipe);
	}

	void OnDisable()
	{
		playerInventory.OnSelected.RemoveListener(SetPlayerSelectedItem);
		playerInventory.UpdateInventory.RemoveListener(InventoryUpdate);
		craftManager.RemoveListener(CheckCraftItemReceipe);
		craftManager.RemoveListener(CraftItem);
	}

	void SetPlayerSelectedItem(Item item)
	{
		player.SetHoldItem(item);
	}

	void InventoryUpdate()
	{
		if (craftManager.gameObject.activeSelf)
		{
			ItemData craftItem = craftManager.GetCurrentSelectedItem();
			CheckCraftItemReceipe(craftItem);
		}
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
		ItemData item = craftManager.GetCurrentSelectedItem();
		foreach (var receipe in item.craftReceipes)
		{
			playerInventory.CraftItem(receipe);
		}
	}
}
