using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanObject : MonoBehaviour
{
    public float startTimeBtwDeaths = 4;
    private float timeBtwDeaths;
    public float roty;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwDeaths = startTimeBtwDeaths;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, roty, 0);
        transform.position = new Vector3(transform.position.x + 0.2f, -1.05f, transform.position.z);
        if (timeBtwDeaths <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timeBtwDeaths -= Time.deltaTime;
        }
    }
}
