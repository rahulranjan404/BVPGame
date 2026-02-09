using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float detectionRadius = 6f;
    public float attackRadius = 2f;
    public float moveSpeed = 3f;

    private bool isAttacking = false;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // ATTACK ZONE
        if (distance <= attackRadius)
        {
            isAttacking = true;
            AttackPlayer();
        }
        // FOLLOW ZONE
        else if (distance <= detectionRadius)
        {
            isAttacking = false;
            FollowPlayer();
        }
        // OUT OF RANGE
        else
        {
            isAttacking = false;
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        // Stop movement automatically because FollowPlayer() is not called
        // Put animation / damage logic here
        Debug.Log("Enemy Attacking!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
