using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    float time;
    public GameObject popup;
    public bool col = false;
    public GameObject vat;
    public GameObject pei;
    public GameObject[] itens = new GameObject[3];
    GameObject ball = null;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    void Update()
    {
        if(ball != null)
        {
            time += Time.deltaTime;
            if (time > 10)
            {
                Destroy(ball);
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Vitoria");
            }
        }
        if (col == true)
        {
            if (Input.GetKeyDown("e"))
            {
                if (itens[0].active && itens[1].active && itens[2].active)
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject enemy in enemies) { 
                        GameObject.Destroy(enemy);
                    }
                    popup.SetActive(false);
                    itens[0].SetActive(false);
                    itens[1].SetActive(false);
                    itens[2].SetActive(false);
                    ball = Instantiate(pei, vat.transform.position, Quaternion.identity);
                    
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (itens[0].active && itens[1].active && itens[2].active)
            {
                popup.SetActive(true);
                col = true;
            }
 
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
