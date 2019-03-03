using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeManager : MonoBehaviour
{
    public Slider Volume;
    public AudioSource Music;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Music.volume = Volume.value;
    }
}
