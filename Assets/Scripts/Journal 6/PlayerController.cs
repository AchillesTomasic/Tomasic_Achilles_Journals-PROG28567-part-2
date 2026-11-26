using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.RenderGraphModule;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D; // rigidbody for the player
    // movement affected controls //
    [Header("Horizontal")]
    private float acclerationX; //rate of acceleration for the player
    public float timeToReachMaxSpeedX; // time to reach the max speed for the player
    public float maxSpeedX; // max Speed for the player
    public Vector2 velocity; // used to determine the left and right velocity of the player 
    private float deacclerationX; //rate of acceleration for the player
    public float timeToReachDeAccSpeedX; // time to reach the max speed for the player
    public float lastDirection; // gets the previous direction for the player

    // collision boxcast 2d //
    public Collider2D playerCollider; // obtains the players box collider value
    public float BoxcastDistance; // distance the boxcast travels
    public LayerMask groundMask; // used to only check the ground layers

    // jumping mechanics //
    [Header("Vertical")]
    public float apexHeight = 3f; // apex height for the players jump
    public float apexTime = 0.5f; // time that the character will remain in the apex of their jump

    private float gravity; // used for the character gravity
    private float jumpVelocity; // used for the characters jump velocity and how hight they jump
    private float terminalVelocity; // used to stop the character after reaching a terminal speed
    public bool isJumping; // checks if player is jumping
    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        
        acclerationX = maxSpeedX/ timeToReachMaxSpeedX; // calculatuion to get the acceleration
        deacclerationX = maxSpeedX / timeToReachDeAccSpeedX; // calculation to get deacceleration
        rb2D = GetComponent<Rigidbody2D>(); // used to set the rigidbody2d to the value set on this instance
        rb2D.gravityScale = 0; // sets the gravity equal to 0
        playerCollider = rb2D.GetComponent<Collider2D>();

        gravity = -2 * apexHeight / Mathf.Pow(apexTime, 2); // Sets gravity
        jumpVelocity = 2 * apexHeight / apexTime; // sets the jump velocity
        terminalVelocity = 2 * jumpVelocity;
    }

    void FixedUpdate()
    {
        
        Vector2 playerInput = new Vector2();

        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        // used to determine input 

        // if player moves left // 
        if (Keyboard.current.aKey.isPressed)
        {
            playerInput.x = -1; // sets the player input equal to -1
        }
        //if player moves right
        if (Keyboard.current.dKey.isPressed)
        {
            playerInput.x = 1; // sers the player input value equal to 1
        }
        // input to detect if the player presses the w or space key
        if( Keyboard.current.wKey.isPressed && IsGrounded() == true)
        {
            isJumping = true;
        }
        MovementUpdate(playerInput);
        
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        
        // raises the acceleration if the players controller moves
        if (Mathf.Abs(playerInput.x) > 0)
        {
            lastDirection = playerInput.x; // sets the direction that the player is traveling equal to the current input
            velocity.x += acclerationX * Time.fixedDeltaTime; // determines the velocituy of the player over fixed deltatime
        }
        // slows player velocity if player is not moving or the controller is below an epsilon value
        if (Mathf.Abs(playerInput.x) <= 0)
        {
            velocity.x -= deacclerationX * Time.fixedDeltaTime;
        }
        // caps the speed for the player
        if (velocity.x > maxSpeedX)
        {
            velocity.x = maxSpeedX; //sets velocity equal to the max speed x
        }
        // stops the speed from going below zero
        if (velocity.x < 0)
        {
            velocity.x = 0f; // ensures the speed will never 
        }
        rb2D.linearVelocityX = velocity.x * lastDirection; // changes the x linear velocity for he rigidbody on the player
        if (IsGrounded() == false)
        {
            velocity.y += gravity * Time.fixedDeltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -terminalVelocity, velocity.y);
        }
        else //c
        {
            velocity.y = 0f;
        }
        if (isJumping)
        {
            velocity.y = jumpVelocity;
            Debug.Log("CHECJED");
            isJumping = false;
        }
        rb2D.linearVelocityY = velocity.y;
    }
   
    public bool IsWalking()
    {
        // is the velocity of the player above 
        if(Mathf.Abs(rb2D.linearVelocityX) > 0)
        {
            return true; // sets movement to true
        }
        return false; // sets the movement to false
    }
    public bool IsGrounded()
    {
        // boxcast for the player to detect if they are touching the ground
        RaycastHit2D collisionCheck= Physics2D.BoxCast(
            playerCollider.bounds.center, 
            new Vector2(playerCollider.bounds.size.x,BoxcastDistance), 
            0f, // always points downward
            Vector2.down, 
            playerCollider.bounds.extents.y + BoxcastDistance,
            groundMask);
        // checks if there is ground in the boxcast check
        if (collisionCheck)
        {
            return true; // player is on ground
        }
        
        return false;// if the player is vertically moving
    }

    public FacingDirection GetFacingDirection()
    {
        if(lastDirection == 1)
        {
            return FacingDirection.right;
        }
        return FacingDirection.left;
    }
}
