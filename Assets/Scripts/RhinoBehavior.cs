using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RhinoBehavior : AnimalBase
{
    public float searchRadius = 15f;
    public string[] targetTags = { "Enemy", "RhinoWall" };

    private GameObject chargeTarget;

    protected override void BeginBehavior()
    {
        base.BeginBehavior();

        // Find the closest valid target
        chargeTarget = FindNearestTarget();

        if (chargeTarget == null)
        {
            Debug.LogWarning($"{name}: No Enemy or RhinoWall found within {searchRadius} units.");
            Idle();
            return;
        }

        Debug.Log($"{name}: Chasing {chargeTarget.name}");
    }

    protected override void Update()
    {
        base.Update();

        if (chargeTarget != null)
        {
            // Continuously update the agent's destination to the live target position
            agent.SetDestination(chargeTarget.transform.position);

            // Optional: check if we reached the target
            float distance = Vector3.Distance(transform.position, chargeTarget.transform.position);
            if (distance <= 1.5f) // Adjust to your contact range
            {
                // Trigger target's despawn or any other effect
                var targetBehavior = chargeTarget.GetComponent<SkeletonBehavior>();
                if (targetBehavior != null)
                {
                    StartCoroutine(targetBehavior.ShrinkAndDestroy());
                }

                // Stop moving after contact
                Idle();
                chargeTarget = null;
            }
        }
    }

    private GameObject FindNearestTarget()
    {
        List<GameObject> allTargets = new List<GameObject>();

        foreach (var tag in targetTags)
        {
            GameObject[] found = GameObject.FindGameObjectsWithTag(tag);
            if (found.Length > 0)
                allTargets.AddRange(found);
        }

        if (allTargets.Count == 0)
        {
            Debug.Log($"{name}: No targets found with tags {string.Join(", ", targetTags)}");
            return null;
        }

        GameObject nearest = null;
        float bestDist = Mathf.Infinity;

        foreach (var obj in allTargets)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist < bestDist && dist <= searchRadius)
            {
                bestDist = dist;
                nearest = obj;
            }
        }

        return nearest;
    }
}
