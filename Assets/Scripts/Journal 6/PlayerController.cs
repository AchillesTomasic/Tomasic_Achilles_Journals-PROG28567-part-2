using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.RenderGraphModule;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D; // rigidbody for the player
    // movement affected controls //
    private float acclerationX; //rate of acceleration for the player
    public float timeToReachMaxSpeedX; // time to reach the max speed for the player
    public float maxSpeedX; // max Speed for the player
    public float velocityX; // used to determine the left and right velocity of the player 
    private float deacclerationX; //rate of acceleration for the player
    public float timeToReachDeAccSpeedX; // time to reach the max speed for the player
    public float lastDirection; // gets the previous direction for the player

    // collision boxcast 2d //
    public Collider2D playerCollider; // obtains the players box collider value
    public float BoxcastDistance; // distance the boxcast travels
    public LayerMask groundMask; // used to only check the ground layers

    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        acclerationX = maxSpeedX/ timeToReachMaxSpeedX; // calculatuion to get the acceleration
        deacclerationX = maxSpeedX / timeToReachDeAccSpeedX; // calculation to get deacceleration
        rb2D = GetComponent<Rigidbody2D>(); // used to set the rigidbody2d to the value set on this instance
        playerCollider = rb2D.GetComponent<Collider2D>();
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
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        
        // raises the acceleration if the players controller moves
        if (Mathf.Abs(playerInput.x) > 0)
        {
            lastDirection = playerInput.x; // sets the direction that the player is traveling equal to the current input
            velocityX += acclerationX * Time.fixedDeltaTime; // determines the velocituy of the player over fixed deltatime
        }
        // slows player velocity if player is not moving or the controller is below an epsilon value
        if (Mathf.Abs(playerInput.x) <= 0)
        {
            velocityX -= deacclerationX * Time.fixedDeltaTime;
        }
        // caps the speed for the player
        if (velocityX > maxSpeedX)
        {
            velocityX = maxSpeedX; //sets velocity equal to the max speed x
        }
        // stops the speed from going below zero
        if (velocityX < 0)
        {
            velocityX = 0f; // ensures the speed will never 
        }

        rb2D.linearVelocityX = lastDirection * velocityX; // changes the x linear velocity for he rigidbody on the player
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
