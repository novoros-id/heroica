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
    public GameObject Chooze;
    public GameObject other;
    // Start is called before the first frame update
    void Start()
    {
        other.SetActive(false);
    }
    public void start()
    {
        Camera.go = 1;
        Camera.startTime = Time.time;
        Camera.journeyLength = Vector3.Distance(Camera.startMarker.position,Camera.endMarker.position);
    }

    public void ChoozeKnight()
    {
        Player_ pl_script = Knight.GetComponent<Player_>();
        Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        Player_ pl_script2 = Mage.GetComponent<Player_>();
        Player_ pl_script3 = Priest.GetComponent<Player_>();
        pl_script.comp = false;
        pl_script1.comp = true;
        pl_script2.comp = true;
        pl_script3.comp = true;
        start();
        Chooze.SetActive(false);
        other.SetActive(true);
    }
    public void ChoozeBarbarian()
    {
        Player_ pl_script = Knight.GetComponent<Player_>();
        Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        Player_ pl_script2 = Mage.GetComponent<Player_>();
        Player_ pl_script3 = Priest.GetComponent<Player_>();
        pl_script.comp = true;
        pl_script1.comp = false;
        pl_script2.comp = true;
        pl_script3.comp = true;
        start();
        Chooze.SetActive(false); 
        other.SetActive(true);
    }
    public void ChoozeMage()
    {
        Player_ pl_script = Knight.GetComponent<Player_>();
        Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        Player_ pl_script2 = Mage.GetComponent<Player_>();
        Player_ pl_script3 = Priest.GetComponent<Player_>();
        pl_script.comp = true;
        pl_script1.comp = true;
        pl_script2.comp = false;
        pl_script3.comp = true;
        start();
        Chooze.SetActive(false);
        other.SetActive(true);
    }
    public void ChoozePriest()
    {
        Player_ pl_script = Knight.GetComponent<Player_>();
        Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        Player_ pl_script2 = Mage.GetComponent<Player_>();
        Player_ pl_script3 = Priest.GetComponent<Player_>();
        pl_script.comp = true;
        pl_script1.comp = true;
        pl_script2.comp = true;
        pl_script3.comp = false;
        start();
        Chooze.SetActive(false);
        other.SetActive(true);
    }

}
