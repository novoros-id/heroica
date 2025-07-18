﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class Settings : MonoBehaviour
{
    public Dropdown LanButton;
    public Toggle ChatBoxButton;
    public GameObject Chatbox;
    public GameObject SettingsUI;
    public AudioMixer masterMixer;
    public Slider volumeSlider;
    public Text volumeText;
    public GameObject MainMenu;
    public GameObject Delay;
    public float delay;
    public Toggle AutoPlay;
    public InputField delayfield;
    public Toggle MusicTog;
    public Toggle VoiceTog;

    void Start()
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        if (Chatbox != null)
        {
            Chatbox.SetActive(mScript.ChatBox == 1);
        }
        float savedVolume = PlayerPrefs.GetFloat("volume", 0.5f);
        volumeSlider.value = savedVolume;

        // Обновляем текст и звук
        UpdateVolumeText(savedVolume);
        SetVolume(savedVolume);

        SettingsUI.SetActive(false);

        if (PlayerPrefs.HasKey("Delay"))
        {
            delay = PlayerPrefs.GetFloat("Delay");
        }
        else
        {
            delay = 0;
        }
        if (SceneManager.GetActiveScene().name != "Start")
        {
            GameObject cb = GameObject.Find("Cube");
            ClickOnCube cbScript = cb.GetComponent<ClickOnCube>();
            cbScript.computerMoveDelay = delay;
        }
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
        ChatBoxButton.isOn = mScript.ChatBox == 1 ? true : false;
        if (mScript.lang == "ru")
        {
            LanButton.value = 0;
        }
        else
        {
            LanButton.value = 1;
        }
        
        if (PlayerPrefs.GetFloat("Delay") == 0f)
        {
            Delay.SetActive(false);
            AutoPlay.isOn = false;
        }
        else
        {
            delay = PlayerPrefs.GetFloat("Delay");
            Delay.SetActive(true);
            AutoPlay.isOn = true;
            delayfield.text = PlayerPrefs.GetFloat("Delay").ToString();
        }
        MusicTog.isOn = mScript.MusicOn == 1 ? true : false;
        VoiceTog.isOn = mScript.VoiceOn == 1 ? true : false;
    }
    public void ChangeLang(Int32 val)
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        mScript.SetLang(val);
    }
    public void ToggleChatBox_(bool cb)
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        mScript.SetChatbox(cb);
        if (mScript.ChatBox == 1)
        {
            if (Chatbox != null)
            {
                Chatbox.SetActive(true);
            }
        }
        else if (mScript.ChatBox == 0)
        {
            if (Chatbox != null)
            {
                Chatbox.SetActive(false);
            }
        }
    }
    public void SettingsOff()
    {
        SettingsUI.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Start")
        {
            MainMenu.SetActive(true);
        }

    }
    public void SettingsOn()
    {
        SettingsUI.SetActive(true);
        if (SceneManager.GetActiveScene().name == "Start")
        {
            MainMenu.SetActive(false);
        }
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

    public void ChangeAutoPlay(bool ap)
    {
        Delay.SetActive(ap);
        if (ap == false)
        {
            delay = 0;
        }
        PlayerPrefs.SetFloat("Delay", delay);
        if (SceneManager.GetActiveScene().name != "Start")
        {
            GameObject cb = GameObject.Find("Cube");
            ClickOnCube cbScript = cb.GetComponent<ClickOnCube>();
            cbScript.computerMoveDelay = delay;
        }
        PlayerPrefs.Save();
    }
    public void changeDelay(string delay_)
    {
        delay = float.Parse(delay_);
        PlayerPrefs.SetFloat("Delay", delay);
        if (SceneManager.GetActiveScene().name != "Start")
        {
            GameObject cb = GameObject.Find("Cube");
            ClickOnCube cbScript = cb.GetComponent<ClickOnCube>();
            cbScript.computerMoveDelay = delay;
        }
        PlayerPrefs.Save();
    }

    public void ExitLvl()
    {
        SceneManager.LoadScene("Start");
    }
    public void MusicOnChange(bool moc)
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        mScript.MusicOn = moc ? 1 : 0;
        AudioSource music = GameObject.Find("Camera").GetComponent<AudioSource>();
        music.volume = moc ? 1 : 0;
        PlayerPrefs.SetInt("MusicOn", moc ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void VoiceOnChange(bool voc)
    {
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();
        mScript.VoiceOn = voc ? 1 : 0;
        
        PlayerPrefs.SetInt("VoiceOn", voc ? 1 : 0);
        PlayerPrefs.Save();
    }
}
