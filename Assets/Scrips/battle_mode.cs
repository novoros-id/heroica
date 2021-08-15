using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class battle_mode : MonoBehaviour
{

    public bool crystal_use;
    public GameObject[] player;
    public GameObject CubeButton;

    public bool Weapon_use;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        if (crystal_use == false && Weapon_use == false)
        {
            return;

        }

        // найдем врага который стоит на этом поле
        // уничтожим врага
        // переведем сюда игрока если это Рыцарь
        // сдвинем ход

        // сначала найдем текущего игрока


        // GameObject  Curent_player = return_curent_player(); // нашли текущего игркока

        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        GameObject Curent_player = mScript.return_curent_player(); // нашли текущего игркока
        Player_ pl_script = Curent_player.GetComponent<Player_>();


        // найдем врага который стоит на этой клетке

        Vector3 CurFloorPos;
        Collider[] colliders;

        CurFloorPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if ((colliders = Physics.OverlapSphere(CurFloorPos, 0.5f)).Length > 0)
        {

            foreach (var collider in colliders)
            {
                // обрабатываем только некоторые тэги

                if (collider.tag == "Enemy_1" ||
                    collider.tag == "Enemy_2" )

                {

                    // уничтожим его
                    GameObject enemy_distr = GameObject.Find(collider.name);
                    Destroy(enemy_distr);

                    // и сыграем музыку
                    GameObject cube = GameObject.Find("Cube");
                    ClickOnCube clicCube = cube.GetComponent<ClickOnCube>();
                    clicCube.play_victory_fight();


                }
            }


        }

        if (Curent_player.name == "Knight" && crystal_use == true)
        {
            Vector3 MovePlayerPosition = new Vector3(transform.position.x+0.4f,transform.position.y,transform.position.z);
            move_player(MovePlayerPosition);
        }
        else if (Curent_player.name == "Mage" || (Curent_player.name == "Knight" && Weapon_use == true))
        {
            //GameObject cam = GameObject.Find("Directional Light");
            //Main mScript = cam.GetComponent<Main>();
            mScript.set_current_move();

            //CubeButton = GameObject.Find("Button");
            //CubeButton.SetActive(true);
            pl_script.show_the_cube();
        }

        GameObject[] sword_ = GameObject.FindGameObjectsWithTag("crossed_sword");

        for (int b = 0; b < sword_.Length; b++)
        {
            Destroy(sword_[b]);

        }

    }


    public void move_player(Vector3 position_blue)
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();

        GameObject Curent_player = mScript.return_curent_player(); // нашли текущего игркока
        Player_ pl_script = Curent_player.GetComponent<Player_>();

        pl_script.set_previus_position(Curent_player.transform.position);

        pl_script.startTime = Time.time;
        pl_script.startMarker = Curent_player.transform;
        pl_script.endMarker = new Vector3(position_blue.x, 0.7f, position_blue.z);
        pl_script.journeyLength = Vector3.Distance(Curent_player.transform.position, new Vector3(position_blue.x, 0.7f, position_blue.z));
        pl_script.move = true;
        pl_script.SoundStep();

    }

    public void set_crystal_use()
    {
        crystal_use = true;
    }
    public void set_weapon_use()
    {
        Weapon_use = true;
    }
}
