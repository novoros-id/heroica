using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeSprite : MonoBehaviour
{
    public ClickOnCube cb;

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
        if(cb.cube_step == 1)
        {
            this.GetComponent<Image>().sprite= one;
        }
        if (cb.cube_step == 2)
        {
            this.GetComponent<Image>().sprite = two;
        }
        if (cb.cube_step == 3)
        {
            this.GetComponent<Image>().sprite = three;
        }
        if (cb.cube_step == 4)
        {
            this.GetComponent<Image>().sprite = four;
        }
    }
}
