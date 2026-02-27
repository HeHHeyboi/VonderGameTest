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
        isChestUIOpen = !isChestUIOpen;
        if (isChestUIOpen)
        {
            chestUI.SetInventory(chest.GetComponent<Inventory>());
            chestUI.gameObject.SetActive(isChestUIOpen);
        }
        else
        {
            chestUI.SetInventory(null);
            chestUI.gameObject.SetActive(isChestUIOpen);
        }
    }
}
