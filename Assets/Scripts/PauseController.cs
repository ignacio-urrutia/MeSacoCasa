using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject muteButton;
    [SerializeField] GameObject unmuteButton;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject instructionMenu;

    // Start is called before the first frame update
    void Start()
    {  
        Time.timeScale = 0f; 
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void UpdateValueOnChange(float value)
    {
        // mixer.SetFloat(volumenName, value);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MuteMusic()
    {
        AudioListener.volume = 0;
        unmuteButton.SetActive(false);
        muteButton.SetActive(true);
    } 
    
    public void UnmuteMusic() 
    {
        AudioListener.volume = 1;
        unmuteButton.SetActive(true);
        muteButton.SetActive(false);
    }


    public void Resume()
    {
        instructionMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
