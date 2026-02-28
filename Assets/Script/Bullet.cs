using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float bulletSpeed = 20;
	public float bulletLifeTime = 1f;
	public float curLifeTime = 0f;
	int damage = 0;

	void Update()
	{
		var deltaTime = Time.deltaTime;
		curLifeTime += deltaTime;
		if (curLifeTime >= bulletLifeTime)
		{
			Destroy(gameObject);
		}

		transform.position += bulletSpeed * deltaTime * transform.right;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			var enemy = collision.GetComponent<Enemy>();
			enemy.TakeDamage(damage);
			Destroy(gameObject);
		}
	}

	public void SetBulletDamage(int damage)
	{
		this.damage = damage;
	}

	public int GetBulletDamage()
	{
		return damage;
	}
}
