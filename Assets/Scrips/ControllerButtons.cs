using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void StartLevel1()
    {
        SceneManager.LoadScene("Test");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void StartLevel4()
    {
        SceneManager.LoadScene("Level4");
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
    public void Rate()
    {
        Application.OpenURL("https://www.instagram.com/invites/contact/?i=skg3kios5rk3&utm_content=lp3lin0");
    }
}


