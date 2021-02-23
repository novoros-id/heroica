using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    public GameObject EndGameMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        EndGameMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame1()
    {
        SceneManager.LoadScene("Start");
    }
    public void EndGameOn()
    {
        EndGameMenu.SetActive(true);
    }
}
