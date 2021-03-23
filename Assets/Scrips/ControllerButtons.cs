using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerButtons : MonoBehaviour
{

    public GameObject ButtonsMenu;
    public GameObject Levels;
    public GameObject Back;
    public GameObject RoolsButtons;
    public int page;
    public GameObject Page1;
    public GameObject Page2;
    public GameObject Page3;



    
    // Start is called before the first frame update
    void Start()
    {
        Levels.SetActive(false);
        Back.SetActive(false);
        RoolsButtons.SetActive(false);
        Page2.SetActive(false);
        Page3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(page == 1)
        {
            Page1.SetActive(true);
            Page2.SetActive(false);
            Page3.SetActive(false);
        }
        if (page == 2)
        {
            Page1.SetActive(false);
            Page2.SetActive(true);
            Page3.SetActive(false);
        }
        if (page == 3)
        {
            Page1.SetActive(false);
            Page2.SetActive(false);
            Page3.SetActive(true);
        }
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Test");
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
        RoolsButtons.SetActive(false);
    }

    public void Rools()
    {
        RoolsButtons.SetActive(true);
        Back.SetActive(true);
        ButtonsMenu.SetActive(false);
    }

    public void NextPage()
    {
        if(page < 3)
        {
            page++;
        }
        else
        {
            page = 1;
        }
    }

    public void Rate()
    {
        Application.OpenURL("https://forms.gle/A2k1LEMybaXwfGei7");
    }
}


