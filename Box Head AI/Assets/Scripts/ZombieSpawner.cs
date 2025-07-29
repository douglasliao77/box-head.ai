using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public int numberOfZombies = 5;
    public float spawnRadius = 5f;

    public Transform zombiesContainer; // Assign the "Zombies" empty GameObject here

    void Start()
    {
        for (int i = 0; i < numberOfZombies; i++)
        {
            Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
            if (zombiesContainer != null)
                zombie.transform.parent = zombiesContainer;
        }
    }
}

