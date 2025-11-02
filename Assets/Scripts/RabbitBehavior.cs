using UnityEngine;
using UnityEngine.AI;

public class RabbitBehavior : AnimalBase
{
    public float searchRadius = 10f;
    public string holeTag = "RabbitWall";

    protected override void Start()
    {
        base.Start();
        
        GameObject target = FindNearestHole();
        if (target == null)
        {
            Debug.Log($"{gameObject.name}: No target found!");
        }
        else
        {
            Debug.Log($"{gameObject.name}: Moving toward {target.name} at {target.transform.position}");
        }
        if (target != null)
        {
            MoveTo(target.transform.position);
        }
        else
        {
            Idle();
        }
    }

    private GameObject FindNearestHole()
    {
        GameObject[] holes = GameObject.FindGameObjectsWithTag(holeTag);
        if (holes == null)
        {
            Debug.LogWarning($"{name}: No Gamobject Found with tag 'RabbitWall'!");
        }
        GameObject nearest = null;
        float bestDist = Mathf.Infinity;

        foreach (var h in holes)
        {
            float d = Vector3.Distance(transform.position, h.transform.position);
            if (d < bestDist && d < searchRadius)
            {
                bestDist = d;
                nearest = h;
            }
        }

        return nearest;
    }
}
