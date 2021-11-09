using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    
    public ClickOnCube CoC;
    private AudioSource audiosrc;
    public AudioClip click;

    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        GameObject CubeButton_ = GameObject.Find("CubeButton");
        cube_button_script mCube_ = CubeButton_.GetComponent<cube_button_script>();
        //if (CoC.Curent_player.GetComponent<Player_>().battle_mode == false)
        //{
        if (mCube_.cube_is_available == true)
        {
            audiosrc.PlayOneShot(click);
        }
        
        //}
    }

}
