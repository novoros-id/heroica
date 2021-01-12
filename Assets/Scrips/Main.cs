using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float pl_fl_x = 1.2f;
    public float pl_fl_z = 0.8f;
    private int current_move = 1;

    public int get_current_move()
    {
        return current_move;
    }

    public void set_current_move()
    {
        if (current_move == 4)
            current_move = 1;
        else
            current_move += 1;
    }
}
