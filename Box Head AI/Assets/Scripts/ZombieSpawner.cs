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
            // Generate a random point in XZ plane
            Vector2 randomPos2D = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(
                transform.position.x + randomPos2D.x,
                transform.position.y,
                transform.position.z + randomPos2D.y
            );

            GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

            if (zombiesContainer != null)
                zombie.transform.parent = zombiesContainer;
        }
    }
}
