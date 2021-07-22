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

    // Update is called once per frame
    void Start()
    {
        shop.SetActive(false);
        weaponButtonsSellAxe.SetActive(false);
        weaponButtonsSellBaton.SetActive(false);
        weaponButtonsSellScythe.SetActive(false);
    }
    void Update()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        int current_move = mScript.get_current_move();
        if (1 == current_move)
        {
            transform.localPosition = new Vector2(-1519, -197.1f);
            Current_player = GameObject.Find("Knight");
        }
        if (2 == current_move)
        {
            transform.localPosition = new Vector2(-1319, -197.1f);
            Current_player = GameObject.Find("Barbarian");
        }
        if (3 == current_move)
        {
            transform.localPosition = new Vector2(-1119, -197.1f);
            Current_player = GameObject.Find("Mage");
        }
        if (4 == current_move)
        {
            transform.localPosition = new Vector2(-919, -197.1f);
            Current_player = GameObject.Find("Priest");
        }
        
        

    }
    public void ShopOn()
    {
        UI.SetActive(false);
        shop.SetActive(true);
        CheckButtons();
    }
    public void ShopOff()
    {
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
}
