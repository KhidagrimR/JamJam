using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Entity
{
    private PenguinManager penguinManager;

    protected override void Start()
    {
        base.Start();
        penguinManager = GameObject.FindObjectOfType<PenguinManager>();
    }

    protected override void Update()
    {
        base.Update();
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
        penguinManager.followPenguins.Remove(gameObject);
        base.Death();
    }

    public override void TakeDamage(int damageTaken)
    {
        base.TakeDamage(damageTaken);
    }
}
