using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
	public ItemData item_data;
	public int amount = 1;
	public Image ItemImage;
	public TMP_Text text;
	public Transform parentAfterDrag;

	public Item(ItemData data)
	{
		item_data = data;
	}

	void Start()
	{
		if (item_data != null && ItemImage != null)
		{
			ItemImage.sprite = item_data.sprite;
		}
	}

	void Update()
	{
		if (amount > 1)
		{
			text.text = amount.ToString();
		}
		else
		{
			text.text = "";
		}
	}

	public bool IsStackable()
	{
		return item_data != null && item_data.stackable;
	}

	public void SetItemData(ItemData data)
	{
		item_data = data;
		ItemImage.sprite = item_data.sprite;
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
