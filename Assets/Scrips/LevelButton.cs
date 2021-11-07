using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public GameObject Text;

    public GameObject ConstantObjects;

    public string numberlevel;

   

    public void SetNumberLevel(string number_level)
    {
        numberlevel = number_level;
        string Level = PlayerPrefs.GetString(number_level);

        LevelSave asd = JsonUtility.FromJson<LevelSave>(Level);

        Text.GetComponent<Text>().text = asd.levelname;
    }
    public void StartLevel()
    {
        Instantiate(ConstantObjects, new Vector3(0f, 0f, 0f), Quaternion.identity);

        GameObject CanvasOne = GameObject.Find("CanvasOne");

        CanvasOne.SetActive(false);

        string Level = PlayerPrefs.GetString(numberlevel);

        LevelSave asd = JsonUtility.FromJson<LevelSave>(Level);

        //Debug.Log(asd.levelname);
        //Debug.Log(asd.HeroKnight);
        //Debug.Log(asd.HeroBarbarian);
        //Debug.Log(asd.HeroMage);
        //Debug.Log(asd.HeroPriest);

        SetSettings();

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

        CanvasOne.SetActive(false);
    }

    public void SetSettings()
    {
        string Level = PlayerPrefs.GetString(numberlevel);

        LevelSave asd = JsonUtility.FromJson<LevelSave>(Level);

        Main DLight = GameObject.Find("Directional Light").GetComponent<Main>();

        if (asd.HeroKnight == "1")
        {
            DLight.Knight_aviable = true;
        }
        else
        {
            DLight.Knight_aviable = false;
        }

        if (asd.HeroBarbarian == "1")
        {
            DLight.Barbarian_aviable = true;
        }
        else
        {
            DLight.Barbarian_aviable = false;
        }

        if (asd.HeroMage == "1")
        {
            DLight.Mage_aviable = true;
        }
        else
        {
            DLight.Mage_aviable = false;
        }

        if (asd.HeroPriest == "1")
        {
            DLight.Priest_aviable = true;
        }
        else
        {
            DLight.Priest_aviable = false;
        }

        if (asd.ShopAvai == "1")
        {
            DLight.Shop_aviable = true;
        }
        else
        {
            DLight.Shop_aviable = false;
        }

        if (asd.CrystalAvai == "1")
        {
            DLight.Crystal_aviable = true;
        }
        else
        {
            DLight.Crystal_aviable = false;
        }

        if (asd.SoloAvai == "1")
        {
            DLight.solo_player = true;
        }
        else
        {
            DLight.solo_player = false;
        }

        if (asd.SurvivalAvai == "1")
        {
            DLight.survival = true;
        }
        else
        {
            DLight.survival = false;
        }
    }
}
