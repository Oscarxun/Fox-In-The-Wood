using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    

    public float GrassAlpha;
    public bool enter = true;
    public GameObject player;
    // Start is called before the first frame update
    MeshRenderer thisRend;
    PlayerVisible playerscript;


    void Start()
    {
        thisRend = GetComponent<MeshRenderer>();
        player = GameObject.Find("Player");
        playerscript = player.GetComponent<PlayerVisible>();

        //thisRend.material.color = new Color(thisRend.material.color.r, thisRend.material.color.g, thisRend.material.color.b, 0.2f);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (enter && other.gameObject.name == "Foxmodel")
        {
            //Debug.Log("Entered");
            playerscript.visibility = false;
            
            thisRend.material.color = new Color(thisRend.material.color.r, thisRend.material.color.g, thisRend.material.color.b, 0.2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        playerscript.visibility = true;
        thisRend.material.color = new Color(thisRend.material.color.r, thisRend.material.color.g, thisRend.material.color.b, 1.0f);
    }

}
