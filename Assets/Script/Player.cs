using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Rigidbody2D rb;
	public float speed = 20;
	public Item curItem;
	public bool flipH;
	public HoldItem holdItem;
	public int prevDir;

	void Update()
	{
		float horizontal = 0;
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			horizontal = -1;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			horizontal = 1;
		}
		// float horizontal = Input.GetAxis("Horizontal");
		// holdItem = transform.GetComponent<HoldItem>();
		transform.Translate(horizontal * speed * Time.deltaTime * new Vector2(1, 0));
	}

	public void SetHoldItem(Item item)
	{
		curItem = item;
		if (curItem != null)
		{
			holdItem.SetItemSprite(curItem.item_data.sprite);
		}
	}
}
