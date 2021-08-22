using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    public string LevelName;

    public void StartLevel_()
    {
        Application.LoadLevel(LevelName);
    }
}
