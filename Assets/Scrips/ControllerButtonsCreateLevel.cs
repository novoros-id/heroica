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
        GameObject Select_ob = FindSelectedObject();
        Vector3 new_position = new Vector3(Select_ob.transform.position.x + 1f, 0, Select_ob.transform.position.z);

        if (Select_ob.tag == "Floor")
        {
            GameObject test_obj = object_exist_on_position_with_tag(new_position, "Floor");
            if (test_obj == null)
            {
                newobj = Instantiate(Floor1, new_position, Quaternion.identity);
                newobj.GetComponent<MayCreatedItems>().SetSelected();
            }
            else
            {
                test_obj.GetComponent<MayCreatedItems>().SetSelected();
            }
        }
        else if (Select_ob.tag == "Furniture")
        {
            Select_ob.transform.position = new_position;
        }

        
    }
    public void CreateFloorRight()
    {
        FindSelectedObject();
        GameObject newobj;
        GameObject Select_ob = FindSelectedObject();

        Vector3 new_position = new Vector3(Select_ob.transform.position.x - 1f, 0, Select_ob.transform.position.z);
        if (Select_ob.tag == "Floor")
        {
            GameObject test_obj = object_exist_on_position_with_tag(new_position, "Floor");
            if (test_obj == null)
            {
                newobj = Instantiate(Floor1, new_position, Quaternion.identity);
                newobj.GetComponent<MayCreatedItems>().SetSelected();
            }
            else
            {
                test_obj.GetComponent<MayCreatedItems>().SetSelected();
            }
        }
        else if (Select_ob.tag == "Furniture")
        {
            Select_ob.transform.position = new_position;
        }
    }
    public void CreateFloorUp()
    {
        FindSelectedObject();
        GameObject newobj;
        GameObject Select_ob = FindSelectedObject();

        Vector3 new_position = new Vector3(Select_ob.transform.position.x, 0, Select_ob.transform.position.z - 1f);
        if (Select_ob.tag == "Floor"|| Select_ob.name == "StartFloor")
        {
            GameObject test_obj = object_exist_on_position_with_tag(new_position, "Floor");
            if (test_obj == null)
            {
                newobj = Instantiate(Floor1, new_position, Quaternion.identity);
                newobj.GetComponent<MayCreatedItems>().SetSelected();
            }
            else
            {
                test_obj.GetComponent<MayCreatedItems>().SetSelected();
            }
        }
        else if (Select_ob.tag == "Furniture")
        {
            Select_ob.transform.position = new_position;
        }
    }
    public void CreateFloorDown()
    {
        FindSelectedObject();
        GameObject newobj;
        GameObject Select_ob = FindSelectedObject();

        Vector3 new_position = new Vector3(Select_ob.transform.position.x, 0, Select_ob.transform.position.z + 1f);
        if (Select_ob.tag == "Floor" || Select_ob.name == "StartFloor")
        {
            GameObject test_obj = object_exist_on_position_with_tag(new_position, "Floor");
            if (test_obj == null)
            {
                newobj = Instantiate(Floor1, new_position, Quaternion.identity);
                newobj.GetComponent<MayCreatedItems>().SetSelected();
            }
            else
            {
                test_obj.GetComponent<MayCreatedItems>().SetSelected();
            }
        }
        else if (Select_ob.tag == "Furniture")
        {
            Select_ob.transform.position = new_position;
        }

    }


    public void CreateLinkedObject(string nameObject)
    {
        FindSelectedObject();
        GameObject newobj;
        GameObject Select_ob = FindSelectedObject();


        Vector3 new_position = new Vector3(Select_ob.transform.position.x, 0.35f, Select_ob.transform.position.z);

        if (object_exist_on_position(new_position) == false)
        {
            if (nameObject == "door")
            {
                newobj = Instantiate(Resources.Load(nameObject) as GameObject, new Vector3(Select_ob.transform.position.x, 0.05f, Select_ob.transform.position.z), Quaternion.identity);
               
            }
            else if (nameObject == "windmill")
            {
                GameObject camera_cent = GameObject.Find("CameraCenter");
                newobj = Instantiate(Resources.Load(nameObject) as GameObject, new Vector3(camera_cent.transform.position.x, 0, camera_cent.transform.position.z), Quaternion.identity);
            }
            else {
                    newobj = Instantiate(Resources.Load(nameObject) as GameObject, new Vector3(Select_ob.transform.position.x, 0.7f, Select_ob.transform.position.z), Quaternion.identity);

            }

            newobj.GetComponent<MayCreatedItems>().SetSelected();
        }

       
    }

    public void DeleteSelectedObject()
    {
        if(FindSelectedObject().name != "StartFloor")
        {
            Destroy(FindSelectedObject());
            GameObject[] AllLinkedButtons = GameObject.FindGameObjectsWithTag("LinkedButton");
            for (int b = 0; b < AllLinkedButtons.Length; b++)
            {
                AllLinkedButtons[b].GetComponent<Button>().interactable = false;
            }
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
            if(AllObject[b].GetComponent<MayCreatedItems>() != null && AllObject[b].GetComponent<MayCreatedItems>().selected == true)
            {
                    return AllObject[b];             
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
                AllLinkedButtons[b].GetComponent<Button>().interactable = true;
            }
        }
    }
    public void ChangeNameSelectedObj()
    {
        NameSelectedObject.text = FindSelectedObject().name;
    }

    private GameObject object_exist_on_position_with_tag(Vector3 new_position, string tag_object)
    {
        GameObject[] g_obj = GameObject.FindGameObjectsWithTag(tag_object);

        for (int i = 0; i < g_obj.Length; i++)
        {

            if (new_position.x == g_obj[i].transform.position.x && new_position.z == g_obj[i].transform.position.z)
            {
                return g_obj[i];

            }

        }
        return null;
    }

    private bool object_exist_on_position(Vector3 new_position)
    {
        Collider[] colliders;

        if ((colliders = Physics.OverlapSphere(new_position, 0.35f)).Length > 0)
        {

            foreach (var collider in colliders)
            {
                // обрабатываем только некоторые тэги

                if (collider.tag != "Floor")
                {
                    return true;
                }
            }
        }

    return false;

    }

}
