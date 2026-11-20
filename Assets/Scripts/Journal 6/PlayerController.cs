using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // colliders for the player //
    private Rigidbody2D rb2D; // rigidbody on the player

    // changable values for the player // 
    public float maxSpeed; // sets the max speed that the player can reach
    public float timeToReachMaxSpeed; // time it takes to reach the max speed for the player
    public float acceleration; // aceleration for the player
    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();// sets this value to the current rigidbody2D on the player
    }

    void Update()
    {
        
        Vector2 playerInput = new Vector2();

        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.

        MovementUpdate(playerInput);
    }
    // sets the input for the player
    private Vector2 Controls(Vector2 input)
    {
        
        return input;
    }

    private void MovementUpdate(Vector2 playerInput)
    {

    }

    public bool IsWalking()
    {
        return false;
    }
    public bool IsGrounded()
    {
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        return FacingDirection.left;
    }
}
