using UnityEngine;
using UnityEngine.Events;

public class Area2D : MonoBehaviour
{
	public UnityEvent<Collider2D> OnEnter = new();

	public UnityEvent<Collider2D> OnExit = new();

	public void AddOnEnterListener(UnityAction<Collider2D> listener)
	{
		OnEnter.AddListener(listener);
	}

	public void AddOnExitListener(UnityAction<Collider2D> listener)
	{
		OnExit.AddListener(listener);
	}

	public void RemoveOnEnterListener(UnityAction<Collider2D> listener)
	{
		OnEnter.RemoveListener(listener);
	}

	public void RemoveOnExitListener(UnityAction<Collider2D> listener)
	{
		OnExit.RemoveListener(listener);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		OnEnter.Invoke(collider);
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		OnExit.Invoke(collider);
	}
}
