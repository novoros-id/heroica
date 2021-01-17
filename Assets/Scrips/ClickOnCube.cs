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


    void Start()
    {
        
    }

    void OnMouseDown()
    {

        make_move();

    }

    public void make_move()
    {
        list_steps.Clear(); // очистили  технический list куда можно идти
        clear_blue();  // очистили все синие квадратики
        cube_step = throw_a_bone(); // кинули кубик
        Curent_player = return_curent_player(); // нашли текущего игркока
        if (Curent_player != null)
        {
            // нашли где он стоит
            CurFloorName = Return_floor_player(new Vector3(Curent_player.transform.position.x, Curent_player.transform.position.y, Curent_player.transform.position.z));
             // подсветили синими квадратиками
            if (CurFloorName != null)
            {
                Get_Steps(cube_step, CurFloorName);
            }
           
        }
    }

    public int throw_a_bone()
    {
        transform.Rotate(0, Random.Range(1, 100) * 90, 0);
        transform.Rotate(Random.Range(1, 100) * 90, 0, 0);
        transform.Rotate(0, 0, Random.Range(1, 10) * 90);

        Cube_ = GameObject.FindGameObjectsWithTag("Cube");

        max_name = "";
        max_position = 0;

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

    public void clear_blue()
    { 
        Blue = GameObject.FindGameObjectsWithTag("Blue");

        for (int b = 0; b < Blue.Length; b++)
        {
            Destroy(Blue[b]);
        }
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

    void Get_Steps(int steps_, string CurFloorName_)
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
            List<Collider> center = new List<Collider>();
            List<Collider> back = new List<Collider>();
            List<Collider> left = new List<Collider>();
            List<Collider> right = new List<Collider>();

            List<List<Collider>> myList = new List<List<Collider>>();

            foreach (var collider in colliders)
            {
                // обрабатываем только некоторые тэги

                if (collider.tag == "Player" || collider.tag == "Floor" || collider.tag == "Enemy" ||  collider.tag == "Item" || collider.tag == "Door")

                {

                    Vector3 pos1 = collider.transform.position - CurFloorPos;

                    //  Заполняем List направлений;

                    if (pos1.x > 0 && pos1.z == 0)
                    {
                        forward.Add(collider);
                    }

                    if (pos1.x == 0 && pos1.z == 0)
                    {
                        center.Add(collider);
                    }

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
            myList.Add(center);
            myList.Add(back);
            myList.Add(right);

            foreach (var l_n in myList)
            {

                // проверим, а есть ли куда наступать, что там на каждом направлениия

                s_step = Checking_Step(l_n, steps_);

                // вот тут проверка можно ли по этому направлению рассчитывать следующий ход
                // .................................


                if ((s_step[0] != null && steps_ > 0) ||                                            // есть куда ходить и есть шаги
                    (steps_ <= 0 && steps_ > -3 && s_step[1] != null && s_step[1].tag == "Player")) // если шаги закончились и на последнем ходе стоит игрок

                {
                    Get_Steps(steps_, s_step[0].name);
                }

            }

        }

    }

    public Collider[] Checking_Step(List<Collider> NextStep, int steps_)
    {

        // Если на floor дверь(закрыта) и нет ключа - пройти нельзя
        // Если на floor дверь(закрыта) и ключ есть - пройти можно(-ключ)
        // Если на floor дверь(открыта) - пройти можно
        // Если ты хочешь забрать item то ты наступаешь на этот floor и останавливаешься
        // + Если на floor стоит игрок и это последний шаг - идешь на след floor
        // Если рядом с полем монстр - останавливаешься на этом поле
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
                    Debug.Log("in step " + steps_ + "count = 1 in" + NextStep[0].name);
                    list_steps.Add(NextStep[0].name);
                    Instantiate(selected1, new Vector3(NextStep[0].transform.position.x, 0.05f, NextStep[0].transform.position.z), Quaternion.identity);

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
                list_steps.Add(FloorList.name);
                Debug.Log("in step " + steps_ + "tag = player in floor " + FloorList.name + " in item " + ItemList.name);
                return mas_return;
            }

            if (ItemList.tag == "Door") // если на кубике дверь
            {

                // получим статус двери
                Door_script d_script = ItemList.GetComponent<Door_script>();
                if (d_script.door_is_open() == true)
                {
                    list_steps.Add(FloorList.name);
                    Debug.Log("in step " + steps_ + "tag = door in floor " + FloorList.name + " in item " + ItemList.name);
                    return mas_return;
                }
                else
                {
                    //проверим есть ли у игрока ключ
                    Player_ pl_script = Curent_player.GetComponent<Player_>();

                    if (pl_script.get_key() == false) // ключа нет
                    {
                        mas_return[0] = null;
                        mas_return[1] = null;
                        return mas_return;
                    }
                    else // ключ есть
                    {
                        list_steps.Add(FloorList.name);
                        pl_script.clear_key();
                        Debug.Log("in step " + steps_ + "tag = door in floor " + FloorList.name + " in item " + ItemList.name);
                        return mas_return;
                    }

                }

                list_steps.Add(FloorList.name);
                Debug.Log("in step " + steps_ + "tag = player in floor " + FloorList.name + " in item " + ItemList.name);
                return mas_return;
            }

            if (FloorList.name != CurFloorName)
            {
                if (available_floor_in_list(FloorList.name) == false) //  если подсвечивали, то вернуть надо, а подсвечивать не надо
                {
                    list_steps.Add(FloorList.name);
                    Debug.Log("in step " + steps_ + " count = 2, floor " + FloorList.name + " in item " + ItemList.name);
                    Instantiate(selected1, new Vector3(NextStep[0].transform.position.x, 0.05f, NextStep[0].transform.position.z), Quaternion.identity);
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

}
