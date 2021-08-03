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
        check_item();
        //Destroy(this.gameObject);
    }

 

    void check_item()
    {
        GameObject[] Floors;
        GameObject[] Enemys1;
        GameObject[] Enemys2;
        GameObject[] doors;
        GameObject[] keys;
        
        Floors = GameObject.FindGameObjectsWithTag("Floor");
        Enemys1 = GameObject.FindGameObjectsWithTag("Enemy_1");
        Enemys2 = GameObject.FindGameObjectsWithTag("Enemy_2");
        doors = GameObject.FindGameObjectsWithTag("Door");
        keys = GameObject.FindGameObjectsWithTag("Key");

        for (int b = 0; b < Floors.Length; b++)
        {
            Debug.Log(Floors[b].name + " " + Floors[b].transform.position.x + " " + Floors[b].transform.position.z);
        }
        for (int b = 0; b < Enemys2.Length; b++)
        {
            Debug.Log(Enemys2[b].name + " " + Enemys2[b].transform.position.x + " " + Enemys2[b].transform.position.z);
        }
        for (int b = 0; b < Enemys1.Length; b++)
        {
            Debug.Log(Enemys1[b].name + " " + Enemys1[b].transform.position.x + " " + Enemys1[b].transform.position.z);
        }
        for (int b = 0; b < doors.Length; b++)
        {
            Debug.Log(doors[b].name + " " + doors[b].transform.position.x + " " + doors[b].transform.position.z);
        }
        for (int b = 0; b < keys.Length; b++)
        {
            Debug.Log(keys[b].name + " " + keys[b].transform.position.x + " " + keys[b].transform.position.z);
        }
    }
}
