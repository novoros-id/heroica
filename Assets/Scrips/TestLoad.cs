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


        string Level = PlayerPrefs.GetString("LastJSON");

        LevelSave asd = JsonUtility.FromJson<LevelSave>(Level);

        Debug.Log(asd.levelname);
        Debug.Log(asd.HeroKnight);
        Debug.Log(asd.HeroBarbarian);
        Debug.Log(asd.HeroMage);
        Debug.Log(asd.HeroPriest);

        string[] words = asd.FieldsObjects_.Split('|');
        for (int b = 0; b < words.Length; b++)
        {
            FieldsObjects bcd = JsonUtility.FromJson<FieldsObjects>(words[b]);
            //Debug.Log(bcd.name);
            //Debug.Log("name:" + bcd.name + "prefab:" + bcd.prefab);
            //prefab = Resources.Load("Floor1") as GameObject;
            //Debug.Log(bcd.prefab);
            GameObject clone;
            clone = Instantiate(Resources.Load(bcd.prefab) as GameObject, bcd.position_, bcd.rotation_);
            clone.name = clone.name + b as string;

        }
    }
}
