using TMPro;
using UnityEngine;

public class ClosestPointTest : MonoBehaviour
{
    public Rigidbody2D rb; // rigidbody
    public GameObject detectorObject; //used as the object that closest point tries to detect
    public TextMeshProUGUI textvalue; // used to detect the text on the screen
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        testForClosetPoints();
    }
    void testForClosetPoints()
    {
        Vector2 closestpoint = rb.ClosestPoint(detectorObject.transform.position); // obtains the point on shape
        textvalue.text = closestpoint.ToString(); // sets the value where it is
        Debug.DrawLine(closestpoint, detectorObject.transform.position); // debugdrawline
    }
}
