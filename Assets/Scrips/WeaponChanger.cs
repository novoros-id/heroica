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

        GameObject Curent_player = mScript.return_curent_player(); // нашли текущего игркока
        Player_ pl_script = Curent_player.GetComponent<Player_>();
 
        pl_script.set_CurWeapon();
        mScript.WeaponIcon(pl_script);

    }
    public void CheckWeapons()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        GameObject Curent_player = mScript.return_curent_player(); // нашли текущего игркока
        Player_ pl_script = Curent_player.GetComponent<Player_>();

        Weapons = pl_script.axe + pl_script.baton + pl_script.scythe + pl_script.sword + pl_script.dagger + pl_script.bow;
        
    }
}
