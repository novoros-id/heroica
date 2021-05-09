//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class TouchCameraControl : MonoBehaviour
//{
//    public float moveSensitivityX = 0.5f;
//    public float moveSensitivityZ = 0.5f;
//    public bool updateZoomSensitivity = true;
//    public float orthoZoomSpeed = 0.05f;
//    public float minZoom = 1.0f;
//    public float maxZoom = 20.0f;  //best set at 20
//    public bool invertMoveX = false;
//    public bool invertMoveZ = false;
//    public float mapWidth = 85.0f;
//    public float mapLength = 60.0f;

//    public float inertiaDuration = 1.0f;

//    private Camera _camera;

//    private float minX, maxX, minY, maxY;
//    private float horizontalExtent, verticalExtent;

//    private float scrollVelocity = 0.0f;
//    private float timeTouchPhaseEnded;
//    private Vector3 scrollDirection = Vector3.zero;


//    void Start()
//    {
//        Debug.Log(this.name);
        
//        _camera = Camera.main;

//        //maxZoom = 0.5f * (mapWidth / _camera.aspect);  //use this if you need max zoom more than 20

//        //if (mapWidth > mapLength)  //use this if you need max zoom more than 20
//        //    maxZoom = 0.5f * (mapLength/ _camera.aspect);  //use this if you need max zoom more than 20

//        if (_camera.orthographicSize > maxZoom)
//            _camera.orthographicSize = maxZoom;

//        CalculateLevelBounds();
//    }

//    void Update()
//    {
//        if (updateZoomSensitivity)
//        {
//            moveSensitivityX = _camera.orthographicSize / 5.0f;
//            moveSensitivityZ = _camera.orthographicSize / 5.0f;
//        }

//        Touch[] touches = Input.touches;

//        if (touches.Length < 1)
//        {
//            //if the camera is currently scrolling
//            if (scrollVelocity != 0.0f)
//            {
//                //slow down over time
//                float t = (Time.time - timeTouchPhaseEnded) / inertiaDuration;
//                float frameVelocity = Mathf.Lerp(scrollVelocity, 0.0f, t);
//                _camera.transform.position += -(Vector3)scrollDirection.normalized * (frameVelocity * 0.05f) * Time.deltaTime;

//                if (t >= 1.0f)
//                    scrollVelocity = 0.0f;
//            }
            
//        }

//        if (touches.Length > 0)
//        {
//            //Single touch (move)
//            if (touches.Length == 1)
//            {
//                if (touches[0].phase == TouchPhase.Began)
//                {
//                    scrollVelocity = 0.0f;
//                }
//                else if (touches[0].phase == TouchPhase.Moved)
//                {
//                    Vector3 delta = touches[0].deltaPosition;

//                    float positionX = delta.x * moveSensitivityX * Time.deltaTime;
//                    positionX = invertMoveX ? positionX : positionX * -1;

//                    float positionZ = delta.z * moveSensitivityZ * Time.deltaTime;
//                    positionZ = invertMoveZ ? positionZ : positionZ * -1;

//                    _camera.transform.position += new Vector3(positionX, 0, positionZ);

//                    scrollDirection = touches[0].deltaPosition.normalized;
//                    scrollVelocity = touches[0].deltaPosition.magnitude / touches[0].deltaTime;


//                    if (scrollVelocity <= 100)
//                        scrollVelocity = 0;
//                }
//                else if (touches[0].phase == TouchPhase.Ended)
//                {
//                    timeTouchPhaseEnded = Time.time;
//                }
//            }


//            //Double touch (zoom)
//            if (touches.Length == 2)
//            {
//                Vector3 cameraViewsize = new Vector3(_camera.pixelWidth, _camera.pixelHeight);

//                Touch touchOne = touches[0];
//                Touch touchTwo = touches[1];

//                Vector3 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
//                Vector3 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

//                float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
//                float touchDeltaMag = (touchOne.position - touchTwo.position).magnitude;

//                float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

//                _camera.transform.position += _camera.transform.TransformDirection((touchOnePrevPos + touchTwoPrevPos - cameraViewsize) * _camera.orthographicSize / cameraViewsize.y);

//                _camera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
//                _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom, maxZoom) - 0.001f;

//                _camera.transform.position -= _camera.transform.TransformDirection(((Vector3)touchOne.position + (Vector3)touchTwo.position - cameraViewsize) * _camera.orthographicSize / cameraViewsize.y);

//                CalculateLevelBounds();
//            }
//        }
//    }

//    void CalculateLevelBounds()
//    {
//        verticalExtent = _camera.orthographicSize;
//        horizontalExtent = _camera.orthographicSize * Screen.width / Screen.height;
//        minX = horizontalExtent - mapWidth / 2.0f;
//        maxX = mapWidth / 2.0f - horizontalExtent;
//        minY = verticalExtent - mapLength / 2.0f;
//        maxY = mapLength / 2.0f - verticalExtent;
//    }

//    void LateUpdate()
//    {
//        Vector3 limitedCameraPosition = _camera.transform.position;
//        limitedCameraPosition.x = Mathf.Clamp(limitedCameraPosition.x, minX, maxX);
//        limitedCameraPosition.y = Mathf.Clamp(limitedCameraPosition.y, minY, maxY);
//        _camera.transform.position = limitedCameraPosition;
//    }

//    void OnDrawGizmos()
//    {
//        Gizmos.DrawWireCube(Vector3.zero, new Vector3(mapWidth, 0, mapLength));
//    }
//}
