using UnityEngine;

public class Enemy : MonoBehaviour
{
	Transform player;
	public float speed = 10;

	void Update()
	{
		if (player != null)
		{
			if (player.position.x >= transform.position.x)
			{
				transform.Translate(speed * Time.deltaTime * new Vector2(1, 0));
			}
			else
			{
				transform.Translate(-speed * Time.deltaTime * new Vector2(1, 0));
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			player = other.transform;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			player = null;
		}
	}
}
