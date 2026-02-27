using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
	public Image craftImage;
	public TMP_Text craftName;
	public List<ReceipeEntry> entries = new();

	void Start()
	{
		gameObject.SetActive(false);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
		foreach (var i in entries)
		{
			i.gameObject.SetActive(false);
		}
	}

	public void Show(ItemData data)
	{
		gameObject.SetActive(true);
		craftImage.sprite = data.sprite;
		craftName.text = data.itemName;
		for (var i = 0; i < data.craftReceipes.Length; i++)
		{
			var entry = entries[i];
			if (entry == null)
			{
				break;
			}
			var receipe = data.craftReceipes[i];
			entry.gameObject.SetActive(true);
			entry.SetUp(receipe.item.sprite, receipe.item.name, receipe.amount);
		}
	}
}
