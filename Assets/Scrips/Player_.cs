using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour
{
    public int step_move;
    private bool key = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    // Update is called once per frame
    void Update()
    {
        //GameObject cam = GameObject.Find("Directional Light");
        //Main mScript = cam.GetComponent<Main>();

        //if(step_move == mScript.get_current_move())
        //{
        //    Instantiate(selected1, new Vector3(transform.position.x, 2.5f, transform.position.z), Quaternion.identity);
        //}
    }
}
