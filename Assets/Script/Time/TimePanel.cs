using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimePanel : MonoBehaviour
{
	public GameEvent<Period> OnPeriodChange;
	public GameEvent<Day, int> OnDayChange;
	public TMP_Text periodText;
	public TMP_Text dayText;

	private void OnEnable() 
	{
		OnPeriodChange.AddListener(ChangePeriodText);
		OnDayChange.AddListener(ChangeDayText);
	}

	private void OnDisable() 
	{
		OnPeriodChange.RemoveListener(ChangePeriodText);
		OnDayChange.RemoveListener(ChangeDayText);
	}

	void ChangePeriodText(Period period)
	{
		string periodText = "";
		switch (period)
		{
			case Period.Morning:
				periodText = "Morning";
				break;
			case Period.Afternoon:
				periodText = "Afternoon";
				break;
			case Period.Evening:
				periodText = "Evening";
				break;
		}
		this.periodText.text = periodText;
	}

	void ChangeDayText(Day day, int date)
	{
		string dayText = "";
		switch (day)
		{
			case Day.Sun:
				dayText = "Sun.";
				break;
			case Day.Mon:
				dayText = "Mon.";
				break;
			case Day.Tue:
				dayText = "Tue.";
				break;
			case Day.Wed:
				dayText = "Wed.";
				break;
			case Day.Thu:
				dayText = "Thu.";
				break;
			case Day.Fri:
				dayText = "Fri.";
				break;
			case Day.Sat:
				dayText = "Sat.";
				break;
		}
		this.dayText.text = dayText + " day " + date.ToString();
	}
}
