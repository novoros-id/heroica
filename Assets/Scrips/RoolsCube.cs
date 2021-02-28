using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoolsCube : MonoBehaviour
{

    public GameObject text4;
    public GameObject text3;
    public GameObject text2;
    public GameObject text1;
    public Sprite cube4;
    public Sprite cube3;
    public Sprite cube2;
    public Sprite cube1;
    public int step;


    // Start is called before the first frame update
    void Start()
    {
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (step == 1)
        {
            text1.SetActive(true);
            this.GetComponent<Image>().sprite = cube1;
            text2.SetActive(false);
            text3.SetActive(false);
            text4.SetActive(false);
        }
        if (step == 2)
        {
            text1.SetActive(false);
            this.GetComponent<Image>().sprite = cube2;
            text2.SetActive(true);
            text3.SetActive(false);
            text4.SetActive(false);
        }
        if (step == 3)
        {
            text1.SetActive(false);
            this.GetComponent<Image>().sprite = cube3;
            text2.SetActive(false);
            text3.SetActive(true);
            text4.SetActive(false);
        }
        if (step == 4)
        {
            text1.SetActive(false);
            this.GetComponent<Image>().sprite = cube4;
            text2.SetActive(false);
            text3.SetActive(false);
            text4.SetActive(true);
        }
    }

    public void Click()
    {
        if(step < 4)
        {
            step++;
        }
        else
        {
            step = 1;
        }
    }
}
