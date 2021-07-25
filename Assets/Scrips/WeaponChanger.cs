using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public int Weapons;
    public GameObject this_;

    void Start()
    {
       
    }
    void Update()
    {
        if(Weapons < 2)
        {
            this_.SetActive(false);
        }
        if (Weapons >= 2)
        {
            this_.SetActive(true);
        }
    }
    public void ChangeWeapon()
    {
        GameObject cam = GameObject.Find("Directional Light");
           Main mScript = cam.GetComponent<Main>();
            int current_move = mScript.get_current_move();
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < player.Length; i++)
            {

              Player_ pl_script = player[i].GetComponent<Player_>();

              if (pl_script.step_move == current_move)
                {
                   pl_script.set_CurWeapon();
                   mScript.WeaponIcon(pl_script);
                   break;
                }

           }
    }
    public void CheckWeapons()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        int current_move = mScript.get_current_move();
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < player.Length; i++)
        {

            Player_ pl_script = player[i].GetComponent<Player_>();

            if (pl_script.step_move == current_move)
            {
                Weapons = pl_script.axe + pl_script.baton + pl_script.scythe + pl_script.sword + pl_script.dagger + pl_script.bow;
                break;
            }

        }
        
    }
}
