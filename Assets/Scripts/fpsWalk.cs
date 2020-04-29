using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fpsWalk : MonoBehaviour
{
    Vector3 playerAxis;
    Vector3 playerRotAxis;
    Vector3 headAxis;
    Vector3 headRotAxis;
    public CharacterController charac;
    public GameObject prefabProjectile;
    public GameObject dead;
    public GameObject head;
    public float vida;
    public Image hud;
    float calc;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        calc = (hud.transform.localScale.x) / vida;
        vida = hud.transform.localScale.x;
        print(calc);
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

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject ball = Instantiate(prefabProjectile, transform.position + head.transform.forward, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(head.transform.forward * 2000 + Vector3.up * 200);
            //ball.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right * 500, ForceMode.Impulse);
            Destroy(ball, 3);
        }
        if(hud.transform.localScale.x > vida)
        {
            hud.rectTransform.localScale = new Vector3(hud.transform.localScale.x - 0.01f, hud.transform.localScale.y, hud.transform.localScale.z);
        }
        if (hud.transform.localScale.x < 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Derrota");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyWeapon"))
        {
            collision = null;
            print(vida);
            vida -= calc;
        }
    }
}
