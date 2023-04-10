using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource clickSound;
    void Start()
    {
        clickSound = GameObject.Find("Click").GetComponent<AudioSource>();
    }

    public void click(){
        clickSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
