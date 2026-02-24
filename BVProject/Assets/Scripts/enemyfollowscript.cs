using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float detectionRadius = 6f;
    public float attackRadius = 2f;

    public float damage = 10f;
    public float attackCooldown = 2f;
    float lastAttackTime = 0f;

    public PlayerStats playerStats;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRadius;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRadius)
        {
            agent.SetDestination(transform.position); // stop moving
            AttackPlayer();
        }
        else if (distance <= detectionRadius)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Enemy Attacking!");
            playerStats.TakeDamage((int)damage);
            lastAttackTime = Time.time;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}