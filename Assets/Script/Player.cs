using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
	public Rigidbody2D rb;
	public float speed = 20;
	public Item curItem;
	public int curItemIndex;
	public GameObject playerHand;
	public bool flipH;
	public HoldItem holdItem;
	public int prevDir;
	public readonly UnityEvent<int> PlaceItem = new();

	void Update()
	{
		float horizontal = 0;
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			horizontal = -1;
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			horizontal = 1;
		}

		if (Input.GetMouseButtonDown(0))
		{
			// Only use item if not clicking on UI
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				UseItem();
			}
		}
		transform.Translate(horizontal * speed * Time.deltaTime * new Vector2(1, 0));

		UpdatePlayerItem();
	}

	void UseItem()
	{
		if (curItem == null || curItem.GetItemData() == null)
		{
			return;
		}

		ItemData data = curItem.GetItemData();
		switch (data.type)
		{
			case ItemType.Placeable:
				PlaceItem placeItem = (PlaceItem)data;
				var obj = Instantiate(placeItem.placePrefab, null, true);
				obj.transform.position = transform.position;
				PlaceItem.Invoke(curItemIndex);
				break;
			case ItemType.Weapon:
				Weapon weapon = (Weapon)data;
				var bullet = Instantiate(weapon.BulletPrefab, null, true);
				bullet.transform.position = transform.position;
				bullet.transform.rotation = playerHand.transform.rotation;
				break;
		}
	}

	void UpdatePlayerItem()
	{
		if (curItem == null || curItem.GetItemData() == null)
		{
			return;
		}
		if (curItem.GetItemData().type == ItemType.Weapon)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = mousePos - playerHand.transform.position;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			playerHand.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
		else
		{
			playerHand.transform.rotation = new Quaternion();
		}

		if (curItem.amount <= 0)
		{
			curItem = null;
			holdItem.SetItemSprite(null);
		}
	}

	public void SetHoldItem(Item item)
	{
		if (item == null || item.GetItemData() == null)
		{
			curItem = null;
			holdItem.SetItemSprite(null);
			return;
		}

		curItem = item;
		holdItem.SetItemSprite(curItem.GetItemData().sprite);
	}
}
