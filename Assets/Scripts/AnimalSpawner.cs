using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [Header("Player Reference")]
    public Transform player;

    [Header("Spawn Settings")]
    [Tooltip("How far in front of the player to spawn animals.")]
    public float spawnDistance = 2.0f;

    [Tooltip("Optional height offset for spawning.")]
    public float heightOffset = 0.0f;

    [Header("Lifetime Settings")]
    [Tooltip("Default idle time before despawning, if no task is active.")]
    public float defaultIdleLifetime = 10f;

    private GameObject activeAnimal;   // currently spawned animal

    // --- Spawn by prefab (Inspector-friendly version) ---
    public void SpawnAnimal(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("AnimalSpawner: No prefab provided to spawn.");
            return;
        }

        if (activeAnimal != null)
        {
            Debug.Log("AnimalSpawner: Cannot spawn — another animal is still active.");
            return;
        }

        if (player == null)
        {
            Debug.LogWarning("AnimalSpawner: Player reference not assigned.");
            return;
        }

        Vector3 spawnPos = player.position + player.forward * spawnDistance;
        spawnPos.y += heightOffset;

        activeAnimal = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Add lifecycle controller and initialize
        var life = activeAnimal.AddComponent<AnimalLifecycle>();
        life.Initialize(defaultIdleLifetime, this);

        Debug.Log($"Spawned {prefab.name} at {spawnPos}");
    }

    // Called by AnimalLifecycle when the creature despawns
    public void OnAnimalDespawned()
    {
        activeAnimal = null;
        Debug.Log("AnimalSpawner: Animal despawned, spawner unlocked.");
    }
}
