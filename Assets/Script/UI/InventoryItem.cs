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
	public GameObject parentAfterDrag;

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
		if(item == null || item.GetItemData() == null)
		{
			this.item = null;
			ItemImage.sprite = null;
			text.text = "";
			ItemImage.enabled = false;
			return;
		}

		ItemImage.enabled = true;
		ItemImage.sprite = this.item.GetItemData().sprite;
	}

	void Update()
	{
		if(item ==null || item.GetItemData() == null)
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
			ItemImage.enabled = false;
			return;
		}

		this.item = item;
		ItemImage.enabled = true;
		ItemImage.sprite = this.item.GetItemData().sprite;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		ItemImage.raycastTarget = false;
		parentAfterDrag = transform.parent.gameObject;
		var canvas = GetComponentInParent<Canvas>();
		transform.SetParent(canvas.transform);
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		ItemImage.raycastTarget = true;
		transform.SetParent(parentAfterDrag.transform);
	}
}
