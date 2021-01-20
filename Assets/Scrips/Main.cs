using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float pl_fl_x = 1.2f;
    public float pl_fl_z = 0.8f;
    private int current_move = 1;
    public GameObject[] player;
    public GameObject[] pr_hod;
    public GameObject selected1;
    public GameObject cam_focus;

    public void Start()
    {
        move_priznak_step();
    }

    public int get_current_move()
    {
        return current_move;
    }

    public void set_current_move()
    {
        // сдвинем ход 

        if (current_move == 4)
            current_move = 1;
        else
            current_move += 1;

        // передвинем знак хода
        move_priznak_step();

    }

    public void move_priznak_step()
    {

        // очистим значок хода

        pr_hod = GameObject.FindGameObjectsWithTag("pr_hod");

        for (int b = 0; b < pr_hod.Length; b++)
        {
            Destroy(pr_hod[b]);
        }

        player = GameObject.FindGameObjectsWithTag("Player");
        cam_focus = GameObject.Find("CameraFocus");

        for (int i = 0; i < player.Length; i++)
        {

            Player_ pl_script = player[i].GetComponent<Player_>();

            if (pl_script.step_move == current_move)
            {
                Instantiate(selected1, new Vector3(player[i].transform.position.x, 1.4f, player[i].transform.position.z), Quaternion.identity);
                cam_focus.transform.position = new Vector3(player[i].transform.position.x, 0, player[i].transform.position.z);
            }

        }
    }
}
