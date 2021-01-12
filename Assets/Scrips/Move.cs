using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Player_ Cs;
    public GameObject[] Blue;
    public GameObject[] player;

    void Start()
    {
        
    }
    
    void OnMouseDown()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        player = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < player.Length; i++)
        {

            Player_ pl_script = player[i].GetComponent<Player_>();

            if (pl_script.step_move == mScript.get_current_move())
            {


                player[i].transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);

                Blue = GameObject.FindGameObjectsWithTag("Blue");

                for (int b = 0; b < Blue.Length; b++)
                {
                    Destroy(Blue[b]);
                }

                mScript.set_current_move();
                break;
            }

        }

    }
}
