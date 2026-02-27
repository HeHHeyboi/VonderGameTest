using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanel : MonoBehaviour
{
	public GameObject CraftList;
	public InformationPanel info;
	public CraftEntry craftEntryPrefab;
	CraftEntry currentEntry = null;
	public List<ItemData> craftList = new();
	public Button craftButton;

	void Start()
	{
		craftButton.gameObject.SetActive(false);
		foreach (var entry in craftList)
		{
			if (!entry.craftAble)
			{
				continue;
			}
			CraftEntry craftEntry = Instantiate(craftEntryPrefab, CraftList.transform);
			craftEntry.Setup(entry);
			craftEntry.AddListener(ShowCraftInformation);
		}
	}

	void ShowCraftInformation(CraftEntry entry)
	{
		if (currentEntry == null)
		{
			currentEntry = entry;
			currentEntry.Selected(true);
			info.Show(craftList[currentEntry.id]);
		}
		else if (currentEntry != entry)
		{
			currentEntry.Selected(false);
			currentEntry = entry;
			info.Show(craftList[currentEntry.id]);
		}
		else
		{
			currentEntry.Selected(false);
			currentEntry = null;
			info.Hide();
		}
	}
}
