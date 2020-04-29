using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColect : MonoBehaviour
{
    public GameObject popup;
    public GameObject colected;
    bool col = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    { 
      if(col == true)
        {  
            if (Input.GetKeyDown("e"))
            {
                popup.SetActive(false);
                gameObject.SetActive(false);
                print("destroyed");
                colected.SetActive(true);
                col = false;
            }
        }  
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
