using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    public float roamRadius;
    Vector3 startPosition;

    public AudioSource foxSound;
    public float playTime;
    public float pauseTime;
    public Animator anim;
    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Awake()
    {
        startPosition = transform.position;
    }

    void Start()
    {
        foxSound = GetComponent<AudioSource>();
        StartCoroutine("PlayPauseCoroutine");

    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;

        //if (timer >= wanderTimer)
        //{
        //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        //    agent.SetDestination(newPos);
        //    timer = 0;
        //}
        FreeRoam();
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPosition, roamRadius);
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
            timer = 0;
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", true);
                }
            }
        }
        //isWandering = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Boundary" || collision.gameObject.name == "ObjectCollide")
        {
            //Debug.Log("True");

            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.ResetPath();
            agent.isStopped = false;

        }

    }

    IEnumerator PlayPauseCoroutine()
    {
        while (true)
        {
            foxSound.Play();
            yield return new WaitForSeconds(playTime);
            foxSound.Pause();
            yield return new WaitForSeconds(pauseTime);
        }
    }
}
