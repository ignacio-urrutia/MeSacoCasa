using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] instructions;
    int checkpoint;
    bool ready_checkpoint;

    void Start()
    {
        checkpoint = 0;
        ready_checkpoint = false;
        instructions[checkpoint].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (ready_checkpoint) 
        {
            instructions[checkpoint].SetActive(false);
            checkpoint += 1;
            if (checkpoint == 6) 
            {
                // Fin del tutorial
                checkpoint = -1;
            } else 
            {
                instructions[checkpoint].SetActive(true); //Siguiente tarea
            }
        }

        // if () // Cumple la tarea que le toca, entonces pasa al segundo checkpoint
        // {

        // }
    }
}

