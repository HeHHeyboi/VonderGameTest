using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;
	public InventoryUI chestUI;
    public bool isChestUIOpen = false;
	public GameEvent<Chest> gameEvent;

	void Start()
	{
		instance = this;
		gameEvent.AddListener(OpenChestInventory);
		chestUI.gameObject.SetActive(false);
	}

	void OpenChestInventory(Chest chest)
    {
        if (!isChestUIOpen)
        {
            chestUI.SetInventory(chest.GetComponent<Inventory>());
            chestUI.gameObject.SetActive(true);
            isChestUIOpen = true;
        }
        else
        {
            chestUI.SetInventory(null);
            chestUI.gameObject.SetActive(false);
            isChestUIOpen = false;
        }
    }
}
