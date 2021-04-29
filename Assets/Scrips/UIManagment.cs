using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagment : MonoBehaviour
{

    Text hearts;
    public Player_ pl;
    public GameObject key;
    public GameObject ExitGameButton;
    //public Text TextEndGame;
    //public GameObject Cube;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        hearts = GetComponent<Text>();
        ExitGameButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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

        hearts.text = pl.leaves.ToString();

        //TextEndGame.text = "Congratulation!"
        //    +cb.Curent_player.name+" win!";
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("Start");
    }
}
