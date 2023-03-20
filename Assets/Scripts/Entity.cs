using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script sert pour les ennemis et les alliés (les pingouins)
// Pour override les fonctions selon l'entité il faut utiliser les scripts Penguin.cs et Enemy.cs 
public abstract class Entity : MonoBehaviour
{
    [HideInInspector] public bool isFighting = false;
    protected GameObject nearestEnemy;
    [Header("Global Settings")]
    [Tooltip("Nom de l'entité")]
    public string entityName;
    [Tooltip("Points de vie de l'entité")]
    public int healthPoint;
    [Tooltip("Vitesse de déplacement de l'entité")]
    public float speed = 5f;
    [Header("Combat Settings")]
    [Tooltip("Le layer des ennemis à combattre")]
    public LayerMask targerLayer;
    [Tooltip("Les dégâts infligés à chaque attaque")]
    public int damagePerHit;
    [Tooltip("La distance à partir de laquelle l'entité va attaquer un ennemi")]
    public float fightRadius = 5f;
    [Tooltip("Le temps en secondes entre chaque attaque")]
    public float timeBetweenHits = 1f;
    private float timeSinceLastHit = 0f;

    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        if (isFighting)
        {
            // Si l'entité est en mode combat, on attaque l'ennemi toutes les timeBetweenHits secondes
            timeSinceLastHit += Time.deltaTime;
            if (timeSinceLastHit >= timeBetweenHits)
            {
                timeSinceLastHit = 0f;
                AttackEnemy();
            }
        }
        else
        {
            TargetEnemy();
        }
    }

    // Effectue des dégâts à l'ennemi
    protected virtual void AttackEnemy()
    {
        if (nearestEnemy == null)
        {
            isFighting = false;
        }
        else
        {
            nearestEnemy.GetComponent<Entity>().TakeDamage(damagePerHit);
            Debug.Log("<color=#ADD8E6>" + entityName + "</color> attaque du <color=red>" + damagePerHit + "</color> sur <color=#ADD8E6>" + nearestEnemy.GetComponent<Entity>().entityName + "</color>");
        }
    }

    protected virtual void TargetEnemy()
    {
        // Si l'entité n'est pas en train de combattre, on vérifie la distance à un ennemi
        if (!isFighting)
        {
            Collider2D nearestCollider = Physics2D.OverlapCircle(transform.position, fightRadius, targerLayer);
            if (nearestCollider != null)
            {
                // Si un ennemi est dans la zone de combat, on le déplace vers l'ennemi le plus proche
                isFighting = true;
                Vector2 direction = (nearestCollider.transform.position - transform.position).normalized;
                transform.position += (Vector3)(direction * speed * Time.deltaTime);

                nearestEnemy = nearestCollider.gameObject;
            }
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
        Debug.Log("<color=#ADD8E6>" + entityName + "</color> est mort");
    }

    public virtual void TakeDamage(int damageTaken)
    {
        healthPoint -= damageTaken;
        if (healthPoint <= 0)
        {
            Death();
        }
    }
}
