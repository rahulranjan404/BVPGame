//using UnityEngine;
//using System.Collections.Generic;


//public class movementstatemanager : MonoBehaviour
//{
//    [Header("Movement")]
//    public float movespeed = 3f;

//    [Header("References")]
//    [SerializeField] private Animator animator;

//    [HideInInspector] public Vector3 dir;

//    float hinput, vinput;
//    Vector2 lastMoveDir;
//    string lastbutton;
//    SpriteRenderer sr;
//    CharacterController controller;



//    void Start()
//    {
//        controller = GetComponent<CharacterController>();
//        sr = GetComponent<SpriteRenderer>();

//        // Default facing direction (forward)
//        lastMoveDir = Vector2.up;
//    }

//    void Update()
//    {
//        GetDirAndMove();
//    }

//    enum LastInput
//    {
//        None,
//        Left,
//        Right,
//        Up,
//        Down
//    }
//    List<LastInput> heldInputs = new List<LastInput>(); //new
//    LastInput lastInput = LastInput.None;
//    LastInput prevLastInput = LastInput.None;
//    void GetDirAndMove()
//    {
//         HandleKey(KeyCode.W, LastInput.Up);
//        HandleKey(KeyCode.S, LastInput.Down);
//        HandleKey(KeyCode.A, LastInput.Left);
//        HandleKey(KeyCode.D, LastInput.Right);

//        if (heldInputs.Count > 0)
//        {
//            lastInput = heldInputs[heldInputs.Count - 1];
//            prevLastInput = lastInput;
//        }
//        else
//        {
//            lastInput = LastInput.None;
//        }

//        // Debug.Log(prevLastInput + " " + lastInput);

//        if (lastInput == LastInput.Left)
//        {
//            hinput = -1;
//            vinput = 0;
//        }
//        else if (lastInput == LastInput.Right)
//        {
//            hinput = 1;
//            vinput = 0;
//        }
//        else if (lastInput == LastInput.Up)
//        {
//            hinput = 0;
//            vinput = 1;
//        }
//        else if (lastInput == LastInput.Down)
//        {
//            hinput = 0;
//            vinput = -1;
//        }
//        else if (lastInput == LastInput.None)
//        {
//            hinput = 0;
//            vinput = 0;
//        }

//        dir = new Vector3(hinput, 0f, vinput);
//        controller.Move(dir * movespeed * Time.deltaTime);

//        UpdateAnimations();

//    }
//    void HandleKey(KeyCode key, LastInput dir)
//    {
//        if (Input.GetKeyDown(key) && !heldInputs.Contains(dir))
//            heldInputs.Add(dir);

//        if (Input.GetKeyUp(key))
//            heldInputs.Remove(dir);
//    }

//    void UpdateAnimations()
//    {
//        // Reset all animation states
//        animator.SetBool("runL", false);
//        animator.SetBool("runF", false);
//        animator.SetBool("runB", false);

//        animator.SetBool("idleL", false);
//        animator.SetBool("idleF", false);
//        animator.SetBool("idleB", false);

//        // ───────── MOVING ─────────
//        if (hinput != 0 || vinput != 0)
//        {
//            lastMoveDir = new Vector2(hinput, vinput);

//            if (Mathf.Abs(hinput) > Mathf.Abs(vinput))
//            {
//                animator.SetBool("runL", true);
//                sr.flipX = hinput > 0;
//            }
//            else if (vinput > 0)
//            {
//                animator.SetBool("runF", true);
//                sr.flipX = false;
//            }
//            else
//            {
//                animator.SetBool("runB", true);
//                sr.flipX = false;
//            }
//        }
//        // ───────── IDLE ─────────
//        else
//        {
//            if (prevLastInput == LastInput.Left)
//            {
//                animator.SetBool("idleL", true);
//                sr.flipX = false;
//            }
//            else if (prevLastInput == LastInput.Right)
//            {
//                animator.SetBool("idleL", true);
//                sr.flipX = true;
//            }
//            else if (prevLastInput == LastInput.Up)
//            {
//                animator.SetBool("idleF", true);
//                sr.flipX = false;
//            }
//            else
//            {
//                animator.SetBool("idleB", true);
//                sr.flipX = false;
//            }
//        }
//    }

//}
using UnityEngine;
using System.Collections.Generic;


public class movementstatemanager : MonoBehaviour
{
    [Header("Movement")]
    public float movespeed = 3f;

    [Header("References")]
    [SerializeField] private Animator animator;

    [HideInInspector] public Vector3 dir;

    float hinput, vinput;
    Vector2 lastMoveDir;
    string lastbutton;
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

    public enum LastInput
    {
        None,
        Left,
        Right,
        Up,
        Down
    }
    List<LastInput> heldInputs = new List<LastInput>(); //new
    public LastInput lastInput = LastInput.None;
    public LastInput prevLastInput = LastInput.None;
    void GetDirAndMove()
    {
        HandleKey(KeyCode.W, LastInput.Up);
        HandleKey(KeyCode.S, LastInput.Down);
        HandleKey(KeyCode.A, LastInput.Left);
        HandleKey(KeyCode.D, LastInput.Right);

        if (heldInputs.Count > 0)
        {
            lastInput = heldInputs[heldInputs.Count - 1];
            prevLastInput = lastInput;
        }
        else
        {
            lastInput = LastInput.None;
        }

        // Debug.Log(prevLastInput + " " + lastInput);

        if (lastInput == LastInput.Left)
        {
            hinput = -1;
            vinput = 0;
        }
        else if (lastInput == LastInput.Right)
        {
            hinput = 1;
            vinput = 0;
        }
        else if (lastInput == LastInput.Up)
        {
            hinput = 0;
            vinput = 1;
        }
        else if (lastInput == LastInput.Down)
        {
            hinput = 0;
            vinput = -1;
        }
        else if (lastInput == LastInput.None)
        {
            hinput = 0;
            vinput = 0;
        }

        dir = new Vector3(hinput, 0f, vinput);
        controller.Move(dir * movespeed * Time.deltaTime);

        UpdateAnimations();

    }
    void HandleKey(KeyCode key, LastInput dir)
    {
        if (Input.GetKeyDown(key) && !heldInputs.Contains(dir))
            heldInputs.Add(dir);

        if (Input.GetKeyUp(key))
            heldInputs.Remove(dir);
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
            if (prevLastInput == LastInput.Left)
            {
                animator.SetBool("idleL", true);
                sr.flipX = false;
            }
            else if (prevLastInput == LastInput.Right)
            {
                animator.SetBool("idleL", true);
                sr.flipX = true;
                
                
            }
            else if (prevLastInput == LastInput.Up)
            {
                animator.SetBool("idleF", true);
                sr.flipX = false;
            }
            else
            {
                animator.SetBool("idleB", true);
                sr.flipX = false;
            }
        }
    }

}
