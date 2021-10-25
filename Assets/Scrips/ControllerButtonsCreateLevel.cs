using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerButtonsCreateLevel : MonoBehaviour
{
    public GameObject floorupbutton;
    public GameObject floordownbutton;
    public GameObject floorleftbutton;
    public GameObject floorrightbutton;

    public GameObject Floor1;

    public GameObject ButtonsPanel;

    public Text NameSelectedObject;

    public void Awake()
    {
        NameSelectedObject = GameObject.Find("NameSelectedObj").GetComponent<Text>();

        ButtonsPanel = GameObject.Find("PanelWeapons");
    }
    public void Start()
    {
        LinkedButtonsCheck();
        ButtonsPanel.SetActive(false);
    }
    public void CreateFloorLeft()
    {
        FindSelectedObject();
        GameObject newobj;
        newobj = Instantiate(Floor1, new Vector3(FindSelectedObject().transform.position.x - 1f, FindSelectedObject().transform.position.y, FindSelectedObject().transform.position.z),Quaternion.identity);
        newobj.GetComponent<MayCreatedItems>().SetSelected();
    }
    public void CreateFloorRight()
    {
        FindSelectedObject();
        GameObject newobj;
        newobj = Instantiate(Floor1, new Vector3(FindSelectedObject().transform.position.x + 1f, FindSelectedObject().transform.position.y, FindSelectedObject().transform.position.z), Quaternion.identity);
        newobj.GetComponent<MayCreatedItems>().SetSelected();
    }
    public void CreateFloorUp()
    {
        FindSelectedObject();
        GameObject newobj;
        newobj = Instantiate(Floor1, new Vector3(FindSelectedObject().transform.position.x, FindSelectedObject().transform.position.y, FindSelectedObject().transform.position.z + 1f), Quaternion.identity);
        newobj.GetComponent<MayCreatedItems>().SetSelected();
    }
    public void CreateFloorDown()
    {
        FindSelectedObject();
        GameObject newobj;
        newobj = Instantiate(Floor1, new Vector3(FindSelectedObject().transform.position.x, FindSelectedObject().transform.position.y, FindSelectedObject().transform.position.z - 1f), Quaternion.identity);
        newobj.GetComponent<MayCreatedItems>().SetSelected();
    }


    public void CreateLinkedObject(string nameObject)
    {
        FindSelectedObject();
        GameObject newobj;
        if(nameObject == "door")
        {
            newobj = Instantiate(Resources.Load(nameObject) as GameObject, new Vector3(FindSelectedObject().transform.position.x, 0.05f, FindSelectedObject().transform.position.z), Quaternion.identity);
            newobj.GetComponent<MayCreatedItems>().SetSelected();
        }
        else
        {
            newobj = Instantiate(Resources.Load(nameObject) as GameObject, new Vector3(FindSelectedObject().transform.position.x, 0.7f, FindSelectedObject().transform.position.z), Quaternion.identity);
            newobj.GetComponent<MayCreatedItems>().SetSelected();
        }  
    }

    public void ShowWeapons()
    {
        ButtonsPanel.SetActive(true);
        LinkedButtonsCheck();
    }
    public void CloseWeapons()
    {
        ButtonsPanel.SetActive(false);
    }
    public GameObject FindSelectedObject()
    {
        GameObject[] AllObject = FindObjectsOfType<GameObject>();
        for(int b = 0; b < AllObject.Length; b++)
        {
            if(AllObject[b].GetComponent<MayCreatedItems>() != null)
            {
                if(AllObject[b].GetComponent<MayCreatedItems>().selected == true)
                {
                    return AllObject[b];
                } 
            }
        }
        return null;
    }
    public void LinkedButtonsCheck()
    {
        GameObject[] AllLinkedButtons = GameObject.FindGameObjectsWithTag("LinkedButton");
        if(FindSelectedObject() != null && FindSelectedObject().name == "StartFloor")
        {
            for (int b = 0; b < AllLinkedButtons.Length; b++)
            {
                AllLinkedButtons[b].GetComponent<Button>().interactable = false;
            }
            floorupbutton.GetComponent<Button>().interactable = true;
            floordownbutton.GetComponent<Button>().interactable = true;
            floorleftbutton.GetComponent<Button>().interactable = false;
            floorrightbutton.GetComponent<Button>().interactable = false;
        }
        else if (FindSelectedObject() != null && FindSelectedObject().tag == "Floor")
        {
            for(int b = 0; b < AllLinkedButtons.Length; b++)
            {
                AllLinkedButtons[b].GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            for (int b = 0; b < AllLinkedButtons.Length; b++)
            {
                AllLinkedButtons[b].GetComponent<Button>().interactable = false;
            }
        }
    }
    public void ChangeNameSelectedObj()
    {
        NameSelectedObject.text = FindSelectedObject().name;
    }
}
