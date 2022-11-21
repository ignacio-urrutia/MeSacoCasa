using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScriptTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI result_text;
    [SerializeField] TextMeshProUGUI end_text;
    [SerializeField] GameObject button;
    [SerializeField] GameObject black;
    [SerializeField] private GameObject extra_1;
    float time = 60f;
    // GAME 
    private int points = 0;
    private bool table_rest = false;
    [SerializeField] private GameObject green_table;
    [SerializeField] private GameObject table;
    private bool wardrobe_rest = false;
    [SerializeField] private GameObject green_wardrobe;
    [SerializeField] private GameObject wardrobe;
    public TextMeshProUGUI text_points;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = "Tiempo: " + time;
        button.SetActive(false);
        result_text.enabled = false;
        end_text.enabled = false;
        black.SetActive(false);
        text_points.text = "Puntos " + points;
        float timeToLoadScene = 60;
        Invoke("GameFinished", timeToLoadScene);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0){
            time -= Time.deltaTime;
            timer.text = "Tiempo: " + time.ToString("f0");
        }

        text_points.text = "Puntos " + points; 
        if (((green_table.transform.position - table.transform.position).magnitude > 0.1) && !table_rest) {
            // Mal puesta
            points -= 2;
            table_rest = true;
        }    
        if (((green_table.transform.position - table.transform.position).magnitude < 0.1) && table_rest){
            // Bien puesta
            points += 2;
            table_rest = false;
        }

        if (((green_wardrobe.transform.position - wardrobe.transform.position).magnitude > 0.1) && !wardrobe_rest) {
            // Mal puesta
            points -= 2;
            wardrobe_rest = true;
        }    
        if (((green_wardrobe.transform.position - wardrobe.transform.position).magnitude < 0.1) && wardrobe_rest){
            // Bien puesta
            points += 2;
            wardrobe_rest = false;
        }
    }

    void GameFinished(){
        black.SetActive(true);
        button.SetActive(true);
        end_text.enabled = true;
        if (points < 0) {
            result_text.text = "No lograste ordenar a tiempo";
        } else {
            result_text.text = "Felicitaciones, Lograste ordenar a tiempo";
        }
        result_text.enabled = true;
        // SceneManager.LoadScene("EndGame");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
