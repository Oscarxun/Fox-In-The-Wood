using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour
{
    public Rigidbody rb;
    public float ShootForce = 2000;
    public bool dead;
    GameObject player;
    PlayerControl playerScript;

    TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerControl>();
        dead = false;
        //trail.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        //SpinArrow();
        //transform.forward = Vector3.Slerp(transform.forward, rb.velocity.normalized, Time.deltaTime);
        //transform.LookAt(transform.position - rb.velocity);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.5f);
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        ApplyForce();
    }

    public void ApplyForce()
    {
        //rb.velocity = enemy.transform.forward * 10;
        rb.AddRelativeForce(Vector3.forward * ShootForce);
        //Debug.Log(rb.velocity);
    }

    public void SpinArrow()
    {
        float yVelcocity = rb.velocity.y;
        float zVelcocity = rb.velocity.z;
        float xVelcocity = rb.velocity.x;
        float CombinedVelocity = Mathf.Sqrt(xVelcocity * xVelcocity + zVelcocity * zVelcocity);
        float fallAngle = Mathf.Atan2(yVelcocity, CombinedVelocity) * 1 / Mathf.PI;

        transform.eulerAngles = new Vector3(fallAngle, transform.eulerAngles.y, transform.eulerAngles.x);
    }

    void OnTriggerEnter(Collider other)
    {
        //transform.parent = other.transform;
        if(other.gameObject.tag == "Terrain")
        {
            dead = true;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
            //trail.enabled = false;
        }

        if (other.gameObject.tag == "Player Model" && !dead)
        {
            playerScript.isDamaged = true;
            //SceneManager.LoadScene("SampleScene");
        }
    }
}
