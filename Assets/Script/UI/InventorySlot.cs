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
	public readonly UnityEvent<InventorySlot,InventorySlot> OnItemChangeSlot = new();

	// void Start()
	// {
	// }

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
		InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
		if (transform.childCount == 0)
		{
			var prevSlot = item.parentAfterDrag.GetComponent<InventorySlot>();
			item.parentAfterDrag = this.gameObject;
			OnItemChangeSlot.Invoke(prevSlot, this);
		}
		else
		{
			var prevSlot = item.parentAfterDrag.GetComponent<InventorySlot>();
			InventoryItem cur_item = transform.GetComponentInChildren<InventoryItem>();
			cur_item.transform.SetParent(item.parentAfterDrag.transform);
			item.parentAfterDrag = this.gameObject;
			OnItemChangeSlot.Invoke(prevSlot, this);
		}
	}

	public void Clear()
	{
		InventoryItem item = transform.GetComponentInChildren<InventoryItem>();
		if (item.GetItem() == null)
		{
			return;
		}
		item.SetItem(null);
	}

	public InventoryItem GetCurrentInventoryItem()
	{
		return transform.GetComponentInChildren<InventoryItem>();
	}
}
