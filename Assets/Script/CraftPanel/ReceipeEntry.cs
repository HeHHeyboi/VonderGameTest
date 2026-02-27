using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReceipeEntry : MonoBehaviour
{
	public Image recipeImage;
	public TMP_Text recipeName,
		amount;

	public void SetUp(Sprite sprite, string recipeName, int amount)
	{
		recipeImage.sprite = sprite;
		this.recipeName.text = recipeName;
		this.amount.text = "x" + amount;
	}
}
