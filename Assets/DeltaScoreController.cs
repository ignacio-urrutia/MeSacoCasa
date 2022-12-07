using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeltaScoreController : MonoBehaviour
{

    public int deltaScore = 0;
    public float delay = 2f;
    // the text object that displays the delta score
    public TextMeshPro deltaScoreText;

    public 
    // Start is called before the first frame update
    void Start()
    {
        deltaScoreText = GetComponent<TextMeshPro>();
        // if the delta score is positive, add a "+" sign in front of it and set the color to green
        if (deltaScore > 0)
        {
            deltaScoreText.text = "+" + deltaScore.ToString();
            deltaScoreText.color = Color.green;
        }
        // if the delta score is negative, set the color to red
        else if (deltaScore < 0)
        {
            deltaScoreText.text = deltaScore.ToString();
            deltaScoreText.color = Color.red;
        }
        // if the delta score is 0, set the color to white
        else
        {
            deltaScoreText.text = deltaScore.ToString();
            deltaScoreText.color = Color.white;
        }

        // Destroy th eobject after timer seconds
        Object.Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
