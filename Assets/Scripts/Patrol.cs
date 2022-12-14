using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public GameObject[] waypoints;
    public float speed = 1f;
    public float random;
    public float time;
    public float maxWaitTime = 10f;
    public float minWaitTime = 1f;
    public float waitTime;
    public float grabDistance = 0.5f;
    public Collider2D[] objectsInRadius;
    public GameObject stainPrefab;
    public GameObject trashPrefab;
    public float throwObjectProbability = 0.2f;
    public float cleanProbability = 0.1f;

    public int trashNumber = 5;

    public GameObject gameController;
    public ScoreChange scoreChange;

    // Personality
    public bool isCleaner = false;
    public bool isMessy = false;

    bool blinkingRed = false;
    public float stopBlinkingTime = 10f; // seconds;

    public bool inverse = false;
    float speedDirection;

    bool startedToggling = false;

    public AudioSource liquidSound;
    public AudioSource chipsSound;

    public Sprite[] sprites;

    void stopBlinking ()
    {
        blinkingRed = false;
        CancelInvoke("toggleColor");
        GetComponent<Renderer>().material.color = Color.white;
    }

    void toggleColor()
    {
        if (GetComponent<Renderer>().material.color == Color.red)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void playChipsSound()
    {
        chipsSound.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        // gameController = GameObject.Find("GameController");
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        random = Random.Range(0, waypoints.Length);
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        stainPrefab = Resources.Load<GameObject>("Stain");
        trashPrefab = Resources.Load<GameObject>("Trash");
        // ignore collitions with other AI
        Physics2D.IgnoreLayerCollision(8, 8);
        scoreChange = GetComponent<ScoreChange>();

        // Assign a random sprite from the AI sprites folder
        int randomSprite = Random.Range(0, sprites.Length);
        Sprite sprite = sprites[randomSprite];
        GetComponent<SpriteRenderer>().sprite = sprite;

        // Change hue of the sprite
        float maxColorVariation = 0.5f;
        Color randomVariation = new Color(
            1f - Random.Range(0, maxColorVariation),
            1f - Random.Range(0, maxColorVariation),
            1f - Random.Range(0, maxColorVariation),
            1f
        );
        Debug.Log(randomVariation);
        GetComponent<SpriteRenderer>().color = randomVariation;

        // Stop blinking after a while
        Invoke("stopBlinking", stopBlinkingTime);

    }

    // Update is called once per frame
    void Update()
    {   
        if (isMessy)
        {
            // Add the furniture to the list of waypoints
            GameObject[] furniture = GameObject.FindGameObjectsWithTag("Furniture");
            GameObject[] waypoints_aux = GameObject.FindGameObjectsWithTag("Waypoint");
            waypoints = new GameObject[waypoints_aux.Length + furniture.Length];
            for (int i = 0; i < waypoints_aux.Length; i++)
            {
                waypoints[i] = waypoints_aux[i];
            }
            for (int i = 0; i < furniture.Length; i++)
            {
                waypoints[waypoints_aux.Length + i] = furniture[i];
            }
            throwObjectProbability = 0.5f;
            
            if (!startedToggling)
            {
                InvokeRepeating("toggleColor", 0f, 0.3f);
                startedToggling = true;
            }

        } else
        {
            throwObjectProbability = 0f;
        }

        if (isCleaner)
        {
            cleanProbability = 0.5f;
        } else
        {
            cleanProbability = 0f;
        }

        if (!inverse)
        {
            speedDirection = speed;
        }
        else
        {
            speedDirection = -speed;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[(int)random].transform.position, speedDirection * Time.deltaTime);

        time = time += Time.deltaTime;
        if (time >= waitTime)
        {
            random = Random.Range(0, waypoints.Length);
            time = 0;
            inverse = false;
        }

        // If the AI pass near an object with the tah "Glass", throw it 
        // List of all objects in a radius of grabDistance
        objectsInRadius = Physics2D.OverlapCircleAll(transform.position, grabDistance);
        // Show a circle in the editor to see the radius
        Debug.DrawRay(transform.position, Vector2.up * grabDistance, Color.red);

        foreach (Collider2D obj in objectsInRadius)
        {
            if (obj.gameObject.tag == "Glass")
            {
                if (Random.Range(0f, 1f) < throwObjectProbability)
                {
                    // Throw the glass
                    Destroy(obj.gameObject);
                    // Spawn a stain
                    Instantiate(
                        stainPrefab, 
                        obj.transform.position + new Vector3(0f, -0.5f, 0f),
                        Quaternion.identity);
                    scoreChange.setScoreChange(-GlobalParameters.glassesPenalty - GlobalParameters.stainPenalty, transform.position);
                    Debug.Log("Glass thrown");
                    liquidSound.Play();
                }

            }

            if (obj.gameObject.tag == "Food")
            {
                if (Random.Range(0f, 1f) < throwObjectProbability)
                {
                    // Throw the food
                    Destroy(obj.gameObject);
                    scoreChange.setScoreChange(-GlobalParameters.foodReward, transform.position);
                    // Spawn some trash
                    for (int i = 0; i < trashNumber; i++)
                    {
                        int randomX = Random.Range(-1, 1);
                        int randomY = Random.Range(-1, 1);
                        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                        Instantiate(
                            trashPrefab, 
                            obj.transform.position + new Vector3(randomX, randomY, 0f),
                            randomRotation);
                        scoreChange.setScoreChange(-GlobalParameters.trashPenalty, obj.transform.position + new Vector3(randomX, randomY, 0f));
                        // Play the sound after i*0.1 seconds
                        Invoke("playChipsSound", i*0.01f);
                    }

                }
            }

            if (obj.gameObject.tag == "Door")
            {
                // If the door is not open, delete the AI
                if (obj.gameObject.GetComponent<DoorController>().isOpen == false)
                {
                    Destroy(gameObject);
                    scoreChange.setScoreChange(-GlobalParameters.peopleReward, transform.position);
                }
            }

            if (obj.gameObject.tag == "Stain")
            {
                if (Random.Range(0f, 1f) < cleanProbability)
                {
                    // Throw the glass
                    Destroy(obj.gameObject);
                }
            }

            if (obj.gameObject.tag == "Trash")
            {
                if (Random.Range(0f, 1f) < cleanProbability)
                {
                    // Throw the glass
                    Destroy(obj.gameObject);
                }
            }

            if (obj.gameObject.tag == "Furniture")
            {
                if (!isMessy)
                {
                    inverse = true;
                }
            }


        }

    }
}
