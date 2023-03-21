using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private EnemyManager enemyManager;
    bool hasTarget = false;
    Transform targetToHunt;

    protected override void Start()
    {
        base.Start();
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
    }

    protected override void Update()
    {
        base.Update();

        if(!hasTarget)
        {
            targetToHunt = enemyManager.AssignEnemyTarget();
            hasTarget = true;
        }

        if(hasTarget && ! isFighting)
        {
            Vector2 direction = (targetToHunt.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    protected override void AttackEnemy()
    {
        base.AttackEnemy();
    }

    protected override void TargetEnemy()
    {
        base.TargetEnemy();
    }

    protected override void Death()
    {
        base.Death();
    }

    public override void TakeDamage(int damageTaken)
    {
        base.TakeDamage(damageTaken);
    }
    
}
