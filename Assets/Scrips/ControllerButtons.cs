using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButtons : MonoBehaviour
{

    public GameObject ButtonsMenu;
    public GameObject Levels;
    public GameObject Back;
    
    // Start is called before the first frame update
    void Start()
    {
        Levels.SetActive(false);
        Back.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel1()
    {
        Application.LoadLevel("Test");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ChoozeLvl()
    {
        ButtonsMenu.SetActive(false);
        Levels.SetActive(true);
        Back.SetActive(true);
    }
    public void Menu()
    {
        ButtonsMenu.SetActive(true);
        Levels.SetActive(false);
        Back.SetActive(false);
    }
}
