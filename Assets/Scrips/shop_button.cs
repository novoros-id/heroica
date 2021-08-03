using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_button : MonoBehaviour
{

    public GameObject UI;
    public GameObject shop;
    public Text coins;
    public GameObject weaponButtonsBuy;
    public GameObject weaponButtonsSellBaton;
    public GameObject weaponButtonsSellAxe;
    public GameObject weaponButtonsSellScythe;
    public GameObject Current_player;
    public GameObject WeaponButtonController;
    // Update is called once per frame
    void Start()
    {
        shop.SetActive(false);
        //Debug.Log("we");
        
    }
    void Update()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();


        Current_player = mScript.return_curent_player(); // нашли текущего игркока  

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
        Current_player.GetComponent<Player_>().gold += 2;
        Current_player.GetComponent<Player_>().baton = 0;
        CheckButtons();
    }
    public void SellAxe()
    {
        Current_player.GetComponent<Player_>().gold += 2;
        Current_player.GetComponent<Player_>().axe = 0;
        CheckButtons();
    }
    public void SellScythe()
    {
        Current_player.GetComponent<Player_>().gold += 2;
        Current_player.GetComponent<Player_>().scythe = 0;
        CheckButtons();
    }
    public void BuyBaton()
    {
        Current_player.GetComponent<Player_>().gold -= 3;
        Current_player.GetComponent<Player_>().baton = 1;
        CheckButtons();
    }
    public void BuyAxe()
    {
        Current_player.GetComponent<Player_>().gold -= 3;
        Current_player.GetComponent<Player_>().axe = 1;
        CheckButtons();
    }
    public void BuyScythe()
    {
        Current_player.GetComponent<Player_>().gold -= 3;
        Current_player.GetComponent<Player_>().scythe = 1;
        CheckButtons();
    }
    public void CheckButtons()
    {
        if (Current_player.GetComponent<Player_>().gold < 3)
        {
            weaponButtonsBuy.SetActive(false);
        }
        else
        {
            weaponButtonsBuy.SetActive(true);
        }
        if (Current_player.GetComponent<Player_>().baton == 1)
        {
            weaponButtonsSellBaton.SetActive(true);
        }
        else
        {
            weaponButtonsSellBaton.SetActive(false);
        }
        if (Current_player.GetComponent<Player_>().axe == 1)
        {
            weaponButtonsSellAxe.SetActive(true);
        }
        else
        {
            weaponButtonsSellAxe.SetActive(false);
        }
        if (Current_player.GetComponent<Player_>().scythe == 1)
        {
            weaponButtonsSellScythe.SetActive(true);
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
