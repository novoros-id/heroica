using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour
{
    public int step_move;
    public bool key = false;
    public bool battle_mode = false;
    public Vector3 previus_position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Key

    public bool get_key()
    {
        return key;
    }

    public void set_key()
    {
        key = true;
    }

    public void clear_key()
    {
        key = false;
    }

    // battle mode
    public bool get_battle_mode()
    {
        return battle_mode;
    }

    public void switch_battle_mode()
    {
        if (battle_mode == true)
        {
            battle_mode = false;
        }
        else
        {
            battle_mode = true;
        }
    }

    // previus position
    public Vector3 get_previus_position()
    {
        return previus_position;
    }

    public void set_previus_position(Vector3 pos_)
    {
        previus_position = pos_;
    }

}
