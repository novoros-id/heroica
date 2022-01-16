using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MayCreatedItems : MonoBehaviour
{
    public bool selected;
    public string prefabname;
    public int Screenhe;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "CreateLevel")
        {
            this.GetComponent<Outline>().enabled = false;
        }
        Screenhe = Screen.height; 
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
            if (mousePos.y < Screenhe/4.3f)
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
