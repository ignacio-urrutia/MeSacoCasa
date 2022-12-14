using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagController : MonoBehaviour
{   
    public float grabDistance = 0.5f;
    public float cleanProbability = 0.1f;
    public Collider2D[] objectsInRadius;
    ScoreChange scoreChange;

    // Start is called before the first frame update
    void Start()
    {
        scoreChange = GetComponent<ScoreChange>();
    }

    // Update is called once per frame
    void Update()
    {
        objectsInRadius = Physics2D.OverlapCircleAll(transform.position, grabDistance);
        // Show a circle in the editor to see the radius
        Debug.DrawRay(transform.position, Vector2.up * grabDistance, Color.red);

        foreach (Collider2D obj in objectsInRadius)
        {
            if (obj.gameObject.tag == "Stain")
            {
                if (Random.Range(0f, 1f) < cleanProbability)
                {
                    // Throw the glass
                    Destroy(obj.gameObject);
                    scoreChange.setScoreChange(GlobalParameters.stainPenalty, transform.position);
                }
            }   
        }
    }
}
