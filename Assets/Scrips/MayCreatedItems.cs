using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Убираем selected со всех объектов
        GameObject[] AllObject = FindObjectsOfType<GameObject>();
        for (int b = 0; b<AllObject.Length; b++)
        {
            if (AllObject[b].GetComponent<MayCreatedItems>() != null)
            {
                AllObject[b].GetComponent<MayCreatedItems>().selected = false;
            }
        }
        // и делаем этот объект selected
        selected = true;

        GameObject ControllerButtons = GameObject.Find("ControllerButtonsCreatedLevels");
        ControllerButtons.GetComponent<ControllerButtonsCreateLevel>().FloorButtonsCheck();
        ControllerButtons.GetComponent<ControllerButtonsCreateLevel>().ChangeNameSelectedObj();
    }
}
