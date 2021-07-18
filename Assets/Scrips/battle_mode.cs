using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class battle_mode : MonoBehaviour
{

    public bool crystal_use;
    public GameObject[] player;
    public GameObject CubeButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (crystal_use == false)
        {
            return;

        }

        // найдем врага который стоит на этом поле
        // уничтожим врага
        // переведем сюда игрока если это Рыцарь
        // сдвинем ход

        // сначала найдем текущего игрока
        GameObject  Curent_player = return_curent_player(); // нашли текущего игркока
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

        if (Curent_player.name == "Knight")
        {
            move_player(transform.position);
        }
        else if (Curent_player.name == "Mage")
        {
            GameObject cam = GameObject.Find("Directional Light");
            Main mScript = cam.GetComponent<Main>();
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
        int current_move = mScript.get_current_move();
        player = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < player.Length; i++)
        {

            Player_ pl_script = player[i].GetComponent<Player_>();

            if (pl_script.step_move == current_move)
            {

                pl_script.set_previus_position(player[i].transform.position);

                pl_script.startTime = Time.time;
                pl_script.startMarker = player[i].transform;
                pl_script.endMarker = new Vector3(position_blue.x, 0.7f, position_blue.z);
                pl_script.journeyLength = Vector3.Distance(player[i].transform.position, new Vector3(position_blue.x, 0.7f, position_blue.z));
                pl_script.move = true;
                pl_script.SoundStep();
                // player[i].transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);

                break;
            }

        }
    }
    public GameObject return_curent_player()

    {
        player = GameObject.FindGameObjectsWithTag("Player");
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        int current_move = mScript.get_current_move();

        for (int i = 0; i < player.Length; i++)
        {

            Player_ pl_script = player[i].GetComponent<Player_>();

            if (pl_script.step_move == current_move)
            {

                return player[i];

            }

        }

        return null;
    }

    public void set_crystal_use()
    {
        crystal_use = true;
    }
}
