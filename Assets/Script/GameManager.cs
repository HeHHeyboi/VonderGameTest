using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player player;
	public Inventory inventory;

	void OnEnable()
	{
		inventory.OnSelected.AddListener(SetPlayerSelectedItem);
	}

	void OnDisable()
	{
		inventory.OnSelected.RemoveListener(SetPlayerSelectedItem);
	}

	void SetPlayerSelectedItem(Item item)
	{
		player.SetHoldItem(item);
	}
}
