using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAxe : MonoBehaviour
{

    private Animator _animator;

    public float lookRadius;

    Transform target;
    NavMeshAgent agent;
    public GameObject boundary;
    //private bool isWandering;

    public float wanderRadius;
    public float wanderTimer;

    private float timer;

    float distance = 9999;
    public GameObject player;
    PlayerVisible playerscript;


    public float roamRadius;
    Vector3 startPosition;

    public bool isAttacking = false;
    public bool isStanding;

    //Rigidbody rb;

    // Use this for initialization

    void Awake()
    {
        startPosition = transform.position;
    }

    void Start()
    {

        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerscript = player.GetComponent<PlayerVisible>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        //rb = GetComponent<Rigidbody>();
        //rb.inertiaTensor = rb.inertiaTensor + new Vector3(rb.inertiaTensor.x * 500, rb.inertiaTensor.y * 500, rb.inertiaTensor.z * 500);
    }

    // Update is called once per frame
    void Update()
    {

        if (playerscript.visibility == true)
        {
            distance = Vector3.Distance(target.position, transform.position);
        }

        //Debug.Log(playerscript.visibility);
        if (distance <= lookRadius)
        {
            if (!isAttacking && !isStanding)
            {
                agent.SetDestination(target.position);
                _animator.SetBool("run", true);
            }

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();


                if (!isAttacking)
                {
                    _animator.SetTrigger("attack_1");
                    isAttacking = true;
                }
            }
        }
        else if (distance > lookRadius && roamRadius > 0)
        {
            if (!isAttacking)
                FreeRoam();

            //timer += Time.deltaTime;

            //if (timer >= wanderTimer)
            //{
            //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            //    agent.SetDestination(newPos);
            //    timer = 0;
            //}
            //isWandering = true;

        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        //Debug.Log(direction);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPosition, roamRadius);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Boundary" || collision.gameObject.tag == "ObjectCollide")
        {
            //Debug.Log("True");

            agent.isStopped = true;
            _animator.SetBool("run", false);
            agent.velocity = Vector3.zero;
            agent.ResetPath();
            agent.isStopped = false;

        }

    }

    void FreeRoam()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += startPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
            Vector3 finalPosition = hit.position;
            agent.destination = finalPosition;
            _animator.SetBool("run", true);
            timer = 0;
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    _animator.SetBool("run", false);
                }
            }
        }
    }

}