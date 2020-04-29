using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 15;
    public float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnTime)
        {
            GameObject respawn = Instantiate(enemy, transform.position + gameObject.transform.forward, Quaternion.identity);
            respawn.SetActive(true);
            time = 0;
            spawnTime += spawnTime / 4;
        }
    }
}
