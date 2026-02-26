using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
	public int id;
	public Outline selectedBorder;
	public readonly UnityEvent<InventorySlot> OnSlotClick = new();

	// void Start()
	// {
	// }

	public void AddItem(Item item)
	{
		if (transform.childCount > 0)
		{
			Debug.Log("this " + name + " Already Have item");
			return;
		}

		item.transform.SetParent(this.transform);
	}

	public void OnClick()
	{
		OnSlotClick.Invoke(this);
	}

	public void SetSelected(bool selected)
	{
		selectedBorder.enabled = selected;
	}

	public void OnDrop(PointerEventData eventData)
	{
		Item item = eventData.pointerDrag.GetComponent<Item>();
		if (transform.childCount == 0)
		{
			item.parentAfterDrag = transform;
		}
		else
		{
			Item cur_item = transform.GetComponentInChildren<Item>();
			cur_item.transform.SetParent(item.parentAfterDrag);
			item.parentAfterDrag = transform;
		}
	}

	public Item GetCurrentItem()
	{
		return transform.GetComponentInChildren<Item>();
	}
}
