using UnityEngine;
using UnityEngine.AI;

public class RabbitBehavior : AnimalBase
{
    public float searchRadius = 10f;
    public string holeTag = "Rabbit Wall";

    protected override void Start()
    {
        base.Start();

        GameObject targetHole = FindNearestHole();
        if (targetHole != null)
        {
            MoveTo(targetHole.transform.position);
        }
        else
        {
            Idle();
        }
    }

    private GameObject FindNearestHole()
    {
        GameObject[] holes = GameObject.FindGameObjectsWithTag(holeTag);
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
