using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiant : MonoBehaviour
{
    public GameObject selected;
    public GameObject[] Floors;
    public GameObject[] Blue;
    
    void Start() 
    {
        Floors = GameObject.FindGameObjectsWithTag("Floor");


        for (int i = 0;i<Floors.Length;i++) 
        {
            //Instantiate(selected, new Vector3(Floors[i].transform.position.x,1.9f,Floors[i].transform.position.z), Quaternion.identity);
            //Blue[i].SetActive(false);
        }

        

        for (int b = 0;b<Blue.Length;b++)
        {
            Blue[b].SetActive(false);
        }
        Blue = GameObject.FindGameObjectsWithTag("Blue");


    }

}
