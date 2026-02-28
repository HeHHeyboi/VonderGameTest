using UnityEngine;

public class Slime : Enemy
{
	Player player;
	public GameObject SmallSlimeStore;
	[SerializeField]
	float smallSlimeLaunchForce = 3f;
	LayerMask groundLayer;
	[SerializeField]
	Collider2D bodyCollider;

	void Awake()
	{
		bodyCollider = GetComponent<Collider2D>();
		groundLayer = LayerMask.GetMask("Floor");
	}
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

	void Start()
	{
		attackCooldown = maxCooldown;
	}

	void Update()
	{
		if (player != null && !enterAttackRange && IsGrounded())
		{
			float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
			transform.Translate(direction * moveSpeed * Time.deltaTime * Vector2.right);
		}

		attackCooldown -= Time.deltaTime;

		if (attackCooldown <= 0)
		{
			canAttack = true;
			if (player != null && enterAttackRange)
			{
				AttackPlayer();
			}
		}
	}

	public override void OnPlayerEnterDetection(Collider2D collider)
	{
		if (!collider.CompareTag("Player"))
			return;
		player = collider.GetComponent<Player>();
		Debug.Log("Player Enter");
	}

	public override void OnPlayerExitDetection(Collider2D collider)
	{
		if (!collider.CompareTag("Player"))
			return;
		player = null;
		Debug.Log("Player Left");
	}

	public override void OnPlayerEnterAttackRange(Collider2D collider)
	{
		if (!collider.CompareTag("Player"))
			return;
		Debug.Log("Attack");
		player = collider.GetComponent<Player>();
		enterAttackRange = true;
		if (canAttack)
		{
			AttackPlayer();
		}
	}

	public override void OnPlayerExitAttackRange(Collider2D collider)
	{
		if (!collider.CompareTag("Player"))
			return;
		Debug.Log("Out Of Attack Range");
		enterAttackRange = false;
	}

	void AttackPlayer()
	{
		if (player != null)
		{
			player.TakeDamage(damage);
			attackCooldown = maxCooldown;
			canAttack = false;
		}
	}

	public override void TakeDamage(int damage)
	{
		health.TakeDamage(damage);
	}

	public override void OnDeath()
	{
		Debug.Log("Slime Died");
		SmallSlimeStore.SetActive(true);
		var smallSlimes = SmallSlimeStore.GetComponentsInChildren<SmallSlime>();
		foreach (var smallSlime in smallSlimes)
		{
			smallSlime.transform.SetParent(null, true);
			var rb = smallSlime.GetComponent<Rigidbody2D>();
			if (rb == null)
				continue;

			rb.velocity = Vector2.zero;
			var launchDirection = Random.insideUnitCircle;
			if (launchDirection == Vector2.zero)
			{
				launchDirection = Vector2.up;
			}
			rb.AddForce(launchDirection.normalized * smallSlimeLaunchForce, ForceMode2D.Impulse);
		}
		Destroy(gameObject);
	}

	bool IsGrounded()
	{
		if (bodyCollider == null)
			return true;
		return groundLayer.value == 0 ? bodyCollider.IsTouchingLayers() : bodyCollider.IsTouchingLayers(groundLayer);
	}
}
