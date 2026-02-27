using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CraftEntry : MonoBehaviour
{
	public Image craftImage;
	public TMP_Text craftName;
	public Outline border;
	public static int entryCount = 0;
	public int id;
	readonly UnityEvent<CraftEntry> OnEntryClick = new();

	public void AddListener(UnityAction<CraftEntry> func)
	{
		OnEntryClick.AddListener(func);
	}

	public void Setup(ItemData data)
	{
		craftImage.sprite = data.sprite;
		craftName.text = data.itemName;
		id = entryCount;
		entryCount += 1;
	}

	public void OnClick()
	{
		OnEntryClick.Invoke(this);
	}

	public void Selected(bool selected)
	{
		border.enabled = selected;
	}
}
