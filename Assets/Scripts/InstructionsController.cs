using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    public GameObject[] instructions;
    public float[] instructionsAppearTime;
    public float[] instructionsDisappearTime;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        for (int i = 0; i < instructions.Length; i++)
        {
            if (timer > instructionsAppearTime[i] && timer < instructionsDisappearTime[i])
            {
                instructions[i].SetActive(true);
            }
            else
            {
                instructions[i].SetActive(false);
            }
        }
    }
}
