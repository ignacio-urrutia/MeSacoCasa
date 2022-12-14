using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChange : MonoBehaviour
{
    public GameObject deltaScorePrefab;
    GameObject deltaScoreObject;
    
    // Start is called before the first frame update
    void Start()
    {
        deltaScorePrefab = Resources.Load<GameObject>("ScoreDelta");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScoreChange(int score, Vector3 position)
    {
        Debug.Log("Score: " + score);
        deltaScoreObject = Instantiate(deltaScorePrefab, position, Quaternion.identity);
        deltaScoreObject.GetComponent<DeltaScoreController>().deltaScore = score;

    }
}
