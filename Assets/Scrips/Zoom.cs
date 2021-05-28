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
    private float speed = 0.5f;
    private float Androidspeed = 0.006F;
    private Touch touch;
    private float zoomSpeed = 1f;

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


    private Touch _touchA;
    private Touch _touchB;
    private Vector2 _touchAdirection;
    private Vector2 _touchBdirection;
    private float _dstBtwTouchesPosition;
    private float _dstBtwTpuchesDirections;
    private float _zoom;
    public GameObject CameraCenter;

    public float ZoomMax;
    public float ZoomMin;
    public float Sensitivity;

    private float max_zoom = 100.8f;
    private float min_zoom = -1.5f;

    public Main mn;

    void Start()
	{

        CameraCenter = GameObject.Find("CameraCenter");

    }



    private void FixedUpdate()
	{
        if(go == 0)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///  android
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

                ///
                ///ROTATION
                ///

                if (_touchA.deltaPosition.y > _touchB.deltaPosition.y && Mathf.Abs(difference) > 14)
                {
                    //Debug.Log("Left");
                    CameraCenter.transform.Rotate(new Vector3(0, Mathf.Abs(difference) * 4, 0) * Time.deltaTime);
                }
                else if (_touchA.deltaPosition.y < _touchB.deltaPosition.y && Mathf.Abs(difference) > 14)
                {
                    //Debug.Log("Right");
                    CameraCenter.transform.Rotate(new Vector3(0, -Mathf.Abs(difference) * 4, 0) * Time.deltaTime);
                }

                ///
                ///ZOOM
                ///

                if (_zoom != 0.0f)
                {

                    float y_goal = CameraCenter.transform.position.y - _zoom * 0.01f;
                    if (y_goal <= max_zoom && y_goal >= min_zoom)
                    {
                        CameraCenter.transform.Translate(0, -_zoom * 0.01f, -_zoom * 0.01f, CameraCenter.transform);
                    }

                }

            }


            ///
            ///ПЕРЕМЕЩЕНИЕ
            ///

            if (Input.touchCount == 1)
            {

                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {

                    CameraCenter.transform.Translate(touch.deltaPosition.x * Androidspeed, 0, touch.deltaPosition.y * Androidspeed, CameraCenter.transform);
                }

            }


            if(mn.Pc == true)
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///  computer
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                ///
                ///ZOOM
                ///
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    // GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView - 5;        


                    float scroll = Input.GetAxis("Mouse ScrollWheel");
                    if (scroll != 0.0f)
                    {
                        //GetComponent<Camera>().transform.position.y += scroll * zoomSpeed;
                        //GetComponent<Camera>().transform.position.z += scroll * zoomSpeed;
                        // GetComponent<Camera>().transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
                        //Debug.Log("y - " + CameraCenter.transform.position.y);
                        //Debug.Log("trans " + scroll * zoomSpeed);
                        float y_goal = CameraCenter.transform.position.y - scroll * zoomSpeed;
                        //Debug.Log("y_goal " + y_goal);
                        //Debug.Log("itogo " + scroll * zoomSpeed);
                        if (y_goal <= max_zoom && y_goal >= min_zoom)
                        {
                            CameraCenter.transform.Translate(0, -scroll * zoomSpeed, -scroll * zoomSpeed, CameraCenter.transform);
                        }
                        // CameraCenter.transform.Translate(0, -scroll * zoomSpeed, -scroll * zoomSpeed, CameraCenter.transform);
                        //GetComponent<Camera>().transform.Translate(0, scroll * zoomSpeed, 0, Space.World);
                    }

                }


                ///
                ///ПЕРЕМЕЩЕНИЕ
                ///
                if (Input.GetMouseButton(0))

                {

                    CameraCenter.transform.Translate(Input.GetAxis("Mouse X") * speed, 0, Input.GetAxis("Mouse Y") * speed, CameraCenter.transform);

                }

                ///
                /// Вращение
                ///
                if (Input.GetMouseButton(1))

                {
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        CameraCenter.transform.Rotate(new Vector3(0, Mathf.Abs(Input.GetAxis("Mouse X")) * 200, 0) * Time.deltaTime);
                    }

                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        CameraCenter.transform.Rotate(new Vector3(0, -Mathf.Abs(Input.GetAxis("Mouse X")) * 200, 0) * Time.deltaTime);
                    }

                }

                ///
                /// Ограничение камеры
                ///
                //Debug.Log(transform.position.y);

                //if (transform.position.y >= 13)
                //{ 
                //    //transform.position = new Vector3(transform.position.x, 13, transform.position.z);
                //}


                //if (transform.position.y <= 1)
                //{
                //    // transform.Translate(transform.position.x, 1, transform.position.z, Space.World);
                //    transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
                //}

                ///
            }

        }

        /// ПЕрвое движение

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
