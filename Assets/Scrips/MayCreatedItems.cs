﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MayCreatedItems : MonoBehaviour
{
    public bool selected;
    public string prefabname;
    public float Screenhe;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "CreateLevel")
        {
            Outline outline = GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
        // Screenhe = Screen.height;
        //GameObject knOrge = GameObject.Find("en_ogre");
        //Screenhe = knOrge.transform.position.y + 150;
        GameObject knOrge = GameObject.Find("en_ogre");
        if (knOrge != null)
        {
            Screenhe = knOrge.transform.position.y + 150;
        }
        else
        {
            //Debug.LogError("GameObject 'en_ogre' не найден!");
            Screenhe = 0; // или любое дефолтное значение
        }
    }
    public void OnMouseDown()
    { 
        SetSelected();
    }
    public void SetSelected()
    {
        if(SceneManager.GetActiveScene().name == "CreateLevel")
        {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log(mousePos.y);
            if (mousePos.y < Screenhe)
            {
                return;
            }
            //Убираем selected со всех объектов
            GameObject[] AllObject = FindObjectsOfType<GameObject>();
            for (int b = 0; b < AllObject.Length; b++)
            {
                if (AllObject[b].GetComponent<MayCreatedItems>() != null && AllObject[b].GetComponent<Outline>() != null)
                {
                    AllObject[b].GetComponent<Outline>().enabled = false;
                    AllObject[b].GetComponent<MayCreatedItems>().selected = false;
                }
            }
            // и делаем этот объект selected
            selected = true;

            this.GetComponent<Outline>().enabled = true;

            GameObject ControllerButtons = GameObject.Find("ControllerButtonsCreatedLevels");
            ControllerButtons.GetComponent<ControllerButtonsCreateLevel>().LinkedButtonsCheck();
            ControllerButtons.GetComponent<ControllerButtonsCreateLevel>().ChangeNameSelectedObj();
        }      
    }
}
