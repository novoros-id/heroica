using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_Plat : MonoBehaviour
{
    public Vector3 curpos;
    
    void Start()
    {
        curpos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
        
    void OnMouseDown()
    {
        Debug.Log(curpos);
    }
}
