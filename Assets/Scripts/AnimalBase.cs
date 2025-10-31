using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AnimalBase : MonoBehaviour
{
    protected NavMeshAgent agent;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        // Default idle behavior
        agent.isStopped = true;
    }

    protected virtual void Update()
    {
        // Shared movement logic could go here (later animations, etc.)
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
