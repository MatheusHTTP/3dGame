using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;
    public NavMeshAgent agent;
    public GameObject mob;
    public GameObject prefabProjectile;
    public Animator anim;

    public int lives = 5;

    public enum States
    {
        idle,
        berserk,
        attack,
        die,
        damage,
        patrol,

    }
    public States state;
    void Start()
    {
        ChangeState();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown("space"))
        //if(mob.transform.rotation.x == -0.01899877)
        //{
          //  print(mob.transform.rotation.x);
            //Destroy(gameObject);
        //}
        
        anim.SetFloat("velocidade", agent.velocity.magnitude);
    }

    void ChangeState()
    {
        switch (state)
        {
            case States.idle:
                StartCoroutine(Idle());
                break;
            case States.berserk:
                StartCoroutine(Berserk());
                break;
            case States.attack:
                StartCoroutine(Attack());
                break;
            case States.die:
                StartCoroutine(Die());
                break;
            case States.damage:
                StartCoroutine(Damage());
                break;
            case States.patrol:
                StartCoroutine(Patrol());
                break;
        }
    }

    private IEnumerator Patrol()
    {
        Vector3 newplace = new Vector3(transform.position.x + UnityEngine.Random.Range(-10, 10), transform.position.y, transform.position.z + UnityEngine.Random.Range(-10, 10));
        agent.isStopped = false;

        float waitingtime = UnityEngine.Random.Range(1, 5);
        while (state == States.patrol)
        {

            agent.SetDestination(newplace);
            yield return new WaitForEndOfFrame();

            if (Vector3.Distance(transform.position, target.transform.position) < 1)
            {
                ChangeState(States.idle);
            }
            waitingtime -= Time.deltaTime;
            if (waitingtime < 0)
            {
                ChangeState(States.idle);
            }
        }
        ChangeState();
    }

    private IEnumerator Damage()
    {
        agent.isStopped = true;
        anim.SetBool("Dano", true);
        while (state == States.damage)
        {

            yield return new WaitForSeconds(0.5f);
            ChangeState(States.idle);
        }
        anim.SetBool("Dano", false);
        ChangeState();
    }

    private IEnumerator Attack()
    {
        agent.isStopped = false;
        yield return new WaitForSeconds(1);
        anim.SetBool("Ataque", true);
        yield return new WaitForSeconds(0.65f);
        while (state == States.attack)
        {
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) > 10)
            {
                agent.isStopped = false;

            }
            else
            {
                agent.isStopped = true;
            }
                
            GameObject ball;
            ball = Instantiate(prefabProjectile, mob.transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(mob.transform.forward * 2000 + Vector3.up * 300);
            yield return new WaitForSeconds(1.5f);
        }
        ChangeState();
        anim.SetBool("Ataque", false);
    }

    void ChangeState(States mystate)
    {
        state = mystate;
    }

    IEnumerator Idle()
    {
        agent.isStopped = true;
        float waitingtime = UnityEngine.Random.Range(1, 5);
        while (state == States.idle)
        {

            yield return new WaitForEndOfFrame();
            
            if (Vector3.Distance(transform.position, target.transform.position) < 35)
            {
                ChangeState(States.berserk);
            }
            
            waitingtime -= Time.deltaTime;
            if (waitingtime < 0)
            {
                ChangeState(States.patrol);
            }
        }
        ChangeState();
    }
    IEnumerator Berserk()
    {
        agent.isStopped = false;
        while (state == States.berserk)
        {
            agent.SetDestination(target.transform.position);
            yield return new WaitForEndOfFrame();

            if (Vector3.Distance(transform.position, target.transform.position) < 25)
            {
                ChangeState(States.attack);
            }
        }
        ChangeState();
    }

    IEnumerator Die()
    {
        agent.isStopped = true;
        anim.SetBool("Morrendo", true);
        Destroy(gameObject, 7);
        yield return new WaitForEndOfFrame();

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {
            lives--;
            

            if (lives < 0)
            {
                ChangeState(States.die);
            }
            else
            {
                print("Lives: " + lives);
                ChangeState(States.damage);
            }
        }
    }
}
