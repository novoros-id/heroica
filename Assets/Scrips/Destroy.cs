﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject Obj;
    // Start is called before the first frame update
    void Start()
    {
        Obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
