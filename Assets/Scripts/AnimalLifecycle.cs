using UnityEngine;
using System.Collections;

public class AnimalLifecycle : MonoBehaviour
{
    private float lifetime;
    private float timer;
    private bool activeTask;
    private bool despawning;
    private AnimalSpawner spawner;  // reference back to the spawner

    public void Initialize(float idleDuration, AnimalSpawner originSpawner)
    {
        lifetime = idleDuration;
        spawner = originSpawner;
        timer = 0f;
        activeTask = false;
        despawning = false;
    }

    private void Update()
    {
        if (despawning) return;

        if (!activeTask)
        {
            timer += Time.deltaTime;

            if (timer >= lifetime)
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
        despawning = true;
        Debug.Log($"{gameObject.name} is despawning after idling.");
        yield return new WaitForSeconds(1f); // optional fade buffer
        spawner?.OnAnimalDespawned();        // notify spawner
        Destroy(gameObject);
    }
}
