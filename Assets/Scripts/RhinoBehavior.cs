using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class RhinoBehavior : AnimalBase
{
    public float searchRadius = 15f;
    public string[] targetTags = { "Enemy", "RhinoWall" };
    public float chargeSpeed = 12f;

    private GameObject chargeTarget;

    protected override void BeginBehavior()
    { 
        // Find the closest valid target
        chargeTarget = FindNearestTarget();

        if (chargeTarget == null)
        {
            Debug.LogWarning($"{name}: No Enemy or RhinoWall found within {searchRadius} units.");
            Idle();
            return;
        }

        // Log and move toward it
        Debug.Log($"{name}: Charging toward {chargeTarget.name} at {chargeTarget.transform.position}");

        agent.speed = chargeSpeed;
        MoveTo(chargeTarget.transform.position);
    }

    private GameObject FindNearestTarget()
    {
        List<GameObject> allTargets = new List<GameObject>();

        // Find all possible targets by tags
        foreach (var tag in targetTags)
        {
            GameObject[] found = GameObject.FindGameObjectsWithTag(tag);
            if (found.Length > 0)
            {
                allTargets.AddRange(found);
            }
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
