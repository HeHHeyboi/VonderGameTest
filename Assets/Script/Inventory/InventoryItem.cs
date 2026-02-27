using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
	public Item item;
	public Image ItemImage;
	public TMP_Text text;
	public Transform parentAfterDrag;

	public InventoryItem(Item item)
	{
		this.item = item;
	}

	public Item GetItem()
	{
		return item;
	}

	void Start()
	{
		if (item.GetItemData() != null && ItemImage != null)
		{
			ItemImage.sprite = item.GetItemData().sprite;
		}
	}

	void Update()
	{
		if(item == null)
		{
			return;
		}
		if (item.amount > 1)
		{
			text.text = item.amount.ToString();
		}
		else
		{
			text.text = "";
		}
	}

	public void SetItem(Item item)
	{
		if(item == null || item.GetItemData() == null)
		{
			this.item = null;
			ItemImage.sprite = null;
			text.text = "";
			return;
		}

		this.item = item;
		ItemImage.sprite = this.item.GetItemData().sprite;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		ItemImage.raycastTarget = false;
		parentAfterDrag = transform.parent.transform;
		transform.SetParent(transform.root);
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		ItemImage.raycastTarget = true;
		transform.SetParent(parentAfterDrag);
	}
}
