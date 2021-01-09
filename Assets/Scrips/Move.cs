using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public OnClick Cs;
    public GameObject Player;
    public GameObject[] Blue;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void OnMouseDown()
    {      

        Player.transform.position = new Vector3(transform.position.x,0.55f, transform.position.z);
        Blue = GameObject.FindGameObjectsWithTag("Blue");


        for (int b = 0; b < Blue.Length; b++)
        {
                Destroy(Blue[b]);
        }
        

    }
}
