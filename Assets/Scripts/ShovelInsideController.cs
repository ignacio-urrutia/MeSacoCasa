using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelInsideController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // if this object gets in contact with a trash, set the trash to stay inside the shovel
    void OnTriggerEnter2D(Collider2D collision)
    {   
        Debug.Log("Triggered");
        if (collision.gameObject.tag == "Trash")
        {
            collision.gameObject.transform.parent = transform;
            // set the trash to stay in a random position inside the shovel
            collision.gameObject.transform.localPosition = new Vector3(Random.Range(-0.15f, 0.15f), Random.Range(-0.3f, -0.1f), 0);

            // set the public variable isGrabbed to true
            collision.gameObject.GetComponent<TrashController>().isGrabbed = true;

            // Remove the trash's rigidbody
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            // Remove the trash's collider
            Destroy(collision.gameObject.GetComponent<Collider2D>());
            // Move the trash's sprite to the GrabbedTrash layer
            collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "GrabbedTrash";
        }
    }

    // check if the shovel is in contact with a garbage can
    public bool isTouchingGarbageCan()
    {
        Debug.Log("Checking if touching garbage can");
        // list of all the objects with a collider that are within a radius of the shovel
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        // draw a circle around the shovel to show the radius
        Debug.DrawRay(transform.position, Vector3.zero, Color.red, 0.1f);


        // if the list contains a garbage can, return true
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.tag == "GarbageCan")
            {
                Debug.Log("Touching garbage can");
                return true;
            }
        }
        Debug.Log("Not touching garbage can");
        return false;
    }

}
