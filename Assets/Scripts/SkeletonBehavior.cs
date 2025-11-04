using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SkeletonBehavior : AnimalBase
{
    [Header("Skeleton Settings")]
    public string playerTag = "Player";
    public float searchRadius = 12f;
    public float stopDistanceBeforeAttack = 2f; // how close before stopping
    public float deathDelay = 5f;
    public float attackDelay = 2f;
    public GameObject darken;
    public AudioSource shoot; 
    public GameObject attackEffect;

    private bool hasAttacked = false;

    protected override void Update()
    {
        base.Update();
        BeginBehavior();
    }

    private void BeginBehavior()
    {
        if (hasAttacked)
            return;

        GameObject target = FindNearestPlayer();
        if (target == null)
        {
            Debug.Log($"{name}: No player found!");
            Idle();
            return;
        }

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > stopDistanceBeforeAttack)
        {
            MoveTo(target.transform.position);
            Debug.Log($"{name}: Moving toward player at {target.transform.position}");
        }
        else
        {
            StartCoroutine(AttackSequence(target.transform.position));
        }
    }

    private GameObject FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
        if (players == null || players.Length == 0)
        {
            return null;
        }

        GameObject nearest = null;
        float bestDist = Mathf.Infinity;

        foreach (var p in players)
        {
            float d = Vector3.Distance(transform.position, p.transform.position);
            if (d < bestDist && d < searchRadius)
            {
                bestDist = d;
                nearest = p;
            }
        }

        return nearest;
    }

    private IEnumerator AttackSequence(Vector3 targetPosition)
    {
        hasAttacked = true;
        agent.isStopped = true;
        animator.SetBool("IsAttacking", true);
        shoot.Play();

        if (attackEffect != null)
        {
            yield return new WaitForSeconds(attackDelay);
            Instantiate(attackEffect, transform.position + Vector3.up * 1f, Quaternion.identity);
        }

        yield return new WaitForSeconds(deathDelay);
        
        StartCoroutine(KillPlayer());
    }

    public IEnumerator ShrinkAndDestroy()
    {
        animator.SetBool("IsDead", true);
        Vector3 originalScale = transform.localScale;
        float duration = 1.5f;
        float t = 0;

        while (t < duration)
        {
            if (this == null) // stops if object destroyed externally
                yield break;

            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t / duration);
            t += Time.deltaTime;
            yield return null;
        }


        Destroy(gameObject);
    }

    public IEnumerator KillPlayer()
    {
        darken.SetActive(true);
        yield return new WaitForSeconds(4f);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
