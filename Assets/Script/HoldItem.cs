using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
	public Player player;
	public SpriteRenderer itemSprite;

	public void SetItemSprite(Sprite image)
	{
		itemSprite.sprite = image;
	}

	public void FlipImage()
	{
		itemSprite.flipX = !itemSprite.flipX;
	}
}
