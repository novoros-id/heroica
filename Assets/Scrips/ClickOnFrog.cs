using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnFrog : MonoBehaviour
{
    static AudioSource audiosrc;
    public AudioClip Click;
    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        audiosrc.PlayOneShot(Click);
    }

    void Update()
    {
        
    }
}
