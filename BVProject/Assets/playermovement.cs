using UnityEngine;

public class movementstatemanager : MonoBehaviour
{
    public float movespeed = 3;
    public LayerMask groundMask;
    [HideInInspector] public Vector3 dir;
    float hinput, vinput;
    SpriteRenderer sr;
    [SerializeField] private Animator animator;
    CharacterController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();   
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        getdirandmove();
        RotateToMouse();
    }

    void getdirandmove()
    {
        hinput = Input.GetAxisRaw("Horizontal");
        vinput = Input.GetAxisRaw("Vertical");

        UpdateAnimations();

        dir = new Vector3(hinput, 0f, vinput);
        

        controller.Move(dir*movespeed*Time.deltaTime);
    }

    void UpdateAnimations()
    {
        animator.SetBool("runR", false);
        animator.SetBool("runL", false);
        animator.SetBool("runF", false);
        animator.SetBool("runB", false);

        if (hinput > 0)
        {
            animator.SetBool("runR", true);
            sr.flipX = true;
        }
            
        else if (hinput < 0)
        {
            animator.SetBool("runL", true);
            sr.flipX = false;
        }
            
        else if (vinput > 0)
        {
            animator.SetBool("runF", true);
            sr.flipX = false;
        }
            
        else if (vinput < 0)
        {
            animator.SetBool("runB", true);
            sr.flipX = false;
        }
            
    }

    void RotateToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
        {
            Vector3 lookPoint = hit.point;
            lookPoint.y = transform.position.y;

            Vector3 direction = lookPoint - transform.position;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
            }
        }

    }
}
