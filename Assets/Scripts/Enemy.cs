using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private EnemyManager enemyManager;
    bool hasTarget = false;
    Transform targetToHunt;

    [Tooltip("Rayon du cercle d'attque")] 
    [SerializeField] float attackRadius;
    [SerializeField] ParticleSystem attack;

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
        if (nearestEnemy == null)
        {
            isFighting = false;
        }
        else
        {
            //Create a circle around him and attack all penguins in the circle
            LayerMask penguinMask = LayerMask.GetMask("Penguin");
            Collider2D[] penguins = Physics2D.OverlapCircleAll(transform.position, attackRadius, penguinMask);
            attack.Play();
            foreach (Collider2D pengu in penguins)
            {
                pengu.gameObject.GetComponent<Entity>().TakeDamage(damagePerHit);
            }
        }

        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
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
