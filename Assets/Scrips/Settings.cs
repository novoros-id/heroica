using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public GameObject LanButton;
    public GameObject ChatBoxButton;
    public GameObject Chatbox;
    public Sprite LanButtonRu;
    public Sprite LanButtonEn;
    public GameObject SettingsUI;
    public AudioMixer masterMixer;
    public Slider volumeSlider;
    public Text volumeText;

    void Start()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        if (Chatbox != null)
        {
            Debug.Log(mScript.ChatBox);
            Chatbox.SetActive(mScript.ChatBox == 1);
        }
        float savedVolume = PlayerPrefs.GetFloat("volume", 0.5f);
        volumeSlider.value = savedVolume;

        // Обновляем текст и звук
        UpdateVolumeText(savedVolume);
        SetVolume(savedVolume);

        SettingsUI.SetActive(false);
    }
    

    // Вызывается из UI Slider
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();

        float linearVolume = Mathf.Lerp(0f, 2f, volume);
        int percent = Mathf.RoundToInt(linearVolume * 100);
        volumeText.text = percent + "%";

        // Управляем звуком
        if (volume <= 0f)
        {
            masterMixer.SetFloat("MasterVolume", -80f); // Полное отключение звука
        }
        else
        {
            float decibel = Mathf.Log10(linearVolume) * 20f;
            masterMixer.SetFloat("MasterVolume", decibel);
        }
    }
    public void UpdateVolumeText(float volume)
    {
        float linearVolume = Mathf.Lerp(0f, 2f, volume);
        int percent = Mathf.RoundToInt(linearVolume * 100);
        volumeText.text = percent + "%";
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
        if (mScript.ChatBox == 1)
        {
            ChatBoxButton.GetComponent<Image>().sprite = LanButtonRu;
        }
        else if (mScript.ChatBox == 0)
        {
            ChatBoxButton.GetComponent<Image>().sprite = LanButtonEn;
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
    public void ToggleChatBox_()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        if (mScript.ChatBox == 1)
        {
            ChatBoxButton.GetComponent<Image>().sprite = LanButtonEn;
            if (Chatbox != null)
            {   
                Chatbox.SetActive(false);
            }
        }
        else if (mScript.ChatBox == 0)
        {
            ChatBoxButton.GetComponent<Image>().sprite = LanButtonRu;
            if (Chatbox != null)
            {
                Chatbox.SetActive(true);
            }
        }
        mScript.SetChatbox();
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
