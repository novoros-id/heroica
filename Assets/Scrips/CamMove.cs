using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Camera cam;
    Vector3 pos;
    
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    pos = Input.mousePosition;
        //    pos.z = 4.499992f;
        //    transform.position = (cam.ScreenToWorldPoint(pos));
        //}

        //if (Input.GetKeyDown("a"))
        //{
        //    transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        //}

        //if (Input.GetKeyDown("d"))
        //{
        //    transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        //}

        //if (Input.GetKeyDown("w"))
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        //}

        //if (Input.GetKeyDown("s"))
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        //}

    }
}
