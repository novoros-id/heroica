using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMod : MonoBehaviour
{

    public GameObject Hero;
    Text CompPl;

    // Start is called before the first frame update
    void Start()
    {
        CompPl = GetComponent<Text>();
        change();
    }

    public void change()
    {
        Player_ pl_script = Hero.GetComponent<Player_>();
        //Debug.Log("true");
        if (pl_script.comp == false)
        {
            CompPl.text = "Computer";
            pl_script.comp = true;
        }
        else 
        {
            CompPl.text = "Player";
            pl_script.comp = false;
        }
    }
}
