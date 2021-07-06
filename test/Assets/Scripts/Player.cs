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



    }

    void moveRight()
    {
        myController.Move(Vector3.right*Time.deltaTime*moveSpeed);
    }

    void moveLeft() 
    {
        myController.Move(Vector3.left*Time.deltaTime*moveSpeed);
    }

    void moveForward()
    {
        myController.Move(Vector3.forward*Time.deltaTime*moveSpeed);
    }

    void moveBackward()
    {
        myController.Move(Vector3.back*Time.deltaTime*moveSpeed);
    }

    void Jump()
    {
        //Make the character
        yForce = jumpSpeed; 
        isJumping = true;

        //Whenever the character jumps, the first jump counts as an actual jump
        doubleJump.amountJumps --;
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