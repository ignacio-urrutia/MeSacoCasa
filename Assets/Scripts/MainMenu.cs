using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void InstructionScene()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void TutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
