using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Craft Receipe")]
public class CraftReceipe : ScriptableObject
{
	public ItemData item;
	public int amount;
}
