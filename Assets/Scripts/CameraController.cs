using UnityEngine;

// Ce script permet de suivre n'importe quel gameobject

public class CameraController : MonoBehaviour
{
    public GameObject target; // L'objet que la caméra doit suivre
    public float followSpeed = 2f; // La vitesse à laquelle la caméra doit suivre l'objet
    public Vector3 offset; // La distance à laquelle la caméra doit être par rapport à l'objet

    void Update()
    {
        // On calcule la position que la caméra doit atteindre pour suivre l'objet
        Vector3 targetPosition = target.transform.position + offset;
        // On calcule la position actuelle de la caméra
        Vector3 currentPosition = transform.position;
        // On calcule la nouvelle position de la caméra en interpolant entre sa position actuelle et la position de l'objet à suivre
        Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, followSpeed * Time.deltaTime);
        // On affecte la nouvelle position à la caméra
        transform.position = newPosition;
    }
}
