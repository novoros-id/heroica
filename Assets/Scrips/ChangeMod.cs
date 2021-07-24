using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMod : MonoBehaviour
{
    public Zoom Camera;
    public GameObject Knight;
    public GameObject Barbarian;
    public GameObject Mage;
    public GameObject Priest;
    public Text Knightext;
    public Text Barbariantext;
    public Text Magetext;
    public Text Priesttext;
    public GameObject Chooze;
    public GameObject other;
    public Main _main;
    public GameObject AndroidUI;
    public GameObject shop;
    // Start is called before the first frame update
    void Start()
    {
        other.SetActive(false);
        shop.SetActive(false);
    }
    public void start()
    {
        //AndroidUI.SetActive(false);
        Camera.go = 1;
        Camera.startTime = Time.time;
        Camera.journeyLength = Vector3.Distance(Camera.startMarker.position,Camera.endMarker.position);
        Chooze.SetActive(false);
        other.SetActive(true);
        if (_main.Pc == true)
        {
            //AndroidUI.SetActive(false);
        }
    }

    public void ChoozeKnight()
    {
        Player_ pl_script = Knight.GetComponent<Player_>();
        //Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        //Player_ pl_script2 = Mage.GetComponent<Player_>();
        //Player_ pl_script3 = Priest.GetComponent<Player_>();
        //pl_script.comp = false;
        //pl_script1.comp = true;
        //pl_script2.comp = true;
        //pl_script3.comp = true;
        //start();
        //Chooze.SetActive(false);
        //other.SetActive(true);
        if(pl_script.comp == true)
        {
            pl_script.comp = false;
            Knightext.text = "Player";
        }
        else
        {
            pl_script.comp = true;
            Knightext.text = "Computer";
        }
    }
    public void ChoozeBarbarian()
    {
        //Player_ pl_script = Knight.GetComponent<Player_>();
        Player_ pl_script = Barbarian.GetComponent<Player_>();
        //Player_ pl_script2 = Mage.GetComponent<Player_>();
        //Player_ pl_script3 = Priest.GetComponent<Player_>();
        //pl_script.comp = true;
        //pl_script1.comp = false;
        //pl_script2.comp = true;
        //pl_script3.comp = true;
        //start();
        //Chooze.SetActive(false); 
        //other.SetActive(true);
        if (pl_script.comp == true)
        {
            pl_script.comp = false;
            Barbariantext.text = "Player";
        }
        else
        {
            pl_script.comp = true;
            Barbariantext.text = "Computer";
        }
    }
    public void ChoozeMage()
    {
        //Player_ pl_script = Knight.GetComponent<Player_>();
        //Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        Player_ pl_script = Mage.GetComponent<Player_>();
        //Player_ pl_script3 = Priest.GetComponent<Player_>();
        //pl_script.comp = true;
        //pl_script1.comp = true;
        //pl_script2.comp = false;
        //pl_script3.comp = true;
        //start();
        //Chooze.SetActive(false);
        //other.SetActive(true);
        if (pl_script.comp == true)
        {
            pl_script.comp = false;
            Magetext.text = "Player";
        }
        else
        {
            pl_script.comp = true;
            Magetext.text = "Computer";
        }
    }
    public void ChoozePriest()
    {
        //Player_ pl_script = Knight.GetComponent<Player_>();
        //Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        //Player_ pl_script2 = Mage.GetComponent<Player_>();
        Player_ pl_script = Priest.GetComponent<Player_>();
        //pl_script.comp = true;
        //pl_script1.comp = true;
        //pl_script2.comp = true;
        //pl_script3.comp = false;
        //start();
        //Chooze.SetActive(false);
        //other.SetActive(true);
        if (pl_script.comp == true)
        {
            pl_script.comp = false;
            Priesttext.text = "Player";
        }
        else
        {
            pl_script.comp = true;
            Priesttext.text = "Computer";
        }
    }

}
