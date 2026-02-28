using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
	protected Transform player;
	public float moveSpeed = 10;
	public bool enterAttackRange = false;
	public Area2D detectionZone;
	public Area2D attackZone;

	void OnEnable()
	{
		detectionZone.AddOnEnterListener(OnPlayerEnterDetection);
		attackZone.AddOnEnterListener(OnPlayerEnterAttackRange);

		detectionZone.AddOnExitListener(OnPlayerExitDetection);
		attackZone.AddOnExitListener(OnPlayerExitAttackRange);
	}

	void OnDisable()
	{
		detectionZone.RemoveOnEnterListener(OnPlayerEnterDetection);
		detectionZone.RemoveOnExitListener(OnPlayerExitDetection);

		attackZone.RemoveOnEnterListener(OnPlayerEnterAttackRange);
		attackZone.RemoveOnExitListener(OnPlayerExitAttackRange);
	}

	void Update()
	{
		if (player != null && !enterAttackRange)
		{
			float direction = Mathf.Sign(player.position.x - transform.position.x);
			transform.Translate(direction * moveSpeed * Time.deltaTime * Vector2.right);
		}
	}

	public void OnPlayerEnterDetection(Collider2D collider)
	{
		if (!collider.CompareTag("Player"))
			return;
		player = collider.transform;
		Debug.Log("Player Enter");
	}

	public void OnPlayerExitDetection(Collider2D collider)
	{
		if (!collider.CompareTag("Player"))
			return;
		player = null;
		Debug.Log("Player Left");
	}

	public void OnPlayerEnterAttackRange(Collider2D collider)
	{
		if (!collider.CompareTag("Player"))
			return;
		Debug.Log("Attack");
		enterAttackRange = true;
	}

	public void OnPlayerExitAttackRange(Collider2D collider)
	{
		if (!collider.CompareTag("Player"))
			return;
		Debug.Log("Out Of Attack Range");
		enterAttackRange = false;
	}
}
