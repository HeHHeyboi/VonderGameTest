using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public Item itemPrefab;
	public ItemData wand;
	public ItemData log;
	public int maxItem = 5;
	public InventorySlot selectedSlot = null;
	public List<InventorySlot> InventorySlots;

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
		foreach (var i in InventorySlots)
		{
			Item slotItem = i.GetCurrentItem();
			if (i.transform.childCount == 0)
			{
				i.AddItem(item);
				break;
			}
			else if (item.IsStackable() && item.item_data == slotItem.item_data)
			{
				if (slotItem.amount < item.item_data.maxStack)
				{
					slotItem.amount += 1;
					break;
				}
				continue;
			}
		}
	}

	public void AddWand()
	{
		var item = Instantiate(itemPrefab, this.transform);
		item.SetItemData(wand);
		AddItem(item);
	}

	public void AddLog()
	{
		var item = Instantiate(itemPrefab, this.transform);
		item.SetItemData(log);
		AddItem(item);
	}

	public void PrintSelected()
	{
		if (selectedSlot == null)
		{
			return;
		}
		Debug.Log(selectedSlot.GetCurrentItem());
	}

	public void UseItem()
	{
		if (selectedSlot == null)
		{
			return;
		}
	}

	public void SetSelectedSlot(InventorySlot i)
	{
		if (selectedSlot == null)
		{
			selectedSlot = i;
			selectedSlot.SetSelected(true);
		}
		else if (selectedSlot == i)
		{
			selectedSlot.SetSelected(false);
			selectedSlot = null;
		}
		else
		{
			selectedSlot.SetSelected(false);
			selectedSlot = i;
			selectedSlot.SetSelected(true);
		}
	}
}
