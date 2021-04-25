using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zoom : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	public float sensitivity = 3; // чувствительность мышки
	public float limit = 80; // ограничение вращения по Y
	public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
	public float zoomMax = 10; // макс. увеличение
	public float zoomMin = 3; // мин. увеличение
	private float X, Y;
    private float speed = 0.1f;
    private float Androidspeed = 0.01F;
    private Touch touch;
    private float zoomSpeed = 1.0f;

    public Transform startMarker;
    public Transform endMarker;
    public float fract;
    public int go = 0;

    // Movement speed in units per second.
    public float speed_ = 0.5F;
    // Time when the movement started.
    public float startTime;
    // Total distance between the markers.
    public float journeyLength;

    public GameObject btnLeft;
    public GameObject btnRight;
    public GameObject btnBack;
    public GameObject btnForward;
    float PosBtnLeft;
    float PosBtnRight;
    float PosBtnBack;
    float PosBtnForward;

    private Touch _touchA;
    private Touch _touchB;
    private Vector2 _touchAdirection;
    private Vector2 _touchBdirection;
    private float _dstBtwTouchesPosition;
    private float _dstBtwTpuchesDirections;
    private float _zoom;
    public GameObject CameraCenter;

    private Camera _mainCamera;

    public float ZoomMax;
    public float ZoomMin;
    public float Sensitivity;

    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position

    private float rotation_position;
    private float rotationRate = 3.0f;


    void Start()
	{

        CameraCenter = GameObject.Find("CameraCenter");

        _mainCamera = Camera.main;
        
        PosBtnBack = btnBack.transform.position.y;
        PosBtnForward = btnForward.transform.position.y;
        PosBtnLeft = btnLeft.transform.position.y;
        PosBtnRight = btnRight.transform.position.y;


        //limit = Mathf.Abs(limit);
        //if (limit > 90) limit = 90;
        //offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
        //transform.position = target.position + offset;

        //X = transform.localEulerAngles.y + 0 * sensitivity;
        //Y += -7 * sensitivity;
        //Y = Mathf.Clamp(Y, -limit, limit);
        //transform.localEulerAngles = new Vector3(-Y, X, 0);
        //transform.position = transform.localRotation * offset + target.position;

    }



    private void FixedUpdate()
	{
       
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///  android

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

            //////////////////
            ///ROTATION
            ///
            //Debug.Log(Mathf.Abs(difference));

            //Debug.Log("a " +_touchA.deltaPosition.y);
            //Debug.Log("b " + _touchB.deltaPosition.y);

            if (_touchA.deltaPosition.y > _touchB.deltaPosition.y && Mathf.Abs(difference) > 14)
            {
                //Debug.Log("Left");
                CameraCenter.transform.Rotate(new Vector3(0, Mathf.Abs(difference) * 4, 0) * Time.deltaTime);
            }
            else if(_touchA.deltaPosition.y < _touchB.deltaPosition.y && Mathf.Abs(difference) > 14)
            {
               //Debug.Log("Right");
                CameraCenter.transform.Rotate(new Vector3(0, -Mathf.Abs(difference) * 4, 0) * Time.deltaTime);
            }

            //if (_touchBdirection != _touchB.position)
            //{
            //    CameraCenter.transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);

            //}
            //if (_touchAdirection != _touchA.position)
            //{
            //    CameraCenter.transform.Rotate(new Vector3(0, -45, 0) * Time.deltaTime);

            //}

            ////////////////////
            ///ZOOM
            ///
            if (_zoom != 0.0f)
            {


                //if (transform.position.y <= 1.0f)
                //{
                //    transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);

                //}


                //if (transform.position.y >= 20.0f)
                //{
                //    transform.position = new Vector3(transform.position.x, 20.0f, transform.position.z);

                //}

                //CameraCenter.transform.Translate(0, -_zoom * 0.01f, -_zoom * 0.01f, Space.World);
                CameraCenter.transform.Translate(0, -_zoom * 0.01f, -_zoom * 0.01f, CameraCenter.transform);
                //CameraCenter.transform.Translate(0, _zoom * 0.03f * -1, 0, Camera.main.transform);




            }

            //var CurrentZoom = _mainCamera.orthographicSize - _zoom * Sensitivity;

            //_mainCamera.orthographicSize = Mathf.Clamp(CurrentZoom, ZoomMin, ZoomMax);
            
           
        }

  

        ////////////////////
        ///ПЕРЕМЕЩЕНИЕ

        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
               
                //CameraCenter.transform.position = new Vector3(CameraCenter.transform.position.x + touch.deltaPosition.x * Androidspeed, CameraCenter.transform.position.y, CameraCenter.transform.position.z + touch.deltaPosition.y * Androidspeed);

                // Vector3 moveVector = (touch.deltaPosition.x  + touch.deltaPosition.y ).normalized;
                // CameraCenter.transform.Translate(touch.deltaPosition.x * Androidspeed * -1,0, touch.deltaPosition.y * Androidspeed* -1, Camera.main.transform);
                CameraCenter.transform.Translate(touch.deltaPosition.x * Androidspeed , 0, touch.deltaPosition.y * Androidspeed , CameraCenter.transform);
            }
        }
 

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///  computer
        ///

        //if (Input.GetMouseButton(0))
        //{
        //    if (Input.mousePosition.x > 0)
        //    {
        //        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 0.2f, transform.localEulerAngles.z);
        //    }
        //    else
        //    {
        //        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0.2f, transform.localEulerAngles.z);
        //    }
        //}


        //if ((int)PosBtnBack != (int)btnBack.transform.position.y)
        //{
        //    transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        //}
        //if ((int)PosBtnForward != (int)btnForward.transform.position.y)
        //{
        //    transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        //}
        //if ((int)PosBtnLeft != (int)btnLeft.transform.position.y)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
        //}
        //if ((int)PosBtnRight != (int)btnRight.transform.position.y)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
        //}

        if (GetComponent<Camera>().fieldOfView >= 90)
        {
          GetComponent<Camera>().fieldOfView = 90;
        }

        if (GetComponent<Camera>().fieldOfView <= 5)
        {
            GetComponent<Camera>().fieldOfView = 5;
        }

        if (Input.GetKey("q"))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 0.2f, transform.localEulerAngles.z);
        }

        if (Input.GetKey("e"))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0.2f, transform.localEulerAngles.z);
        }

        //transform.position = new Vector3(target.position.x - 3.4f, 4.5f, target.position.z + 1.5f);

        //if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
        //else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
        //offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
           // GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView - 5;
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                //GetComponent<Camera>().transform.position.y += scroll * zoomSpeed;
                //GetComponent<Camera>().transform.position.z += scroll * zoomSpeed;
                GetComponent<Camera>().transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
                //GetComponent<Camera>().transform.Translate(0, scroll * zoomSpeed, 0, Space.World);
            }
        }



        //if (Input.GetAxis("Mouse ScrollWheel") < 0)
        //{
        //    GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView + 5;
        //}

        //if (Input.GetMouseButton(1))
        //{
        //    X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        //    Y += Input.GetAxis("Mouse Y") * sensitivity;
        //    Y = Mathf.Clamp(Y, -limit, limit);
        //    transform.localEulerAngles = new Vector3(-Y, X, 0);
        //    transform.position = transform.localRotation * offset + target.position;
        //}

        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {

                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            }

            if (Input.GetAxis("Mouse X") < 0)

            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            }
            if (Input.GetAxis("Mouse Y") > 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
            }

            if (Input.GetAxis("Mouse Y") < 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
            }


        }

        //if (Input.GetKeyDown("a"))
        //{
        //    transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        //}

        //if (Input.GetKeyDown("d"))
        //{
        //    transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        //}

        //if (Input.GetKeyDown("w"))
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        //}

        //if (Input.GetKeyDown("s"))
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        //}

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed_;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        if (go == 1)
        {
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
            if (Mathf.Abs(transform.position.x - endMarker.transform.position.x) < 0.01 && Mathf.Abs(transform.position.z - endMarker.transform.position.z) < 0.01)
            {
                go = 0;
                transform.position = endMarker.position;
            }
            //(transform.position == endMarker.position) 
        }
    }
}
