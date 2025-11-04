using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class WinHandler : MonoBehaviour
{
    [Header("Scene to Load")]
    public string sceneName = "VictoryScene";
    public GameObject darken;

    [Header("Timing")]
    public float delayBeforeLoad = 4f;

    private bool triggered = false;

    private void Awake()
    {
        // Ensure this object has a Rigidbody (required for triggers to work)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true; // prevent physics movement
        rb.useGravity = false;

        // Warn if missing a collider
        if (GetComponent<Collider>() == null)
            Debug.LogWarning($"{name} has no Collider! Trigger won't work.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger once and only when hitting a CapsuleCollider (e.g., player body)
        if (!triggered && other is CapsuleCollider)
        {
            triggered = true;
            StartCoroutine(ChangeSceneCoroutine());
        }
    }

    private IEnumerator ChangeSceneCoroutine()
    {
        if (darken != null)
            darken.SetActive(true);

        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene(sceneName);
    }
}