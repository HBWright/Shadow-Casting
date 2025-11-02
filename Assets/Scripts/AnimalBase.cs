using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AnimalBase : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator animator;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //agent.updateRotation = true;
    }

    protected virtual void Start()
    {
        // Default idle behavior
        agent.isStopped = true;
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
}
