using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[SerializeField]
	protected Health health;
	[SerializeField]
	protected float moveSpeed = 10;
	[SerializeField]
	protected float maxCooldown = 0.5f;
	[SerializeField]
	protected float attackCooldown;
	protected bool canAttack = true;
	protected bool enterAttackRange = false;
	[SerializeField]
	protected Area2D detectionZone;
	[SerializeField]
	protected Area2D attackZone;
	protected int damage = 10;
	protected abstract void OnPlayerEnterDetection(Collider2D collider);
	protected abstract void OnPlayerExitDetection(Collider2D collider);
	protected abstract void OnPlayerEnterAttackRange(Collider2D collider);
	protected abstract void OnPlayerExitAttackRange(Collider2D collider);
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
