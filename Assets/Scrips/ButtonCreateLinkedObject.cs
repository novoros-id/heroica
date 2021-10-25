using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCreateLinkedObject : MonoBehaviour
{
    public void CreateObject()
    {
        GameObject Controller = GameObject.Find("ControllerButtonsCreatedLevels");
        Controller.GetComponent<ControllerButtonsCreateLevel>().CreateLinkedObject(gameObject.name);
    }
}
