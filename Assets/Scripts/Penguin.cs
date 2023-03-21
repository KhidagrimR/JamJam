using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Entity
{
    [Header("Throw your penguin")]
    private PenguinManager penguinManager;
    [HideInInspector]public bool isBeingThrowed;
    Vector3 positionToBeThrew;
    Animator animator;

    [Tooltip("Duration to go to the position where it is supposed to be threw")]
    public float throwDuration = 1.0f;

    protected override void Start()
    {
        base.Start();
        penguinManager = GameObject.FindObjectOfType<PenguinManager>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if(!isBeingThrowed)
            base.Update();
        else
            SetThrowMovement();
        
        animator.SetBool("isFighting",isFighting);
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

    public void GetThrowed(Vector3 direction, float throwPower)
    {
        isBeingThrowed = true;
        positionToBeThrew = direction * throwPower;
        Invoke("StopThrow", throwDuration);
    }

    private void StopThrow()
    {
        isBeingThrowed = false;
    }

    public void SetThrowMovement()
    {
        // speed : 
        //v = d/t
        float speed = (penguinManager.player.transform.position - positionToBeThrew).magnitude / throwDuration;
        transform.position += (Vector3)(positionToBeThrew * speed * Time.deltaTime);
    }
}
