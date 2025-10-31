using UnityEngine;

public class WolfBehavior : AnimalBase
{
    public float attackRange = 5f;
    public string enemyTag = "Enemy";

    protected override void Update()
    {
        base.Update();

        GameObject target = FindNearestEnemy();
        if (target != null)
        {
            MoveTo(target.transform.position);
        }
        else
        {
            Idle();
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearest = null;
        float bestDist = Mathf.Infinity;

        foreach (var e in enemies)
        {
            float d = Vector3.Distance(transform.position, e.transform.position);
            if (d < bestDist && d < attackRange)
            {
                bestDist = d;
                nearest = e;
            }
        }

        return nearest;
    }
}
