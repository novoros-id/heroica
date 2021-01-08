using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    public Main bs;
    public Instantiant massive;
    public bool clicked = false;
    public Vector3 pos;
    public string CurFloorName;
    public GameObject[] Blue;


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

        Blue = GameObject.FindGameObjectsWithTag("Blue");
 

        if (Blue.Length > 0)
        {
            for (int b = 0; b < Blue.Length; b++)
            {
                Destroy(Blue[b]);
            }
        }
        else
        {
        
            Return_floor_player(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            Get_Steps(2, CurFloorName);

        }
        
    }


    private void Return_floor_player(Vector3 pos)
    {
            GameObject[] Floors;
            float x_f = pos.x - bs.pl_fl_x;
            float z_f = pos.z + bs.pl_fl_z;

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
     
    }


    void Get_Steps(int steps_, string CurFloorName_)
    {
        GameObject CurFloor;
        Vector3 CurFloorPos;
        Collider[] colliders;
        bool step_true;

        steps_ -= 1;

        CurFloor = GameObject.Find(CurFloorName_);
        CurFloorPos = new Vector3(CurFloor.transform.position.x, CurFloor.transform.position.y, CurFloor.transform.position.z);

        if ((colliders = Physics.OverlapSphere(CurFloorPos, 2)).Length > 0)
        {
            foreach (var collider in colliders)
            {
                //Тут проверка можно ли суда идти

                step_true = true;

                if (step_true == true && steps_ >= 0 && collider.name != CurFloorName_)
                {
                    Debug.Log("шаг " + steps_ + " из точки " + CurFloorName_ + " в точку " + collider.name);
                    Instantiate(massive.selected, new Vector3(collider.transform.position.x, 1.9f, collider.transform.position.z), Quaternion.identity);
                    Get_Steps(steps_, collider.name);
                    
                }


            }
        }

    }


}
