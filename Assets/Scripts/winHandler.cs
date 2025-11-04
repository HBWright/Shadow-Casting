using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class WinHandler : MonoBehaviour
{
    [Header("Scene to Load")]
    public string sceneName = "VictoryScene";
    public GameObject darken;

    private bool triggered = false;

    // Make sure your CapsuleCollider is set as "Is Trigger"
    private void OnTriggerEnter(Collider other)
    {
        // Only trigger once
        if (!triggered && other is CapsuleCollider)
        {
            triggered = true;
            StartCoroutine(ChangeSceneCoroutine());
        }
    }

    private IEnumerator ChangeSceneCoroutine()
    {
        darken.SetActive(true);
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("VictoryScene");
    }
}
