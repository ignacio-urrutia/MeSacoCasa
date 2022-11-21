using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    public GameObject goal;
    public float delta = 0.1f;
    // Start is called before the first frame update
    public bool IsInGoal()
    {
        return (transform.position - goal.transform.position).magnitude < delta;
    }

    void Start()
    {

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
    }

}
