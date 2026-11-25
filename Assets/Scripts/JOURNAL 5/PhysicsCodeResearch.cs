using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsCodeResearch : MonoBehaviour
{
    public LayerMask physicsLayer; // layer for physics objects
    public TextMeshProUGUI textBubble;// used for the textbubble
    public GameObject scanner; // scanner origin for the object boxcast
    public Vector2 boxSize;
    public float boxcastDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            testForBoxCast();
        }
    }
    void testForBoxCast()
    {
       RaycastHit2D objectInScene =  Physics2D.BoxCast( scanner.transform.position, boxSize,0f,Vector2.right,boxcastDistance,physicsLayer);// activates a boxcast
        textBubble.text = objectInScene.collider.name; // obtains the name of the object we have hit
        //draws a debug box to show the hitrange
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + boxSize.y/2,0), new Vector3(transform.position.x + boxcastDistance,
            transform.position.y + boxSize.y / 2, 0),Color.green);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - boxSize.y / 2, 0), new Vector3(transform.position.x + boxcastDistance,
            transform.position.y - boxSize.y / 2, 0), Color.green);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + boxSize.y / 2, 0), new Vector3(transform.position.x,
            transform.position.y - boxSize.y / 2, 0), Color.green);
        Debug.DrawLine(new Vector3(transform.position.x + boxcastDistance, transform.position.y + boxSize.y / 2, 0), new Vector3(transform.position.x + boxcastDistance,
            transform.position.y - boxSize.y / 2, 0), Color.green);
    }

}
