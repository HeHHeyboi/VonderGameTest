using UnityEngine;

public interface IEnemy
{
	public void OnPlayerEnterDetection(Collider2D collider);
	public void OnPlayerExitDetection(Collider2D collider);
	public void OnPlayerEnterAttackRange(Collider2D collider);
	public void OnPlayerExitAttackRange(Collider2D collider);
}
