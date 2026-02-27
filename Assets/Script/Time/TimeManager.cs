using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
	public GameEvent ChangePeriod;
	public GameEvent ChangeDay;

	public GameEvent<Period> OnPeriodChange;
	public GameEvent<Day, int> OnDayChange;
	public Period curPeriod = Period.Morning;
	public Day curDay = Day.Mon;
	public int curDate = 1;

	void Start()
	{
		ChangePeriod.AddListener(NextPeriod);
		ChangeDay.AddListener(NextDay);
		OnPeriodChange.Raise(curPeriod);
		OnDayChange.Raise(curDay, curDate);
	}

	public void NextPeriod()
	{
		switch (curPeriod)
		{
			case Period.Morning:
				curPeriod = Period.Afternoon;
				break;
			case Period.Afternoon:
				curPeriod = Period.Evening;
				break;
			case Period.Evening:
				curPeriod = Period.Morning;
				NextDay();
				break;
		}
		OnPeriodChange.Raise(curPeriod);
	}

	public void NextDay()
	{
		switch (curDay)
		{
			case Day.Sun:
				curDay = Day.Mon;
				curDate += 1;
				break;
			default:
				curDay += 1;
				curDate += 1;
				break;
		}
		OnDayChange.Raise(curDay, curDate);
	}
}

public enum Period
{
	Morning,
	Afternoon,
	Evening,
}

public enum Day
{
	Mon,
	Tue,
	Wed,
	Thu,
	Fri,
	Sat,
	Sun,
}
