using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    public GameObject[] spawns;
    public Transform[] spawnpoints;

    private int rand;
    private int randspawns;
    private int randPosition;
    public float startTimeBtwSpawns;
    private float timeBtwSpawns;

    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    void Update()
    {
        
        if(timeBtwSpawns <= 0)
        {
            rand = Random.Range(0, spawnpoints.Length);
            randspawns = Random.Range(0, spawns.Length);
            randPosition = Random.Range(0, spawnpoints.Length);
            Instantiate(spawns[randspawns], spawnpoints[randPosition].transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }

    }
}
