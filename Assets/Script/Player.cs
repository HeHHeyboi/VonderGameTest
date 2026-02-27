using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Rigidbody2D rb;
	public float speed = 20;
	public Item curItem;
	public GameObject playerHand;
	public bool flipH;
	public HoldItem holdItem;
	public int prevDir;

	void Update()
	{
		float horizontal = 0;
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			horizontal = -1;
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			horizontal = 1;
		}
		transform.Translate(horizontal * speed * Time.deltaTime * new Vector2(1, 0));

		UpdatePlayerItem();
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
	}

	public void SetHoldItem(Item item)
	{
		if(item == null || item.GetItemData() == null)
		{
			curItem = null;
			holdItem.SetItemSprite(null);
			return;
		}

		curItem = item;
		holdItem.SetItemSprite(curItem.GetItemData().sprite);
	}
}
