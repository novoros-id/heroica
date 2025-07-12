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
    public GameObject Information;

    public Text level_Description;
    public Text level_Description_ru;

    public GameObject KnightImage;
    public GameObject BarbarianImage;
    public GameObject MageImage;
    public GameObject PriestImage;

    public GameObject KnightIcon;
    public GameObject BarbarianIcon;
    public GameObject MageIcon;
    public GameObject PriestIcon;

    private string lang;

    private bool solo_level;
    private bool level_comleted;

    private bool Knight_aviable;
    private bool Barbarian_aviable;
    private bool Mage_aviable;
    private bool Priest_aviable;

    // Start is called before the first frame update

    private void Start()
    {
        //AndroidUI.SetActive(false);
        Camera = GameObject.Find("Camera").GetComponent<Zoom>();
        Knight = GameObject.Find("Knight");
        Barbarian = GameObject.Find("Barbarian");
        Mage = GameObject.Find("Mage");
        Priest = GameObject.Find("Priest");
        _main = GameObject.Find("Directional Light").GetComponent<Main>();

        solo_level = _main.solo_player;
        level_comleted = _main.level_complete;

        Knight_aviable = _main.Knight_aviable;
        Barbarian_aviable = _main.Barbarian_aviable;
        Mage_aviable = _main.Mage_aviable;
        Priest_aviable = _main.Priest_aviable;

        if (PlayerPrefs.HasKey("lang"))
        {
             lang = PlayerPrefs.GetString("lang");
        }
        else
        {
             lang = "en";
        }

        if (lang == "ru")
        {
            level_Description.enabled = false;
            level_Description_ru.enabled = true;
        }
        else
        {
            level_Description.enabled = true;
            level_Description_ru.enabled = false;
        }

        // покажем необходимые иконки
        if (_main.Knight_aviable == false) {

            KnightImage.SetActive(false);
            KnightIcon.SetActive(false);
        }

        if (_main.Barbarian_aviable == false)
        {

            BarbarianImage.SetActive(false);
            BarbarianIcon.SetActive(false);

        }

        if (_main.Mage_aviable == false)
        {

            MageImage.SetActive(false);
            MageIcon.SetActive(false);
        }

        if (_main.Priest_aviable == false)
        {

            PriestImage.SetActive(false);
            PriestIcon.SetActive(false);
        }
        // покажем необходимые иконки


        // выставим названия кнопок
        bool player_select = false;
        if (level_comleted == true || solo_level == false)
        {

            if (_main.Knight_aviable == true && player_select == false)
            {
                Knightext.text = "Player";
                Barbariantext.text = "Computer";
                Magetext.text = "Computer";
                Priesttext.text = "Computer";
                player_select = true;
            }

            if (_main.Barbarian_aviable == true && player_select == false)
            {
                Knightext.text = "Computer";
                Barbariantext.text = "Player";
                Magetext.text = "Computer";
                Priesttext.text = "Computer";
                player_select = true;
            }

            if (_main.Mage_aviable == true && player_select == false)
            {
                Knightext.text = "Computer";
                Barbariantext.text = "Computer";
                Magetext.text = "Player";
                Priesttext.text = "Computer";
                player_select = true;
            }

            if (_main.Priest_aviable == true && player_select == false)
            {
                Knightext.text = "Computer";
                Barbariantext.text = "Computer";
                Magetext.text = "Computer";
                Priesttext.text = "Player";
                player_select = true;
            }

        }
        else if (solo_level == true)
        {

            if (_main.Knight_aviable == true && player_select == false)
            {
                Knightext.text = "Player";
                Barbariantext.text = "--";
                Magetext.text = "--";
                Priesttext.text = "--";
                player_select = true;
            }

            if (_main.Barbarian_aviable == true && player_select == false)
            {
                Knightext.text = "--";
                Barbariantext.text = "Player";
                Magetext.text = "--";
                Priesttext.text = "--";
                player_select = true;
            }

            if (_main.Mage_aviable == true && player_select == false)
            {
                Knightext.text = "--";
                Barbariantext.text = "--";
                Magetext.text = "Player";
                Priesttext.text = "--";
                player_select = true;
            }

            if (_main.Priest_aviable == true && player_select == false)
            {
                Knightext.text = "--";
                Barbariantext.text = "--";
                Magetext.text = "--";
                Priesttext.text = "Player";
                player_select = true;
            }
        }

        other.SetActive(false);
        shop.SetActive(false);
        Chooze.SetActive(false);

    }
    void Awake()
    {

        
    }
    
    public void ChoozeOn()
    {
        Chooze.SetActive(true);
        Information.SetActive(false);
    }
    public void start()
    {
        // Остановить музыку на объекте Camera
        AudioSource cameraAudio = Camera.GetComponent<AudioSource>();
        if (cameraAudio != null && cameraAudio.isPlaying)
        {
            cameraAudio.Stop();
        }
        //
        _main.Knight_aviable = Knight_aviable;
        _main.Barbarian_aviable = Barbarian_aviable;
        _main.Mage_aviable = Mage_aviable;
        _main.Priest_aviable = Priest_aviable;

        _main.set_player_aviable();

        Camera.go = 1;
        Camera.startTime = Time.time;
        Camera.journeyLength = Vector3.Distance(Camera.startMarker.position,Camera.endMarker.position);
        Chooze.SetActive(false);
        other.SetActive(true);
        if (_main.Pc == true)
        {
            //AndroidUI.SetActive(false);
        }


        Main mScript = GameObject.Find("Directional Light").GetComponent<Main>();
        GameObject Curent_player = mScript.return_curent_player(); // нашли текущего игркока
        Player_ pl_script = Curent_player.GetComponent<Player_>();

        mScript.WeaponIcon(pl_script);

        //if (SceneManager.GetActiveScene().name != "Start")
        //{
        mScript.move_priznak_step();
        // }
    }

    public void ChoozeKnight()
    {
        Player_ pl_script = Knight.GetComponent<Player_>();
        Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        Player_ pl_script2 = Mage.GetComponent<Player_>();
        Player_ pl_script3 = Priest.GetComponent<Player_>();

        // если уровень не пройден и соло
        // если уровень не пройден и НЕ соло
        // имначе

        if (level_comleted == false && solo_level == true)
        {
            pl_script.comp = false;
            pl_script1.comp = true;
            pl_script2.comp = true;
            pl_script3.comp = true;


            Knightext.text = "Player";
            Barbariantext.text = "--";
            Magetext.text = "--";
            Priesttext.text = "--";

            Knight_aviable = true;
            Barbarian_aviable = false;
            Mage_aviable = false;
            Priest_aviable = false;
        }
        else if (level_comleted == false && solo_level == false)
        {
            pl_script.comp = false;
            pl_script1.comp = true;
            pl_script2.comp = true;
            pl_script3.comp = true;


            Knightext.text = "Player";
            Barbariantext.text = "Computer";
            Magetext.text = "Computer";
            Priesttext.text = "Computer";
        }
        else
        {
            if (pl_script.comp == true)
            {
                pl_script.comp = false;
                Knightext.text = "Player";
            }
            else if (pl_script.comp == false)
            {
                pl_script.comp = true;
                Knightext.text = "Computer";
            }
        }

    }
    public void ChoozeBarbarian()
    {
        Player_ pl_script = Knight.GetComponent<Player_>();
        Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        Player_ pl_script2 = Mage.GetComponent<Player_>();
        Player_ pl_script3 = Priest.GetComponent<Player_>();

        // если уровень не пройден и соло
        // если уровень не пройден и НЕ соло
        // имначе

        if (level_comleted == false && solo_level == true)
        {
            pl_script.comp = true;
            pl_script1.comp = false;
            pl_script2.comp = true;
            pl_script3.comp = true;


            Knightext.text = "--";
            Barbariantext.text = "Player";
            Magetext.text = "--";
            Priesttext.text = "--";

            Knight_aviable = false;
            Barbarian_aviable = true;
            Mage_aviable = false;
            Priest_aviable = false;
        }
        else if (level_comleted == false && solo_level == false)
        {
            pl_script.comp = true;
            pl_script1.comp = false;
            pl_script2.comp = true;
            pl_script3.comp = true;


            Knightext.text = "Computer";
            Barbariantext.text = "Player";
            Magetext.text = "Computer";
            Priesttext.text = "Computer";
        }
        else
        {
            if (pl_script1.comp == true)
            {
                pl_script1.comp = false;
                Barbariantext.text = "Player";
            }
            else if (pl_script1.comp == false)
            {
                pl_script1.comp = true;
                Barbariantext.text = "Computer";
            }
        }

       
    }
    public void ChoozeMage()
    {
        Player_ pl_script = Knight.GetComponent<Player_>();
        Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        Player_ pl_script2 = Mage.GetComponent<Player_>();
        Player_ pl_script3 = Priest.GetComponent<Player_>();

        // если уровень не пройден и соло
        // если уровень не пройден и НЕ соло
        // имначе

        if (level_comleted == false && solo_level == true)
        {
            pl_script.comp = true;
            pl_script1.comp = true;
            pl_script2.comp = false;
            pl_script3.comp = true;


            Knightext.text = "--";
            Barbariantext.text = "--";
            Magetext.text = "Player";
            Priesttext.text = "--";

            Knight_aviable = false;
            Barbarian_aviable = false;
            Mage_aviable = true;
            Priest_aviable = false;
        }
        else if (level_comleted == false && solo_level == false)
        {
            pl_script.comp = true;
            pl_script1.comp = true;
            pl_script2.comp = false;
            pl_script3.comp = true;


            Knightext.text = "Computer";
            Barbariantext.text = "Computer";
            Magetext.text = "Player";
            Priesttext.text = "Computer";

        }
        else
        {
            if (pl_script2.comp == true)
            {
                pl_script2.comp = false;
                Magetext.text = "Player";
            }
            else if (pl_script2.comp == false)
            {
                pl_script2.comp = true;
                Magetext.text = "Computer";
            }
        }

       
    }
    public void ChoozePriest()
    {
        Player_ pl_script = Knight.GetComponent<Player_>();
        Player_ pl_script1 = Barbarian.GetComponent<Player_>();
        Player_ pl_script2 = Mage.GetComponent<Player_>();
        Player_ pl_script3 = Priest.GetComponent<Player_>();

        // если уровень не пройден и соло
        // если уровень не пройден и НЕ соло
        // имначе

        if (level_comleted == false && solo_level == true)
        {
            pl_script.comp = true;
            pl_script1.comp = true;
            pl_script2.comp = true;
            pl_script3.comp = false;


            Knightext.text = "--";
            Barbariantext.text = "--";
            Magetext.text = "--";
            Priesttext.text = "Player";

            Knight_aviable = false;
            Barbarian_aviable = false;
            Mage_aviable = false;
            Priest_aviable = true;
        }
        else if (level_comleted == false && solo_level == false)
        {
            pl_script.comp = true;
            pl_script1.comp = true;
            pl_script2.comp = true;
            pl_script3.comp = false;


            Knightext.text = "Computer";
            Barbariantext.text = "Computer";
            Magetext.text = "Computer";
            Priesttext.text = "Player";

        }
        else
        {
            if (pl_script3.comp == true)
            {
                pl_script3.comp = false;
                Priesttext.text = "Player";
            }
            else if (pl_script3.comp == false)
            {
                pl_script3.comp = true;
                Priesttext.text = "Computer";
            }
        }


       
    }

}
