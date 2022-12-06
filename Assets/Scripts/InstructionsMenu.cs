using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            // time_text.text = "Tiempo: ";
            SceneManager.LoadScene("Game");
        }        
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
