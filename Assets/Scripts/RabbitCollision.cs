using UnityEngine;

public class RabbitCollision : MonoBehaviour
{
    private static int currentIndex = 0;  // Shared progress across all rabbits
    private static readonly string[] destroyTags = { "RabbitDestory1", "RabbitDestory2" };
    private static readonly string[] AddTags = { "RabbitFloor1", "RabbitFloor2" };

    private bool hasTriggered = false;    // Prevents duplicate triggers from same rabbit

    private void OnTriggerEnter(Collider other)
    {
        // Ignore if already processed
        if (hasTriggered) return;

        if (other.CompareTag("RabbitWall"))
        {
            hasTriggered = true;

            // Disable this rabbit’s collider immediately
            Collider myCollider = GetComponent<Collider>();
            if (myCollider != null)
                myCollider.enabled = false;

            if (currentIndex >= destroyTags.Length)
            {
                Debug.Log("🐇 All RabbitDestroy targets cleared!");
                return;
            }

            string targetTag = destroyTags[currentIndex];
            string floorTag = AddTags[currentIndex];
            GameObject target = GameObject.FindWithTag(targetTag);
            GameObject floor = GameObject.FindWithTag(floorTag);

            if (target != null)
            {
                Destroy(target);
                Debug.LogWarning($"{name}: Destroyed {targetTag}");

                floor.SetActive(true);
                Debug.LogWarning($"{name}: Added {floorTag}");

                // Play the second audio source if available
                AudioSource[] sources = GetComponents<AudioSource>();
                if (sources.Length > 1 && sources[1] != null)
                    sources[1].Play();

                // Safely increment the index for next bunny
                currentIndex++;
            }
            else
            {
                Debug.LogWarning($"🐇 No object found with tag {targetTag}");
            }
        }
    }
}
