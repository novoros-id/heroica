using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Player_ Cs;
    public GameObject[] Blue;
    public GameObject[] player;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    void OnMouseDown()
    {
        move_player(transform.position);

    }

    public void move_player(Vector3 position_blue)
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

                pl_script.startTime = Time.time;
                pl_script.startMarker = player[i].transform;
                pl_script.endMarker = new Vector3(position_blue.x, 0.7f, position_blue.z);
                pl_script.journeyLength = Vector3.Distance(player[i].transform.position, new Vector3(position_blue.x, 0.7f, position_blue.z));
                pl_script.move = true;
                pl_script.SoundStep();
                // player[i].transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);

                ItemFromFloor(player[i], position_blue);
                clear_blue();

                //Debug.Log("a");
                break;
            }

        }
    }

    public void clear_blue()
    {
        Blue = GameObject.FindGameObjectsWithTag("Blue");
        //audiosrc.PlayOneShot(step);
        for (int b = 0; b < Blue.Length; b++)
        {
            //audiosrc.PlayOneShot(step);
            Destroy(Blue[b]);

        }
    }

    public void ItemFromFloor(GameObject player_, Vector3 position_)

    {

        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        // найдем расположено ли на этой клетке что-то
        // если расположено, то выполним действия и удалим этот объект с поля

        GameObject _items = mScript.return_tag_item_on_position(position_);
        Player_ pl_script = player_.GetComponent<Player_>();

        //GameObject cam = GameObject.Find("Directional Light");
        //Main mScript = cam.GetComponent<Main>();

        if (_items == null)
        {
            // ничего не делаем
            // mScript.set_current_move();
        }
        else if (_items.tag == "Key")
        {
            if (pl_script.get_key() == false)
            {
                pl_script.set_key();
                Destroy(_items);
               // pl_script.define_goal();
            }
            //mScript.set_current_move();
        }
        // --------------------------------------
        else if (_items.tag == "item_blood")
        {
            if (pl_script.get_leaves() != 4)
            {
                pl_script.add_leaves(1);
            }
            // pl_script.add_leaves(1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_luck")
        {
            pl_script.add_item("luck", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_speed")
        {
            pl_script.add_item("speed", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_power")
        {
            pl_script.add_item("power", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_gold")
        {
            pl_script.add_item("gold", 1);
            Destroy(_items);
            // mScript.set_current_move();
        }
        else if (_items.tag == "item_axe")
        {
            pl_script.add_item("axe", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_baton")
        {
            pl_script.add_item("baton", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_scythe")
        {
            pl_script.add_item("scythe", 1);
            Destroy(_items);
            // mScript.set_current_move();
        }
        else if (_items.tag == "item_bow")
        {
            pl_script.add_item("bow", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_dagger")
        {
            pl_script.add_item("dagger", 1);
            Destroy(_items);
            // mScript.set_current_move();
        }
        else if (_items.tag == "item_sword")
        {
            pl_script.add_item("sword", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        // -------------------------------------
        else if (_items.tag == "Door")
        {
            pl_script.clear_key();
            Destroy(_items);
            //audiosrc.PlayOneShot(open);
            // mScript.set_current_move();
        }
        else if (_items.tag == "Enemy_1" || _items.tag == "Enemy_2" || _items.tag == "Enemy_boss")
        {
            //if (pl_script.get_battle_mode() == false) // это первый раз. Сразу ДРАКА
            //{
            //    mScript.move_priznak_step();
            //}
            //else
            //{
            //    mScript.set_current_move();
            //}

            pl_script.switch_battle_mode();


            // если первый раз
            // Destroy(_items);
        }

    }

    //public GameObject return_tag_item_on_position(Vector3 player_position)
    //{
    //    //string tag_item = "";
    //    Vector3 CurFloorPos;
    //    Collider[] colliders;

    //    CurFloorPos = new Vector3(player_position.x, player_position.y, player_position.z);

    //    //Debug.Log("Текущая позиция платформы" + CurFloorPos);

    //    if ((colliders = Physics.OverlapSphere(CurFloorPos, 0.5f)).Length > 0)
    //    {

    //        foreach (var collider in colliders)
    //        {
    //            // обрабатываем только некоторые тэги

    //            if (collider.tag == "Enemy_1" ||
    //                collider.tag == "Enemy_2" ||
    //                collider.tag == "Enemy_boss" ||
    //                collider.tag == "Key" ||
    //                collider.tag == "Door" ||
    //                collider.tag == "item_blood" ||
    //                collider.tag == "item_luck" ||
    //                collider.tag == "item_speed" ||
    //                collider.tag == "item_power" ||
    //                collider.tag == "item_gold" ||
    //                collider.tag == "item_axe" ||
    //                collider.tag == "item_baton" ||
    //                collider.tag == "item_scythe" ||
    //                collider.tag == "item_bow" ||
    //                collider.tag == "item_dagger" ||
    //                collider.tag == "item_sword")

    //            {

    //                return collider.gameObject;


    //            }
    //        }
    //    }


    //    //string[] tag_item = new string[] { "Key", "Door", "Enemy_1","Enemy_2","Enemy_boss","item_blood"
    //    //                                  , "item_luck", "item_speed", "item_power", "item_gold", "item_axe", "item_baton"
    //    //                                  , "item_scythe", "item_bow", "item_dagger", "item_sword"};

    //    //foreach (string tag in tag_item)
    //    //{

    //    //    GameObject[] item = GameObject.FindGameObjectsWithTag(tag);

    //    //    for (int i = 0; i < item.Length; i++)
    //    //    {
    //    //        if (item[i].transform.position.x == player_position.x && item[i].transform.position.z == player_position.z)
    //    //        {
    //    //            return item[i];
    //    //        }
    //    //    }


    //    //}


    //    return null;

    //}


}