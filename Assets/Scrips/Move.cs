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

                pl_script.set_previus_position(player[i].transform.position);
                player[i].transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);

                ItemFromFloor(player[i], transform.position);
                clear_blue();

                break;
            }

        }

        void ItemFromFloor(GameObject player_, Vector3 position_)
        {

            // найдем расположено ли на этой клетке что-то
            // если расположено, то выполним действия и удалим этот объект с поля

            GameObject _items = return_tag_item_on_position(position_);
            Player_ pl_script = player_.GetComponent<Player_>();

            //GameObject cam = GameObject.Find("Directional Light");
            //Main mScript = cam.GetComponent<Main>();

            if (_items == null)
            {
                // ничего не делаем
                mScript.set_current_move();
            }
            else if (_items.tag == "Key")
            {
                pl_script.set_key();
                Destroy(_items);
                mScript.set_current_move();
            }
            else if (_items.tag == "Door")
            {
                pl_script.clear_key();
                Destroy(_items);
                mScript.set_current_move();
            }
            else if (_items.tag == "Enemy_1")
            {
                if (pl_script.get_battle_mode() == false) // это первый раз. Сразу ДРАКА
                {
                    mScript.move_priznak_step();
                }
                else
                {
                    mScript.set_current_move();
                }

                pl_script.switch_battle_mode();
                
                
                // если первый раз
                // Destroy(_items);
            }

        }

        GameObject return_tag_item_on_position (Vector3 player_position)
        {
            //string tag_item = "";
            string[] tag_item = new string[] { "Key", "Door", "Enemy_1" };

            foreach (string tag in tag_item)
            {

                GameObject[] item = GameObject.FindGameObjectsWithTag(tag);

                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i].transform.position.x == player_position.x && item[i].transform.position.z == player_position.z)
                    {
                        return item[i];
                    }
                }


            }


            return null;

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
