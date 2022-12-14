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

    public float timeToOpenDoor = 15f;

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
            // Debug.Log("Random: " + random);
            // Debug.Log("Prob: " + messyProbability);
            if (random < messyProbability)
            {
                Debug.Log("Messy AI");
                ai.GetComponent<Patrol>().isMessy = true;
            }
            timer = 0;
            timeToSpawn = 10 - 10*(gameController.GetComponent<GameController>().initialTime - gameController.GetComponent<GameController>().time) / gameController.GetComponent<GameController>().initialTime;
            // Set the AI's 
        }
    }

    public void OpenDoor()
    {   
        if (isOpen == true)
        {
            isOpen = false;
            animator.SetBool("open", false);
            Invoke("OpenOpenDoor", timeToOpenDoor);
        }
        else
        {
            isOpen = true;
            animator.SetBool("open", true);
            CancelInvoke("OpenOpenDoor");
        }
    }

    public void OpenOpenDoor()
    {
        isOpen = true;
        animator.SetBool("open", true);
    }
}
