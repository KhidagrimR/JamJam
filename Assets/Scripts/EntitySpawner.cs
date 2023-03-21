using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [Tooltip("Tableau contenant les entités à faire apparaître")]
    public GameObject[] entitiesToSpawn;
    [Tooltip("Les poids de spawn pour chaque entité")]
    public float[] entitySpawnWeights;
    [Tooltip("Center of the game object")]
    public Transform spawnCenter;

    [Tooltip("Le rayon minimal de spawn des entités")]
    public float minSpawnRadius = 5f;
    [Tooltip("Le rayon maximal de spawn des entités")]
    public float maxSpawnRadius = 10f;

    public Vector2 minMaxAmountOfEntityToSpawn;


    public void SpawnEntities(float multiplier = 1.0f)
    {
        int amount = (int) Random.Range(minMaxAmountOfEntityToSpawn.x, minMaxAmountOfEntityToSpawn.y + 1); // +1 beacause exclusive
        amount = (int) Mathf.Round(amount * multiplier);

        for(int i = 0; i < amount; i++)
        {
            SpawnEntity();
        }
    }

    private void SpawnEntity()
    {
        // Calcule le total des poids de spawn des entités
        float totalSpawnWeight = 0f;
        for (int i = 0; i < entitiesToSpawn.Length; i++)
        {
            totalSpawnWeight += entitySpawnWeights[i];
        }

        // Générer un nombre aléatoire entre 0 et le poids total
        float randomWeight = Random.Range(0f, totalSpawnWeight);

        // Boucle sur les entités et soustrait le poids de leur spawn du nombre aléatoire
        // jusqu'à ce que le nombre aléatoire devienne négatif ou nul. L'entité qui rend le nombre aléatoire
        // aléatoire à zéro ou positif est l'entité qui est spawnée.
        int indexToSpawn = 0;
        while (randomWeight >= 0f)
        {
            randomWeight -= entitySpawnWeights[indexToSpawn];
            indexToSpawn++;
        }
        indexToSpawn--;

        // On choisit un objet aléatoire à faire apparaître
        GameObject objectToSpawn = entitiesToSpawn[Random.Range(0, entitiesToSpawn.Length)];
        // On calcule une position aléatoire dans le rayon autour du joueur
        float spawnRadius = Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector3 spawnPosition = spawnCenter.position + Random.insideUnitSphere * spawnRadius;
        // On ajuste la position pour qu'elle soit au niveau du sol
        spawnPosition.z = 0f;
        // On crée l'objet à la position calculée
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        // Dessine un cercle à l'emplacement du centre de spawn
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(spawnCenter.position, 0.2f);

        // Dessine un cercle représentant le rayon minimal de spawn
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnCenter.position, minSpawnRadius);

        // Dessine un cercle représentant le rayon maximal de spawn
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnCenter.position, maxSpawnRadius);
    }
}
