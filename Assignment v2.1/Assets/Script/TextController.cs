using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour {

    public GameObject MessagePanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenMessagePanel()
    {
        MessagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }
}
