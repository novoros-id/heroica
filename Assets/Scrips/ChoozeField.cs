using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoozeField : MonoBehaviour
{
    public string Field;
    public GameObject cross;
    public GameObject fi1;
    public GameObject fi2;
    public GameObject Floor;
    private void Awake()
    {
        Floor.SetActive(false);
        cross.SetActive(false);
    }
    public void ChangeField()
    {
        Field = "1";
        cross.SetActive(true);
        fi1.GetComponent<ChoozeField>().OtherOff();
        fi2.GetComponent<ChoozeField>().OtherOff();
        Floor.SetActive(true);
    }
    public void OtherOff()
    {
        Field = "0";
        cross.SetActive(false);
        Floor.SetActive(false);
    }
}
