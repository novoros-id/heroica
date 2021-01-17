using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_script : MonoBehaviour
{
    private bool door_open = false;

    public bool door_is_open()
    {

        return door_open;
    }

    public void set_status_door_open()
    {
        door_open = true;
    }
}
