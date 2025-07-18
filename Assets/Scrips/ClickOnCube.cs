﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ClickOnCube : MonoBehaviour
{

    public GameObject[] Cube_;
    public string max_name;
    public float max_position;
    public int cube_step;
    public List<string> list_steps = new List<string>();
    public int count_magic_crystall;
    public int cube_step_crystall =0;


    public Instantiant massive;
    public bool clicked = false;
    public Vector3 pos;
    public string CurFloorName;
    public GameObject[] Blue;
    public GameObject selected1;
    public GameObject[] player;
    public GameObject Curent_player;
    static AudioSource audiosrc;
    //public AudioClip WinEnemy;
    //public AudioClip DefeatEnemy;
    public AudioClip Click;
    public GameObject finalUI;
    public AudioClip Hp;
    public AudioClip fight;
    public AudioClip sound_chat;
    public AudioClip Step;
    public AudioClip sound_proigr_battle;
    public AudioClip sound_final;
    public AudioClip sound_final_loss;
    public GameObject UI;
    public Text TextEndGame;
    public GameObject CrystalButton_;

    public Sprite ImageEndWin;
    public Sprite ImageEndLoose;

    public float computerMoveDelay = 2f; // Время ожидания хода компьютера (секунды)
    private bool waitingForComputerMove = false;


    private void Awake()
    {
        finalUI = GameObject.Find("EndGame");
        UI = GameObject.Find("UI");
        TextEndGame = GameObject.Find("TextEnd").GetComponent<Text>();
        CrystalButton_ = GameObject.Find("CrystalButton");
    }

    void Start()
    {
        
        audiosrc = GetComponent<AudioSource>();
        CrystalButton_.SetActive(false);


        if (PlayerPrefs.HasKey("count_magic_crystall"))
        {
            count_magic_crystall = PlayerPrefs.GetInt("count_magic_crystall");
        }
        else
        {
            count_magic_crystall = 0;
        }

    }

    void OnMouseDown()
    {

        make_move();

    }

    public void make_move()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();


        GameObject cube_button = GameObject.Find("CubeButton");
        cube_button_script mCube = cube_button.GetComponent<cube_button_script>();

        if (mCube.cube_is_available == false)
        {
            return;
        }

        list_steps.Clear(); // очистили  технический list куда можно идти
       
        clear_blue(); // убрали голубые

        if (cube_step_crystall == 0)
        {
            cube_step = throw_a_bone(); // кинули кубик
        }
        else
        {
            cube_step = cube_step_crystall;
            cube_step_crystall = 0;
        }

        //cube_step = throw_a_bone(); // кинули кубик
        Curent_player = mScript.return_curent_player(); // нашли текущего игркока
        string currentPlayerName = Curent_player.name;

        //GameLogger.Instance.Log($"Текущий ход: {currentPlayerName}"); // Логируем игрока
        GameLogger.Instance.Log(new List<string> { "текущий ход", currentPlayerName });
        //GameLogger.Instance.Log($"Игрок {currentPlayerName} бросил кубик и у него выпало {cube_step}");
        GameLogger.Instance.Log(new List<string> { "бросил кубик", currentPlayerName, $"выпало{cube_step}"});

        bool current_player_mode_battle = return_current_player_mode(Curent_player);
        //GameLogger.Instance.Log($"Игрок {currentPlayerName} находится в режиме боя {current_player_mode_battle}");
        GameLogger.Instance.Log(new List<string> { "режим боя", currentPlayerName, $"{current_player_mode_battle}"});
        bool current_player_mode_recovery = return_current_mode_recovery(Curent_player);
        //GameLogger.Instance.Log($"Игрок {currentPlayerName} находится в режиме восстановления {current_player_mode_recovery}");
        GameLogger.Instance.Log(new List<string> { "режим восстановления", currentPlayerName, $"{current_player_mode_recovery}"});
        if (Curent_player != null && current_player_mode_battle == false && current_player_mode_recovery == false) // режим хода
        {
            //GameLogger.Instance.Log($"Игрок {currentPlayerName} должен сделать ход");
            GameLogger.Instance.Log(new List<string> { "ход", currentPlayerName});

            // GameObject.Find("CubeButton").SetActive(false);
            mCube.reverse_cube_aviable();

            if (cube_step == 4 && mScript.Crystal_aviable == true)
            {
                CrystalButton_.SetActive(true);
            }

            // нашли где он стоит
            CurFloorName = Return_floor_player(new Vector3(Curent_player.transform.position.x, Curent_player.transform.position.y, Curent_player.transform.position.z));
            // подсветили синими квадратиками
            if (CurFloorName != null)
            {
                Get_Steps(cube_step, CurFloorName, false);
            }

            // Проверка нет ли уигрока на выделенном
            // check_the_player_on_the_field();

            if (Curent_player.GetComponent<Player_>().get_comp() == true)
            {
                //Debug.Log(list_steps.Count);
                // необходимо выбрать кубик и вызвать ход
                //Curent_player.GetComponent<Player_>().define_goal();
                Invoke("step_comp_player", 0.3f);
            }

        }
        else if (Curent_player != null && current_player_mode_battle == true && current_player_mode_recovery == false) // режим боя
        {
            //GameLogger.Instance.Log($"Игрок {currentPlayerName} начинает бой с врагом");
            GameLogger.Instance.Log(new List<string> { "бой с врагом", currentPlayerName});
            battle_whith_enemy(cube_step);
        }
        else // режим восстановления здоровья
        {
            //GameLogger.Instance.Log($"Игрок {currentPlayerName} восстановил здоровье");
            GameLogger.Instance.Log(new List<string> { "восстановил здоровье", currentPlayerName});
            leave_recovery(cube_step);
        }
    }

    /// <summary>
    /// ////////////////////
    /// </summary>

    void step_comp_player()
    {
        GameObject rnd_Floor;
        Vector3 rnd_Floor_Pos;
        Player_ pl_script = Curent_player.GetComponent<Player_>();
        string way_player;

        if (pl_script.goal_live() == false)
        {
            way_player = pl_script.define_way();
        }
        else
        {
            way_player = pl_script.return_way_to_goal();

        }

        //

        string[] subs = way_player.Split(',');

        // переберем все доступные ходы и найдем с самым большим индексом


        float max_distance = 0;
        int index_max_distance = 0;

        //// выбор самого близкого объекта
        

        int index_ls = 0;
        foreach (var lst in list_steps)
        {

            //index_ls++;
            int index = System.Array.IndexOf(subs, lst);
            if (index >= max_distance)
            {
                max_distance = index;
                index_max_distance = index_ls;


                if (item_on_the_field(list_steps[index_max_distance]) == true) /// если на поле есть предмет, то забираем его
                {
                    break;
                }

            }

            index_ls++;


        }


        rnd_Floor = GameObject.Find(list_steps[index_max_distance]);


        rnd_Floor_Pos = new Vector3(rnd_Floor.transform.position.x, rnd_Floor.transform.position.y, rnd_Floor.transform.position.z);

        Blue = GameObject.FindGameObjectsWithTag("Blue");

        for (int b = 0; b < Blue.Length;)
        {

            pl_script.set_previus_position(Curent_player.transform.position);
            // Curent_player.transform.position = new Vector3(rnd_Floor.transform.position.x, 0.7f, rnd_Floor.transform.position.z);

            pl_script.startTime = Time.time;
            pl_script.startMarker = Curent_player.transform;
            pl_script.endMarker = new Vector3(rnd_Floor.transform.position.x, 0.7f, rnd_Floor.transform.position.z);
            pl_script.journeyLength = Vector3.Distance(Curent_player.transform.position, new Vector3(rnd_Floor.transform.position.x, 0.7f, rnd_Floor.transform.position.z));
            pl_script.move = true;
            //audiosrc.PlayOneShot(Moving);
            //GameLogger.Instance.Log($"Игрок {Curent_player.name} совершил ход на поле {rnd_Floor.name}");
            GameLogger.Instance.Log(new List<string> { "сделал ход", Curent_player.name});
            Curent_player.GetComponent<Player_>().SoundStep();
            //audiosrc.PlayOneShot(Step);
            Move bScript = Blue[b].GetComponent<Move>();
            bScript.ItemFromFloor(Curent_player, rnd_Floor_Pos);
            
            bScript.clear_blue();
            break;
        }

    }

    /// <summary>
    /// //////////
    /// </summary>
    /// <returns></returns>
    ///

    bool item_on_the_field (string name_field)
    {
        GameObject _Floor = GameObject.Find(name_field);
        Vector3 position_ = _Floor.transform.position;

        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        // найдем расположено ли на этой клетке что-то

        GameObject _items = mScript.return_tag_item_on_position(position_);

        if (_items != null)
        {
            return true;

        }

        return false;
    }

    private void check_the_player_on_the_field()
    {

        foreach (var lst in list_steps)
        {
            GameObject go_ = GameObject.Find(lst);

            player = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < player.Length; i++)
            {

                if (go_.transform.position.x == player[i].transform.position.x && go_.transform.position.z == player[i].transform.position.z)
                {
                    Debug.Log("для игрока " + player[i].name + " совпадение ");

                }

            }


        }
    }

    public void save_count_crystal(int count_crystal)
    {

        PlayerPrefs.SetInt("count_magic_crystall", count_crystal);
        PlayerPrefs.Save();
    }


    public int throw_a_bone()
    {
        transform.Rotate(0, Random.Range(1, 100) * 90, 0);
        transform.Rotate(Random.Range(1, 100) * 90, 0, 0);
        transform.Rotate(0, 0, Random.Range(1, 10) * 90);

        Cube_ = GameObject.FindGameObjectsWithTag("Cube");


        max_name = "";
        max_position = 0;

         audiosrc.PlayOneShot(Click);

        for (int i = 0; i < Cube_.Length; i++)
        {

            if (Cube_[i].transform.position.y > max_position)
            {
                max_position = Cube_[i].transform.position.y;
                max_name = Cube_[i].name;
            }

        }

        //Debug.Log(max_name);
        //return 4;

        if (max_name == "gold")
        {
            //cube_step = 4;
            count_magic_crystall += 1;

            //PlayerPrefs.SetInt("count_magic_crystall", count_magic_crystall);
            //PlayerPrefs.Save();

            save_count_crystal(count_magic_crystall);

            return 4;
        }

        if (max_name == "One")
        {
            //cube_step = 1;
            return 1;
        }

        if (max_name == "Two11" || max_name == "Two12" || max_name == "Two21" || max_name == "Two22")
        {
            //cube_step = 2;
            return 2;
        }

        if (max_name == "Three11" || max_name == "Three12" || max_name == "Three13" || max_name == "Three21" || max_name == "Three22" || max_name == "Three23")
        {
            //cube_step = 3;
            return 3;
        }

        return 0;

        // Debug.Log(cube_step);
    }


    public bool return_current_player_mode(GameObject cur_player)
    {
        Player_ pl_script = cur_player.GetComponent<Player_>();
        return pl_script.get_battle_mode();
    }

    public bool return_current_mode_recovery(GameObject cur_player)
    {
        Player_ pl_script = cur_player.GetComponent<Player_>();
        return pl_script.recovery_mode;
    }

    private string Return_floor_player(Vector3 pos)
    {
        GameObject[] Floors;
        float x_f = pos.x - 0;
        float z_f = pos.z + 0;
        string curlfloorname_ = "";

        Floors = GameObject.FindGameObjectsWithTag("Floor");

        for (int i = 0; i < Floors.Length; i++)
        {
            if (Mathf.Abs(Floors[i].transform.position.x - x_f) < 0.01 && Mathf.Abs(Floors[i].transform.position.z - z_f) < 0.01)
            {
                //CurFloorName = Floors[i].name;
                //Debug.Log(Floors[i].name);
                return Floors[i].name;
            }
        }

        if (curlfloorname_ == "")
        {
            return GameObject.Find("StartFloor").name;

        }

        return null;

    }

    void Get_Steps(int steps_, string CurFloorName_, bool local_step)
    {
        GameObject CurFloor;
        Vector3 CurFloorPos;
        Collider[] colliders;
        Collider[] s_step;
        float x,z;


        steps_ -= 1; // уменьшим шаг на 1

        CurFloor = GameObject.Find(CurFloorName_);
        CurFloorPos = new Vector3(CurFloor.transform.position.x, CurFloor.transform.position.y, CurFloor.transform.position.z);

        //Debug.Log("Текущая позиция платформы" + CurFloorPos);

        if ((colliders = Physics.OverlapSphere(CurFloorPos, 1)).Length > 0)
        {

            List<Collider> forward = new List<Collider>();
            //List<Collider> center = new List<Collider>();
            List<Collider> back = new List<Collider>();
            List<Collider> left = new List<Collider>();
            List<Collider> right = new List<Collider>();

            List<List<Collider>> myList = new List<List<Collider>>();

            foreach (var collider in colliders)
            {
                // обрабатываем только некоторые тэги

                if (collider.tag == "Player" ||
                    collider.tag == "Floor" ||
                    collider.tag == "Enemy_1" ||
                    collider.tag == "Enemy_2" ||
                    collider.tag == "Enemy_boss" ||
                    collider.tag == "Door")

                {

                    Vector3 pos1 = collider.transform.position - CurFloorPos;
                    //float pos_x = collider.transform.position.x - CurFloorPos.x;

                    // так как есть небольшие смещения, приведем к 0 округления
                    if (Mathf.Abs(pos1.x) < 0.01)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = pos1.x;
                    }

                    if (Mathf.Abs(pos1.z) < 0.01)
                    {
                        z = 0;
                    }
                    else
                    {
                        z = pos1.z;
                    }

                    //  Заполняем List направлений;

                    if (CurFloorName_ == "StartFloor")
                    {
                        forward.Add(collider);
                    }

                    if (x > 0 && z == 0)
                    {
                        forward.Add(collider);
                    }

                    if (x < 0 && z == 0)
                    {
                        back.Add(collider);
                    }

                    if (z > 0 && x == 0)
                    {
                        left.Add(collider);
                    }

                    if (z < 0 && x == 0)
                    {
                        right.Add(collider);
                    }

                }

            }

            myList.Add(forward);
            //myList.Add(center);
            myList.Add(back);
            myList.Add(left);
            myList.Add(right);

            foreach (var l_n in myList)
            {

                // проверим, а есть ли куда наступать, что там на каждом направлениия

                s_step = Checking_Step(
                    l_n, steps_);

                // вот тут проверка можно ли по этому направлению рассчитывать следующий ход
                // .................................

                if ((s_step[0] != null && steps_ > 0) ||   // есть куда ходить и есть шаги
                    (s_step[0] != null && steps_ <= 0 && steps_ > -3 && s_step[1] != null && s_step[1].tag == "Player")) // если шаги закончились и на последнем ходе стоит игрок
                {

                    Get_Steps(steps_, s_step[0].name, false);
                }

            }

        }

    }

    public Collider[] Checking_Step(List<Collider> NextStep, int steps_)
    {

        // + Если на floor дверь(закрыта) и нет ключа - пройти нельзя
        // + Если на floor дверь(закрыта) и ключ есть - пройти можно(-ключ)
        // + Если ты хочешь забрать item то ты наступаешь на этот floor и останавливаешься
        // + Если на floor стоит игрок и это последний шаг - идешь на след floor
        // Если на поле монстр - останавливаешься на этом поле
        //

        Collider FloorList = null;
        Collider ItemList = null;
        Collider[] mas_return = new Collider[2];
        // string Blue = "Blue1(Clone)";

        if (NextStep.Count == 0)
        {
            mas_return[0] = null;
            mas_return[1] = null;
            return mas_return;
        }
        if (NextStep.Count == 1)
        {
            if (NextStep[0].name != CurFloorName && NextStep[0].tag == "Floor")
            {

                mas_return[0] = NextStep[0];
                mas_return[1] = null;

                if (available_floor_in_list(NextStep[0].name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                {
                    //  Debug.Log("447 in step " + steps_ + "count = 1 in" + NextStep[0].name);
                    list_steps.Add(NextStep[0].name);
                    show_blue_on_floor(NextStep[0]);
                }

                return mas_return;
            }

        }
        if (NextStep.Count > 1)
        {
            foreach (var l in NextStep)
            {
                if (l.tag == "Floor")
                {
                    FloorList = l;
                }
                else
                {
                    ItemList = l;
                }
            }

            mas_return[0] = FloorList;
            mas_return[1] = ItemList;

            if (ItemList.tag == "Player") // если на кубике стоит игрок
            {
                //if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                //{
                //    list_steps.Add(FloorList.name);
                //}
                //  Debug.Log("479 in step " + steps_ + "tag = player in floor " + FloorList.name + " in item " + ItemList.name);
                return mas_return;
            }

            if (ItemList.tag == "Enemy_1" || ItemList.tag == "Enemy_2" || ItemList.tag == "Enemy_boss") // если на кубике стоит враг
            {
                //GameLogger.Instance.Log($"На пути игрока находится враг");
                GameLogger.Instance.Log(new List<string> { "впереди", "враг" });
                if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                {
                    list_steps.Add(FloorList.name);
                    show_blue_on_floor(NextStep[0]);
                }
                // Debug.Log("490 in step " + steps_ + "tag = player in floor " + FloorList.name + " in item " + ItemList.name);
                mas_return[0] = null;
                mas_return[1] = null;
                return mas_return;
            }

            if (ItemList.tag == "Door") // если на кубике дверь
            {
                //GameLogger.Instance.Log($"На пути игрока находится дверь");
                GameLogger.Instance.Log(new List<string> { "впереди", "дверь" });
                //проверим есть ли у игрока ключ
                Player_ pl_script = Curent_player.GetComponent<Player_>();

                if (pl_script.get_key() == false) // ключа нет, идти нельзя
                {
                    mas_return[0] = null;
                    mas_return[1] = null;
                    return mas_return;
                }
                else // ключ есть, подсветим, но дальше идти нельзя
                {
                    //GameLogger.Instance.Log($"На пути игрока находится ключ");
                    GameLogger.Instance.Log(new List<string> { "впереди", "ключ" });
                    if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                    {
                        list_steps.Add(FloorList.name);
                        show_blue_on_floor(NextStep[0]);
                    }
                    //  Debug.Log("in step " + steps_ + "tag = door in floor " + FloorList.name + " in item " + ItemList.name);
                    mas_return[0] = null;
                    mas_return[1] = null;
                    return mas_return;
                }
            }

            if (FloorList.name != CurFloorName)
            {

                if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                {
                    list_steps.Add(FloorList.name);
                    show_blue_on_floor(NextStep[0]);
                }
                // Debug.Log("531 in step " + steps_ + " count = 2, floor " + FloorList.name + " in item " + ItemList.name);
                // show_blue_on_floor(NextStep[0]);

                return mas_return;
            }

        }

        mas_return[0] = null;
        mas_return[1] = null;
        return mas_return;

    }

    bool available_floor_in_list(string name_floor)
    {
        string f_zn = list_steps.Find(item => item == name_floor);

        if (f_zn == null || f_zn == "")
        {
            // Debug.Log(name_floor + " in List false");
            return false;
        }
        else
        {
            // Debug.Log(name_floor + " in List true");
            return true;
        }
    }

    void show_blue_on_floor(Collider floor_)
    {
        // Instantiate(selected1, new Vector3(floor_.transform.position.x, 0.05f, floor_.transform.position.z), Quaternion.identity);

        //bool player_in_floor = false;

        //player = GameObject.FindGameObjectsWithTag("Player");

        //for (int i = 0; i < player.Length; i++)
        //{

        //    if (floor_.transform.position.x == player[i].transform.position.x && floor_.transform.position.z == player[i].transform.position.z)
        //    {
        //        player_in_floor = true;
        //        Debug.Log("игрок на поле");

        //    }

        //}

        //if (player_in_floor == false)
        //{
            Instantiate(selected1, new Vector3(floor_.transform.position.x, 1.05f, floor_.transform.position.z), Quaternion.identity);

        //}
    }

    void leave_recovery(int cube_s)
    {
        Player_ pl_script = Curent_player.GetComponent<Player_>();
        Main mScript = GameObject.Find("Directional Light").GetComponent<Main>();
         audiosrc.PlayOneShot(Hp);
        //Debug.Log("Hp+");
        pl_script.add_leaves(cube_s);

        //if (mScript.lang == "ru")
        //{
        //    mScript.set_current_move("Восстановлено " + cube_s + " жизней");
        //}
        //else if (mScript.lang == "en")
        //{
            mScript.set_current_move("Restored " + cube_s + " lives");
        //}

        
    }

    void battle_whith_enemy(int cube_s)
    {

        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        Player_ pl_script = Curent_player.GetComponent<Player_>();

        Vector3 CurFloorPos;
        Collider[] colliders;

        // enemy 1 - 1
        // enemy 2 - 2
        // enemy boss - 3

        // 4 - победа
        // 3 - победа
        // 2 - потеря здоровья как сила монстра и возврат назад
        // 1 - победа, потеря здоровья как сила монстра, возврат назад

        // найдем монстра

        
        

        CurFloorPos = new Vector3(Curent_player.transform.position.x, Curent_player.transform.position.y, Curent_player.transform.position.z);

        //audiosrc.PlayOneShot(fight);
        //Debug.Log("Текущая позиция платформы" + CurFloorPos);

        if ((colliders = Physics.OverlapSphere(CurFloorPos, 1)).Length > 0)
        {

            foreach (var collider in colliders)
            {
                // обрабатываем только некоторые тэги

                if (collider.tag == "Enemy_1" ||
                    collider.tag == "Enemy_2" ||
                    collider.tag == "Enemy_boss")

                {

                    // Vector3 pos1 = collider.transform.position; Mathf.Abs(float f)
                    // if (collider.transform.position.x == Curent_player.transform.position.x && collider.transform.position.z == Curent_player.transform.position.z)
                    if (Mathf.Abs(collider.transform.position.x -Curent_player.transform.position.x) < 0.01 && Mathf.Abs(collider.transform.position.z - Curent_player.transform.position.z) < 0.01)
                    {
                        // Debug.Log("battlle");

                        if (cube_s == 4) // победа
                        {
                            if (collider.tag != "Enemy_boss")
                            {
                                AudioClip[] victoryClips = Resources.LoadAll<AudioClip>("victory");
                                if (victoryClips != null && victoryClips.Length > 0)
                                {
                                    AudioClip randomvictoryClip = victoryClips[Random.Range(0, victoryClips.Length)];
                                    audiosrc.PlayOneShot(randomvictoryClip);
                                }
                            }
                            
                            GameObject Swords = GameObject.Find("crossed sword(Clone)");
                            Destroy(Swords);
                            Destroy(collider.gameObject);
                            if (collider.tag == "Enemy_boss")
                            {
                                //GameLogger.Instance.Log($"Босс побежден");
                                GameLogger.Instance.Log(new List<string> { "босс побежден" });
                                final(false);
                            }
                            pl_script.switch_battle_mode();


                            if (mScript.lang == "ru")
                            {
                                //GameLogger.Instance.Log($"Игрок победил");
                                GameLogger.Instance.Log(new List<string> { "игрок победил" });
                                mScript.set_current_move("Победа !!!");
                            }
                            else if (mScript.lang == "en")
                            {
                                GameLogger.Instance.Log(new List<string> { "игрок победил" });
                                mScript.set_current_move("Victory !!!");
                            }
                            
                            if (pl_script.comp == true)
                            {
                                mScript.write_to_the_chat(Curent_player, "Cube_fight_victory"); 
                            }
                            
                        }
                        else if (cube_s == 3) // победа
                        {

                            if (collider.tag != "Enemy_boss")
                            {
                                AudioClip[] victoryClips = Resources.LoadAll<AudioClip>("victory");
                                if (victoryClips != null && victoryClips.Length > 0)
                                {
                                    AudioClip randomvictoryClip = victoryClips[Random.Range(0, victoryClips.Length)];
                                    audiosrc.PlayOneShot(randomvictoryClip);
                                }
                            }

                            GameObject Swords = GameObject.Find("crossed sword(Clone)");
                            Destroy(Swords);
                            Destroy(collider.gameObject);
                            if (collider.tag == "Enemy_boss")
                            {
                                final(false);
                            }
                            pl_script.switch_battle_mode();

                            if (mScript.lang == "ru")
                            {
                                GameLogger.Instance.Log(new List<string> { "игрок победил" });
                                mScript.set_current_move("Победа !!!");
                            }
                            else if (mScript.lang == "en")
                            {
                                GameLogger.Instance.Log(new List<string> { "игрок победил" });
                                mScript.set_current_move("Victory !!!");
                            }

                            if (pl_script.comp == true)
                            {
                                mScript.write_to_the_chat(Curent_player, "Cube_fight_victory");
                            }


                        }
                        else if (cube_s == 2) // проигрышь, шаг назад 
                        {
                            // audiosrc.PlayOneShot(sound_proigr_battle);
                            GameObject Swords = GameObject.Find("crossed sword(Clone)");
                            Destroy(Swords);

                            if (collider.tag == "Enemy_1")
                            {
                                GameLogger.Instance.Log(new List<string> { "проиграл", "потерял жизнь" });
                                pl_script.add_leaves(-1);
                            }
                            else if (collider.tag == "Enemy_2")
                            {
                                GameLogger.Instance.Log(new List<string> { "проиграл", "потерял две жизнь" });
                                pl_script.add_leaves(-2);
                            }
                            else if (collider.tag == "Enemy_boss")
                            {

                                GameLogger.Instance.Log(new List<string> { "проиграл", "потерял три жизни" });
                                pl_script.add_leaves(-3);
                            }


                            //Curent_player.transform.position = pl_script.get_previus_position();
                            if (pl_script.get_leaves() == 0 && level_survival() == true)
                            {

                                final(true);
                   
                            }


                            pl_script.startTime = Time.time;
                            pl_script.startMarker = Curent_player.transform;
                            pl_script.endMarker = pl_script.get_previus_position();
                            pl_script.journeyLength = Vector3.Distance(Curent_player.transform.position, pl_script.get_previus_position());
                            pl_script.move = true;
                            pl_script.switch_battle_move = true;

                            //pl_script.switch_battle_mode();

                            if (mScript.lang == "ru")
                            {
                                mScript.set_current_move("Проигрыш, были потеряны жизни");
                            }
                            else if (mScript.lang == "en")
                            {
                                mScript.set_current_move("Losing, lives were lost");
                            }

                            if (pl_script.comp == true)
                            {
                                mScript.write_to_the_chat(Curent_player, "Cube_fight_loss"); 
                            }

                        }
                        else if (cube_s == 1) // победа и шаг назад
                        {
                            if (collider.tag != "Enemy_boss")
                            {
                                AudioClip[] victoryClips = Resources.LoadAll<AudioClip>("victory");
                                if (victoryClips != null && victoryClips.Length > 0)
                                {
                                    AudioClip randomvictoryClip = victoryClips[Random.Range(0, victoryClips.Length)];
                                    audiosrc.PlayOneShot(randomvictoryClip);
                                }
                            }

                            GameObject Swords = GameObject.Find("crossed sword(Clone)");
                            Destroy(Swords);

                            if (collider.tag == "Enemy_1")
                            {
                                GameLogger.Instance.Log(new List<string> { "победил", "потерял жизнь" });
                                pl_script.add_leaves(-1);
                            }
                            else if (collider.tag == "Enemy_2")
                            {
                                GameLogger.Instance.Log(new List<string> { "победил", "потерял две жизнь" });
                                pl_script.add_leaves(-2);
                            }
                            else if (collider.tag == "Enemy_boss")
                            {
                                GameLogger.Instance.Log(new List<string> { "победил", "потерял три жизни" });
                                final(false);
                                pl_script.add_leaves(-3);
                            }

                            if (pl_script.get_leaves() == 0 && level_survival() == true)
                            {

                                final(true);

                            }

                            Destroy(collider.gameObject);
                            // Curent_player.transform.position = pl_script.get_previus_position();

                            pl_script.startTime = Time.time;
                            pl_script.startMarker = Curent_player.transform;
                            pl_script.endMarker = pl_script.get_previus_position();
                            pl_script.journeyLength = Vector3.Distance(Curent_player.transform.position, pl_script.get_previus_position());
                            pl_script.move = true;

                            pl_script.switch_battle_mode();

                            if (mScript.lang == "ru")
                            {
                                mScript.add_text("Победа, ценой потеряных жизней");
                            }
                            else if (mScript.lang == "en")
                            {
                                mScript.add_text("Victory, at the cost of lost lives");
                            }

                            if (pl_script.comp == true)
                            {
                                mScript.write_to_the_chat(Curent_player, "Cube_fight_victory");
                            }
                        }
                    }
                }
            }
        }


    }

    public void clear_blue()
    {
        Blue = GameObject.FindGameObjectsWithTag("Blue");

        for (int b = 0; b < Blue.Length; b++)
        {
            Destroy(Blue[b]);
            //Debug.Log("a");
            CrystalButton_.SetActive(false);
        }
    }

    private bool level_survival()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        Player_ pl_script = Curent_player.GetComponent<Player_>();

        if (mScript.survival == true && mScript.level_complete == false &&  pl_script.comp == false)
        {
            return true;
        }

        return false;
    }


    void final(bool lose)
    {       
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        finalUI.SetActive(true);
        UI.SetActive(false);
        GameObject ImageEnd = GameObject.Find("ImageEnd");
        Player_ pl_script = Curent_player.GetComponent<Player_>();
        
        string fullGameLog = GameLogger.Instance.GetFullLog();
        Debug.Log("История действий:\n" + fullGameLog);

        // Вывод использованных voice lines
        Debug.Log("Использованные voice lines:\n" + VoiceLineManager.Instance.GetUsedVoiceLinesAsString());

        if (lose == false)
        {
            // проверим, а выполнил ли он уровень
            if (mScript.challenge_level == true)
            {
                // Player_ pl_script = Curent_player.GetComponent<Player_>();

                if (pl_script.test_goal_challenge_level() == false)
                {
                    ImageEnd.GetComponent<Image>().sprite = ImageEndLoose;
                    ImageEnd.GetComponent<RectTransform>().sizeDelta = new Vector2(125, 185.7f);
                    ImageEnd.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
                    audiosrc.PlayOneShot(sound_final_loss);
                    if (mScript.lang == "ru")
                    {
                        TextEndGame.text = "Проигрыш " + Curent_player.name + " вы не выполнили цель миссии!";

                    }
                    else if (mScript.lang == "en")
                    {
                        TextEndGame.text = "Loss " + Curent_player.name + " you have not completed the mission goal!";
                    }

                    return;

                }
            }
            ImageEnd.GetComponent<Image>().sprite = ImageEndWin;
            ImageEnd.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
            ImageEnd.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            audiosrc.PlayOneShot(sound_final);

            if (pl_script.comp == false)
            {
                mScript.save_level_complete(SceneManager.GetActiveScene().name);
                if (mScript.lang == "ru")
                {
                    TextEndGame.text = "Поздравляем " + Curent_player.name + " с победой!";
                }
                else if (mScript.lang == "en")
                {
                    TextEndGame.text = "Congratulation " + Curent_player.name + " win!";
                }

            }
            else
            {
                if (mScript.lang == "ru")
                {
                    TextEndGame.text = "Поздравляем " + Curent_player.name + " с победой, но уровень не защитан!";
                }
                else if (mScript.lang == "en")
                {
                    TextEndGame.text = "Congratulation " + Curent_player.name + " win, but the level is not protected!";
                }
            }




        }
        else
        {
            audiosrc.PlayOneShot(sound_final_loss);
            ImageEnd.GetComponent<Image>().sprite = ImageEndLoose;
            ImageEnd.GetComponent<RectTransform>().sizeDelta = new Vector2(125, 185.7f);
            ImageEnd.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
            if (mScript.lang == "ru")
            {
                TextEndGame.text = "Проигрыш " + Curent_player.name + " из за потери всех жизней!";
            }
            else if (mScript.lang == "en")
            {
                TextEndGame.text = "Loss " + Curent_player.name + " loss of all lives!";
            }
        }

   

    }

    public void play_victory_fight()
    {
        audiosrc.PlayOneShot(fight);
    }


    public void play_chat()
    {
        // audiosrc.PlayOneShot(sound_chat);
    }

    void Update()
    {
        if (!waitingForComputerMove)
        {
            GameObject cam = GameObject.Find("Directional Light");
            if (cam == null) return;

            Main mScript = cam.GetComponent<Main>();
            if (mScript == null) return;

            GameObject currentPlayer = mScript.return_curent_player();
            if (currentPlayer != null && currentPlayer.GetComponent<Player_>().get_comp() == true)
            {
                GameObject cube_button = GameObject.Find("CubeButton");
                if (cube_button == null) return;

                cube_button_script mCube = cube_button.GetComponent<cube_button_script>();
                if (mCube == null) return;

                if (mCube.cube_is_available && computerMoveDelay > 0f)
                {
                    waitingForComputerMove = true;
                    StartCoroutine(ComputerMoveCoroutine());
                }
            }
        }
    }

    private IEnumerator ComputerMoveCoroutine()
    {
        yield return new WaitForSeconds(computerMoveDelay);

        // Здесь разместите код, который должен выполниться после задержки
        make_move();

        waitingForComputerMove = false;
    }
}