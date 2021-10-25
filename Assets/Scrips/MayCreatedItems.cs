using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MayCreatedItems : MonoBehaviour
{
    public bool selected;
    public string prefabname;

    public void OnMouseDown()
    {
        SetSelected();
    }
    public void SetSelected()
    {
        if(SceneManager.GetActiveScene().name == "CreateLevel")
        {
            //Убираем selected со всех объектов
            GameObject[] AllObject = FindObjectsOfType<GameObject>();
            for (int b = 0; b < AllObject.Length; b++)
            {
                if (AllObject[b].GetComponent<MayCreatedItems>() != null)
                {
                    AllObject[b].GetComponent<MayCreatedItems>().selected = false;
                }
            }
            // и делаем этот объект selected
            selected = true;

            GameObject ControllerButtons = GameObject.Find("ControllerButtonsCreatedLevels");
            ControllerButtons.GetComponent<ControllerButtonsCreateLevel>().LinkedButtonsCheck();
            ControllerButtons.GetComponent<ControllerButtonsCreateLevel>().ChangeNameSelectedObj();
        }      
    }
}
