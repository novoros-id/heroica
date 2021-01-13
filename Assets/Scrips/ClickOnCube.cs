using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickOnCube : MonoBehaviour
{

    public GameObject[] Cube_;
    public string max_name;
    public float max_position;
    public int cube_step;


    public Instantiant massive;
    public bool clicked = false;
    public Vector3 pos;
    public string CurFloorName;
    public GameObject[] Blue;
    public GameObject selected1;
    public GameObject[] player;


    void Start()
    {
        
    }

    void OnMouseDown()
    {

        cube_step = throw_a_bone();
        show_where_to_go();

    }

    int throw_a_bone()
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

    void show_where_to_go()
    {
        // получим количество шагов (сколько вышло на кубике)
        // получим floor на котором стоит человек - Поле_расчета

        // шаги N (для каждого Поля_Расчета)
        // получим куда можно пойти (с учектом шагов)
        // подсветим их синим - это новые поля расчета
        // для каждого поля расчета расчитаем следующий шаг

        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        Blue = GameObject.FindGameObjectsWithTag("Blue");

        for (int b = 0; b < Blue.Length; b++)
            {
                Destroy(Blue[b]);
            }


        player = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < player.Length; i++)
            {

            Player_ pl_script = player[i].GetComponent<Player_>();

            if (pl_script.step_move == mScript.get_current_move())
            {
                Return_floor_player(new Vector3(player[i].transform.position.x, player[i].transform.position.y, player[i].transform.position.z));
                     Get_Steps(cube_step, CurFloorName);
            }

        }

  
    }

    private void Return_floor_player(Vector3 pos)
    {
        GameObject[] Floors;
        float x_f = pos.x - 0;
        float z_f = pos.z + 0;

        Floors = GameObject.FindGameObjectsWithTag("Floor");

        for (int i = 0; i < Floors.Length; i++)
        {
            if (Floors[i].transform.position.x == x_f && Floors[i].transform.position.z == z_f)
            {
                CurFloorName = Floors[i].name;
                //Debug.Log(Floors[i].name);
                break;
            }
        }

        if (CurFloorName == "")
        {
            CurFloorName = GameObject.Find("StartFloor").name;
        }

    }

    void Get_Steps(int steps_, string CurFloorName_)
    {
        GameObject CurFloor;
        Vector3 CurFloorPos;
        Collider[] colliders;
        string [] m_step_tag = new string [5] { "Player", "Floor", "Enemy", "Item" , "Door"};

        steps_ -= 1;

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
            Collider s_step;
            Vector3 pos1;

            foreach (var collider in colliders)
            {
                if (collider.tag == "Player" || collider.tag == "Floor" || collider.tag == "Enemy" ||  collider.tag == "Item" || collider.tag == "Door")

                {

                    pos1 = collider.transform.position - CurFloorPos;

                    //  Заполняем массивы направлений;

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

            //  Определяем можно ли идти по направлениям;

            s_step = Checking_Step(forward);
            if (s_step != null && steps_ > 0)
            {
                Get_Steps(steps_, s_step.name);
            }

            s_step = Checking_Step(back);
            if (s_step != null && steps_ > 0)
            {
                Get_Steps(steps_, s_step.name);
            }

            s_step = Checking_Step(right);
            if (s_step != null && steps_ > 0)
            {
                Get_Steps(steps_, s_step.name);
            }

            s_step = Checking_Step(left);
            if (s_step != null && steps_ > 0)
            {
                Get_Steps(steps_, s_step.name);
            }
        }

    }

    public Collider Checking_Step(List<Collider> NextStep)
    {

        // Если на floor дверь(закрыта) и нет ключа - пройти нельзя
        // Если на floor дверь(закрыта) и ключ есть - пройти можно(-ключ)
        // Если на floor дверь(открыта) - пройти можно
        // Если ты хочешь забрать item то ты наступаешь на этот floor и останавливаешься
        // Если на floor стоит игрок и это последний шаг - идешь на след floor
        // Если рядом с полем монстр - останавливаешься на этом поле
        // 
        Collider FloorList = null;
        Collider ItemList = null;
        // string Blue = "Blue1(Clone)";

        if (NextStep.Count == 0)
        {
            return null;
        }
        if (NextStep.Count == 1)
        {
            if (NextStep[0].name != CurFloorName)
            {
        
                FloorList = NextStep[0];
                Instantiate(selected1, new Vector3(NextStep[0].transform.position.x, 0.05f, NextStep[0].transform.position.z), Quaternion.identity);
                return NextStep[0];
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

            if (ItemList.tag == "Player")
            {
                return FloorList;
            }

            if (FloorList.name != CurFloorName)
            {
                Instantiate(selected1, new Vector3(NextStep[0].transform.position.x, 0.05f, NextStep[0].transform.position.z), Quaternion.identity);
                return FloorList;
            }
            
        }
        return null;

    }

}
