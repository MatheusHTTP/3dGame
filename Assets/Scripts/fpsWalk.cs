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
    public Text lifebar;
    public int vidas = 7;
    string life;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        life = "vidas: " + vidas;
        lifebar.text = life;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyWeapon"))
        {
            collision = null;
            vidas--;
            life = "vidas: " + vidas;
            lifebar.text = life;
            if (vidas < 0)
            {
                dead.SetActive(true);
                this.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Derrota");
            }
            else
            {
                print("vidas: " + vidas);
            }
 
        }
    }
}
