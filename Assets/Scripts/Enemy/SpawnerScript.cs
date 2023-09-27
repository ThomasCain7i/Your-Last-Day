using System.Collections;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform[] spawnPoints; // Array of spawn locations
    public float initialSpawnDelay = 2f; // Initial delay before spawning begins
    public float spawnInterval = 3f; // Time between enemy spawns
    public int initialSpawnCount = 1; // Initial number of enemies to spawn
    public int spawnIncreaseAmount = 1; // Amount to increase spawns after each wave

    [SerializeField] Light playerLight;

    [SerializeField] private GameObject doorOpen, doorClosed;

    private bool spawningEnabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !spawningEnabled)
        {
            spawningEnabled = true;
            StartCoroutine(SpawnEnemies());
            doorOpen.SetActive(false);
            doorClosed.SetActive(true);
            playerLight.enabled = true;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        int currentSpawnCount = initialSpawnCount;

        while (spawningEnabled)
        {
            SpawnWave(currentSpawnCount);
            currentSpawnCount += spawnIncreaseAmount;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnWave(int count)
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned to the enemy spawner.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
