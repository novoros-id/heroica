using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_button_script : MonoBehaviour
{

    public bool cube_is_available;

    // Start is called before the first frame update
    void Start()
    {
        cube_is_available = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reverse_cube_aviable()
    {
        if (cube_is_available == true)
        {
            cube_is_available = false;
        }
        else
        {
            cube_is_available = true;
        }
    }

    public bool return_cube_is_aviable()
    {
        return cube_is_available;
    }
}
