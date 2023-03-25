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
    [SerializeField] ParticleSystem feather;

    [Tooltip("Duration to go to the position where it is supposed to be threw")]
    public float throwDuration = 1.0f;

    [Header("Sons")]
    public AudioSource audioSource;
    public AudioClip attack;
    public AudioClip death;
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
        PlaySound(attack);
        base.AttackEnemy();
    }

    protected override void TargetEnemy()
    {
        base.TargetEnemy();
    }

    protected override void Death()
    {
        penguinManager.followPenguins.Remove(gameObject);
        animator.SetTrigger("Dead");
        feather.Play();
        PlaySound(death);
    }

    //Function call directly in the death animation
    public void DefinitiveDeath()
    {
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

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
