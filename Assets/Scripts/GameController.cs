using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{   
    public int initialScore = -50;
    public float initialTime = 180f;
    public float time;
    public int score = 0;

    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] TextMeshProUGUI result_text;
    [SerializeField] TextMeshProUGUI final_text;
    [SerializeField] TextMeshProUGUI text_score;
    [SerializeField] GameObject button_back;
    // [SerializeField] GameObject black;
    [SerializeField] private GameObject green_table;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject green_wardrobe;
    [SerializeField] private GameObject wardrobe;

    public ProgressBar progressbar;
    public int maxHealth = 100;
    public int currentHealth;

    public int trashPenalty = 3;        // For each trash
    public int stainPenalty = 10;       // For each stain
    public int furniturePenalty = 5;    // For each furniture out of place
    public int furnitureReward = 5;     // For each furniture in place
    public int peopleReward = 15;       // For each person in the room

    public int glassesReward = 10;      // For each glass in a table
    public int glassesPenalty = 10;     // If the amount of glasses is too low

    public int foodReward = 10;         // For each food in a table
    public int foodPenalty = 0;         // If the amount of food is too low

    public float timePenalty = 0.5f;    // For each second that passes

    public GameObject[] furniture;
    // Start is called before the first frame update
    void Start()
    {
        time = initialTime;
        time_text.text = "Tiempo: " + time;
        result_text.enabled = false;
        final_text.enabled = false;
        // black.SetActive(false);
        button_back.SetActive(false);
        text_score.text = "Puntos " + score;
        float timeToLoadScene = initialTime;
        currentHealth = maxHealth;
        progressbar.SetMaxHealth(maxHealth);
        Invoke("GameFinished", timeToLoadScene);

        furniture = GameObject.FindGameObjectsWithTag("Furniture");
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0){
            time -= Time.deltaTime;
            // Set it in format 00:00
            int seconds = (int)time % 60;
            int minutes = (int)time / 60;
            time_text.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            GameFinished();
        }

        // Update score
        score = calculateScore();

        text_score.text = "" + score;
        currentHealth = score;
        progressbar.SetHealth(currentHealth);
        // if (currentHealth >= maxHealth) {
        //     GameFinished();
        if (currentHealth <= -maxHealth) {
            GameFinished();
        }
    }

    void GameFinished(){
        // black.SetActive(true);
        // button_back.SetActive(true);
        // final_text.enabled = true;
        if (score < 0) {
            // Open the game over scene
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        } else {
            // Open the game win scene
            Debug.Log("Game Win");
            SceneManager.LoadScene("GameWin");
        }
        // result_text.enabled = true;
        // SceneManager.LoadScene("EndGame");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    int calculateScore() {
        int score = initialScore;
        // Iterate over the furniture
        foreach (GameObject f in furniture) {
            if (f.GetComponent<FurnitureController>().IsInGoal()) {
                score += furnitureReward;
            }
            else {
                score -= furniturePenalty;
            }
        }
        
        // Count the number of trash in the scene and remove the score
        GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
        score -= trash.Length * trashPenalty;
        // Count the number of stains in the scene and remove the score
        GameObject[] stains = GameObject.FindGameObjectsWithTag("Stain");
        score -= stains.Length * stainPenalty;

        // Count the number of people in the scene and add the score
        GameObject[] people = GameObject.FindGameObjectsWithTag("Person");
        score += people.Length * peopleReward;

        // Count the number of glasses
        GameObject[] glasses = GameObject.FindGameObjectsWithTag("Glass");
        score += glasses.Length * glassesReward;

        // If the quantity of glasses is lower than the number of people, decrease the score
        if (glasses.Length < people.Length) {
            score -= glassesPenalty * (people.Length - glasses.Length);
        }

        // Count the number of food
        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        score += food.Length * foodReward;

        // If the quantity of food is lower than half the number of people, decrease the score
        if (food.Length < people.Length / 2) {
            score -= foodPenalty;
        }

        score -= (int) ((initialTime - time) * timePenalty);

        return score;
    }
}
