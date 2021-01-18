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
        int current_move = mScript.get_current_move();

        player = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < player.Length; i++)
        {

            Player_ pl_script = player[i].GetComponent<Player_>();

            if (pl_script.step_move == current_move)
            {

                player[i].transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);

                addItemFromFloor(player[i], transform.position);

                Blue = GameObject.FindGameObjectsWithTag("Blue");

                for (int b = 0; b < Blue.Length; b++)
                {
                    Destroy(Blue[b]);
                }

                mScript.set_current_move();
                break;
            }

        }

        void addItemFromFloor(GameObject player_, Vector3 position_)
        {

            // найдем расположено ли на этой клетке что-то
            // если расположено, то добавим игроку и удалим этот объект с поля

            GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");

            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].transform.position.x == position_.x && keys[i].transform.position.z == position_.z)
                {
                    Player_ pl_script = player_.GetComponent<Player_>();
                    pl_script.set_key();
                    Destroy(keys[i]);
                    break;
                }
            }
        }
    }
}
