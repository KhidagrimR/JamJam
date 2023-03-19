using UnityEngine;

// Ce script permet de déplacer le joueur à la position cliqué (avec clique gauche)

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 targetPosition;

    private void Update()
    {
        ClickToMove();
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

}
