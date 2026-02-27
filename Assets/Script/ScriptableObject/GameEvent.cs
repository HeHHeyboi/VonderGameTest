using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "GameEvent")]
public class GameEvent<T> : ScriptableObject
{
	public UnityEvent<T> OnEventRaised = new UnityEvent<T>();

	public void AddListener(UnityAction<T> listener)
	{
		OnEventRaised.AddListener(listener);
	}

	public void RemoveListener(UnityAction<T> listener)
	{
		OnEventRaised.RemoveListener(listener);
	}

	public void Raise(T value)
	{
		OnEventRaised.Invoke(value);
	}
}

public class GameEvent<T1, T2> : ScriptableObject
{
    public UnityEvent<T1, T2> OnEventRaised = new UnityEvent<T1, T2>();

    public void AddListener(UnityAction<T1, T2> listener)
    {
        OnEventRaised.AddListener(listener);
    }

    public void RemoveListener(UnityAction<T1, T2> listener)
    {
        OnEventRaised.RemoveListener(listener);
    }

    public void Raise(T1 value1, T2 value2)
    {
        OnEventRaised.Invoke(value1, value2);
    }
}
