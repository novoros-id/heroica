using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_button : MonoBehaviour
{

    public GameObject UI;
    public GameObject shop;
    public Text coins;
    public GameObject weaponButtonsBuyBaton;
    public GameObject weaponButtonsBuyAxe;
    public GameObject weaponButtonsBuyScythe;
    public GameObject weaponButtonsSellBaton;
    public GameObject weaponButtonsSellAxe;
    public GameObject weaponButtonsSellScythe;
    public GameObject Current_player;
    public GameObject WeaponButtonController;
    public Main mScript;
    // Update is called once per frame
    void Start()
    {
        shop.SetActive(false);
        //Debug.Log("we");

        //GameObject cam = GameObject.Find("Directional Light");
        //Main mScript = cam.GetComponent<Main>();

    }

    public void ShopOn()
    {
        UI.SetActive(false);
        shop.SetActive(true);
        CheckButtons();
        ChangeDesText();
    }
    public void ShopOff()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        WeaponChanger wc_script = WeaponButtonController.GetComponent<WeaponChanger>();

        GameObject Curent_player = mScript.return_curent_player(); // нашли текущего игркока
        Player_ pl_script = Curent_player.GetComponent<Player_>();

        pl_script.set_CurWeapon();
        mScript.WeaponIcon(pl_script);
        wc_script.CheckWeapons();

        UI.SetActive(true);
        shop.SetActive(false);
    }
    public void SellBaton()
    {

        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        Current_player = mScript.return_curent_player(); // нашли текущего игркока  

        Current_player.GetComponent<Player_>().gold += 2;

        Player_ pl_script = Current_player.GetComponent<Player_>();
        pl_script.save_gold();

        Current_player.GetComponent<Player_>().baton = 0;
        CheckButtons();
    }

    public void SellAxe()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        Current_player = mScript.return_curent_player(); // нашли текущего игркока

        Current_player.GetComponent<Player_>().gold += 2;
        Player_ pl_script = Current_player.GetComponent<Player_>();
        pl_script.save_gold();
        Current_player.GetComponent<Player_>().axe = 0;
        CheckButtons();
    }
    public void SellScythe()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        Current_player = mScript.return_curent_player(); // нашли текущего игркока

        Current_player.GetComponent<Player_>().gold += 2;
        Player_ pl_script = Current_player.GetComponent<Player_>();
        pl_script.save_gold();
        Current_player.GetComponent<Player_>().scythe = 0;
        CheckButtons();
    }
    public void BuyBaton()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        Current_player = mScript.return_curent_player(); // нашли текущего игркока

        Current_player.GetComponent<Player_>().gold -= 3;
        Player_ pl_script = Current_player.GetComponent<Player_>();
        pl_script.save_gold();
        Current_player.GetComponent<Player_>().baton = 1;
        CheckButtons();
    }
    public void BuyAxe()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        Current_player = mScript.return_curent_player(); // нашли текущего игркока

        Current_player.GetComponent<Player_>().gold -= 3;
        Player_ pl_script = Current_player.GetComponent<Player_>();
        pl_script.save_gold();
        Current_player.GetComponent<Player_>().axe = 1;
        CheckButtons();
    }
    public void BuyScythe()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        Current_player = mScript.return_curent_player(); // нашли текущего игркока

        Current_player.GetComponent<Player_>().gold -= 3;
        Player_ pl_script = Current_player.GetComponent<Player_>();
        pl_script.save_gold();
        Current_player.GetComponent<Player_>().scythe = 1;
        CheckButtons();
    }
    public void CheckButtons()
    {

        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        Current_player = mScript.return_curent_player(); // нашли текущего игркока

        if (Current_player.GetComponent<Player_>().gold < 3)
        {
            weaponButtonsBuyBaton.SetActive(false);
            weaponButtonsBuyAxe.SetActive(false);
            weaponButtonsBuyScythe.SetActive(false);
        }
        else
        {
            weaponButtonsBuyBaton.SetActive(true);
            weaponButtonsBuyAxe.SetActive(true);
            weaponButtonsBuyScythe.SetActive(true);
        }
        if (Current_player.GetComponent<Player_>().baton == 1)
        {
            weaponButtonsSellBaton.SetActive(true);
            weaponButtonsBuyBaton.SetActive(false);
        }
        else
        {
            weaponButtonsSellBaton.SetActive(false);
        }
        if (Current_player.GetComponent<Player_>().axe == 1)
        {
            weaponButtonsSellAxe.SetActive(true);
            weaponButtonsBuyAxe.SetActive(false);
        }
        else
        {
            weaponButtonsSellAxe.SetActive(false);
        }
        if (Current_player.GetComponent<Player_>().scythe == 1)
        {
            weaponButtonsSellScythe.SetActive(true);
            weaponButtonsBuyScythe.SetActive(false);
        }
        else
        {
            weaponButtonsSellScythe.SetActive(false);
        }

        coins.text = Current_player.GetComponent<Player_>().gold.ToString();
    }
    public void ChangeDesText()
    {
        Text BatonText = GameObject.Find("DescriptionWeapon1").GetComponent<Text>();
        Text AxeText = GameObject.Find("DescriptionWeapon2").GetComponent<Text>();
        Text ScytheText = GameObject.Find("DescriptionWeapon3").GetComponent<Text>();
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        if(mScript.lang == "ru")
        {
            BatonText.text = "Победа над монстром на расстоянии до 3 полей, даже при повороте за угол";
            AxeText.text = "Победа над ВСЕМИ соседними монстрами";
            ScytheText.text = "Восстанавливает 2 кубика здоровья";
        }
        else if (mScript.lang == "en")
        {
            BatonText.text = "Defeat the monster at a distance of up to 3 fields, even when turning a corner";
            AxeText.text = "Defeat ALL the neighboring monsters";
            ScytheText.text = "Restores 2 health cubes";
        }
    }
}
