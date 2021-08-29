using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalCount : MonoBehaviour
{
    Text Count;
    GameObject cb;
    ClickOnCube cbScript;

    // Start is called before the first frame update
    void Start()
    {
        Count = GetComponent<Text>();
        cb = GameObject.Find("Cube");
        cbScript = cb.GetComponent<ClickOnCube>();
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject cb = GameObject.Find("Cube");
        //ClickOnCube cbScript = cb.GetComponent<ClickOnCube>();
        Count.text = cbScript.count_magic_crystall.ToString();
    }

    public void OnClick()
    {
        // найдем текущего игрока
        // проверим что игрок еще не кинул кубик
        // полуич скрипт кубика
        // установим вариант кубика на 4
        // и вызовем функцию


        if (cbScript.count_magic_crystall >= 25 && blue_aviable() == false)
        {
            cbScript.count_magic_crystall -= 25;
            cbScript.cube_step_crystall = 4;
            cbScript.save_count_crystal(cbScript.count_magic_crystall);
            GameObject cam = GameObject.Find("Directional Light");
            Main mScript = cam.GetComponent<Main>();
            GameObject Curent_player = mScript.return_curent_player(); // нашли текущего игркока
            Player_ pl_script = Curent_player.GetComponent<Player_>();
            pl_script.play_sound_use_crystal();

            cbScript.make_move();
        }

        //GameObject cb = GameObject.Find("Cube");
        //ClickOnCube cbScript = cb.GetComponent<ClickOnCube>();
    }

    private bool blue_aviable()
    {
        GameObject[] Blue = GameObject.FindGameObjectsWithTag("Blue");
        //audiosrc.PlayOneShot(step);
        if (Blue.Length == 0)
        {
            return false;
        }

        return true;
    }
}
