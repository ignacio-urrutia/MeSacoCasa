using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FurnitureController : MonoBehaviour
{
    bool isSoundPlaying = false;
    public GameObject goal;
    public float delta = 0.1f;
    // Start is called before the first frame update

    Vector3 currentPosition;
    Queue<Vector2> previousPositions;
    Vector3 previousPosition;
    int maxPreviousPositions = 20;
    

    public bool IsInGoal()
    {
        return (transform.position - goal.transform.position).magnitude < delta;
    }

    void Start()
    {
        // Get the current position of the table
        currentPosition = transform.position;
        // Save the current position of the table for the next frame
        previousPositions = new Queue<Vector2>();
        previousPosition = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInGoal())
        {
            // Set the color of the goal to green using the 
            goal.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            // Set the color of the goal to red using the
            goal.GetComponent<Renderer>().material.color = Color.red;
        }

        Debug.Log("Velocity: " + GetComponent<Rigidbody2D>().velocity.magnitude);
        Debug.Log("Position: " + transform.position);
        Debug.Log("Delta: " + (transform.position - previousPosition).magnitude);

        previousPosition= transform.position;
    

        // If the furniture is moving then play the sound
        if (GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f || (transform.position - previousPosition).magnitude > 0.001f)
        {
            // Check if the sound is currently playing
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
    }

}
