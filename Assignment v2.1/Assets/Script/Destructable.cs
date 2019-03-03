using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject crackedCrate;
    //private bool canDestroy = false;

    //public TextController destroyPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(canDestroy && Input.GetMouseButtonDown(0))
        //{
        //    DestroyCrate();
        //}
    }

    public void DestroyCrate()
    {
        Instantiate(crackedCrate, transform.position, transform.rotation);
        Destroy(gameObject);
        //destroyPanel.CloseMessagePanel();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        canDestroy = true;
    //        destroyPanel.OpenMessagePanel();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        canDestroy = false;
    //        destroyPanel.CloseMessagePanel();
    //    }
    //}
}
