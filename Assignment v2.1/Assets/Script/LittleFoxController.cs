using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleFoxController : MonoBehaviour {

    public float moveSpeed;
    public float rotateSpeed;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    //Rigidbody rb;

	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody>();
        //rb.inertiaTensor = rb.inertiaTensor + new Vector3(0, 0, rb.inertiaTensor.z * 500);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isWandering)
            StartCoroutine(Wander());

        rotateSpeed = Random.Range(100, 200);

        if (isRotatingLeft)
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        if (isRotatingRight)
            transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed);
        if(isWalking)
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    IEnumerator Wander()
    {
        int rotateTime = Random.Range(1, 2);
        int rotateWait = Random.Range(1, 3);
        int rotateLorR = Random.Range(0, 3);
        int walkTime = Random.Range(1, 5);
        int walkWait = Random.Range(1, 3);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if(rotateLorR == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotateTime);
            isRotatingLeft = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotateTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }
}
