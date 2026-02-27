using UnityEngine;

public interface IEquipable
{
	void Equiped(GameObject parent);
}

public interface IUseable
{
	void Use();
}

public interface IPlaceable
{
	void Place();
}
