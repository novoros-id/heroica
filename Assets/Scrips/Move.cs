﻿using System.Collections;
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
        //transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    void OnMouseDown()
    {
        move_player(transform.position);

    }

    public void move_player(Vector3 position_blue)
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        GameObject Curent_player = mScript.return_curent_player(); // нашли текущего игркока
        Player_ pl_script = Curent_player.GetComponent<Player_>();


        pl_script.set_previus_position(Curent_player.transform.position);

        pl_script.startTime = Time.time;
        pl_script.startMarker = Curent_player.transform;
        pl_script.endMarker = new Vector3(position_blue.x, 0.7f, position_blue.z);
        pl_script.journeyLength = Vector3.Distance(Curent_player.transform.position, new Vector3(position_blue.x, 0.7f, position_blue.z));
        pl_script.move = true;
        pl_script.SoundStep();
        // player[i].transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);
        // ЛОГИРУЕМ ХОД
        //GameLogger.Instance.Log($"Игрок {Curent_player.name} совершил ход на поле ({position_blue.x}, {position_blue.z})");
        GameLogger.Instance.Log(new List<string> { "сделал ход", Curent_player.name});

        ItemFromFloor(Curent_player, position_blue);
        clear_blue();

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
                //GameLogger.Instance.Log($"Игрок взял ключ");
                GameLogger.Instance.Log(new List<string> { "взял ключ"});
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
                //GameLogger.Instance.Log($"Игрок взял жизни");
                GameLogger.Instance.Log(new List<string> { "взял жизнь"});
                pl_script.add_leaves(1);
            }
            // pl_script.add_leaves(1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_luck")
        {
            //GameLogger.Instance.Log($"Игрок взял удачу");
            GameLogger.Instance.Log(new List<string> { "взял удачу"});
            pl_script.add_item("luck", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_speed")
        {
            //GameLogger.Instance.Log($"Игрок взял прибавку к скорости");
            GameLogger.Instance.Log(new List<string> { "взял скорость"});
            pl_script.add_item("speed", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_power")
        {
            //GameLogger.Instance.Log("Игрок взял прибавку к силе");
            GameLogger.Instance.Log(new List<string> { "взял силу"});
            pl_script.add_item("power", 1);
            Destroy(_items);
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_gold")
        {
            //GameLogger.Instance.Log($"Игрок взял золото");
            GameLogger.Instance.Log(new List<string> { "взял золото"});
            pl_script.add_item("gold", 1);
            pl_script.save_gold();
            Destroy(_items);
            // mScript.set_current_move();
        }
        else if (_items.tag == "item_axe")
        {
            //GameLogger.Instance.Log($"Игрок взял топор");
            GameLogger.Instance.Log(new List<string> { "взял топор"});
            pl_script.add_item("axe", 1);
            Destroy(_items);
            pl_script.set_CurWeapon();
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_baton")
        {
            //GameLogger.Instance.Log($"Игрок взял дубинку");
            GameLogger.Instance.Log(new List<string> { "взял дубинку"});
            pl_script.add_item("baton", 1);
            Destroy(_items);
            pl_script.set_CurWeapon();
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_scythe")
        {
            //GameLogger.Instance.Log($"Игрок взял косу");
            GameLogger.Instance.Log(new List<string> { "взял косу"});
            pl_script.add_item("scythe", 1);
            Destroy(_items);
            pl_script.set_CurWeapon();
            // mScript.set_current_move();
        }
        else if (_items.tag == "item_bow")
        {
            //GameLogger.Instance.Log($"Игрок взял лук");
            GameLogger.Instance.Log(new List<string> { "взял лук"});
            pl_script.add_item("bow", 1);
            Destroy(_items);
            pl_script.set_CurWeapon();
            //mScript.set_current_move();
        }
        else if (_items.tag == "item_dagger")
        {
            //GameLogger.Instance.Log($"Игрок взял кинжал");
            GameLogger.Instance.Log(new List<string> { "взял кинжал"});
            pl_script.add_item("dagger", 1);
            Destroy(_items);
            pl_script.set_CurWeapon();
            // mScript.set_current_move();
        }
        else if (_items.tag == "item_sword")
        {
            //GameLogger.Instance.Log($"Игрок взял меч");
            GameLogger.Instance.Log(new List<string> { "взял меч"});
            pl_script.add_item("sword", 1);
            Destroy(_items);
            pl_script.set_CurWeapon();
            //mScript.set_current_move();
        }
        // -------------------------------------
        else if (_items.tag == "Door")
        {
            //GameLogger.Instance.Log($"Игрок открыл дверь");
            GameLogger.Instance.Log(new List<string> { "открыл дверь"});
            pl_script.clear_key();
            Destroy(_items);
            //audiosrc.PlayOneShot(open);
            // mScript.set_current_move();
        }
        else if (_items.tag == "Enemy_1" || _items.tag == "Enemy_2" || _items.tag == "Enemy_boss")
        {
            //GameLogger.Instance.Log($"Игрок встретился с врагом");
            GameLogger.Instance.Log(new List<string> { "встретил врага"});
            //if (pl_script.get_battle_mode() == false) // это первый раз. Сразу ДРАКА
            //{
            //    mScript.move_priznak_step();
            //}
            //else
            //{
            //    mScript.set_current_move();
            //}

            pl_script.switch_battle_mode();

            if (pl_script.comp == true)
            {

                mScript.write_to_the_chat(player_, "Waiting_cube_before_the_fight");
            }
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