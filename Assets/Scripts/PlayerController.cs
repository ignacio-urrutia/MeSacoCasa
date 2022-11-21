using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Animator animator;
    public Vector2 direction;

    // Define a public string for the button that will be used to move up, down, left, and right
    public string upButton;
    public string downButton;
    public string leftButton;
    public string rightButton;

    public int sortingOrder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(upButton) || Input.GetKey(downButton) || Input.GetKey(leftButton) || Input.GetKey(rightButton))
        {
            direction = new Vector2(0, 0);

            if (Input.GetKey(leftButton))
            {
                    animator.SetFloat("Horizontal", -1f);
                    direction += new Vector2(-1f, 0f);
            }

            if (Input.GetKey(rightButton))
            {
                animator.SetFloat("Horizontal", 1f);
                direction += new Vector2(1f, 0f);
            }

            if (Input.GetKey(upButton))
            {
                animator.SetFloat("Vertical", 1f);
                direction += new Vector2(0f, 1f);
            }

            if (Input.GetKey(downButton))
            {
                animator.SetFloat("Vertical", -1f);
                direction += new Vector2(0f, -1f);
            }

            if (!Input.GetKey(leftButton) && !Input.GetKey(rightButton))
            {
                animator.SetFloat("Horizontal", 0f);
            }

            if (!Input.GetKey(upButton) && !Input.GetKey(downButton))
            {
                animator.SetFloat("Vertical", 0f);
            }

            // Normalize the direction vector
            direction.Normalize();
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed * Time.deltaTime);

            // Set the order in layer of the player's sprite equal to the y position of the player
            
            sortingOrder = (int) (Screen.height - Camera.main.WorldToScreenPoint(transform.position).y);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        }
    }
}

