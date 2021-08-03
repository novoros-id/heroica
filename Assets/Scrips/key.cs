using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    void OnMouseDown()
    {

        //Curent_player = return_curent_player(); // нашли текущего игркока
        //CurFloorName = Return_floor_player(new Vector3(Curent_player.transform.position.x, Curent_player.transform.position.y, Curent_player.transform.position.z));
        //// Debug.Log(name);

        //GameObject cam = GameObject.Find("Directional Light");
        //Main mScript = cam.GetComponent<Main>();
        //mScript.show_the_way(CurFloorName,name);
        GameObject[] Blue = GameObject.FindGameObjectsWithTag("Blue");

        for (int i = 0; i < Blue.Length; i++)
        {
            if (Mathf.Abs(Blue[i].transform.position.x - transform.position.x) < 0.01 && Mathf.Abs(Blue[i].transform.position.z - transform.position.z) < 0.01)
            {
                Move mScript = Blue[i].GetComponent<Move>();
                mScript.move_player(Blue[i].transform.position);
               //  Debug.Log("move player");
            }
        }


    }
}
