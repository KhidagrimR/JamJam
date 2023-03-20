using UnityEngine;

// Ce script permet de faire spawn des entités (pensé pour des pingouins) dans un rayon défini

public class SpawnManager : MonoBehaviour
{
    [Tooltip("Tableau contenant les entités à faire apparaître")]
    public GameObject[] entitiesToSpawn;
    [Tooltip("Le transform du joueur à utiliser comme centre pour le rayon")]
    public Transform spawnCenter;
    [Tooltip("Le rayon minimal de spawn des entités")]
    public float minSpawnRadius = 5f;
    [Tooltip("Le rayon maximal de spawn des entités")]
    public float maxSpawnRadius = 10f;
    [Tooltip("L'intervalle de temps en seconde entre chaque spawn")]
    public float spawnInterval = 5f;
    private float timeSinceLastSpawn = 0f;
    [Tooltip("Les poids de spawn pour chaque entité")]
    public float[] entitySpawnWeights;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEntity();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEntity()
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
