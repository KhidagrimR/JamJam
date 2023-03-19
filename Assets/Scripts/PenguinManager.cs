using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère tout le déplacement des pingouins

public class PenguinManager : MonoBehaviour
{
    [Tooltip("Rayon dans lequel les pingouins vont suivre le joueur")]
    public float followRadius = 5f;

    [Tooltip("Rayon de collision entre les pingouins")]
    public float collisionRadius = 0.5f;

    private GameObject player;
    private Penguin penguin;
    [HideInInspector] public List<GameObject> followPenguins = new List<GameObject>();

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        FollowPlayer();
    }

    // Suis le joueur quand un penguin est dans un rayon X de lui. Les penguins en combat ne suivent pas le joueur
    private void FollowPlayer()
    {
        // On vérifie la distance de chaque pingouin à l'entité joueur
        foreach (GameObject penguin in GameObject.FindGameObjectsWithTag("Penguin"))
        {
            Penguin penguinScript = penguin.GetComponent<Penguin>();
            if (!penguinScript.isFighting)
            {
                float distance = Vector2.Distance(penguin.transform.position, player.transform.position);

                // Si le pingouin est dans le rayon de suivi et n'est pas déjà un pingouin suiveur, on l'ajoute à la liste
                if (distance <= followRadius && !followPenguins.Contains(penguin))
                {
                    followPenguins.Add(penguin);
                }
            }
            else if (followPenguins.Contains(penguin))
            {
                followPenguins.Remove(penguin);
            }
        }

        // On déplace chaque pingouin suiveur vers le joueur
        foreach (GameObject penguin in followPenguins)
        {
            Penguin penguinScript = penguin.GetComponent<Penguin>();
            Vector2 direction = (player.transform.position - penguin.transform.position).normalized;

            // On détecte les collisions entre pingouins
            Collider2D[] colliders = Physics2D.OverlapCircleAll(penguin.transform.position, collisionRadius);
            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.CompareTag("Penguin") && col.gameObject != penguin)
                {
                    // On calcule la direction pour sortir de la zone de collision
                    Vector2 exitDirection = (penguin.transform.position - col.transform.position).normalized;
                    penguin.transform.position += (Vector3)(exitDirection * penguinScript.speed * Time.deltaTime);
                }
            }

            penguin.transform.position += (Vector3)(direction * penguinScript.speed * Time.deltaTime);
        }
    }

    // Affiche la zone de suivi du joueur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
