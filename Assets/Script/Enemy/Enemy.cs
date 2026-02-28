using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[SerializeField]
	protected Health health;
	public float moveSpeed = 10;
	public float maxCooldown = 0.5f;
	public float attackCooldown;
	public bool canAttack = true;
	public bool enterAttackRange = false;
	public Area2D detectionZone;
	public Area2D attackZone;
	public int damage = 10;
	public abstract void OnPlayerEnterDetection(Collider2D collider);
	public abstract void OnPlayerExitDetection(Collider2D collider);
	public abstract void OnPlayerEnterAttackRange(Collider2D collider);
	public abstract void OnPlayerExitAttackRange(Collider2D collider);
	public abstract void TakeDamage(int damage);
	public virtual void OnDeath()
	{
		Debug.Log("Enemy Died");
	}
}

// public interface IEnemy
// {
// 	public void OnPlayerEnterDetection(Collider2D collider);
// 	public void OnPlayerExitDetection(Collider2D collider);
// 	public void OnPlayerEnterAttackRange(Collider2D collider);
// 	public void OnPlayerExitAttackRange(Collider2D collider);
// 	public void TakeDamage(int damage);
// }
