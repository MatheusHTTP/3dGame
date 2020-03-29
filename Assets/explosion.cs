using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public float bombForce = 2000;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Explosion();
    }
    // Update is called once per frame
    void Explosion()
    {
        Destroy(gameObject, 1);
        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, 5, Vector3.up, 10);

        if(hits.Length > 0) { 
            foreach(RaycastHit hit in hits) {
                if (hit.rigidbody) {
                    hit.rigidbody.isKinematic = false;
                    hit.rigidbody.AddExplosionForce(bombForce, transform.position, 10);
                }
            
            }

        }
    }
}
