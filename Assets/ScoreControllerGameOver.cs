using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreControllerGameOver : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score_text;
    // Start is called before the first frame update
    void Start()
    {
        // Set the score in the game over screen
        int time = PlayerPrefs.GetInt("score");
        int seconds = (int)time % 60;
        int minutes = (int)time / 60;
        score_text.text = minutes.ToString("00") + ":" + seconds.ToString("00") + " horas";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
