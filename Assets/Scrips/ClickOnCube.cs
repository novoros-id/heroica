using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickOnCube : MonoBehaviour
{

    public GameObject[] Cube_;
    public string max_name;
    public float max_position;
    public int cube_step;
    public List<string> list_steps = new List<string>();


    public Instantiant massive;
    public bool clicked = false;
    public Vector3 pos;
    public string CurFloorName;
    public GameObject[] Blue;
    public GameObject selected1;
    public GameObject[] player;
    public GameObject Curent_player;
    static AudioSource audiosrc;
    public AudioClip WinEnemy;
    public AudioClip DefeatEnemy;
    public AudioClip Click;


    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }
    
    void OnMouseDown()
    {

        make_move();

    }

    public void make_move()
    {
        list_steps.Clear(); // очистили  технический list куда можно идти
        clear_blue(); // убрали голубые
        cube_step = throw_a_bone(); // кинули кубик
        Curent_player = return_curent_player(); // нашли текущего игркока
        bool current_player_mode_battle = return_current_player_mode(Curent_player);
        if (Curent_player != null && current_player_mode_battle == false) // режим хода
        {
            // нашли где он стоит
            CurFloorName = Return_floor_player(new Vector3(Curent_player.transform.position.x, Curent_player.transform.position.y, Curent_player.transform.position.z));
             // подсветили синими квадратиками
            if (CurFloorName != null)
            {
                Get_Steps(cube_step, CurFloorName,false);
            }

           
            if (Curent_player.GetComponent<Player_>().get_comp() == true)
            {
                //Debug.Log(list_steps.Count);
                // необходимо выбрать кубик и вызвать ход
                step_comp_player();
            }

        }
        else if (Curent_player != null && current_player_mode_battle == true) // режим боя
        {
            battle_whith_enemy(cube_step);
        }
    }

    /// <summary>
    /// ////////////////////
    /// </summary>

    void step_comp_player()
    {
        GameObject rnd_Floor;
        Vector3 rnd_Floor_Pos;

        // у нас есть цели: сначала ключ, потом дверь, потом , главный

        int count_blue = list_steps.Count;
        int rand_list = Random.Range(0, count_blue - 1);

        rnd_Floor = GameObject.Find(list_steps[rand_list]);
        rnd_Floor_Pos = new Vector3(rnd_Floor.transform.position.x, rnd_Floor.transform.position.y, rnd_Floor.transform.position.z);

        Debug.Log(rnd_Floor_Pos);

        Player_ pl_script = Curent_player.GetComponent<Player_>();

        Blue = GameObject.FindGameObjectsWithTag("Blue");

        for (int b = 0; b < Blue.Length; b++)
        {

            pl_script.set_previus_position(Curent_player.transform.position);
            Curent_player.transform.position = new Vector3(rnd_Floor.transform.position.x, 0.7f, rnd_Floor.transform.position.z);

            Move bScript = Blue[b].GetComponent<Move>();
            bScript.ItemFromFloor(Curent_player, rnd_Floor_Pos);
            bScript.clear_blue();
            break;
            //bScript.clear_blue();
        }

        //GameObject move = GameObject.Find("Directional Light");
        //Main mScript = rnd_Floor.GetComponent<Main>();

        //mScript.ItemFromFloor(Curent_player, rnd_Floor_Pos);

    }

    /// <summary>
    /// //////////
    /// </summary>
    /// <returns></returns>

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

        if (max_name == "gold")
        {
            //cube_step = 4;
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

    public GameObject return_curent_player()

    {
        player = GameObject.FindGameObjectsWithTag("Player");
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        int current_move = mScript.get_current_move();

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

    public bool return_current_player_mode (GameObject cur_player)
    {
        Player_ pl_script = cur_player.GetComponent<Player_>();
        return pl_script.get_battle_mode();
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
            if (Floors[i].transform.position.x == x_f && Floors[i].transform.position.z == z_f)
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

    void Get_Steps(int steps_, string CurFloorName_,bool local_step)
    {
        GameObject CurFloor;
        Vector3 CurFloorPos;
        Collider[] colliders;
        Collider[] s_step;

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

                    //  Заполняем List направлений;

                    if (pos1.x > 0 && pos1.z == 0)
                    {
                        forward.Add(collider);
                    }

                    //if (pos1.x == 0 && pos1.z == 0)
                    //{
                    //    center.Add(collider);
                    //}

                    if (pos1.x < 0 && pos1.z == 0)
                    {
                        back.Add(collider);
                    }

                    if (pos1.z > 0 && pos1.x == 0)
                    {
                        left.Add(collider);
                    }

                    if (pos1.z < 0 && pos1.x == 0)
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

                    Get_Steps(steps_, s_step[0].name,false);
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
            if (NextStep[0].name != CurFloorName)
            {

                mas_return[0] = NextStep[0];
                mas_return[1] = null;
                
                if (available_floor_in_list(NextStep[0].name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                {
                    // Debug.Log("in step " + steps_ + "count = 1 in" + NextStep[0].name);
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
                //if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                //{
                //    list_steps.Add(FloorList.name);
                //}
                // Debug.Log("in step " + steps_ + "tag = player in floor " + FloorList.name + " in item " + ItemList.name);
                return mas_return;
            }

            if (ItemList.tag == "Enemy_1" || ItemList.tag == "Enemy_2" || ItemList.tag == "Enemy_boss") // если на кубике стоит враг
            {
                if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                {
                    list_steps.Add(FloorList.name);
                }
                // Debug.Log("in step " + steps_ + "tag = player in floor " + FloorList.name + " in item " + ItemList.name);
                show_blue_on_floor(NextStep[0]);
                mas_return[0] = null;
                mas_return[1] = null;
                return mas_return;
            }

            if (ItemList.tag == "Door") // если на кубике дверь
            {
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
                    if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                    {
                        list_steps.Add(FloorList.name);
                    }
                   //  Debug.Log("in step " + steps_ + "tag = door in floor " + FloorList.name + " in item " + ItemList.name);
                    show_blue_on_floor(NextStep[0]);
                    mas_return[0] = null;
                    mas_return[1] = null;
                    return mas_return;
                }
            }

            if (FloorList.name != CurFloorName)
            {
                if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                {
                    if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                    {
                        list_steps.Add(FloorList.name);
                    }
                    // Debug.Log("in step " + steps_ + " count = 2, floor " + FloorList.name + " in item " + ItemList.name);
                    show_blue_on_floor(NextStep[0]);
                }
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
        Instantiate(selected1, new Vector3(floor_.transform.position.x, 0.05f, floor_.transform.position.z), Quaternion.identity);
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

                    // Vector3 pos1 = collider.transform.position;
                    if (collider.transform.position == Curent_player.transform.position)
                    {
                        // Debug.Log("battlle");

                        if (cube_s == 4) // победа
                        {
                            audiosrc.PlayOneShot(WinEnemy);
                            Destroy(collider.gameObject);
                            pl_script.switch_battle_mode();
                            mScript.set_current_move();
                        }
                        else if (cube_s == 3) // победа
                        {

                            audiosrc.PlayOneShot(WinEnemy);
                            Destroy(collider.gameObject);
                            pl_script.switch_battle_mode();
                            mScript.set_current_move();

                        }
                        else if (cube_s == 2)
                        {
                            audiosrc.PlayOneShot(DefeatEnemy);

                            if (collider.tag == "Enemy_1")
                            {
                                pl_script.add_leaves(-1);
                            }
                            else if (collider.tag == "Enemy_2")
                            {
                                pl_script.add_leaves(-2);
                            }
                            else if (collider.tag == "Enemy_boss")
                            {

                                pl_script.add_leaves(-3);
                            }


                            Curent_player.transform.position = pl_script.get_previus_position();
                            pl_script.switch_battle_mode();
                            mScript.set_current_move();
                        }
                        else if (cube_s == 1)
                        {
                            audiosrc.PlayOneShot(WinEnemy);

                            if (collider.tag == "Enemy_1")
                            {
                                pl_script.add_leaves(-1);
                            }
                            else if (collider.tag == "Enemy_2")
                            {
                                pl_script.add_leaves(-2);
                            }
                            else if (collider.tag == "Enemy_boss")
                            {

                                pl_script.add_leaves(-3);
                            }

                            Destroy(collider.gameObject);
                            Curent_player.transform.position = pl_script.get_previus_position();
                            pl_script.switch_battle_mode();
                            mScript.set_current_move();
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
        }
    }

}
