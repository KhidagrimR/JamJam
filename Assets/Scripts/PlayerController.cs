using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Ce script permet de déplacer le joueur à la position cliqué (avec clique gauche)

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 targetPosition;
    public Animator animator;
    public List<Egg> eggs;
    PenguinManager penguinManager;

    bool isMoving
    {
        get 
        {
            return transform.hasChanged;
        }
    }

    void Start()
    {
        penguinManager = GameObject.FindObjectOfType<PenguinManager>();
        eggs = new List<Egg>();
    }

    private void Update()
    {
        ClickToMove();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Kick();
        }

        if(isMoving)
        {
            animator.SetBool("isWalking",true);
        }
        else
        {
            animator.SetBool("isWalking",false);
        }
    }

    // Déplace l'entité à la position cliqué
    private void ClickToMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    private void Kick()
    {
        animator.SetTrigger("Kick");
        // if overlapping eggs, make them hatch (spawn)
        if(eggs.Count > 0)
        {
            eggs[0].isBroken = true;
            eggs[0].GetComponent<EntitySpawner>().SpawnEntities();
            eggs.Remove(eggs[0]);
        }
        else
        {
            // on prends un penguin de la bande
            Penguin penguin = penguinManager.GetAFollowingPenguin();

            if(penguin == null) return;
            // on prends la direction de la souris
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 direction = (mousePosition - transform.position).normalized;

            // on le ramasse
            penguin.transform.position = transform.position;

            //et on le tej
            penguin.GetThrowed(direction, 1.0f);
        }

        //else, throw a near penguin in front
    }
    

}
