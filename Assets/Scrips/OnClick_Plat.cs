using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_Plat : MonoBehaviour
{
    public Vector3 curpos;
    public GameObject[] player;
    public GameObject Curent_player;
    //public string CurFloorName;
    public GameObject[] Blue;

    void Start()
    {
        curpos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
        
    void OnMouseDown()
    {

        //Curent_player = return_curent_player(); // нашли текущего игркока
        //CurFloorName = Return_floor_player(new Vector3(Curent_player.transform.position.x, Curent_player.transform.position.y, Curent_player.transform.position.z));
        //// Debug.Log(name);

        //GameObject cam = GameObject.Find("Directional Light");
        //Main mScript = cam.GetComponent<Main>();
        //mScript.show_the_way(CurFloorName,name);

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

    public void clear_blue()
    {
        Blue = GameObject.FindGameObjectsWithTag("Blue");

        for (int b = 0; b < Blue.Length; b++)
        {
            Destroy(Blue[b]);
            //Debug.Log("a");
        }
    }
}
