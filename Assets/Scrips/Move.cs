using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public OnClick Cs;
    public GameObject Player;
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void OnMouseDown()
    {      
        if (Cs.clicked == true)
        {
            //Debug.Log("as");
            Player.transform.position = new Vector3(transform.position.x+0.7f,1.3f, transform.position.z-1.199f);
            //Cs.clicked = false;
        }
    }
}
