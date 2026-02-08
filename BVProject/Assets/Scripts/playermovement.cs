using UnityEngine;

public class movementstatemanager : MonoBehaviour
{
    [Header("Movement")]
    public float movespeed = 3f;

    [Header("References")]
    [SerializeField] private Animator animator;

    [HideInInspector] public Vector3 dir;

    float hinput, vinput;
    Vector2 lastMoveDir;

    SpriteRenderer sr;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        sr = GetComponent<SpriteRenderer>();

        // Default facing direction (forward)
        lastMoveDir = Vector2.up;
    }

    void Update()
    {
        GetDirAndMove();
    }

    void GetDirAndMove()
    {
        hinput = Input.GetAxisRaw("Horizontal");
        vinput = Input.GetAxisRaw("Vertical");

        // ❌ prevent diagonal movement
        if (hinput != 0)
        {
            vinput = 0;
        }

        dir = new Vector3(hinput, 0f, vinput);
        controller.Move(dir * movespeed * Time.deltaTime);

        UpdateAnimations();
    }


    void UpdateAnimations()
    {
        // Reset all animation states
        animator.SetBool("runL", false);
        animator.SetBool("runF", false);
        animator.SetBool("runB", false);

        animator.SetBool("idleL", false);
        animator.SetBool("idleF", false);
        animator.SetBool("idleB", false);

        // ───────── MOVING ─────────
        if (hinput != 0 || vinput != 0)
        {
            lastMoveDir = new Vector2(hinput, vinput);

            if (Mathf.Abs(hinput) > Mathf.Abs(vinput))
            {
                animator.SetBool("runL", true);
                sr.flipX = hinput > 0;
            }
            else if (vinput > 0)
            {
                animator.SetBool("runF", true);
                sr.flipX = false;
            }
            else
            {
                animator.SetBool("runB", true);
                sr.flipX = false;
            }
        }
        // ───────── IDLE ─────────
        else
        {
            if (Mathf.Abs(lastMoveDir.x) > Mathf.Abs(lastMoveDir.y))
            {
                animator.SetBool("idleL", true);
            }
            else if (lastMoveDir.y > 0)
            {
                animator.SetBool("idleF", true);
            }
            else
            {
                animator.SetBool("idleB", true);
            }
        }
    }
}
