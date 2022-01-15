using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject LanButton;
    public Sprite LanButtonRu;
    public Sprite LanButtonEn;
    public GameObject SettingsUI;

    void Start()
    {
        SettingsUI.SetActive(false);
    }
    public void ChangeIcon()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        if (mScript.lang == "ru")
        {
            LanButton.GetComponent<Image>().sprite = LanButtonRu;
        }
        else if (mScript.lang == "en")
        {
            LanButton.GetComponent<Image>().sprite = LanButtonEn;
        }
    }
    public void ChangeLang()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        if (mScript.lang == "ru")
        {
            LanButton.GetComponent<Image>().sprite = LanButtonEn;
        }
        else if (mScript.lang == "en")
        {
            LanButton.GetComponent<Image>().sprite = LanButtonRu;
        }
        mScript.SetLang();
    }
    public void SettingsOff()
    {
        SettingsUI.SetActive(false);
    }
    public void SettingsOn()
    {
        SettingsUI.SetActive(true);
        ChangeIcon();
    }
    public void CreateLevel()
    {
        Application.LoadLevel("CreateLevel");
    }
    public void LoadLevel_()
    {
        Application.LoadLevel("Created_level");
    }

    public void ResetCrystalCount()
    {
        if (SceneManager.GetActiveScene().name != "Start")
        {
            GameObject cb = GameObject.Find("Cube");
            ClickOnCube cbScript = cb.GetComponent<ClickOnCube>();
            cbScript.count_magic_crystall = 0;
        }  
        PlayerPrefs.SetInt("count_magic_crystall", 0);
        PlayerPrefs.Save();
    }
}
