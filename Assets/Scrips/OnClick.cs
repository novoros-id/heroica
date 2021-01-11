using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    //public Main bs;
    //public Instantiant massive;
    //public bool clicked = false;
    //public Vector3 pos;
    //public string CurFloorName;
    //public GameObject[] Blue;
    //public GameObject selected1;
    public int step_move;






    void Update()
    {
        //pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void OnMouseDown()
    {

        // получим количество шагов (сколько вышло на кубике)
        // получим floor на котором стоит человек - Поле_расчета

        // шаги N (для каждого Поля_Расчета)
        // получим куда можно пойти (с учектом шагов)
        // подсветим их синим - это новые поля расчета
        // для каждого поля расчета расчитаем следующий шаг

        //Blue = GameObject.FindGameObjectsWithTag("Blue");



        //if (Blue.Length > 0)
        //{
        //    for (int b = 0; b < Blue.Length; b++)
        //    {
        //        Destroy(Blue[b]);
        //    }
        //}
        //else
        //{

        //    Return_floor_player(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        //    Get_Steps(3, CurFloorName);

        //}

    }


    //private void Return_floor_player(Vector3 pos)
    //{
    //    GameObject[] Floors;
    //    float x_f = pos.x - 0;
    //    float z_f = pos.z + 0;

    //    Floors = GameObject.FindGameObjectsWithTag("Floor");

    //    for (int i = 0; i < Floors.Length; i++)
    //    {
    //        if (Floors[i].transform.position.x == x_f && Floors[i].transform.position.z == z_f)
    //        {
    //            CurFloorName = Floors[i].name;
    //            //Debug.Log(Floors[i].name);
    //            break;
    //        }
    //    }

    //}


    //void Get_Steps(int steps_, string CurFloorName_)
    //{
    //    GameObject CurFloor;
    //    Vector3 CurFloorPos;
    //    Collider[] colliders;
    //    bool step_true;
    //    string Blue = "Blue1(Clone)";

    //    steps_ -= 1;

    //    CurFloor = GameObject.Find(CurFloorName_);
    //    CurFloorPos = new Vector3(CurFloor.transform.position.x, CurFloor.transform.position.y, CurFloor.transform.position.z);

    //    //Debug.Log("Текущая позиция платформы" + CurFloorPos);

    //    if ((colliders = Physics.OverlapSphere(CurFloorPos, 1)).Length > 0)
    //    {

    //        List<Collider> forward = new List<Collider>();
    //        List<Collider> center = new List<Collider>();
    //        List<Collider> back = new List<Collider>();
    //        List<Collider> left = new List<Collider>();
    //        List<Collider> right = new List<Collider>();

    //        Collider s_step;


    //        //GameObject[] center;
    //        //GameObject[] forward;
    //        //GameObject[] back;
    //        //GameObject[] left;
    //        //GameObject[] right;
    //        Vector3 pos1;

    //        foreach (var collider in colliders)
    //        {

    //            pos1 = collider.transform.position - CurFloorPos;

    //            //  Заполняем массивы направлений;

    //            if (pos1.x > 0 && pos1.z == 0)
    //            {
    //                forward.Add(collider);
    //            }

    //            if (pos1.x == 0 && pos1.z == 0)
    //            {
    //                center.Add(collider);
    //            }

    //            if (pos1.x < 0 && pos1.z == 0)
    //            {
    //                back.Add(collider);
    //            }

    //            if (pos1.z > 0 && pos1.x == 0)
    //            {
    //                left.Add(collider);
    //            }

    //            if (pos1.z < 0 && pos1.x == 0)
    //            {
    //                right.Add(collider);
    //            }

                


    //            //Тут проверка можно ли суда идти
    //            //Debug.Log(collider);
    //            //Debug.Log(collider.transform.position);

    //            //Debug.Log(collider);
    //            //Debug.Log(collider.transform.position);
    //            //Debug.Log(Vector3.Distance(CurFloorPos, collider.transform.position));
    //            //step_true = true;

    //            //if (step_true == true && steps_ >= 0 && collider.name != CurFloorName_ && collider.name != Blue && collider.name != "player")
    //            //{
    //            //    Debug.Log("шаг " + steps_ + " из точки " + CurFloorName_ + " в точку " + collider.name);
    //            //    Instantiate(selected1, new Vector3(collider.transform.position.x, 0.05f, collider.transform.position.z), Quaternion.identity);
    //            //    Get_Steps(steps_, collider.name);

    //            //}



    //        }

    //        //  Определяем можно ли идти по направлениям;

    //        s_step = Return_Step(forward);

    //        if (s_step != null && steps_ > 0)
    //        {
    //            Get_Steps(steps_, s_step.name);
    //        }

    //        s_step = Return_Step(back);

    //        if (s_step != null && steps_ > 0)
    //        {
    //            Get_Steps(steps_, s_step.name);
    //        }
    //        s_step = Return_Step(right);

    //        if (s_step != null && steps_ > 0)
    //        {
    //            Get_Steps(steps_, s_step.name);
    //        }
    //        s_step = Return_Step(left);

    //        if (s_step != null && steps_ > 0)
    //        {
    //            Get_Steps(steps_, s_step.name);
    //        }
    //    }

    //}

    //public Collider Return_Step(List<Collider> NextStep)
    //{

    //    // Если на floor дверь(закрыта) и нет ключа - пройти нельзя
    //    // Если на floor дверь(закрыта) и ключ есть - пройти можно(-ключ)
    //    // Если на floor дверь(открыта) - пройти можно
    //    // Если ты хочешь забрать item то ты наступаешь на этот floor и останавливаешься
    //    // Если на floor стоит игрок и это последний шаг - идешь на след floor
    //    // Если рядом с полем монстр - останавливаешься на этом поле
    //    // 

    //    Collider FloorList = null;
    //    Collider ItemList;
        
    //    if (NextStep.Count == 0)
    //    {
    //        return null;
    //    }
    //    if (NextStep.Count == 1)
    //    {
    //        Instantiate(selected1, new Vector3(NextStep[0].transform.position.x, 0.05f, NextStep[0].transform.position.z), Quaternion.identity);
    //        return NextStep[0];
    //    }
    //    if (NextStep.Count >1)
    //    {
    //        foreach (var l in NextStep)
    //        {
    //            if (l.tag == "Floor")
    //            {
    //                Instantiate(selected1, new Vector3(NextStep[0].transform.position.x, 0.05f, NextStep[0].transform.position.z), Quaternion.identity);
    //                FloorList = l;
    //            }
    //            else
    //            {
    //                ItemList = l;
    //            }
    //        }

    //        return FloorList;
    //    }
    //    return null;
        
    //}


}
