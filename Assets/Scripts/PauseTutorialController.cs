using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseTutorialController : MonoBehaviour
{
    [SerializeField] GameObject muteButton;
    [SerializeField] GameObject unmuteButton;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject instructionMenu;
    [SerializeField] GameObject checkpointMenu;
    [SerializeField] GameObject table1;
    [SerializeField] GameObject table2; 
    [SerializeField] GameObject food;
    public Vector2 handPosition;
    public float grabDistance = 1.5f;
    public Animator animator;
    public float timer;
    public GameObject AIPrefab;
    public float timeToSpawn;
    public GameObject trashPrefab;
    [SerializeField] GameObject next_button;
    [SerializeField] GameObject end_button;
    [SerializeField] GameObject door; 
    [SerializeField] GameObject person;
    [SerializeField] TextMeshProUGUI trash_text;
    [SerializeField] TextMeshProUGUI timer_text;
    [SerializeField] TextMeshProUGUI doit_text;
    [SerializeField] TextMeshProUGUI instruction_text;
    public GameObject[] instructions;
    GameObject ai;
    // GameObject extra;
    public GameObject gameController;
    int checkpoint;
    bool ready_checkpoint;

    // Start is called before the first frame update
    void Start()
    {  
        Time.timeScale = 0f; 
        checkpoint = 0;
        ready_checkpoint = false;
        for (int i = 0; i < instructions.Length; i++)
        {
            instructions[i].SetActive(false);
        }
        
        doit_text.enabled = false;
        instruction_text.enabled = false;
        trash_text.enabled = false;
        next_button.SetActive(false);
        trashPrefab = Resources.Load<GameObject>("Trash");
        animator.SetBool("open", false);
        person.SetActive(false);
    }

    // Update is called once per frame 
    void Update()
    {  
        if (checkpoint == 1) // Si es que estamos en el checkpoint de los muebles, entonces los revisamos
        {
            if ((table1.GetComponent<FurnitureController>().IsInGoal()) && (table2.GetComponent<FurnitureController>().IsInGoal()))
            {
                ReadyCheckpoint();
            }
        } else if (checkpoint == 4)
        {
            GameObject[] glasses = GameObject.FindGameObjectsWithTag("Glass");
            if (glasses.Length > 0) 
            {
                ReadyCheckpoint();
            }
        } else if (checkpoint == 7)
        {
            GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
            trash_text.text = "Basura restante: " + trash.Length;
            if (trash.Length == 0) 
            {
                ReadyCheckpoint();
            }
        } else if (checkpoint == 10)
        {
            if (person.active) 
            {
                // Revisar que cuando este cerca de la puerta, desaparezca.
                if ((person.transform.position - door.transform.position).magnitude < 1)
                { 
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    if (((players[0].transform.position - door.transform.position).magnitude < 1) || ((players[1].transform.position - door.transform.position).magnitude < 0.5))
                    {
                        person.SetActive(false);                
                        ReadyCheckpoint();
                    }
                }
            } 
        } 
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MuteMusic()
    {
        AudioListener.volume = 0;
        unmuteButton.SetActive(true);
        muteButton.SetActive(false);
    } 
    
    public void UnmuteMusic() 
    {
        AudioListener.volume = 1;
        unmuteButton.SetActive(false);
        muteButton.SetActive(true);
    }


    public void Resume()
    {
        instructionMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void TutorialStart()
    {
        instructionMenu.SetActive(false);
        pauseMenu.SetActive(false);
        checkpointMenu.SetActive(true);
        instructions[0].SetActive(true);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextCheckpoint()
    {
        if (checkpoint == 0) // Checkpoint de los muebles 1
        {
            instructions[checkpoint].SetActive(false);
            timer_text.enabled = false;
            checkpoint = 1;
            instructions[checkpoint].SetActive(true);
            instructions[8].SetActive(true);
        } else if (checkpoint == 1) // Checkpoint de los muebles 2
        {
            instructions[8].SetActive(false);
            checkpointMenu.SetActive(false);
            doit_text.enabled = true;
            instruction_text.text = "¡Mueve ambos muebles y deja su cuadrado de color verde!";
            instruction_text.enabled = true;
            Time.timeScale = 1f;
        } else if (checkpoint == 2) // Checkpoint de la barra
        {
            instruction_text.enabled = false;
            next_button.SetActive(false);
            checkpointMenu.SetActive(true);
            instructions[checkpoint].SetActive(true);
            instructions[9].SetActive(true);
            checkpoint = 3;
        } else if (checkpoint == 3) { // Checkpoint de los vasos 1
            instructions[checkpoint - 1].SetActive(false);
            instructions[9].SetActive(false);
            instructions[checkpoint].SetActive(true);
            checkpoint = 4;
        } else if (checkpoint == 4) { // Checkpoint de los vasos 2
            instructions[checkpoint - 1].SetActive(false);
            checkpointMenu.SetActive(false);
            Time.timeScale = 1f;
            doit_text.enabled = true;
            instruction_text.text = "¡Saca un vaso de la torre de vasos rojos y ponlo sobre alguna de las mesas!";
            instruction_text.enabled = true;
        } else if (checkpoint == 5) // Checkpoint de limpiar
        {
            instruction_text.enabled = false;
            next_button.SetActive(false);
            checkpointMenu.SetActive(true);
            instructions[checkpoint - 1].SetActive(true);
            checkpoint = 6;
        } else if (checkpoint == 6) { // Checkpoint de limpiar 2
            instructions[checkpoint - 2].SetActive(false);
            checkpointMenu.SetActive(false);
            Time.timeScale = 1f;
            doit_text.enabled = true;
            instruction_text.text = "¡Barre la comida del suelo y llevala al basurero!";
            instruction_text.enabled = true;

            // Throw the food
            for (int i = 0; i < 5; i++)
            {
                int randomX = Random.Range(-1, 1);
                int randomY = Random.Range(-1, 1);
                Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                Instantiate(
                    trashPrefab, 
                    food.transform.position + new Vector3(randomX, randomY, 0f),
                    randomRotation);
            }
            trash_text.enabled = true;
            checkpoint = 7;
        } else if (checkpoint == 8) // Echar invitados
        {
            instruction_text.enabled = false;
            trash_text.enabled = false;
            next_button.SetActive(false);
            checkpointMenu.SetActive(true);
            instructions[5].SetActive(true);
            checkpoint = 9;
        } else if (checkpoint == 9) {
            Time.timeScale = 1f;
            doit_text.enabled = true;
            instruction_text.text = "¡Echa a la persona que entró y cierra la puerta!";
            person.SetActive(true);
            instruction_text.enabled = true;
            instructions[5].SetActive(false);
            checkpointMenu.SetActive(false);
            checkpoint = 10;
        } else if (checkpoint == 11) {
            instruction_text.enabled = false;
            instructions[5].SetActive(false);
            instructions[6].SetActive(true);
            checkpointMenu.SetActive(true);
            next_button.SetActive(false);
            end_button.SetActive(true);
        }
    }

    public void ReadyCheckpoint() 
    {
        if (checkpoint == 1) 
        {
            Time.timeScale = 0f;
            doit_text.enabled = false;
            instruction_text.text = "¡Muy bien, ordenaste los muebles ooo!";
            instructions[checkpoint].SetActive(false);
            checkpoint = 2;
            next_button.SetActive(true);
        } else if (checkpoint == 4) 
        {
            Time.timeScale = 0f;
            doit_text.enabled = false;
            instruction_text.text = "¡Muy bien, pusiste un vaso sobre la mesa!";
            instructions[checkpoint].SetActive(false);
            checkpoint = 5;
            next_button.SetActive(true);
        } else if (checkpoint == 7) 
        {
            Time.timeScale = 0f;
            doit_text.enabled = false;
            instruction_text.text = "¡Muy bien, recogiste toda la basura y la llevaste al basurero!";
            instructions[checkpoint].SetActive(false);
            checkpoint = 8;
            next_button.SetActive(true);
        } else if (checkpoint == 10) 
        {
            Time.timeScale = 0f;
            doit_text.enabled = false;
            instruction_text.text = "¡Muy bien, echaste a la persona!";
            checkpoint = 11;
            next_button.SetActive(true);
        }
    }
}
