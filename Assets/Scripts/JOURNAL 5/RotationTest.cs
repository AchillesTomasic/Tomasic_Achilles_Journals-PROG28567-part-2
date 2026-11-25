using UnityEngine;
using UnityEngine.InputSystem;

public class RotationTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rb; // rigidbody of object
    public float angularVel; // used to set angular velocity value
    public float angularDamp; // used to set the angular damping value
    void FixedUpdate()
    {
        if (Keyboard.current.sKey.isPressed)
        {
            testForAngularVelocity();
        }

        if (Keyboard.current.dKey.isPressed)
        {
            testForDampingVelocity();
        }
    }
    void testForAngularVelocity()
    {
        rb.angularVelocity = angularVel; // sets the angular velocity
    }
    void testForDampingVelocity()
    {
        rb.angularVelocity = angularDamp; // sets the angular velocity
    }
    
}
