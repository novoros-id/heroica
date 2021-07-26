using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Sprite Weapon1;
    public Sprite Weapon2;
    public Sprite Weapon3;

    public List<string> list_steps = new List<string>();
    public GameObject Curent_player;

    public string CurFloorName;

    public GameObject WeaponlButton_;

    public GameObject sword;

    public GameObject[] Blue;

    // Start is called before the first frame update
    void Start()
    {

        WeaponlButton_ = GameObject.Find("Weapon_Button");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WeaponAction()
    {

        // узнаем кто активный игрок
        // узанем какое у него активное оружие
        // используем его   
        // поле использования оружие уничтожим

        list_steps.Clear(); // очистили  технический list куда можно идти
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        Curent_player = mScript.return_curent_player(); // нашли текущего игркока
        //Curent_player = return_curent_player(); // нашли текущего игркока
        Player_ pl_script = Curent_player.GetComponent<Player_>();
        //Main mScript = GameObject.Find("Directional Light").GetComponent<Main>();
        CurFloorName = pl_script.Return_floor_player(new Vector3(Curent_player.transform.position.x, Curent_player.transform.position.y, Curent_player.transform.position.z));

        string cur_Weapon = pl_script.get_CurWeapon();

        if (cur_Weapon == "axe")
        {
            bool del_enemy = delete_enemy_crystal_use(CurFloorName);

            if (del_enemy == false)
            {
                return;
            }

            pl_script.play_sound_use_crystal();
            clear_blue();
            // clear_blue();

            if (mScript.lang == "ru")
            {
                mScript.set_current_move("Вы воспользовались топором и уничтожили всех врагов на соседних полях");
            }
            else if (mScript.lang == "en")
            {
                mScript.set_current_move("You used a Axe and destroyed all the enemies in the neighboring fields");
            }

            //CubeButton.SetActive(true);
            // pl_script.show_the_cube();
            WeaponlButton_.SetActive(false);

        }
        else if (cur_Weapon == "baton")
        {
            set_enemy_to_destroy_from_crystal(3, CurFloorName, false);

            if (crossed_swords() == false)
            {
                return;
            }

            pl_script.play_sound_use_crystal();
            clear_blue();

            if (mScript.lang == "ru")
            {
                mScript.add_text("Вы выбираете использование Жезла, укажите врага которого хотите победить ");
            }
            else if (mScript.lang == "en")
            {
                mScript.add_text("You choose to use baton, specify the enemy you want to defeat");
            }

            WeaponlButton_.SetActive(false);
        }
        else if (cur_Weapon == "scythe")
        {
            if (pl_script.get_leaves() == 4)
            {
                return;
            }

            pl_script.play_sound_use_crystal();
            clear_blue();
            // clear_blue();
            pl_script.add_leaves(2);

            if (mScript.lang == "ru")
            {
                mScript.set_current_move("Вы воспользовались Косой и у вас восстановлены все жизни");
            }
            else if (mScript.lang == "en")
            {
                mScript.set_current_move("You have used a scythe and all your lives have been restored");
            }

            //CubeButton.SetActive(true);
            pl_script.show_the_cube();
            WeaponlButton_.SetActive(false);
        }


    }

    private bool crossed_swords()
    {
        GameObject[] cr_sword = GameObject.FindGameObjectsWithTag("crossed_sword");

        if (cr_sword.Length == 0)
        {
            return false;
        }

        return true;
    }

    public void set_enemy_to_destroy_from_crystal(int steps_, string CurFloorName_, bool local_step)
    {

        GameObject CurFloor;
        Vector3 CurFloorPos;
        Collider[] colliders;
        Collider[] s_step;
        float x, z;


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

                if (collider.tag == "Floor" ||
                    collider.tag == "Enemy_1" ||
                    collider.tag == "Enemy_2" ||
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

                s_step = Checking_Step_crystal(
                    l_n, steps_);

                // вот тут проверка можно ли по этому направлению рассчитывать следующий ход
                // .................................

                if (s_step[0] != null && steps_ > 0)
                {

                    set_enemy_to_destroy_from_crystal(steps_, s_step[0].name, false);
                }

            }

        }

    }


    public bool delete_enemy_crystal_use(string nameFloor)
    {
        GameObject CurFloor;
        Vector3 CurFloorPos;
        Collider[] colliders;
        bool delete_enemy = false;

        CurFloor = GameObject.Find(nameFloor);
        CurFloorPos = new Vector3(CurFloor.transform.position.x, CurFloor.transform.position.y, CurFloor.transform.position.z);

        if ((colliders = Physics.OverlapSphere(CurFloorPos, 1.2f)).Length > 0)
        {
            foreach (var collider in colliders)
            {
                // обрабатываем только некоторые тэги
                if (collider.tag == "Enemy_1" ||
                    collider.tag == "Enemy_2")
                {
                    GameObject enemy_distr = GameObject.Find(collider.name);
                    Destroy(enemy_distr);
                    delete_enemy = true;

                    // и сыграем музыку
                    GameObject cube = GameObject.Find("Cube");
                    ClickOnCube clicCube = cube.GetComponent<ClickOnCube>();
                    clicCube.play_victory_fight();
                }
            }
        }

        return delete_enemy;
    }


    public Collider[] Checking_Step_crystal(List<Collider> NextStep, int steps_)
    {


        // Если на поле монстр - подсветим его
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


            if (ItemList.tag == "Enemy_1" || ItemList.tag == "Enemy_2") // если на кубике стоит враг
            {
                if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                {
                    list_steps.Add(FloorList.name);
                    show_sword_on_floor(NextStep[0]);
                }

                return mas_return;
            }

            if (ItemList.tag == "Door") // если на кубике дверь
            {

                mas_return[0] = null;
                mas_return[1] = null;
                return mas_return;

            }

            if (FloorList.name != CurFloorName)
            {

                if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                {
                    list_steps.Add(FloorList.name);
                    show_sword_on_floor(NextStep[0]);
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

    void show_sword_on_floor(Collider floor_)
    {
        GameObject clone = Instantiate(sword, new Vector3(floor_.transform.position.x - 0.4f, 1.4f, floor_.transform.position.z), Quaternion.identity);
        battle_mode m_Script = clone.GetComponent<battle_mode>();
        m_Script.set_weapon_use();
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
    public void clear_blue()
    {
        Blue = GameObject.FindGameObjectsWithTag("Blue");
        //audiosrc.PlayOneShot(step);
        for (int b = 0; b < Blue.Length; b++)
        {
            //audiosrc.PlayOneShot(step);
            Destroy(Blue[b]);

        }
    }
}
