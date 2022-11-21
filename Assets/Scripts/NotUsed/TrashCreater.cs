using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCreater : MonoBehaviour
{

    public float spawnTime = 1f;
    public GameObject trashPrefab;


    // Start is called before the first frame update
    void Start()
    {
        // Call the SpawnTrash function every 2 seconds
        InvokeRepeating("SpawnTrash", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnTrash()
    {   
        float MaxX = 1.9f;
        float MaxY = 0.9f;


        // Create a random position for the trash
        float x = Random.Range(-MaxX, MaxX);
        float y = Random.Range(-MaxY, MaxY);

        Vector2 trashPosition = new Vector2(x, y);
        Debug.Log(trashPosition);

        // Create the trash
        Instantiate(trashPrefab, trashPosition, Quaternion.identity);
    }
}
