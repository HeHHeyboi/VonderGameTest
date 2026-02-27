using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
	[SerializeField]
	private Inventory inventory;

	public bool isChestUI = false;
	public InventorySlot selectedSlot = null;
	public List<InventorySlot> InventorySlots;
	public readonly UnityEvent<Item, int> OnSelected = new();

	void Start()
	{
		if (InventorySlots == null)
		{
			InventorySlots = new List<InventorySlot>();
		}

		// Setup slot click listeners
		for (int i = 0; i < InventorySlots.Count; i++)
		{
			var slot = InventorySlots[i];
			slot.id = i;
			slot.parentInventoryUI = this;
			if (isChestUI)
			{
				slot.SetInteractable(false);
			}
			slot.OnItemChangeSlot.AddListener(OnChangeItemSlot);
			slot.OnSlotClick.AddListener(SetSelectedSlot);
		}

		// Subscribe to inventory changes
		if (inventory != null)
		{
			inventory.OnInventoryChanged.AddListener(UpdateUI);
		}

		UpdateUI();
	}

	public void SetInventory(Inventory newInventory)
	{
		if (inventory != null)
		{
			inventory.OnInventoryChanged.RemoveListener(UpdateUI);
		}

		inventory = newInventory;

		if (inventory != null)
		{
			inventory.OnInventoryChanged.AddListener(UpdateUI);
		}

		UpdateUI();
	}

	public void OnChangeItemSlot(InventorySlot slot1, InventorySlot slot2)
	{
		int index1 = slot1.id;
		int index2 = slot2.id;
		int selectedIndex = selectedSlot != null ? selectedSlot.id : -1;

		if (slot1.parentInventoryUI != slot2.parentInventoryUI)
		{
			MoveItemBetweenInventories(slot1, slot2);
		}
		else if (index1 >= 0 && index2 >= 0 && inventory != null)
		{
			inventory.SwapItems(index1, index2);
		}

		if (selectedSlot != null && (selectedIndex == index1 || selectedIndex == index2))
		{
			var (item, index) = GetItemFromSlot(selectedSlot);
			OnSelected.Invoke(item, index);
		}
	}

	private void MoveItemBetweenInventories(InventorySlot fromSlot, InventorySlot toSlot)
	{
		InventoryUI fromInventoryUI = fromSlot.parentInventoryUI;
		InventoryUI toInventoryUI = toSlot.parentInventoryUI;

		if (fromInventoryUI == null || toInventoryUI == null)
		{
			return;
		}

		Inventory fromInventory = fromInventoryUI.inventory;
		Inventory toInventory = toInventoryUI.inventory;

		if (fromInventory == null || toInventory == null)
		{
			return;
		}

		int fromIndex = fromSlot.id;
		int toIndex = toSlot.id;

		Item fromItem = fromInventory.GetItem(fromIndex);
		Item toItem = toInventory.GetItem(toIndex);

		// Move/swap items between inventories
		if (fromItem != null)
		{
			fromInventory.RemoveItem(fromIndex);
			toInventory.SetItem(toIndex, fromItem);
		}

		if (toItem != null)
		{
			toInventory.RemoveItem(toIndex);
			fromInventory.SetItem(fromIndex, toItem);
		}
	}

	public void SetSelectedSlot(InventorySlot slot)
	{
		if (selectedSlot == null)
		{
			selectedSlot = slot;
			selectedSlot.SetSelected(true);
			var (item, index) = GetItemFromSlot(slot);
			OnSelected.Invoke(item, index);
		}
		else if (selectedSlot != slot)
		{
			selectedSlot.SetSelected(false);
			selectedSlot = slot;
			selectedSlot.SetSelected(true);
			var (item, index) = GetItemFromSlot(slot);
			OnSelected.Invoke(item, index);
		}
		else
		{
			selectedSlot.SetSelected(false);
			selectedSlot = null;
			OnSelected.Invoke(null, -1);
		}
	}

	private (Item, int) GetItemFromSlot(InventorySlot slot)
	{
		int index = InventorySlots.IndexOf(slot);
		if (index >= 0 && inventory != null)
		{
			return (inventory.GetItem(index), index);
		}
		return (null, -1);
	}

	private void UpdateUI()
	{
		if (inventory == null)
		{
			return;
		}

		List<Item> items = inventory.GetAllItems();
		for (int i = 0; i < InventorySlots.Count && i < items.Count; i++)
		{
			var uiItem = InventorySlots[i].GetCurrentInventoryItem();

			if (uiItem == null)
			{
				continue;
			}

			if (items[i] == null)
			{
				uiItem.SetItem(null);
			}
			else
			{
				uiItem.SetItem(items[i]);
			}
		}
	}

	public void PrintSelected()
	{
		if (selectedSlot == null)
		{
			return;
		}

		var (item, _) = GetItemFromSlot(selectedSlot);
		if (item != null)
		{
			Debug.Log(item.GetItemData().name);
		}
	}

	public void UseSelectedItem()
	{
		if (selectedSlot == null || inventory == null)
		{
			return;
		}

		int index = InventorySlots.IndexOf(selectedSlot);
		if (index >= 0)
		{
			inventory.UseItem(index);
		}
	}

	public void AddWand()
	{
		if (inventory != null)
		{
			inventory.AddWand();
		}
	}

	public void AddLog()
	{
		if (inventory != null)
		{
			inventory.AddLog();
		}
	}

	public void CraftItem(CraftReceipe recipe)
	{
		if (inventory != null)
		{
			if (inventory.CheckRequireCraftItem(recipe))
			{
				inventory.CraftItem(recipe);
			}
			else
			{
				Debug.LogWarning("Not enough items to craft!");
			}
		}
	}
}
