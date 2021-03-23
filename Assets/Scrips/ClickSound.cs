using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    
    public ClickOnCube CoC;
    static AudioSource audiosrc;
    public AudioClip click;

    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if(CoC.Curent_player.GetComponent<Player_>().battle_mode == false)
        {
            audiosrc.PlayOneShot(click);
        }
    }

}
