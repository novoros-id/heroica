using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCrossedSwords : MonoBehaviour
{
    Animator anim;
    public string AnimationName;
    static AudioSource audiosrc;
    public AudioClip fight;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audiosrc = GetComponent<AudioSource>();
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
    public void Sound()
    {
        audiosrc.PlayOneShot(fight);
    }

    public void OnMouseDown()
    {
        Debug.Log("mouse doen");
    }

}
