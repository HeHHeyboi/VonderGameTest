using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
	public GameObject CraftList;
	public InformationPanel info;
	public CraftEntry craftEntryPrefab;
	CraftEntry currentEntry = null;
	public List<ItemData> craftList = new();
	public Button craftButton;
	public GameObject craftPanel;
	readonly UnityEvent<ItemData> OnSelected = new();
	readonly UnityEvent CraftedItem = new();

	public void AddListener(UnityAction<ItemData> listener)
	{
		OnSelected.AddListener(listener);
	}

	public void RemoveListener(UnityAction<ItemData> listener)
	{
		OnSelected.RemoveListener(listener);
	}

	public void AddListener(UnityAction listener)
	{
		CraftedItem.AddListener(listener);
	}

	public void RemoveListener(UnityAction listener)
	{
		CraftedItem.RemoveListener(listener);
	}

	void Start()
	{
		craftButton.onClick.AddListener(OnClickCraftButton);
		craftButton.interactable = false;
		craftPanel.SetActive(false);
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

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			if (!craftPanel.activeSelf)
			{
				craftPanel.SetActive(true);
			}
			else
			{
				Reset();
				craftPanel.SetActive(false);
			}
		}
	}

	public ItemData GetCurrentSelectedItem()
	{
		if (currentEntry == null)
		{
			return null;
		}
		return craftList[currentEntry.id];
	}

	void Reset()
	{
		info.gameObject.SetActive(false);
		craftButton.interactable = false;
		if (currentEntry != null)
		{
			currentEntry.Selected(false);
			currentEntry = null;
		}
	}

	void ShowCraftInformation(CraftEntry entry)
	{
		if (currentEntry == null)
		{
			currentEntry = entry;
			currentEntry.Selected(true);
			ItemData itemData = craftList[currentEntry.id];
			info.Show(itemData);
			OnSelected.Invoke(itemData);
		}
		else if (currentEntry != entry)
		{
			currentEntry.Selected(false);
			currentEntry = entry;
			ItemData itemData = craftList[currentEntry.id];
			info.Show(itemData);
			OnSelected.Invoke(itemData);
		}
		else
		{
			currentEntry.Selected(false);
			currentEntry = null;
			info.Hide();
			craftButton.interactable = false;
		}
	}

	public void OnClickCraftButton()
	{
		CraftedItem.Invoke();
	}

	public void IsCanCraft(bool buttonActive)
	{
		craftButton.interactable = buttonActive;
	}
}
