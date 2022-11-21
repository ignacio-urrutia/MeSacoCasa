using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffController : MonoBehaviour
{
    [SerializeField] private GameObject red_table;
    [SerializeField] private GameObject green_table;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject red_wardrobe;
    [SerializeField] private GameObject green_wardrobe;
    [SerializeField] private GameObject wardrobe;
    [SerializeField] private GameObject stain;

    // Start is called before the first frame update
    void Start()
    {
        red_table.SetActive(false); 
        red_wardrobe.SetActive(false);
        stain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ----- Table -----
        if ((green_table.transform.position - table.transform.position).magnitude > 0.1) {
            // Mal puesta
            green_table.SetActive(false);
            red_table.SetActive(true);
        }
        if ((green_table.transform.position - table.transform.position).magnitude < 0.1) {
            // Bien puesta
            green_table.SetActive(true);
            red_table.SetActive(false);
        }
        // ----------------
    
    
        // ----- Wardrobe -----
        if ((green_wardrobe.transform.position - wardrobe.transform.position).magnitude > 0.1) {
            // Mal puesta
            green_wardrobe.SetActive(false);
            red_wardrobe.SetActive(true);
        }
        if ((green_wardrobe.transform.position - wardrobe.transform.position).magnitude < 0.1) {
            // Bien puesta
            green_wardrobe.SetActive(true);
            red_wardrobe.SetActive(false);
        }
        // ----------------


        // ----- Stain -----
        // if ((stain.transform.position - extra_3.transform.position).magnitude < 0.1) {
        //         stain.SetActive(true);
        //     }
        if ((stain.transform.position - wardrobe.transform.position).magnitude > 0.1) {
            // Mal puesta
            green_wardrobe.SetActive(false);
            red_wardrobe.SetActive(true);
        }
        if ((green_wardrobe.transform.position - wardrobe.transform.position).magnitude < 0.1) {
            // Bien puesta
            green_wardrobe.SetActive(true);
            red_wardrobe.SetActive(false);
        }
        // ----------------
    }
}
