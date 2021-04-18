using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidCameraControll : MonoBehaviour
{

    public float zoomMax;
    public float zoomMin;
    public float Sensitivity;

    private Camera mainCamera;

    private Touch touchA;
    private Touch touchB;
    private Vector2 touchADirection;
    private Vector2 touchBDirection;
    private float dstBtwTouchesPositions;
    private float dstBtwTouchesDirections;
    private float zoom;


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.touchCount == 2)
        {
            touchA = Input.GetTouch(0);
            touchA = Input.GetTouch(1);
            touchADirection = touchA.position - touchA.deltaPosition;
            touchBDirection = touchB.position - touchB.deltaPosition;

            dstBtwTouchesPositions = Vector2.Distance(touchA.position, touchBDirection);
            dstBtwTouchesDirections = Vector2.Distance(touchADirection, touchBDirection);

            zoom = dstBtwTouchesPositions - dstBtwTouchesDirections;

            var currentZoom = mainCamera.orthographicSize - zoom * Sensitivity;

            mainCamera.orthographicSize = Mathf.Clamp(currentZoom, zoomMin, zoomMax);

            if (touchBDirection != touchB.position)
            {
                var angle = Vector3.SignedAngle(touchB.position - touchA.position, touchBDirection - touchADirection, -mainCamera.transform.forward);
                mainCamera.transform.RotateAround(mainCamera.transform.position, -mainCamera.transform.forward, angle);
            }
        }
        
    }
}
