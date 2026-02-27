using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    [SerializeField]
    ItemData data;
    public int amount = 1;

    public Item(ItemData data)
    {
        this.data = data;
    }

    public ItemData GetItemData()
    {
        return data;
    }

	public bool IsStackable()
	{
		return data != null && data.stackable;
	}

	public void SetItemData(ItemData data)
	{
		this.data = data;
	}
}
