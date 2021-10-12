using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInCreateLevel : MonoBehaviour
{

    private float speed = 0.5f;
    public GameObject CameraCenter;

    private float zoomSpeed = 7f;
    private float max_zoom = 100.8f;
    private float min_zoom = -4.5f;

    private Touch _touchA;
    private Touch _touchB;
    private float _zoom;
    private Vector2 _touchAdirection;
    private Vector2 _touchBdirection;
    private float _dstBtwTouchesPosition;
    private float _dstBtwTpuchesDirections;
    private Touch touch;
    private float Androidspeed = 0.009F;

    public bool Pc;

    void Awake()
    {

        CameraCenter = GameObject.Find("CameraCenter");

    }
    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Pc = true;
        }
        if(Application.isEditor == true)
        {
            Pc = true;
        }
        else
        {
            Pc = false;
        }
    }
    private void FixedUpdate()
    {

        if (Input.touchCount == 2)
        {
            float difference = _touchA.deltaPosition.y - _touchB.deltaPosition.y;

            _touchA = Input.GetTouch(0);
            _touchB = Input.GetTouch(1);
            _touchAdirection = _touchA.position - _touchA.deltaPosition;
            _touchBdirection = _touchB.position - _touchB.deltaPosition;

            _dstBtwTouchesPosition = Vector2.Distance(_touchA.position, _touchB.position);
            _dstBtwTpuchesDirections = Vector2.Distance(_touchAdirection, _touchBdirection);

            _zoom = _dstBtwTouchesPosition - _dstBtwTpuchesDirections;

            //ZOOM

            if (_zoom != 0.0f && _touchA.deltaPosition.y != 0 && _touchB.deltaPosition.y != 0)
            {

                float y_goal = CameraCenter.transform.position.y - _zoom * 0.01f;
                if (y_goal <= max_zoom && y_goal >= min_zoom)
                {
                    CameraCenter.transform.Translate(0, -_zoom * 0.01f, -_zoom * 0.01f, CameraCenter.transform);
                }

            }
        }

        if (Input.touchCount == 1)
        {

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {

                CameraCenter.transform.Translate(touch.deltaPosition.x * Androidspeed, 0, touch.deltaPosition.y * Androidspeed, CameraCenter.transform);
            }

        }

        if (Pc == true)
           {
            //zoom
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                // GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView - 5;        


                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0.0f)
                {
                  
                    float y_goal = CameraCenter.transform.position.y - scroll * zoomSpeed;

                    if (y_goal <= max_zoom && y_goal >= min_zoom)
                    {
                        CameraCenter.transform.Translate(0, -scroll * zoomSpeed, -scroll * zoomSpeed, CameraCenter.transform);
                    }
                    // CameraCenter.transform.Translate(0, -scroll * zoomSpeed, -scroll * zoomSpeed, CameraCenter.transform);
                    //GetComponent<Camera>().transform.Translate(0, scroll * zoomSpeed, 0, Space.World);
                }

            }
            //перемещение
            if (Input.GetMouseButton(0))

            {

                CameraCenter.transform.Translate(Input.GetAxis("Mouse X") * -speed, 0, Input.GetAxis("Mouse Y") * -speed, CameraCenter.transform);

            }
        }
    }
}
