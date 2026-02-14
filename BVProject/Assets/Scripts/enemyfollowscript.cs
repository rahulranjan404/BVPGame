using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;


    public float detectionRadius = 6f;
    public float attackRadius = 2f;
    public float moveSpeed = 3f;

    public float damage = 10f;
    public float attackCooldown = 2f;
    float lastAttackTime = 0f;



    public PlayerStats playerStats;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // ATTACK ZONE
        if (distance <= attackRadius)
        {
            AttackPlayer();
        }
        // FOLLOW ZONE
        else if (distance <= detectionRadius)
        {
            FollowPlayer();
        }
        // OUT OF RANGE
        else
        {
            
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
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
