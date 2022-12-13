using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpen = false;
    public Animator animator;
    public GameObject bot;
    public float timer;
    public ExtraController extraController;
    public GameObject AIPrefab;
    public float timeToSpawn;
    public float messyProbability;

    public GameObject gameController;
    public GameObject deltaScorePrefab;

    GameObject ai;
    GameObject deltaScoreObject;

    ScoreChange scoreChange;

    // Start is called before the first frame update
    void Start()
    {
        timeToSpawn = 1f;
        timer = 0f;
        OpenDoor();
        scoreChange = GetComponent<ScoreChange>();
    }

    // Update is called once per frame
    void Update()
    {

        messyProbability = (gameController.GetComponent<GameController>().initialTime - gameController.GetComponent<GameController>().time) / gameController.GetComponent<GameController>().initialTime;

        if (isOpen == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= timeToSpawn)
        {
            // get the people reward from the game controller
            int peopleReward = GlobalParameters.peopleReward;

            // Spawn AI in the door position
            ai = Instantiate(AIPrefab, transform.position, Quaternion.identity);

            // deltaScoreObject = Instantiate(deltaScorePrefab, transform.position, Quaternion.identity);
            // deltaScoreObject.GetComponent<DeltaScoreController>().deltaScore = peopleReward;
            scoreChange.setScoreChange(peopleReward, transform.position);
            // Play the doorbell sound
            GetComponent<AudioSource>().Play();


            // Set the AI personality
            float random = Random.Range(0, 1f);
            Debug.Log("Random: " + random);
            Debug.Log("Prob: " + messyProbability);
            if (random < messyProbability)
            {
                Debug.Log("Messy AI");
                ai.GetComponent<Patrol>().isMessy = true;
            }
            timer = 0;
            timeToSpawn = 10 - 10*(gameController.GetComponent<GameController>().initialTime - gameController.GetComponent<GameController>().time) / gameController.GetComponent<GameController>().initialTime;
            // Set the AI's 
        }

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
