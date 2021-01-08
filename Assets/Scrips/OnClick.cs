using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    public Main bs;
    public Instantiant massive;
    public bool clicked = false;
    public Vector3 pos;
    public int steps = 3;
    public string CurFloorName;


    
    
    void Update()
    {
        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    
    void OnMouseDown()
	{
        GameObject[] Blue;
        //clicked = true;

        // получим количество шагов (сколько вышло на кубике)
        // получим floor на котором стоит человек - Поле_расчета

        // шаги N (для каждого Поля_Расчета)
        // получим куда можно пойти (с учектом шагов)
        // подсветим их синим - это новые поля расчета
        // для каждого поля расчета расчитаем следующий шаг

        Return_floor_player(pos);
        Get_Steps(steps,CurFloorName);

        //Blue = GameObject.FindGameObjectsWithTag("Blue");
        //Debug.Log(Blue.Length);

        //for (int b = 0; b < Blue.Length; b++)
        //{
        //    Debug.Log(b);
        //}

    }

    void Return_floor_player(Vector3 pos)
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

        Vector3 ForwardFloorPos;
        Vector3 CurFloorPos;
        GameObject CurFloor;
        string forwardFloorName = "";
        GameObject[] Floors;
        GameObject[] Blues;



        steps_ = steps_ - 1;
        //Debug.Log(steps_);


        CurFloor = GameObject.Find(CurFloorName_);
        CurFloorPos = new Vector3(CurFloor.transform.position.x, CurFloor.transform.position.y, CurFloor.transform.position.z);

        // Обработка Forward

        ForwardFloorPos = new Vector3(CurFloor.transform.position.x+0.8f, CurFloor.transform.position.y, CurFloor.transform.position.z);

        Floors = GameObject.FindGameObjectsWithTag("Floor");
        Blues = GameObject.FindGameObjectsWithTag("Blue");

        for (int i = 0; i < Floors.Length; i++)
        {
            if (Floors[i].transform.position.x == ForwardFloorPos.x && Floors[i].transform.position.z == ForwardFloorPos.z)
            {
                forwardFloorName = Floors[i].name;
                Instantiate(massive.selected, new Vector3(Floors[i].transform.position.x, 1.9f, Floors[i].transform.position.z), Quaternion.identity);
                //Blues[i].SetActive(true);
                //Debug.Log(Floors[i].name);
                break;
            }
        }

        if(forwardFloorName != "" && steps_>=0)
        {
            //Debug.Log(steps_);
            //Debug.Log(forwardFloorName);
            
            Get_Steps(steps_, forwardFloorName);
        }

    }

}
