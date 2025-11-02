using UnityEngine;
using System.Collections;

public class AnimalLifecycle : MonoBehaviour
{
    [Header("Spawn/Despawn Effects")]
    public GameObject spawnParticles;
    public GameObject despawnParticles;

    [Header("Scale Settings")]
    public float startScale = 0f;       // scale at spawn
    public float endScale = 1f;         // full scale
    public float scaleDuration = 3f;    // grow/shrink time in seconds

    [Header("Lifetime Settings")]
    public float idleLifetime = 10f;    // default time before despawn
    private float timer = 0f;
    private bool activeTask = false;
    private bool despawning = false;

    
    private AnimalSpawner spawner;

    private void Start()
    {
        // Start invisible
        transform.localScale = Vector3.one * startScale;

        // Play spawn particles
        if (spawnParticles != null)
            Instantiate(spawnParticles, transform.position, Quaternion.identity);

        // Grow to full size
        StartCoroutine(ScaleOverTime(endScale, scaleDuration));
    }

    private void Update()
    {
        if (despawning) return;

        if (!activeTask)
        {
            timer += Time.deltaTime;

            if (timer >= idleLifetime)
                StartCoroutine(Despawn());
        }
    }

    public void StartTask()
    {
        activeTask = true;
        timer = 0f;
    }

    public void EndTask()
    {
        activeTask = false;
        timer = 0f;
    }

    private IEnumerator Despawn()
    {
        if (despawning) yield break;
        despawning = true;

        // Play despawn particles
        if (despawnParticles != null)
            Instantiate(despawnParticles, transform.position, Quaternion.identity);

        // Shrink and destroy
        yield return ScaleOverTime(startScale, scaleDuration, destroyAfter: true);

        // Notify spawner
        spawner?.OnAnimalDespawned();
    }

    /// <summary>
    /// Scales the animal smoothly over time.
    /// </summary>
    private IEnumerator ScaleOverTime(float targetScale, float duration, bool destroyAfter = false)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 finalScale = Vector3.one * targetScale;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, finalScale, time / duration);
            yield return null;
        }

        transform.localScale = finalScale;

        if (destroyAfter)
            Destroy(gameObject);
    }
}
