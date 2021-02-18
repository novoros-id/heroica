using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour
{
    public int step_move;
    public bool key = false;
    public bool battle_mode = false;
    public bool comp;

    public int leaves = 4;

    public int blood = 0;
    public int luck = 0;
    public int speed = 0;
    public int power = 0;
    public int gold = 0;
    public int axe = 0;
    public int baton = 0;
    public int scythe = 0;
    public int bow = 0;
    public int dagger = 0;
    public int sword = 0;


    public Vector3 previus_position;

    // Start is called before the first frame update
    void Start()
    {
        if (name == "chr_knight")
        {
            comp = false;
        }
        else {
            comp = true;
        }
    }

    public bool get_comp()
    {
        return comp;
    }

    // Key
    // --------------------------------

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
    // --------------------------------

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
    // --------------------------------

    public Vector3 get_previus_position()
    {
        return previus_position;
    }

    public void set_previus_position(Vector3 pos_)
    {
        previus_position = pos_;
    }

    // leaves
    // ----------------------------------

    public int get_leaves()
    {
        return leaves;
    }

    public void add_leaves(int leaves_)
    {
        leaves += leaves_;
    }


    // item
    // ------------------------------
    public int get_item(string item)
    {
        if (item == "blood") return blood;
        else if(item == "luck") return luck;
        else if (item == "speed") return speed;
        else if (item == "power") return power;
        else if (item == "gold") return gold;
        else if (item == "axe") return axe;
        else if (item == "baton") return baton;
        else if (item == "scythe") return scythe;
        else if (item == "bow") return bow;
        else if (item == "dagger") return dagger;
        else if (item == "sword") return sword;

        return 0;

    }

    public void add_item(string item,int kol)
    {
        if (item == "blood")  blood += kol;
        else if (item == "luck") luck += kol;
        else if (item == "speed")  speed += kol;
        else if (item == "power")  power += kol;
        else if (item == "gold")  gold += kol;
        else if (item == "axe")  axe += kol;
        else if (item == "baton")  baton += kol;
        else if (item == "scythe")  scythe += kol;
        else if (item == "bow")  bow += kol;
        else if (item == "dagger")  dagger += kol;
        else if (item == "sword")  sword += kol;
  

    }

}
