using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagment : MonoBehaviour
{

    Text hearts;
    public Text coins;
    public Player_ pl;
    public GameObject key;
    public GameObject ExitGameButton;
    public GameObject cross;
    //public Text TextEndGame;
    //public GameObject Cube;




    // Start is called before the first frame update
    void Start()
    {
        hearts = GetComponent<Text>();
        ExitGameButton.SetActive(false);
        cross.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        int current_move = mScript.get_current_move();
        //ClickOnCube cb = Cube.GetComponent<ClickOnCube>();

        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGameButton.SetActive(true);
        }
        
        if (pl.key == true)
        {
            key.SetActive(true);
        }

        if (pl.key == false)
        {
            key.SetActive(false);
        }

        if (pl.recovery_mode == true)
        {
            cross.SetActive(true);
        }

        if (pl.recovery_mode == false)
        {
            cross.SetActive(false);
        }

        hearts.text = pl.leaves.ToString();
        coins.text = pl.gold.ToString();

        //TextEndGame.text = "Congratulation!"
        //    +cb.Curent_player.name+" win!";
        
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("Start");
    }
}
