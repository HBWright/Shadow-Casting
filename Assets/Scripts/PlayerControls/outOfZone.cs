using UnityEngine;
using UnityEngine.Events;

public class outOfZone : MonoBehaviour
{
    public handInLight zone;
    public bool isLeftHand;
    public UnityEvent OnGestureRecognized;

    // Call this from the Selector's Unity Event (instead of directly linking your action)
    public void HandleGesture()
    {
        if (zone == null) return;

        if (isLeftHand && zone.leftHandInside)
            OnGestureRecognized.Invoke();
        else if (!isLeftHand && zone.rightHandInside)
            OnGestureRecognized.Invoke();
    }
}