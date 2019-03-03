using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
    
    TextMesh t;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject text = new GameObject();
        t = text.AddComponent<TextMesh>();
        t.text = "new text set";
        t.fontSize = 300;
        t.transform.localEulerAngles += new Vector3(90, 0, 0);
        t.transform.localPosition += new Vector3(56f, 3f, 40f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
