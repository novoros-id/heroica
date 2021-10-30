using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesAvaibility : MonoBehaviour
{
    public string HeroAvaibility = "1";
    public GameObject cross;

    
    public void ChangeAvaibility()
    {
        if(HeroAvaibility == "1")
        {
            HeroAvaibility = "0";
            cross.SetActive(false);
        }
        else
        {
            HeroAvaibility = "1";
            cross.SetActive(true);
        }
    }
}
