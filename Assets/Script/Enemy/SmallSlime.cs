using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSlime : Enemy
{
    Player player;

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
        if (player != null && !enterAttackRange)
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
        Debug.Log("Small Slime Died");
    }
}

