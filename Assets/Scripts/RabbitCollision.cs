using UnityEngine;

public class RabbitCollision : MonoBehaviour
{
    private static int currentIndex = 0;
    private static readonly string[] destroyTags = { "RabbitDestory1", "RabbitDestory2" };

    [Header("Floors to enable in order")]
    public GameObject[] rabbitFloors; // assign in Inspector (RabbitFloor1, RabbitFloor2)

    private bool hasTriggered = false;
    private AnimalLifecycle self;

    private void Awake()
    {
        self = GetComponent<AnimalLifecycle>();
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("RabbitWall"))
        {
            hasTriggered = true;

            Collider myCollider = GetComponent<Collider>();
            if (myCollider != null)
                myCollider.enabled = false;

            if (currentIndex >= destroyTags.Length)
            {
                Debug.Log("🐇 All RabbitDestroy targets cleared!");
                return;
            }

            string targetTag = destroyTags[currentIndex];
            GameObject target = GameObject.FindWithTag(targetTag);

            if (target != null)
            {
                Destroy(target);
                Debug.LogWarning($"{name}: Destroyed {targetTag}");
            }
            else
            {
                Debug.LogWarning($"🐇 No object found with tag {targetTag}");
            }

            // ✅ Enable floor by array reference (works even if inactive)
            if (rabbitFloors != null && currentIndex < rabbitFloors.Length && rabbitFloors[currentIndex] != null)
            {
                rabbitFloors[currentIndex].SetActive(true);
                Debug.LogWarning($"{name}: Activated floor {rabbitFloors[currentIndex].name}");
            }
            else
            {
                Debug.LogWarning("🐇 No floor reference found for this index!");
            }

            AudioSource[] sources = GetComponents<AudioSource>();
            if (sources.Length > 1 && sources[1] != null)
                sources[1].Play();

            currentIndex++;
        }
        StartCoroutine(self.Despawn());
        Destroy(other.gameObject);
        Debug.LogWarning($"{name}: Destroyed RabbitWall");
    }
}
