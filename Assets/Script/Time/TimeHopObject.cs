using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHopObjet : MonoBehaviour
{
	public GameEvent ChangePeriod;
	public GameEvent ChangeDay;
	public ChangeType type;

	void ChangeTime()
	{
		switch (type)
		{
			case ChangeType.ChangeDay:
				ChangeDay.Raise();
				break;
			case ChangeType.ChangePeriod:
				ChangePeriod.Raise();
				break;
		}
	}

	void OnTriggerEnter2D()
	{
		ChangeTime();
	}

	void OnTriggerExit2D()
	{
		ChangeTime();
	}

	public void OnClick()
	{
		ChangeTime();
	}
}

public enum ChangeType
{
	ChangeDay,
	ChangePeriod,
}
