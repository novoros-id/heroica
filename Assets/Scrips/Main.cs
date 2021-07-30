using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public float pl_fl_x = 1.2f;
    public float pl_fl_z = 0.8f;
    private int current_move = 1;
    //public GameObject[] player;
    public GameObject[] pr_hod;
    public GameObject selected1;
    public GameObject shop_button;
    public GameObject Weapon_Button;

    public GameObject cam_focus;
    public bool Pc;
    public string lang;
    public Text TextComment;

    public WeaponChanger NextButton;
   
    public void Start()
    {
        
        move_priznak_step();

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Pc = true;
        }
        else
        {
            Pc = false;
        }

        if (PlayerPrefs.HasKey("lang"))
        {
            lang = PlayerPrefs.GetString("lang");
        }
        else
        {
            lang = "en";
        }
        //shop_button.SetActive(false);
    }


    /// <summary>
    /// управление ходом
    /// </summary>
   
    public void SetLang()
    {
        if(lang == "ru")
        {
            lang = "en";
            PlayerPrefs.SetString("lang", lang);
            PlayerPrefs.Save();
        }
        else if (lang == "en")
        {
            lang = "ru";
            PlayerPrefs.SetString("lang", lang);
            PlayerPrefs.Save();
        }
    }

    public int get_current_move()
    {
        return current_move;
    }

    public void set_current_move(string text_add = "")
    {
        // сдвинем ход 

        if (current_move == 4)
            current_move = 1;
        else
            current_move += 1;

        // передвинем знак хода
        move_priznak_step(text_add);

    }

    public void move_priznak_step(string text_add = "")
    {
       // var myText = GameObject.Find("Text_").GetComponent<Text>();

        if (text_add != "")
        {
            add_text(text_add);
        }

        // очистим значок хода

        pr_hod = GameObject.FindGameObjectsWithTag("pr_hod");
        

        for (int b = 0; b < pr_hod.Length; b++)
        {
            Destroy(pr_hod[b]);
        }



        GameObject player_ = return_curent_player();

        Player_ pl_script = player_.GetComponent<Player_>();

    
        Instantiate(selected1, new Vector3(player_.transform.position.x, 1.6f, player_.transform.position.z), Quaternion.identity);
        WeaponIcon(pl_script);
        ChangeText(pl_script, player_);
   
    }

    public void add_text(string a_text)
    {
        //var myText = GameObject.Find("Text_").GetComponent<Text>();
        TextComment.text = a_text + "\n" + "\n" + TextComment.text;
    }

    public GameObject return_tag_item_on_position(Vector3 player_position)
    {
        //string tag_item = "";
        Vector3 CurFloorPos;
        Collider[] colliders;

        CurFloorPos = new Vector3(player_position.x, player_position.y, player_position.z);

        //Debug.Log("Текущая позиция платформы" + CurFloorPos);

        if ((colliders = Physics.OverlapSphere(CurFloorPos, 0.5f)).Length > 0)
        {

            foreach (var collider in colliders)
            {
                // обрабатываем только некоторые тэги

                if (collider.tag == "Enemy_1" ||
                    collider.tag == "Enemy_2" ||
                    collider.tag == "Enemy_boss" ||
                    collider.tag == "Key" ||
                    collider.tag == "Door" ||
                    collider.tag == "item_blood" ||
                    collider.tag == "item_luck" ||
                    collider.tag == "item_speed" ||
                    collider.tag == "item_power" ||
                    collider.tag == "item_gold" ||
                    collider.tag == "item_axe" ||
                    collider.tag == "item_baton" ||
                    collider.tag == "item_scythe" ||
                    collider.tag == "item_bow" ||
                    collider.tag == "item_dagger" ||
                    collider.tag == "item_sword")

                {

                    return collider.gameObject;


                }
            }


        }


        return null;

    }


    public GameObject return_curent_player()

    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        //GameObject cam = GameObject.Find("Directional Light");
        //Main mScript = cam.GetComponent<Main>();
        int current_move = get_current_move();

        for (int i = 0; i < player.Length; i++)
        {

            Player_ pl_script = player[i].GetComponent<Player_>();

            if (pl_script.step_move == current_move)
            {

                return player[i];

            }

        }

        return null;
    }

    public void WeaponIcon(Player_ pl_script)
    {
        
        
        if (pl_script.CurWeapon == "baton")
        {
            Weapon_Button.SetActive(true);
            Weapon_Button.GetComponent<Image>().sprite = Weapon_Button.GetComponent<WeaponScript>().Weapon1;
            Weapon_Button.GetComponent<RectTransform>().sizeDelta = new Vector2(49.2f, 109.8f);
            NextButton.CheckWeapons();
        }
        if (pl_script.CurWeapon == "axe")
        {
            Weapon_Button.SetActive(true);
            Weapon_Button.GetComponent<Image>().sprite = Weapon_Button.GetComponent<WeaponScript>().Weapon2;
            Weapon_Button.GetComponent<RectTransform>().sizeDelta = new Vector2(77.4f, 127.2f);
            NextButton.CheckWeapons();
        }
        if (pl_script.CurWeapon == "scythe")
        {
            Weapon_Button.SetActive(true);
            Weapon_Button.GetComponent<Image>().sprite = Weapon_Button.GetComponent<WeaponScript>().Weapon3;
            Weapon_Button.GetComponent<RectTransform>().sizeDelta = new Vector2(43, 103.8f);
            NextButton.CheckWeapons();
        }
        if (pl_script.CurWeapon == "")
        {
            Weapon_Button.SetActive(false);
        }
        if (pl_script.comp == false)
        {
            shop_button.SetActive(true);
        }
        else
        {
            shop_button.SetActive(false);
        }
    }

    public void ChangeText(Player_ pl_script, GameObject curPlayer)
    {
        if (pl_script.get_battle_mode() == true)
        {

            if (lang == "ru")
            {
                add_text("Режим боя " + curPlayer.name + " нажмите на кубик и узнаете исход боя");
            }
            else if (lang == "en")
            {
                add_text("Battle Mode " + curPlayer.name + " click on the cube and find out the outcome of the battle");
            }


        }
        else if (pl_script.recovery_mode == true)
        {
            if (lang == "ru")
            {
                add_text("Режим восстановления здоровья " + curPlayer.name + " нажмите на кубик и будет добавлено столько здоровья, сколько выпало очков");
            }
            else if (lang == "en")
            {
                add_text("Health Recovery Mode " + curPlayer.name + " click on the cube and you will be added as much health as you get points");
            }
        }
        else
        {
            if (pl_script.comp == true)
            {
                if (lang == "ru")
                {
                    add_text("Текущий ход " + curPlayer.name + " нажмите на кубик, затем компьютер сам сделает ход");
                }
                else if (lang == "en")
                {
                    add_text("Current move " + curPlayer.name + " click on the cube, then the computer will make its own move");
                }
            }
            else
            {
                if (lang == "ru")
                {
                    add_text("Текущий ход " + curPlayer.name + " нажмите на кубик, затем нажмите на вращающееся поле");
                }
                else if (lang == "en")
                {
                    add_text("Current move  " + curPlayer.name + " click on the cube, then click on the rotating field");
                }

            }
        }
    }
}
