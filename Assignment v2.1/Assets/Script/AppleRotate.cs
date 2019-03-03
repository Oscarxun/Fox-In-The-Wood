using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleRotate : MonoBehaviour
{
    public float rotateSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 6.0f * rotateSpeed * Time.deltaTime, 0);
    }
}
