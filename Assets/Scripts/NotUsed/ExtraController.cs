using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraController : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject extra_1;
    [SerializeField] private GameObject green_table;
    [SerializeField] private GameObject wardrobe;
    [SerializeField] private GameObject extra_2;
    [SerializeField] private GameObject extra_3;
    [SerializeField] private GameObject green_wardrobe;
    [SerializeField] private GameObject stain;
    [SerializeField] private float speed = 0.5f;
    private bool out_extra1 = false;
    private bool out_extra2 = false;
    public TextMeshProUGUI action_text;
    float time = 60f;
    public bool extra_1_on =  false;
    public bool extra_2_on =  false;
    public bool extra_3_on =  false;

    // Start is called before the first frame update
    void Start()
    {
        action_text.text = "";
        stain.SetActive(false);
    }

    void Hide_text()
    {
        action_text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 55 && extra_1_on == true) {
            if ((player1.transform.position - extra_1.transform.position).magnitude < 0.4) {
                if (Input.GetKey(KeyCode.M))
                {
                    extra_1.transform.position = Vector2.MoveTowards(extra_1.transform.position, new Vector2(-4, -4), speed * Time.deltaTime);
                    out_extra1 = true;
                    action_text.text = "¡FUERA!";
                    float timeToLoadScene = 2;
                    Invoke("Hide_text", timeToLoadScene);
                }
            }
            if (((green_table.transform.position - table.transform.position).magnitude > 0.4) || out_extra1) {
                extra_1.transform.position = Vector2.MoveTowards(extra_1.transform.position, new Vector2(-4, -4), speed * Time.deltaTime);
            } else if(!out_extra1) {
                extra_1.transform.position = Vector2.MoveTowards(extra_1.transform.position, table.transform.position, speed * Time.deltaTime);
            }
            if (extra_1.transform.position == new Vector3(-4, -4, 0)) {
                extra_1.SetActive(false);
            }
        }

        if (time <= 50 && extra_3_on == true) {
            // Botar vaso
            extra_3.transform.position = Vector2.MoveTowards(extra_3.transform.position, new Vector2(-4f, -0.7f), speed * Time.deltaTime);
            if ((stain.transform.position - extra_3.transform.position).magnitude < 0.1) {
                stain.SetActive(true);
            }
        } 

        if (time <= 40 && extra_2_on == true) {
            if ((player1.transform.position - extra_2.transform.position).magnitude < 0.4) {
                if (Input.GetKey(KeyCode.M))
                {
                    extra_2.transform.position = Vector2.MoveTowards(extra_2.transform.position, new Vector2(4, -4), speed * Time.deltaTime);
                    out_extra2 = true;
                    action_text.text = "¡FUERA!";
                    float timeToLoadScene = 2;
                    Invoke("Hide_text", timeToLoadScene);
                }
            }
            if (((green_wardrobe.transform.position - wardrobe.transform.position).magnitude > 0.1) || out_extra2) {
                extra_2.transform.position = Vector2.MoveTowards(extra_2.transform.position, new Vector2(4, -4), speed * Time.deltaTime);
            } else if(!out_extra2) {
                extra_2.transform.position = Vector2.MoveTowards(extra_2.transform.position, wardrobe.transform.position, speed * Time.deltaTime);
            }
            if (extra_2.transform.position == new Vector3(4, -4, 0)) {
                extra_2.SetActive(false);
            }
        }
    }
}
