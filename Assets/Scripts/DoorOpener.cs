using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Vector2 handPosition;
    public float grabDistance = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Create a list of all the objects with a collider that are within a certain distance of the player using RaycastAll (grabDistance)
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            transform.position - grabDistance/2 * transform.right + (Vector3)handPosition,
             transform.right, 
             grabDistance);
        RaycastHit2D hit = new RaycastHit2D();
        bool found = false;

        // Draw a line to show the raycast
        // red line if nothing is hit
        // green line if something is hit

        // Create a variable for the color of the line
        Color lineColor = Color.red;
        if (hits.Length > 1)
        {
            // hit is the first object in the list that is grabbable
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.tag == "Door")
                {
                    lineColor = Color.green;
                    hit = hits[i];
                    found = true;
                    break;
                }
            }
        }
        if (Input.GetKeyDown("space"))
        {
            if (found)
            {
                hit.collider.gameObject.GetComponent<DoorController>().OpenDoor();
            }
        }
    }
}
