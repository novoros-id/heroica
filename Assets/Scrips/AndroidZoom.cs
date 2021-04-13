using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidZoom : MonoBehaviour, IBeginDragHandler, IDragHandler
{

    public GameObject Camera;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //if((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        //{
        //    if(eventData.delta.x > 0)
        //    {
        //        Camera.transform.localEulerAngles = new Vector3(Camera.transform.localEulerAngles.x, Camera.transform.localEulerAngles.y - 0.2f, Camera.transform.localEulerAngles.z);
        //    }
        //    else
        //    {
        //        Camera.transform.localEulerAngles = new Vector3(Camera.transform.localEulerAngles.x, Camera.transform.localEulerAngles.y + 0.2f, Camera.transform.localEulerAngles.z);
        //    }
        //}
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
    //    if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
    //    {
    //        if (eventData.delta.x > 0)
    //        {
    //            Camera.transform.localEulerAngles = new Vector3(Camera.transform.localEulerAngles.x, Camera.transform.localEulerAngles.y + 0.35f, Camera.transform.localEulerAngles.z);
    //        }
    //        else
    //        {
    //            Camera.transform.localEulerAngles = new Vector3(Camera.transform.localEulerAngles.x, Camera.transform.localEulerAngles.y - 0.35f, Camera.transform.localEulerAngles.z);
    //        }
    //    }
    //    else
    //    {
    //        if (eventData.delta.y > 0)
    //        {
    //            Camera.GetComponent<Camera>().fieldOfView = Camera.GetComponent<Camera>().fieldOfView - 1;
    //        }
    //        else
    //        {
    //            Camera.GetComponent<Camera>().fieldOfView = Camera.GetComponent<Camera>().fieldOfView + 1;
    //        }
    //    }
    }
}
