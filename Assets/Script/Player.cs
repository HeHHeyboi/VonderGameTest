using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Rigidbody2D rb;
	public float speed = 20;
	float horizontal;

	void Update()
	{
		horizontal = Input.GetAxis("Horizontal");
	}

	void FixedUpdate()
	{
		transform.Translate(horizontal * speed * Time.fixedDeltaTime * new Vector2(1, 0));
		// rb.AddForce(horizontal * speed * Time.fixedDeltaTime * new Vector2(1, 0));
	}

	void flipPlayer(bool flipped)
	{
		if (flipped)
		{
			transform.Rotate(new Vector3(0, 180));
		}
		else
		{
			transform.Rotate(new Vector3(0, -180));
		}
	}
}
