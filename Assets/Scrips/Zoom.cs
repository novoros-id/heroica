using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    void Start()
	{
        
        

        //limit = Mathf.Abs(limit);
  //      if (limit > 90) limit = 90;
  //      offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
  //      transform.position = target.position + offset;

  //      X = transform.localEulerAngles.y + 0 * sensitivity;
  //      Y += -7 * sensitivity;
  //      Y = Mathf.Clamp(Y, -limit, limit);
  //      transform.localEulerAngles = new Vector3(-Y, X, 0);
  //      transform.position = transform.localRotation * offset + target.position;

    }

	void Update()
	{

        if (GetComponent<Camera>().fieldOfView >= 105)
        {
            GetComponent<Camera>().fieldOfView = 105;
        }

        if (GetComponent<Camera>().fieldOfView <= 5)
        {
            GetComponent<Camera>().fieldOfView = 5;
        }

        if (Input.GetKey("q"))
        {
            transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y - 0.2f, transform.localEulerAngles.z);
        }

        if (Input.GetKey("e"))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0.2f, transform.localEulerAngles.z);
        }

        ////transform.position = new Vector3(target.position.x - 3.4f,4.5f,target.position.z + 1.5f);

        ////if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
        ////else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
        ////offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView - 5;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView + 5;
        }

        //      if (Input.GetMouseButton(1))
        //      {
        //          X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        //	Y += Input.GetAxis("Mouse Y") * sensitivity;
        //	Y = Mathf.Clamp(Y, -limit, limit);
        //	transform.localEulerAngles = new Vector3(-Y, X, 0);
        //	transform.position = transform.localRotation * offset + target.position;
        //      }

        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") > 0 )
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

  //      if (Input.GetKeyDown("a"))
		//{
		//	transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
		//}

		//if (Input.GetKeyDown("d"))
		//{
		//	transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
		//}

		//if (Input.GetKeyDown("w"))
		//{
		//	transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z - 1);
		//}

		//if (Input.GetKeyDown("s"))
		//{
		//	transform.position = new Vector3(transform.position.x , transform.position.y , transform.position.z + 1);
		//}
	}
    private void FixedUpdate()
    {

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed_;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        if (go == 1) 
        {
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
            if(transform.position == endMarker.position)
            {
                go = 0;
                transform.position = endMarker.position;
            }
        }
        
    }
}
