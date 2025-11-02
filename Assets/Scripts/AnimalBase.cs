using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AnimalBase : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator animator;
    [SerializeField] private float startDelay = 3f; // Adjustable wait time before movement
    private bool canMove = false;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.isStopped = true; // prevent movement right away
        StartCoroutine(EnableMovementAfterDelay());
    }

    IEnumerator EnableMovementAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        agent.isStopped = false;
        BeginBehavior();
    }

    protected virtual void Update()
    {
        // If agent is moving, isStopped is false → moving
        bool isMoving = !agent.isStopped && agent.velocity.magnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);
    }

    public virtual void Idle()
    {
        agent.isStopped = true;
    }

    public virtual void MoveTo(Vector3 position)
    {
        agent.isStopped = false;
        agent.SetDestination(position);
    }
    protected virtual void BeginBehavior() { }
}
