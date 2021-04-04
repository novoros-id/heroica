using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public float pl_fl_x = 1.2f;
    public float pl_fl_z = 0.8f;
    private int current_move = 1;
    public GameObject[] player;
    public GameObject[] pr_hod;
    public GameObject selected1;
    public GameObject cam_focus;
    public bool Pc;

    public void Start()
    {
        move_priznak_step();

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Pc = true;
        }
        else
        {
            Pc = false;
        }
    }

    public int get_current_move()
    {
        return current_move;
    }

    public void set_current_move(string text_add = "")
    {
        // сдвинем ход 

        if (current_move == 4)
            current_move = 1;
        else
            current_move += 1;

        // передвинем знак хода
        move_priznak_step(text_add);

    }

    public void move_priznak_step(string text_add = "")
    {
        var myText = GameObject.Find("Text_").GetComponent<Text>();

        if (text_add != "")
        {
            add_text(text_add);
        }

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
                Instantiate(selected1, new Vector3(player[i].transform.position.x, 1.6f, player[i].transform.position.z), Quaternion.identity);
                //cam_focus.transform.position = new Vector3(player[i].transform.position.x, 0, player[i].transform.position.z);

                //text  myText;
               
                if (pl_script.get_battle_mode() == true)
                {
            
                    add_text("Режим боя " + player[i].name + " нажмите на кубик и узнаете исход боя");

                }
                else if (pl_script.recovery_mode == true)
                {
                    add_text("Режим восстановления здоровья " + player[i].name + " нажмите на кубик и будет добавлено столько здоровья, сколько выпало очков");
                }
                else
                {
                    if (pl_script.comp == true)
                    {
                        add_text("Текущий ход " + player[i].name + " нажмите на кубик, затем компьютер сам сделает ход");
                    }
                    else
                    {
                        add_text("Текущий ход " + player[i].name + " нажмите на кубик, затем нажмите на светящееся поле");
                    }
                }

                // text  Text_L;
                //var l_Text = GameObject.Find("Text_L").GetComponent<Text>();
                //l_Text.text = player[i].name + " жизней:" + pl_script.get_leaves();


                break;
            }

        }
    }

    public void add_text(string a_text)
    {
        var myText = GameObject.Find("Text_").GetComponent<Text>();
        myText.text = a_text + "\n" + "\n" + myText.text;
    }
}