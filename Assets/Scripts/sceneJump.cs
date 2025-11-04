using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DetectDestroy : MonoBehaviour
{
    [Header("Object to Watch")]
    public GameObject target;
    public AudioSource steps;
    public AudioSource broken;
    public GameObject darken;

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
        broken.Play();
        yield return new WaitForSeconds(2f);
        steps.Play();
        darken.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("DungeonScene");
    }
}