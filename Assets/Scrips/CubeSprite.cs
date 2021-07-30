using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeSprite : MonoBehaviour
{
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject cb = GameObject.Find("Cube");
        ClickOnCube cbScript = cb.GetComponent<ClickOnCube>();

        if (cbScript.cube_step == 1)
        {
            this.GetComponent<Image>().sprite= one;
        }
        if (cbScript.cube_step == 2)
        {
            this.GetComponent<Image>().sprite = two;
        }
        if (cbScript.cube_step == 3)
        {
            this.GetComponent<Image>().sprite = three;
        }
        if (cbScript.cube_step == 4)
        {
            this.GetComponent<Image>().sprite = four;
        }
    }
}
