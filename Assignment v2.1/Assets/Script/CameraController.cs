using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private const float minAngle = 0.0f;
    private const float maxAngle = 40.0f;

    public Transform target;

    private float distance = -5.0f;
    private float currentX = 50.0f;
    private float currentY = 20.0f;
    private float smoothSpeed = 0.3f;

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, minAngle, maxAngle);
        transform.LookAt(target.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //{
        //    distance = -5.0f;
        //}
        //else
        //{
        //    if(distance > -10.0f)
        //    {
        //        distance -= 1.0f;
        //    }
        //}

        Vector3 desiredPosition = target.position + (Quaternion.Euler(currentY, currentX, 0) * new Vector3(0, 0, distance));
        
        // Smooth camera rotation
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}
