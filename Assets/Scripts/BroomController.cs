using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to control the trash when it is pushed by the broom

public class BroomController : MonoBehaviour
{
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

}
