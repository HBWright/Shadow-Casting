using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [System.Serializable]
    public class AnimalEntry
    {
        public string animalName;
        public GameObject prefab;
    }

    [Header("Animal Prefabs")]
    public AnimalEntry[] animals;

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

    public void SpawnAnimal(string animalName)
    {
        // Prevent multiple spawns
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

        AnimalEntry entry = System.Array.Find(animals, a => a.animalName == animalName);
        if (entry == null || entry.prefab == null)
        {
            Debug.LogWarning($"AnimalSpawner: No prefab found for '{animalName}'.");
            return;
        }

        Vector3 spawnPos = player.position + player.forward * spawnDistance;
        spawnPos.y += heightOffset;

        activeAnimal = Instantiate(entry.prefab, spawnPos, Quaternion.identity);

        // Give it a lifecycle controller and tell it who spawned it
        var life = activeAnimal.AddComponent<AnimalLifecycle>();
        life.Initialize(defaultIdleLifetime, this);

        Debug.Log($"Spawned {animalName} at {spawnPos}");
    }

    // Called by AnimalLifecycle when the creature despawns
    public void OnAnimalDespawned()
    {
        activeAnimal = null;
        Debug.Log("AnimalSpawner: Animal despawned, spawner unlocked.");
    }

    private void Update()
    {
        // Temporary test: press R to spawn a rabbit
        if (Input.GetKeyDown(KeyCode.R))
            SpawnAnimal("Rabbit");
    }
}
