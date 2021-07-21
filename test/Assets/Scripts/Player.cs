using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Different variables
    [Header("Movement Variables")]
    public float moveSpeed;

    public float defaultSpeed;

    [Header("Jumping Variables")]
    public float yForce;

    public float yGravity;

    public float maxGravity;

    public float jumpSpeed;

    public bool isJumping;

    [Header("Dashing Variables")]
    public float dashSpeed;

    public bool canDashLeft;

    public bool canDashRight;

    public bool canDashForward;

    public bool canDashBack;

    public float dashWaitTime;

    public float dashCoolDown;

    public bool canDash;
    
    [Header("Refrences")]

    CharacterController myController;

    DoubleJump doubleJump;

    public GameObject playerModel;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponent<CharacterController>();
        doubleJump = GetComponent<DoubleJump>();
    }



    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.W)){  
            moveForward();
        }
        if(Input.GetKey(KeyCode.A)){  
            moveLeft();
        }
        if(Input.GetKey(KeyCode.S)){  
            moveBackward();
        }
        if(Input.GetKey(KeyCode.D)){  
            moveRight();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Makes the character jump
            if (!isJumping)
            {
                Jump();

            }

            else 
            {
                if(doubleJump.amountJumps > 0)
                {
                    Jump();
                }
            }
        }

        //We constantly apply Y force to the player
        myController.Move(Vector3.up*yForce*Time.deltaTime); 



        
        if(!myController.isGrounded){
            //Gravity calculation
            yForce = Mathf.Max(maxGravity, yForce + (yGravity * Time.deltaTime));
        }


        //If the player is on the ground, it is NOT jumping
        if(myController.isGrounded)
        {
            doubleJump.amountJumps = doubleJump.originalJumps;
            isJumping = false;

            animator.SetBool("IsJumping", false);
        }


        if(Input.GetKeyDown(KeyCode.W))
        {
            if(canDashForward)
            {
                Dash(Vector3.forward);
            }

            else
            {
                canDashForward = true;
                StartCoroutine(DisableDashRoutine());
            }
        }


        if(Input.GetKeyDown(KeyCode.A))
        {
            if(canDashLeft)
            {
                Dash(Vector3.left);
            }

            else
            {
                canDashLeft = true;
                StartCoroutine(DisableDashRoutine());
            }
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            if(canDashBack)
            {
                Dash(Vector3.back);
            }

            else
            {
                canDashBack= true;
                StartCoroutine(DisableDashRoutine());
            }
        }


        if(Input.GetKeyDown(KeyCode.D))
        {
            if(canDashRight)
            {
                Dash(Vector3.right);
            }

            else
            {
                canDashRight = true;
                StartCoroutine(DisableDashRoutine());
            }
        }

        if(IsIdle())
        {
            //Idle Animation
            animator.SetFloat("Speed", 0);

        }



    }

    bool IsIdle()   
    {
        if(!Input.GetKey(KeyCode.W) &&
           !Input.GetKey(KeyCode.A) &&
           !Input.GetKey(KeyCode.S) &&
           !Input.GetKey(KeyCode.D) &&
           !isJumping                 )
        {
            return true;
        }

        return false;
    }

    void moveRight()
    {

        myController.Move(Vector3.right*Time.deltaTime*moveSpeed);
        playerModel.transform.eulerAngles = new Vector3(0,90,0);
        animator.SetFloat("Speed", 1);

    }

    void moveLeft() 
    {
        myController.Move(Vector3.left*Time.deltaTime*moveSpeed);
        playerModel.transform.eulerAngles = new Vector3(0,270,0);
        animator.SetFloat("Speed", 1);

    }

    void moveForward()
    {
        myController.Move(Vector3.forward*Time.deltaTime*moveSpeed);
        playerModel.transform.eulerAngles = new Vector3(0,0,0);
        animator.SetFloat("Speed", 1);
    }

    void moveBackward()
    {
        myController.Move(Vector3.back*Time.deltaTime*moveSpeed);
        playerModel.transform.eulerAngles = new Vector3(0,180,0);
        animator.SetFloat("Speed", 1);
    }

    void Jump()
    {
        //Make the character
        yForce = jumpSpeed; 
        isJumping = true;
        //Jump animation

        //Whenever the character jumps, the first jump counts as an actual jump
        doubleJump.amountJumps --;

        //When isJumping = true, we also set it true for the animator
        animator.SetBool("IsJumping", true);
    }

    void Dash(Vector3 direction)
    {
        myController.Move(direction*Time.deltaTime*dashSpeed);

        DisableDash();
    }

    void DisableDash()
    {
        canDashForward = false;

        canDashRight = false;

        canDashLeft = false;
        
        canDashBack = false;
    }

    IEnumerator DisableDashRoutine()
    {
        yield return new WaitForSeconds(dashWaitTime);
        DisableDash();
    }

    // IEnumerator CoolingDashDown()
    // {

    // }

}