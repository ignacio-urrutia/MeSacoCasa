using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ignore collisions between the player and the trash
public class TrashController : MonoBehaviour
{
    public GameObject container;
    public bool isGrabbed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Ignore collisions with the player
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player2").GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Player")
        // {
        //     Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        // }
    }
}
