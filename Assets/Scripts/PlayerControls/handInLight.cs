using UnityEngine;

public class handInLight : MonoBehaviour
{
    public bool leftHandInside;
    public bool rightHandInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            leftHandInside = true;
            Debug.Log($"Left hand entered {gameObject.name}");
        }
        else if (other.CompareTag("RightHand"))
        {
            rightHandInside = true;
            Debug.Log($"Right hand entered {gameObject.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            leftHandInside = false;
            Debug.Log($"Left hand exited {gameObject.name}");
        }
        else if (other.CompareTag("RightHand"))
        {
            rightHandInside = false;
            Debug.Log($"Right hand exited {gameObject.name}");
        }
    }
}