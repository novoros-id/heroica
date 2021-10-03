using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
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

    public int max_player;

    // настройки игры
    public bool Knight_aviable;
    public bool Barbarian_aviable;
    public bool Mage_aviable;
    public bool Priest_aviable;
    
    public bool Shop_aviable;
    public bool Crystal_aviable;

    public bool solo_player;
    public bool survival;
    public bool challenge_level;

    public bool level_complete;

    public WeaponChanger NextButton;

    private string[] Waiting_cube_before_the_move;
    private string[] Waiting_cube_before_the_fight;
    private string[] Cube_fight_victory;
    private string[] Cube_fight_loss;

    public Dictionary<string, string> chapter_1_levels_name = new Dictionary<string, string>();

    private void Awake()
    {

        if (SceneManager.GetActiveScene().name != "Start")
        {
            TextComment = GameObject.Find("Text_").GetComponent<Text>();
            shop_button = GameObject.Find("shop_button");
            Weapon_Button = GameObject.Find("Weapon_Button");
            NextButton = GameObject.Find("WeaponChangerController").GetComponent<WeaponChanger>();
        }

        chapter_1_levels_name.Add("Test", "chapter_1_level_1");
        chapter_1_levels_name.Add("Level2", "chapter_1_level_2");
        chapter_1_levels_name.Add("Level3", "chapter_1_level_3");
        chapter_1_levels_name.Add("Level4", "chapter_1_level_4");
        chapter_1_levels_name.Add("Level5_", "chapter_1_level_5");
        chapter_1_levels_name.Add("Level6", "chapter_1_level_6");
        chapter_1_levels_name.Add("Level7", "chapter_1_level_7");
        chapter_1_levels_name.Add("Level8", "chapter_1_level_8");
        chapter_1_levels_name.Add("Level9", "chapter_1_level_9");
        chapter_1_levels_name.Add("Level10", "chapter_1_level_10");
        chapter_1_levels_name.Add("Level11", "chapter_1_level_11");
        chapter_1_levels_name.Add("Level12", "chapter_1_level_12");
        chapter_1_levels_name.Add("Level13", "chapter_1_level_13");
        chapter_1_levels_name.Add("Level14", "chapter_1_level_14");
        chapter_1_levels_name.Add("Level15", "chapter_1_level_15");

        string active_scene_name = SceneManager.GetActiveScene().name;



        if (chapter_1_levels_name.ContainsKey(active_scene_name))
        { 
            if (PlayerPrefs.HasKey(chapter_1_levels_name[active_scene_name]))
            {
                int level_save = PlayerPrefs.GetInt(chapter_1_levels_name[active_scene_name]);

                if (level_save == 1)
                {
                    level_complete = true;
                }
                else
                {
                    level_complete = false;
                }
            }
            else
            {
                level_complete = false;
            }
        }

        Waiting_cube_before_the_move = new string[] {
            "Давай-давай кидай как следует | Come on, come on, throw it properly",
            "Я жду! Хоть бы выпал кристалл! | I'm waiting! If only there was a crystal!",
            "Кидай кубик хорошо, не как в прошлый раз | Oh! If only there was a crystal!Roll the dice well, not like last time",
            "Кидай кубик, хочу чтобы выпал кристалл!!! Кристалл!!! Кристалл!!! | Throw a cube, I want a crystal!!! Crystal!!! Crystal!!!",
            "Я уже заждался, давай кидай уже | I've already been waiting, come on, throw it already",
            "Кидай, давай. Проверим, удачный ли день | Throw it, come on. Let's check if it's a good day",
            "Классная сегодня игра. Жду удачный бросок!| Cool game today. I'm waiting for a successful throw!",
            "Как погода? Ладно потом, кидай кубик | How is the weather? Okay then, roll the dice",
            "Хотите расскажу анекдот? а мой ход? ну потом … | Would you like me to tell you a joke? and my move? well then …",
            "Нормальная музыка, мне норм | Normal music, I'm normal",
            "Кручу верчу, выиграть хочу  | I twist and turn, I want to win",
            "Как настроение? Ой мой ход! | How are you feeling? Oh, my move!",
            "Засиделся я уже )| I've been sitting too long already )",
            "Играю не для выигрыша, а для удовольствия| I play not for winning, but for pleasure",
            "парам-пам-пам | param-pam-pam",
            "Развели тут флуд ) | We've got a flood here )",
            "Давайте общаться по существу| Let's communicate essentially",
            "Кто проиграет, тот идет за мороженым ))) | Who loses, he goes for ice cream )))",
            "Хорошо сидим, играем, весело. Ждем пока ты бросаешь кубик! | We sit well, play, have fun. We are waiting for you to roll the dice!",
            "Играем в быстром темпе, не тормозим | We play at a fast pace, do not slow down",
            "Будет здорово, когда можно будет играть в онлайне | It will be great when we can play online",
            "Вот бы добавили редактор уровней | I would like to add a level editor",
            "Что так долго?Пойду заварю себе чай | What's taking so long?I'm going to make myself some tea",
            "В такую погоду, только в такие игры и играть | In this weather, only such games and play",
            "Не паримся, не получится начнём ещё раз | Don't sweat it, it won't work let's start again",
            "Я знаю как зовут разработчиков, Алексей и Владимир | I know the names of the developers, Alexey and Vladimir" };

        Waiting_cube_before_the_fight = new string[] {
            "Я хочу победить !! Ура!! | I want to win !! Hurray!!",
            "Сейчас ты попробуешь моего меча | Now you will try my sword!",
            "Давай же кидай кубик, бой идет | Come on, roll the dice, the fight is on",
            "Броня крепка и танки наши быстры | The armor is strong and our tanks are fast",
            "Кидай кубик или тебе нравится смотреть как мы деремся? | Roll the dice or do you like to watch us fight?",
            "Из последних сил я его заколю| With the last of my strength, I will stab him",
            "СПАРТА !!!!! | SPARTA !!!!!" };

        Cube_fight_victory = new string[] {
            "Получи! Ура!| Get it! Hurray!",
            "Ура! Победа! | Hurray! Victory!",
            "Класс! | Wow!",
            "Я самый сильный !!! | I am the strongest !!!",
            "Если нужно подраться, то я за!| If you need to fight, then I'm for it!"};

        Cube_fight_loss = new string[] {
            "О! Нееееет !| Oh! Noooooo !",
            "Это было больно! | It was painful!",
            "Ну ничего ! | Well, nothing !",
            "Отомстите за меня| Avenge me",
            "Еще сдеремся! | We'll get together again!"};


    }
    public void Start()
    {
     
        // Определим платформу   
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Pc = true;
        }
        else
        {
            Pc = false;
        }

        // Получим язык 
        if (PlayerPrefs.HasKey("lang"))
        {
            lang = PlayerPrefs.GetString("lang");
        }
        else
        {
           lang = "en";
        }

        // Установим видимость выбора игроков

        // set_player_aviable();


        //if (SceneManager.GetActiveScene().name != "Start")
        //{
        //    move_priznak_step();
        //}

    }

    public void save_level_complete(string level_name)
    {
        PlayerPrefs.SetInt(chapter_1_levels_name[level_name], 1);
    }

    public string get_level_complete_name(string level_name)
    {
        if (chapter_1_levels_name.ContainsKey(level_name) == true)
        {
            return chapter_1_levels_name[level_name];
        }
        else
        {
            return "------";
        }
        
    }

    public void set_player_aviable()
    {
        int step_move = 0;
        max_player = 0;
        GameObject Knight_ = GameObject.Find("Knight");

        if (Knight_aviable == false)
        {
            Knight_.SetActive(false);

        }
        else if (Knight_aviable == true)
        {
            Player_ pl_script_knight = Knight_.GetComponent<Player_>();
            step_move += 1;
            pl_script_knight.step_move = step_move;
            max_player += 1;
        }

        GameObject Barbarian_ = GameObject.Find("Barbarian");
        if (Barbarian_aviable == false)
        {
            Barbarian_.SetActive(false);
        }
        else if (Barbarian_aviable == true)
        {
            Player_ pl_script_Barbarian = Barbarian_.GetComponent<Player_>();
            step_move += 1;
            pl_script_Barbarian.step_move = step_move;
            max_player += 1;
        }

        GameObject Mage_ = GameObject.Find("Mage");
        if (Mage_aviable == false)
        {
            Mage_.SetActive(false);
        }
        else if (Mage_aviable == true)
        {
            Player_ pl_script_Mage = Mage_.GetComponent<Player_>();
            step_move += 1;
            pl_script_Mage.step_move = step_move;
            max_player += 1;
        }


        GameObject Priest_ = GameObject.Find("Priest");
        if (Priest_aviable == false)
        {
            Priest_.SetActive(false);
        }
        else if (Priest_aviable == true)
        {
            Player_ pl_script_Priest = Priest_.GetComponent<Player_>();
            step_move += 1;
            pl_script_Priest.step_move = step_move;
            max_player += 1;
        }

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

        if (current_move == max_player)
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
        ChangeText(player_);
   
    }

    public void add_text(string a_text)
    {
        //var myText = GameObject.Find("Text_").GetComponent<Text>();

        if (a_text.IndexOf("<") == -1)
        {
            a_text = "<b> Admin </b>: " + a_text;
        }
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
        if (pl_script.comp == false && Shop_aviable == true)
        {
            shop_button.SetActive(true);
        }
        else
        {
            shop_button.SetActive(false);
        }
    }

    public void ChangeText(GameObject curPlayer)
    {
        Player_ pl_script = curPlayer.GetComponent<Player_>();

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

            if (pl_script.comp == true)
            {
                write_to_the_chat(curPlayer, "Waiting_cube_before_the_fight");
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
                    add_text("Текущий ход " + curPlayer.name + "  нажмите на кубик, затем компьютер сам сделает ход");
                }
                else if (lang == "en")
                {
                    add_text("Current move " + curPlayer.name + " click on the cube, then the computer will make its own move");
                }

                write_to_the_chat(curPlayer, "Waiting_cube_before_the_move");
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

    public void write_to_the_chat(GameObject Player_, string incident)
    {
        // сначала решим будет ли вообще писать игрок // выбираем из двух случайных  чисел
        // потом по инциденту выбирает массив, и пишет случайное сообщение

        string tag_colour = "<color=white><b>";
        string[] subs;

        if (Player_.name == "Knight")
        {
            tag_colour = "<color=grey><b>";
        }
        else if (Player_.name == "Barbarian")
        {
            tag_colour = "<color=orange><b>";
        }
        else if (Player_.name == "Mage")
        {
            tag_colour = "<color=maroon><b>";
        }
        else if (Player_.name == "Priest")
        {
            tag_colour = "<color=darkblue><b>";
        }

        if (Random.Range(1, 4) != 1) // ничего не пишем
        {
            return;
        }


        if (incident == "Waiting_cube_before_the_move")
        {
            int range_el = Random.Range(0, Waiting_cube_before_the_move.Length);
            subs = Waiting_cube_before_the_move[range_el].Split('|');
        }
        else if (incident == "Waiting_cube_before_the_fight") 
        {
            int range_el = Random.Range(0, Waiting_cube_before_the_fight.Length);
            subs = Waiting_cube_before_the_fight[range_el].Split('|');
        }
        else if (incident == "Cube_fight_victory") 
        {
            int range_el = Random.Range(0, Cube_fight_victory.Length);
            subs = Cube_fight_victory[range_el].Split('|');
        }
        else if (incident == "Cube_fight_loss") 
        {
            int range_el = Random.Range(0, Cube_fight_loss.Length);
            subs = Cube_fight_loss[range_el].Split('|');
        }
        else
        {
            subs = new string[] { "......","........"};
        }

        GameObject cube = GameObject.Find("Cube");
        ClickOnCube clicCube = cube.GetComponent<ClickOnCube>();
        clicCube.play_chat();

        if (lang == "ru")
        {
            add_text(tag_colour + Player_.name + " </b></color> : " + subs[0]);
        }
        else
        {
            add_text(tag_colour + Player_.name + "</b></color> : " + subs[1]);
        }

     

    }

}
