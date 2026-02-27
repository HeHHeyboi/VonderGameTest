using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
	public ItemData wandData;
	public ItemData logData;
	public int maxItem = 5;
	public List<Item> items = new();

	public readonly UnityEvent<Item> OnItemAdded = new();
	public readonly UnityEvent<int> OnItemRemoved = new();
	public readonly UnityEvent OnInventoryChanged = new();

	private void Start()
	{
		// Initialize empty inventory with nulls
		for (int i = 0; i < maxItem; i++)
		{
			items.Add(null);
		}
	}

	public void AddItem(Item item)
	{
		if (item == null)
		{
			return;
		}

		// Try to stack item if stackable
		if (item.IsStackable())
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (items[i] != null && items[i].GetItemData() == item.GetItemData())
				{
					if (items[i].amount < items[i].GetItemData().maxStack)
					{
						items[i].amount += item.amount;
						OnInventoryChanged.Invoke();
						return;
					}
				}
			}
		}

		// Add to empty slot
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i] == null || items[i].GetItemData() == null)
			{
				items[i] = item;
				OnItemAdded.Invoke(item);
				OnInventoryChanged.Invoke();
				return;
			}
		}

		Debug.LogWarning("Inventory is full!");
	}

	public void RemoveItem(int index)
	{
		if (index >= 0 && index < items.Count)
		{
			items[index] = null;
			OnItemRemoved.Invoke(index);
			OnInventoryChanged.Invoke();
		}
	}

	public void SwapItems(int index1, int index2)
	{
		// Debug.Log($"Swapped items at index {index1} and {index2}");
		if (index1 >= 0 && index1 < items.Count && index2 >= 0 && index2 < items.Count)
		{
			Item temp = items[index1];
			items[index1] = items[index2];
			items[index2] = temp;
		}
	}

	public Item GetItem(int index)
	{
		if (index >= 0 && index < items.Count)
		{
			return items[index];
		}
		return null;
	}

	public List<Item> GetAllItems()
	{
		return new List<Item>(items);
	}

	public bool CheckRequireCraftItem(CraftReceipe recipe)
	{
		ItemData requireItem = recipe.item;
		int requireAmount = recipe.amount;

		foreach (var item in items)
		{
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

	public void CraftItem(CraftReceipe recipe)
	{
		ItemData requireItem = recipe.item;
		int requireAmount = recipe.amount;

		foreach (var item in items)
		{
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

	public void UseItem(int index)
	{
		if (index < 0 || index >= items.Count || items[index] == null)
		{
			return;
		}

		Item item = items[index];
		Debug.Log("Use " + item.GetItemData().name);
		Debug.Log("Amount " + item.amount);
		if (item.GetItemData().type == ItemType.Placeable)
		{
			item.amount -= 1;
			CleanUpInventory();
		}
	}

	public void CleanUpInventory()
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i] != null && items[i].amount <= 0)
			{
				items[i] = null;
				OnItemRemoved.Invoke(i);
			}
		}
		OnInventoryChanged.Invoke();
	}

	public void AddWand()
	{
		if (wandData != null)
		{
			var item = new Item(wandData);
			AddItem(item);
		}
		else
		{
			Debug.LogError("wandData is not assigned!");
		}
	}

	public void AddLog()
	{
		if (logData != null)
		{
			var item = new Item(logData);
			AddItem(item);
		}
		else
		{
			Debug.LogError("logData is not assigned!");
		}
	}
}
