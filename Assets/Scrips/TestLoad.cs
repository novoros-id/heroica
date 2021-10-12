using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoad : MonoBehaviour
{
    public GameObject ConstantObjects;

    private void OnMouseDown()
    {
        //Debug.Log(PlayerPrefs.GetString("LastJSON"));
        //string[] words = PlayerPrefs.GetString("LastJSON").Split('|');
        //for (int b = 0; b < words.Length; b++)
        //{
        //    ObjectToJSON bcd = JsonUtility.FromJson<ObjectToJSON>(words[b]);
        //    //Debug.Log(bcd.name);
        //    //Debug.Log("name:" + bcd.name + "prefab:" + bcd.prefab);
        //    //prefab = Resources.Load("Floor1") as GameObject;
        //    //Debug.Log(bcd.prefab);
        //    Instantiate(Resources.Load(bcd.prefab), bcd.position_, bcd.rotation_);
        //}
        
    }
   
    public void Awake()
    {
        Instantiate(ConstantObjects, new Vector3(0f, 0f, 0f), Quaternion.identity);

        string[] words = PlayerPrefs.GetString("LastJSON").Split('|');
        for (int b = 0; b < words.Length; b++)
        {
            ObjectToJSON bcd = JsonUtility.FromJson<ObjectToJSON>(words[b]);
            //Debug.Log(bcd.name);
            //Debug.Log("name:" + bcd.name + "prefab:" + bcd.prefab);
            //prefab = Resources.Load("Floor1") as GameObject;
            //Debug.Log(bcd.prefab);
            Instantiate(Resources.Load(bcd.prefab), bcd.position_, bcd.rotation_);
        }
    }
}
