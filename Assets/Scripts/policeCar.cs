using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class policeCar : MonoBehaviour
 { 
    public Vector3 target;
    public int verdadero;
    [SerializeField] [Range(0f, 4f)] public float speed;
    void Start(){
        verdadero = 0;
        target = transform.position;
        target.y *= -1;
        InvokeRepeating("MovePoliceCar", 30.0f, 20.0f);
    }
 
    private void Update()
    {
        if (verdadero == 1) {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }
        
    }
 
    private void Move()
    {
    }
 
     //after 10 seconds, the object moves
    public void MovePoliceCar()
    {   
        verdadero = 1;
        target = transform.position;
        target.y *= -1;
    }

    // IEnumerator SmoothTranlation(Vector3 target, float speed) {
    //     Vector3 a = transform.position;
    //     Vector3 temp = transform.position;
    //     temp.y *= -1;
       
    // }
}
