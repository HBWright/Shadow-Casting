using UnityEngine;
using System.Collections; // Needed for StartCoroutine


public class RhinoCollision : MonoBehaviour
{
    private Animator animator;
    private AnimalLifecycle self;

    private void Awake()
    {
        self = GetComponent<AnimalLifecycle>();
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
            Debug.LogError($"{name}: Animator not found on Rhino or its children!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning($"{name}: RHINO FOUND" + other);
        
        if (other.CompareTag("Enemy"))
        {
            Collider myCollider = GetComponent<Collider>();
            if (myCollider != null)
                myCollider.enabled = false;

            Debug.LogWarning($"{name}: Hit skeleton!");

            SkeletonBehavior target = other.GetComponent<SkeletonBehavior>();
            if (target != null)
            {
                if (animator != null)
                    animator.SetBool("IsAttacking", true);

                StartCoroutine(target.ShrinkAndDestroy());
                StartCoroutine(self.Despawn());
                Debug.LogWarning($"{name}: killed!");
            }
        }
        if (other.CompareTag("RhinoWall"))
        {

            Collider myCollider = GetComponent<Collider>();
            if (myCollider != null)
                myCollider.enabled = false;
            StartCoroutine(self.Despawn());

            Destroy(other.gameObject);
            Debug.LogWarning($"{name}: Destroyed RhinoWall");
        }
    }
}
