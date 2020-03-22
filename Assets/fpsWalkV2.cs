using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsWalkV2 : MonoBehaviour
{
    Vector3 playerAxis;
    Vector3 playerRotAxis;
    Vector3 headAxis;
    Vector3 headRotAxis;
    public CharacterController charac;
    public GameObject[] prefabProjectiles = new GameObject[9];
    public GameObject selected;
    public GameObject hand;
    public GameObject head;
    public GameObject backup;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        playerAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 globalmove = transform.TransformDirection(playerAxis);//local pra global 
        charac.SimpleMove(globalmove * 5);
        playerRotAxis = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        headRotAxis = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);

        transform.Rotate(playerRotAxis);//gira o corpo
        head.transform.Rotate(headRotAxis);//gira cabeca
        if (hand != selected)
        {
            Destroy(hand);
            hand = Instantiate(selected, hand.transform.position, transform.rotation, head.transform);
            hand.GetComponent<Rigidbody>().useGravity = false;
            Destroy(hand.GetComponent<Collider>());

        }

        if (Input.GetKey(KeyCode.Alpha1)) { selected = prefabProjectiles[0]; }
        if (Input.GetKey(KeyCode.Alpha2)) { selected = prefabProjectiles[1]; }
        if (Input.GetKey(KeyCode.Alpha3)) { selected = prefabProjectiles[2]; }
        if (Input.GetKey(KeyCode.Alpha4)) { selected = prefabProjectiles[3]; }
        if (Input.GetKey(KeyCode.Alpha5)) { selected = prefabProjectiles[4]; }
        if (Input.GetKey(KeyCode.Alpha6)) { selected = prefabProjectiles[5]; }
        if (Input.GetKey(KeyCode.Alpha7)) { selected = prefabProjectiles[6]; }
        if (Input.GetKey(KeyCode.Alpha8)) { selected = prefabProjectiles[7]; }
        if (Input.GetKey(KeyCode.Alpha9)) { selected = prefabProjectiles[8]; }
        if (Input.GetKey(KeyCode.Alpha0)) { selected = backup; }

        if (selected != null && selected != backup) {
            if (Input.GetButtonDown("Fire1")) {
                
                GameObject ball = Instantiate(selected, transform.position + head.transform.forward, transform.rotation);
                ball.GetComponent<Rigidbody>().AddForce(head.transform.forward * 2000 + Vector3.up * 200);
                ball.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right * 500, ForceMode.Impulse); // faz o objeto voar no modo beyblade
                Destroy(ball, 3);
            }
        }  
    }
}
