using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrabController : MonoBehaviour
{
    private bool isGrabbing = false;
    private GameObject grabbedObject;
    public float grabDistance = 1.5f;
    public PlayerController controller;
    public Vector2 handPosition;
    public GameObject redGlassPrefab;

    public string grabButton;

    ScoreChange scoreChange;

    bool throwTrash()
    {
        if (!isGrabbing)
        {
            return false;
        }
        else if (grabbedObject.name != "Shovel")
        {
            return false;
        }

        // Throw the trash
        GameObject shovelInside;
                
        // If the player is grabbing the shovel and it is touching the garbage can, destroy the gargabe in the shovel

        // find the child of the shovel that is a ShovelInside
        shovelInside = grabbedObject.transform.Find("ShovelInside").gameObject;
        if (shovelInside.GetComponent<ShovelInsideController>().isTouchingGarbageCan())
            {
                int trashNumber = 0;
                // Destroy all the trash in the shovel
                foreach (Transform child in shovelInside.transform)
                {
                    if (child.gameObject.tag == "Trash")
                    {
                        Destroy(child.gameObject);
                        trashNumber++;
                    }
                }
                scoreChange.setScoreChange(GlobalParameters.trashPenalty * trashNumber, transform.position);
                return true;
            }
        return false;
    } 

    // Start is called before the first frame update
    // Ignore collisions with the player
    void Start()
    {
        scoreChange = GetComponent<ScoreChange>();
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
                if (hits[i].collider.gameObject.tag == "Grabbable")
                {
                    lineColor = Color.green;
                    hit = hits[i];
                    found = true;
                    break;
                }
            }
        }

        Debug.DrawRay(transform.position - grabDistance/2 * transform.right + (Vector3)handPosition, transform.right * grabDistance, lineColor);

         //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position, grabDistance);

        if (Input.GetKeyDown(grabButton))
        {
            if (isGrabbing) {
                
                if (!throwTrash())
                {
                    
                    // if a table is nearby, put the object on the table
                    Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(transform.position, 5*grabDistance);
                    foreach (Collider2D obj in objectsInRadius)
                    {
                        if (obj.gameObject.name == "table" && grabbedObject.name.Contains("GrabbableGlass"))
                        {
                            // Put the object on the table
                            float randomX = Random.Range(-0.2f, 0.2f);
                            float randomY = Random.Range(-0.2f, 0.2f);

                            // Create a "redGlass" prefab and put it on the table
                            GameObject redGlass = Instantiate(redGlassPrefab, obj.transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity);
                            redGlass.transform.position = obj.transform.position + new Vector3(randomX, randomY, 0);
                            redGlass.transform.parent = obj.transform;
                            redGlass.GetComponent<SpriteRenderer>().sortingLayerName = "Table";

                            // Destroy the grabbed object
                            Destroy(grabbedObject);
                            scoreChange.setScoreChange(GlobalParameters.glassesReward, transform.position);

                            // grabbedObject.transform.position = obj.transform.position + new Vector3(randomX, randomY, 0);
                            // grabbedObject.transform.parent = obj.transform;
                            // // set sorting layer of the glass to table
                            // grabbedObject.GetComponent<SpriteRenderer>().sortingLayerName = "Table";
                            // // set the rotation of the glass to 0
                            // grabbedObject.transform.rotation = Quaternion.identity;
                            break;

                        }
                        else
                        {
                            grabbedObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                            if (grabbedObject.GetComponent<Rigidbody2D>() == null)
                            {
                                grabbedObject.transform.SetParent(null);
                            }
                            else
                            {
                                // Clear joint between the player and the object
                                Destroy(grabbedObject.GetComponent<FixedJoint2D>());
                            }
                        }

                    }

                    isGrabbing = false;
                    // if the object is not a rigidbody clear it as child
                    // if the object is a rigidbody clear the joint
                    //grabbedObject = hit.collider.gameObject;
                    

                    // Ignore collisions with the object
                    Physics2D.IgnoreCollision(grabbedObject.GetComponent<Collider2D>(), controller.GetComponent<Collider2D>(), false);

        
                    // Set the grabbed object to null
                    grabbedObject = null;
                }
                
            }

            else if (found && hit.collider.gameObject.tag == "Grabbable")
            {
                isGrabbing = true;
                // Set the parent of the object to the player
                // if the object is not a rigidbody set as child
                // if the object is a rigidbody make a joint
                grabbedObject = hit.collider.gameObject;
                if (grabbedObject.GetComponent<Rigidbody2D>() == null)
                {
                    grabbedObject.transform.SetParent(transform);
                }
                else
                {
                    // Create a joint between the player and the object
                    grabbedObject.AddComponent<FixedJoint2D>();
                    grabbedObject.GetComponent<FixedJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
                    grabbedObject.GetComponent<FixedJoint2D>().enableCollision = true;
                }
                if (grabbedObject.name.Contains("GrabbableGlass"))
                {
                    // Create a new glass in the same position as the old one
                    GameObject newGlass = Instantiate(grabbedObject, grabbedObject.transform.position, grabbedObject.transform.rotation);
                }
                // grabbedObject = hit.collider.gameObject;
            }

        }

        if (isGrabbing)
        {
            // direction is a Vector2 that points where the player is facing
            grabbedObject.transform.position = transform.position + (Vector3)controller.direction * grabDistance/2;
            // rotate the grabbed object to face the player using the direction vector
            grabbedObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(controller.direction.y, controller.direction.x) * Mathf.Rad2Deg) * Quaternion.Euler(0, 0, 90);

            if (controller.direction.y > 0)
            {
                grabbedObject.GetComponent<SpriteRenderer>().sortingLayerName = "GrabbedObject-Bottom";
            }
            else
            {
                grabbedObject.GetComponent<SpriteRenderer>().sortingLayerName = "GrabbedObject-Top";
            }

        }
    }
}
