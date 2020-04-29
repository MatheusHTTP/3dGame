using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public GameObject popup;
    public Text uiText;
    public string txt;
    public bool col = false;
    public GameObject[] itens = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        txt = "Traga os 3 ingredientes nescessários para terminar a poção e expulsar as criaturas!"; 
    }

    void Update()
    {
        if (col)
        {
            if(itens[0].active && itens[1].active && itens[2].active)
            {
                txt = "Parece que todos os ingredientes estão ai, agora jogue-os no caldeirão!";
            }
        }
        uiText.text = txt;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popup.SetActive(true);
            col = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popup.SetActive(false);
            col = false;
        }
    }
}
