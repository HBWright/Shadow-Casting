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

    // --- Main spawn method ---
    public void SpawnAnimal(string animalName)
    {
        if (player == null)
        {
            Debug.LogWarning("AnimalSpawner: Player reference not assigned.");
            return;
        }

        // Find the prefab that matches the requested animal name
        AnimalEntry entry = System.Array.Find(animals, a => a.animalName == animalName);
        if (entry == null || entry.prefab == null)
        {
            Debug.LogWarning($"AnimalSpawner: No prefab found for '{animalName}'.");
            return;
        }

        // Determine spawn position relative to player
        Vector3 spawnPos = player.position + player.forward * spawnDistance;
        spawnPos.y += heightOffset;

        // Instantiate animal prefab
        GameObject spawnedAnimal = Instantiate(entry.prefab, spawnPos, Quaternion.identity);

        Debug.Log($"Spawned {animalName} at {spawnPos}");
    }
}
