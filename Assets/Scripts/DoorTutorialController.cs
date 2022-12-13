using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTutorialController : MonoBehaviour
{
    public bool isOpen = false;
    public Animator animator;
    public GameObject bot;
    public float timer;
    public ExtraController extraController;
    public GameObject AIPrefab;
    public float timeToSpawn;
    public float messyProbability;
    public Vector2 handPosition;
    public float grabDistance = 1.5f;

    public GameObject gameController;

    GameObject ai;
    // Start is called before the first frame update
    void Start()
    {
        timeToSpawn = 1f;
        timer = 0f;
        OpenDoor();
    }

    // Update is called once per frame
    void Update()
    {
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
                OpenDoor();
            }
        }
        // messyProbability = (gameController.GetComponent<TutorialController>().initialTime - gameController.GetComponent<TutorialController>().time) / gameController.GetComponent<TutorialController>().initialTime;

        // if (isOpen == true)
        // {
        //     timer += Time.deltaTime;
        // }
        // if (timer >= timeToSpawn)
        // {
        //     // Spawn AI in the door position
        //     ai = Instantiate(AIPrefab, transform.position, Quaternion.identity);
        //     // Set the AI personality
        //     float random = Random.Range(0, 1f);
        //     Debug.Log("Random: " + random);
        //     Debug.Log("Prob: " + messyProbability);
        //     if (random < messyProbability)
        //     {
        //         Debug.Log("Messy AI");
        //         ai.GetComponent<Patrol>().isMessy = true;
        //     }
        //     timer = 0;
        //     timeToSpawn = 10 - 10*(gameController.GetComponent<TutorialController>().initialTime - gameController.GetComponent<TutorialController>().time) / gameController.GetComponent<TutorialController>().initialTime;
        //     // Set the AI's 
        // }

        /*
        if (isOpen == true && timer > 2f)
        {
            // access to the public variable extra_1_on
            if (extraController.extra_1_on == false)
            {
                extraController.extra_1_on = true;
            }
        if (isOpen == true && timer > 4f)
            if (extraController.extra_2_on == false)
            {
                extraController.extra_2_on = true;
            }
        if (isOpen == true && timer > 6f)
            if (extraController.extra_3_on == false)
            {
                extraController.extra_3_on = true;
            }
        }
        // {
        //     timer = 0;
        //     float x = Random.Range(-5f, 5f);
        //     float y = Random.Range(0.5f, 1f);
        //     Vector2 pos = new Vector2(x, y);
        //     Quaternion rot = new Quaternion();
        //     GameObject a = Instantiate(bot, pos, rot) as GameObject;
        // }
        */
    }

    public void OpenDoor()
    {
        if (isOpen == true)
        {
            isOpen = false;
            animator.SetBool("open", false);
        }
        else
        {
            isOpen = true;
            animator.SetBool("open", true);
        }
    }
}


