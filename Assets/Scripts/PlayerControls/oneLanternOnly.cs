using UnityEngine;

public class OneLanternOnly : MonoBehaviour
{
    [Header("Lanterns")]
    public GameObject lanternL;
    public GameObject lanternR;

    private bool lanternLWasActive;
    private bool lanternRWasActive;

    void Start()
    {
        if (lanternL != null) lanternLWasActive = lanternL.activeInHierarchy;
        if (lanternR != null) lanternRWasActive = lanternR.activeInHierarchy;

        // Ensure only one lantern is active at start
        if (lanternLWasActive && lanternRWasActive)
        {
            lanternR.SetActive(false);
            lanternRWasActive = false;
        }
        else if (!lanternLWasActive && !lanternRWasActive)
        {
            lanternL.SetActive(true);
            lanternLWasActive = true;
        }
    }

    void Update()
    {
        if (lanternL == null || lanternR == null)
            return;

        bool lanternLActive = lanternL.activeInHierarchy;
        bool lanternRActive = lanternR.activeInHierarchy;

        // L changed
        if (lanternLActive != lanternLWasActive)
        {
            if (lanternLActive) lanternR.SetActive(false);
            else if (!lanternRActive) lanternR.SetActive(true);
        }

        // R changed
        if (lanternRActive != lanternRWasActive)
        {
            if (lanternRActive) lanternL.SetActive(false);
            else if (!lanternLActive) lanternL.SetActive(true);
        }

        lanternLWasActive = lanternLActive;
        lanternRWasActive = lanternRActive;
    }
}