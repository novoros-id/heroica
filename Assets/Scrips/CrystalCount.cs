using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalCount : MonoBehaviour
{
    Text Count;
    
    // Start is called before the first frame update
    void Start()
    {
        Count = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject cb = GameObject.Find("Cube");
        ClickOnCube cbScript = cb.GetComponent<ClickOnCube>();
        Count.text = cbScript.count_magic_crystall.ToString();
    }
}
