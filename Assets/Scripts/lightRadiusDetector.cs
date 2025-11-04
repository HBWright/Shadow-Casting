using UnityEngine;

public class LightRadiusDetector : MonoBehaviour
{
    [Header("Targets to Watch")]
    public GameObject[] targets;

    private CapsuleCollider capsuleCollider;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();

        if (capsuleCollider == null)
            Debug.LogWarning($"{name} has no CapsuleCollider component.", this);
    }

    void Update()
    {
        if (targets == null || targets.Length == 0 || capsuleCollider == null)
            return;

        bool anyActive = false;

        // Check if at least one target is active
        foreach (var target in targets)
        {
            if (target != null && target.activeInHierarchy)
            {
                anyActive = true;
                break;
            }
        }

        capsuleCollider.enabled = anyActive;
    }
}
