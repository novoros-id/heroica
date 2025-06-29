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
    private float Androidspeed = 0.009F;
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
    private float min_zoom = -4.5f;

    public Main mn;
    public float SumRotate = 0;

    /// <summary>
    float initialFingersDistance;
    Vector3 initialScale;
    float rotationRate = 0.2f;
    int direction = 1;
    /// </summary>

    void Start()
    {
        CameraCenter = GameObject.Find("CameraCenter");
    }

    private void FixedUpdate()
	{


        if (go == 0)
        {


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///  android
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Input.touchCount == 2)
            {

                Touch t1 = Input.touches[0];
                Touch t2 = Input.touches[1];

                if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
                {
                    initialFingersDistance = Vector2.Distance(t1.position, t2.position);
                    initialScale = CameraCenter.transform.localScale;
                }

                else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
                {
                    var currentFingersDistance = Vector2.Distance(t1.position, t2.position);
                    var scaleFactor = initialFingersDistance / currentFingersDistance; // инвертировали
                    CameraCenter.transform.localScale = initialScale * scaleFactor;

                    float Dx = t1.position.x - transform.position.x;
                    float Dy = t1.position.y - transform.position.y;

                    Vector3 pos = t2.position;

                    //Vector3 pos = t1.position - t2.position;
                    Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10));
                    // CameraCenter.transform.position = Vector3.Lerp(CameraCenter.transform.position, touchedPos, Time.deltaTime * 4);
                    //   transform.position += touchedPos;

                    float pinchAmount = 0;
                    Quaternion desiredRotation = CameraCenter.transform.rotation;

                    TouchLogic.Calculate();

                    if (Mathf.Abs(TouchLogic.pinchDistanceDelta) > 0)
                    { // zoom
                        pinchAmount = TouchLogic.pinchDistanceDelta;
                    }

                    if (Mathf.Abs(TouchLogic.turnAngleDelta) > 0)
                    { // rotate
                        Vector3 rotationDeg = Vector3.zero;
                        rotationDeg.y = -TouchLogic.turnAngleDelta;
                        desiredRotation *= Quaternion.Euler(rotationDeg);
                    }

                    // not so sure those will work:

                    CameraCenter.transform.rotation = desiredRotation;
                    // CameraCenter.transform.position += Vector3.forward * pinchAmount;
                }


                //_touchA = Input.GetTouch(0);
                //_touchB = Input.GetTouch(1);

                //float difference = _touchA.deltaPosition.y - _touchB.deltaPosition.y;

                ////_touchA = Input.GetTouch(0);
                ////_touchB = Input.GetTouch(1);
                //_touchAdirection = _touchA.position - _touchA.deltaPosition;
                //_touchBdirection = _touchB.position - _touchB.deltaPosition;

                //_dstBtwTouchesPosition = Vector2.Distance(_touchA.position, _touchB.position);
                //_dstBtwTpuchesDirections = Vector2.Distance(_touchAdirection, _touchBdirection);

                //_zoom = _dstBtwTouchesPosition - _dstBtwTpuchesDirections;

                //Debug.Log(_dstBtwTpuchesDirections);

                /////
                /////ROTATION
                /////

                //if (_touchA.deltaPosition.y > _touchB.deltaPosition.y && Mathf.Abs(difference) > 14 && (_touchA.deltaPosition.y == 0 || _touchB.deltaPosition.y == 0))
                //{
                //    //Debug.Log("Left");
                //    CameraCenter.transform.Rotate(new Vector3(0, -Mathf.Abs(difference) * 4, 0) * Time.deltaTime);
                //}
                //else if (_touchA.deltaPosition.y < _touchB.deltaPosition.y && Mathf.Abs(difference) > 14 && (_touchA.deltaPosition.y == 0 || _touchB.deltaPosition.y == 0))
                //{
                //    //Debug.Log("Right");
                //    CameraCenter.transform.Rotate(new Vector3(0, +Mathf.Abs(difference) * 4, 0) * Time.deltaTime);
                //}


                ////if (_touchA.deltaPosition.y > _touchB.deltaPosition.y && Mathf.Abs(difference) > 14 && (_touchA.deltaPosition.y == 0 || _touchB.deltaPosition.y == 0))
                ////{
                ////    //Debug.Log("Left");
                ////    CameraCenter.transform.Rotate(new Vector3(0, -Mathf.Abs(difference) * 4, 0) * Time.deltaTime);
                ////}
                ////else if (_touchA.deltaPosition.y < _touchB.deltaPosition.y && Mathf.Abs(difference) > 14 && (_touchA.deltaPosition.y == 0 || _touchB.deltaPosition.y == 0))
                ////{
                ////    //Debug.Log("Right");
                ////    CameraCenter.transform.Rotate(new Vector3(0, +Mathf.Abs(difference) * 4, 0) * Time.deltaTime);
                ////}

                /////
                /////ZOOM
                /////


                //if (_zoom != 0.0f && _touchA.deltaPosition.y != 0 && _touchB.deltaPosition.y != 0)
                //{

                //    float y_goal = CameraCenter.transform.position.y - _zoom * 0.01f;
                //    if (y_goal <= max_zoom && y_goal >= min_zoom)
                //    {
                //        CameraCenter.transform.Translate(0, -_zoom * 0.01f, -_zoom * 0.01f, CameraCenter.transform);
                //    }

                //}

            }


            ///
            ///ПЕРЕМЕЩЕНИЕ
            ///

            if (Input.touchCount == 1)
            {

                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {

                    CameraCenter.transform.Translate( touch.deltaPosition.x * Androidspeed, 0, touch.deltaPosition.y * Androidspeed, CameraCenter.transform);
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
            if(SumRotate < 545)
            {
                CameraCenter.transform.Rotate(new Vector3(0, Mathf.Abs(5) * 4, 0) * Time.deltaTime);
                SumRotate += 5;
            }        
            if (Mathf.Abs(transform.position.x - endMarker.transform.position.x) < 0.01 && Mathf.Abs(transform.position.z - endMarker.transform.position.z) < 0.01)
            {
                go = 0;
                transform.position = endMarker.position;
                //CameraCenter.transform.Rotate(new Vector3(0, Mathf.Abs(545) * 4, 0) * Time.deltaTime);
            }
            //(transform.position == endMarker.position) 
        }
    }
}

public class TouchLogic : MonoBehaviour
{
    const float pinchTurnRatio = Mathf.PI / 2;
    const float minTurnAngle = 0;

    const float pinchRatio = 1;
    const float minPinchDistance = 0;

    const float panRatio = 1;
    const float minPanDistance = 0;

    /// <summary>
    ///   The delta of the angle between two touch points
    /// </summary>
    static public float turnAngleDelta;
    /// <summary>
    ///   The angle between two touch points
    /// </summary>
    static public float turnAngle;

    /// <summary>
    ///   The delta of the distance between two touch points that were distancing from each other
    /// </summary>
    static public float pinchDistanceDelta;
    /// <summary>
    ///   The distance between two touch points that were distancing from each other
    /// </summary>
    static public float pinchDistance;

    /// <summary>
    ///   Calculates Pinch and Turn - This should be used inside LateUpdate
    /// </summary>
    /// 
    static public Touch LastTouch;
    static public void Calculate()
    {
        pinchDistance = pinchDistanceDelta = 0;
        turnAngle = turnAngleDelta = 0;

        // if two fingers are touching the screen at the same time ...


        if (Input.touchCount == 1)
        {

            Touch touch1 = Input.touches[0];

            turnAngle = Angle(touch1.position, LastTouch.position);
            float prevTurn = Angle(touch1.position + touch1.deltaPosition,
                                   LastTouch.deltaPosition + LastTouch.position);
            turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

            // ... if it's greater than a minimum threshold, it's a turn!
            if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
            {
                turnAngleDelta *= pinchTurnRatio;
            }
            else
            {
                turnAngle = turnAngleDelta = 0;
            }
            LastTouch = Input.touches[0];
        }

        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];

            // ... if at least one of them moved ...
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // ... check the delta distance between them ...
                pinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition,
                                                     touch2.position - touch2.deltaPosition);
                pinchDistanceDelta = pinchDistance - prevDistance;

                // ... if it's greater than a minimum threshold, it's a pinch!
                if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance)
                {
                    pinchDistanceDelta *= pinchRatio;
                }
                else
                {
                    pinchDistance = pinchDistanceDelta = 0;
                }

                // ... or check the delta angle between them ...
                turnAngle = Angle(touch1.position, touch2.position);
                float prevTurn = Angle(touch1.position + touch1.deltaPosition,
                                       touch2.position + touch2.deltaPosition);
                turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

                // ... if it's greater than a minimum threshold, it's a turn!
                if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
                {
                    turnAngleDelta *= pinchTurnRatio;
                }
                else
                {
                    turnAngle = turnAngleDelta = 0;
                }
            }
        }
    }


    static private float Angle(Vector2 pos1, Vector2 pos2)
    {
        Vector2 from = pos2 - pos1;
        Vector2 to = new Vector2(1, 0);

        float result = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);

        if (cross.z > 0)
        {
            result = 360f - result;
        }

        return result;
    }
}
