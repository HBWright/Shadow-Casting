using UnityEngine;

public class RabbitCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("RabbitWall"))
        {
            Collider myCollider = GetComponent<Collider>();
            if (myCollider != null)
                myCollider.enabled = false;

            int index = 1;
            GameObject target = null;

            // Find the next RabbitDestroy object that still exists
            while (true)
            {
                target = GameObject.FindWithTag("RabbitDestory" + index); //TYPO TO MATCH TAGS
                if (target != null)
                    break;
                index++;

                // Stop if there are no more
                if (index > 5) // set a reasonable max
                {
                    Debug.LogWarning("No RabbitDestroy objects left!");
                    return;
                }
            }

            // Destroy the found object
            Destroy(target);
            Debug.LogWarning($"{name}: Destroyed {target.tag}");
        }
    }
}

