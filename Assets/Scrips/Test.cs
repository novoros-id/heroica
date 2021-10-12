using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void OnMouseDown()
    {
        //position_ = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        //string a = JsonUtility.ToJson(this);
        //Debug.Log(a);

        //List<ObjectToJSON> massiveJSON = new List<ObjectToJSON>(); ;



        GameObject[] Floors;
        Floors = GameObject.FindGameObjectsWithTag("Floor");
        GameObject[] Furniture;
        Furniture = GameObject.FindGameObjectsWithTag("Furniture");
        GameObject[] Doors;
        Doors = GameObject.FindGameObjectsWithTag("Door");
        GameObject[] Keys;
        Keys = GameObject.FindGameObjectsWithTag("Key");
        GameObject[] enemys_1;
        enemys_1 = GameObject.FindGameObjectsWithTag("Enemy_1");
        GameObject[] enemys_2;
        enemys_2 = GameObject.FindGameObjectsWithTag("Enemy_2");
        GameObject[] enemys_bosses;
        enemys_bosses = GameObject.FindGameObjectsWithTag("Enemy_boss");
        GameObject[] coins;
        coins = GameObject.FindGameObjectsWithTag("item_gold");
        GameObject[] bloods;
        bloods = GameObject.FindGameObjectsWithTag("item_blood");
        GameObject[] axes;
        axes = GameObject.FindGameObjectsWithTag("item_axe");
        GameObject[] batons;
        batons = GameObject.FindGameObjectsWithTag("item_baton");
        GameObject[] scythes;
        scythes = GameObject.FindGameObjectsWithTag("item_scythe");

        ObjectToJSON abcds = new ObjectToJSON();

        string LastJSON = "";

        string LastJSONFloors = "";
        string LastJSONFurniture = "";
        string LastJSONDoors = "";
        string LastJSONKeys = "";
        string LastJSONenemys_1 = "";
        string LastJSONenemys_2 = "";
        string LastJSONenemys_boss = "";
        string LastJSONcoins = "";
        string LastJSONbloods = "";
        string LastJSONaxes = "";
        string LastJSONbatons = "";
        string LastJSONscythes = "";

        if (PlayerPrefs.HasKey("LastJSON"))
        {
            LastJSON = PlayerPrefs.GetString("LastJSON");
        }
        else
        {
            LastJSON = "";
        }

        for (int b = 0; b < Floors.Length; b++)
        {  
            abcds.CreateObject(Floors[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONFloors = LastJSONFloors + ab + " | ";
        }

        for (int b = 0; b < Furniture.Length; b++)
        {
            
            abcds.CreateObject(Furniture[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONFurniture = LastJSONFurniture + ab + " | ";
        }

        for (int b = 0; b < Doors.Length; b++)
        {
            abcds.CreateObject(Doors[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONDoors = LastJSONDoors + ab + " | ";
        }
        for (int b = 0; b < Keys.Length; b++)
        {
            abcds.CreateObject(Keys[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONKeys = LastJSONKeys + ab + " | ";
        }
        for (int b = 0; b < enemys_1.Length; b++)
        {
            abcds.CreateObject(enemys_1[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONenemys_1 = LastJSONenemys_1 + ab + " | ";
        }
        for (int b = 0; b < enemys_2.Length; b++)
        {
            abcds.CreateObject(enemys_2[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONenemys_2 = LastJSONenemys_2 + ab + " | ";
        }
        for (int b = 0; b < enemys_bosses.Length; b++)
        {
            abcds.CreateObject(enemys_bosses[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONenemys_boss = LastJSONenemys_boss + ab + " | ";
        }
        for (int b = 0; b < coins.Length; b++)
        {
            abcds.CreateObject(coins[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONcoins = LastJSONcoins + ab + " | ";
        }
        for (int b = 0; b < bloods.Length; b++)
        {
            abcds.CreateObject(bloods[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONbloods = LastJSONbloods + ab + " | ";
        }
        for (int b = 0; b < axes.Length; b++)
        {
            abcds.CreateObject(axes[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONaxes = LastJSONaxes + ab + " | ";
        }
        for (int b = 0; b < batons.Length; b++)
        {
            abcds.CreateObject(batons[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONbatons = LastJSONbatons + ab + " | ";
        }
        for (int b = 0; b < scythes.Length; b++)
        {
            abcds.CreateObject(scythes[b]);

            string ab = JsonUtility.ToJson(abcds);
            LastJSONscythes = LastJSONscythes + ab + " | ";
        }

        LastJSON = LastJSONaxes + LastJSONbatons + LastJSONbloods + LastJSONcoins + LastJSONDoors + LastJSONenemys_1 + LastJSONenemys_2 + LastJSONenemys_boss + LastJSONFloors + LastJSONFurniture + LastJSONKeys + LastJSONscythes;

        PlayerPrefs.SetString("LastJSON", LastJSON);
        PlayerPrefs.Save();

        //string ab = JsonUtility.ToJson(massiveJSON);
        Debug.Log(LastJSON);

        ////string[] words = LastJSON.Split('|');

        ////for (int b = 0; b < words.Length; b++) 
        ////{
        ////    ObjectToJSON bcd = JsonUtility.FromJson<ObjectToJSON>(words[b]);
        ////    Debug.Log(bcd.name);
        ////    Instantiate(floor, bcd.position_, bcd.rotation_);
        ////}

    }
}
public class ObjectToJSON
{
    public string name;
    public string prefab;
    public Vector3 position_;
    public Quaternion rotation_;

    public void CreateObject(GameObject obj)
    {
        name = obj.name;
        prefab = obj.GetComponent<MayCreatedItems>().prefabname;
        position_ = obj.transform.position;
        rotation_ = obj.transform.rotation;
    }
}
