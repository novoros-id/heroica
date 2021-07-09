using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalCount : MonoBehaviour
{
    Text Count;
    public ClickOnCube cb;
    
    // Start is called before the first frame update
    void Start()
    {
        Count = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Count.text = cb.count_magic_crystall.ToString();
    }
}
