using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class door2 : MonoBehaviour
{
    [Header("Object to Watch")]
    public GameObject target;
    public GameObject floor;

    private bool hasTriggered = false;

    void Update()
    {
        // If target was assigned and is now destroyed (null in Unity's sense)
        if (!hasTriggered && target == null)
        {
            hasTriggered = true;
            StartCoroutine(OnTargetDestroyed());
        }
    }

    private IEnumerator OnTargetDestroyed()
    {
        floor.SetActive(true);
        yield return new WaitForSeconds(2f);
    }
}