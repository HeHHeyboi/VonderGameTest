using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ItemData")]
public class ItemData : ScriptableObject
{
	public string itemName;
	public Sprite sprite;
	public ItemType type;
	public bool stackable = true;
	public bool craftAble = false;
	public CraftReceipe[] craftReceipes;
	public readonly int maxStack = 10;
}

public enum ItemType
{
	Weapon,
	Resource,
	Placeable,
}
