using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
	public InventoryItem inventoryItemPrefab;
	public ItemData wandData;
	public ItemData logData;
	public int maxItem = 5;
	public InventorySlot selectedSlot = null;
	public List<InventorySlot> InventorySlots;
	public readonly UnityEvent<Item> OnSelected = new();
	public readonly UnityEvent UpdateInventory = new();

	// Start is called before the first frame update
	void Start()
	{
		if (InventorySlots == null)
		{
			InventorySlots = new List<InventorySlot>();
		}

		foreach (var i in InventorySlots)
		{
			i.OnSlotClick.AddListener(SetSelectedSlot);
		}
	}

	public void AddItem(Item item)
	{
		if(item == null)
		{
			return;
		}
		foreach (var i in InventorySlots)
		{
			Item slotItem = i.GetCurrentInventoryItem().GetItem();
			if (slotItem == null)
			{
				var inventoryItem = i.GetCurrentInventoryItem();
				inventoryItem.SetItem(item);
				break;
			}
			else if (item.IsStackable() && item.GetItemData() == slotItem.GetItemData())
			{
				if (slotItem.amount < slotItem.GetItemData().maxStack)
				{
					slotItem.amount += 1;
					break;
				}
				continue;
			}
		}
		UpdateInventory.Invoke();
	}

	public bool CheckRequireCraftItem(CraftReceipe receipe)
	{
		ItemData requireItem = receipe.item;
		int requireAmount = receipe.amount;
		foreach (var i in InventorySlots)
		{
			Item item = i.GetCurrentInventoryItem().GetItem();
			if (item == null)
			{
				continue;
			}

			if (item.GetItemData() == requireItem)
			{
				if (item.amount >= requireAmount)
				{
					return true;
				}
				else
				{
					requireAmount -= item.amount;
				}
			}
		}
		return false;
	}

	public void CraftItem(CraftReceipe receipe)
	{
		ItemData requireItem = receipe.item;
		int requireAmount = receipe.amount;
		foreach (var i in InventorySlots)
		{
			Item item = i.GetCurrentInventoryItem().GetItem();
			if (item == null)
			{
				continue;
			}

			if (item.GetItemData() == requireItem)
			{
				if (item.amount >= requireAmount)
				{
					item.amount -= requireAmount;
					break;
				}
				else
				{
					requireAmount -= item.amount;
					item.amount = 0;
				}
			}
		}
		CleanUpInventory();
	}

	public void CleanUpInventory()
	{
		foreach (var i in InventorySlots)
		{
			if (i.GetCurrentInventoryItem() == null)
			{
				continue;
			}

			Item item = i.GetCurrentInventoryItem().GetItem();
			if (item.amount <= 0)
			{
				i.Clear();
			}
		}
	}

	public void AddWand()
	{
		var item = new Item(wandData);
		AddItem(item);
	}

	public void AddLog()
	{
		var item = new Item(logData);
		AddItem(item);
	}

	public void PrintSelected()
	{
		if (selectedSlot == null)
		{
			return;
		}
		Debug.Log(selectedSlot.GetCurrentInventoryItem().GetItem().GetItemData().name);
	}

	public void SetSelectedSlot(InventorySlot i)
	{
		if (selectedSlot == null)
		{
			selectedSlot = i;
			selectedSlot.SetSelected(true);
			Item item = i.GetCurrentInventoryItem().GetItem();
			OnSelected.Invoke(item);
		}
		else if (selectedSlot != i)
		{
			selectedSlot.SetSelected(false);
			selectedSlot = i;
			selectedSlot.SetSelected(true);
			Item item = i.GetCurrentInventoryItem().GetItem();
			OnSelected.Invoke(item);
		}
		else
		{
			selectedSlot.SetSelected(false);
			selectedSlot = null;
			OnSelected.Invoke(null);
		}
	}
}
