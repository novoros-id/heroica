using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public string LevelName;
    public GameObject SettingsObject;
    public string MountAvalaible;
    public string SeaAvalaible;
    public string FieldAvalaible;
    public string KnightAvalaible;
    public string BarbarianAvalaible;
    public string MageAvalaible;
    public string PriestAvalaible;
    public string ShopAvalaible;
    public string CrystalAvalaible;
    public string SoloAvalaible;
    public string SurvivalAvalaible;

    public int LevelMaxCount;

    public void GetLevelName()
    {
        MountAvalaible = GameObject.Find("ImageMn").GetComponent<ChoozeField>().Field;
        SeaAvalaible = GameObject.Find("ImageSe").GetComponent<ChoozeField>().Field;
        FieldAvalaible = GameObject.Find("ImageFi").GetComponent<ChoozeField>().Field;
        KnightAvalaible = GameObject.Find("ImageKn").GetComponent<HeroesAvaibility>().HeroAvaibility;
        BarbarianAvalaible = GameObject.Find("ImageBr").GetComponent<HeroesAvaibility>().HeroAvaibility;
        MageAvalaible = GameObject.Find("ImageMg").GetComponent<HeroesAvaibility>().HeroAvaibility;
        PriestAvalaible = GameObject.Find("ImagePr").GetComponent<HeroesAvaibility>().HeroAvaibility;
        ShopAvalaible = GameObject.Find("ImageSh").GetComponent<HeroesAvaibility>().HeroAvaibility;
        CrystalAvalaible = GameObject.Find("ImageCr").GetComponent<HeroesAvaibility>().HeroAvaibility;
        SoloAvalaible = GameObject.Find("ImageSl").GetComponent<HeroesAvaibility>().HeroAvaibility;
        SurvivalAvalaible = GameObject.Find("ImageSr").GetComponent<HeroesAvaibility>().HeroAvaibility;
        LevelName = GameObject.Find("LevelName").GetComponent<Text>().text;
    }
    public void Start()
    {
        SettingsObject.SetActive(true);

        GameObject Mn = GameObject.Find("ImageMn");
        ChoozeField MnCf = Mn.GetComponent<ChoozeField>();
        MnCf.ChangeField(); 

        GameObject Sl = GameObject.Find("ImageSl");
        HeroesAvaibility SlHr = Sl.GetComponent<HeroesAvaibility>();
        SlHr.HeroAvaibility = "0";
        SlHr.cross.SetActive(false);
    }
    public void SettingsOn()
    {
        SettingsObject.SetActive(true);
    }
    public void SettingsOFF()
    {
        SettingsObject.SetActive(false);
        GameObject StartF = GameObject.Find("StartFloor");
        StartF.GetComponent<MayCreatedItems>().SetSelected();
    }

    public void ExitLevel()
    {
        Application.LoadLevel("Start");
    }

    public void SaveLevel()
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

        FieldsObjects abcds = new FieldsObjects();

        LevelSave LevelSettings = new LevelSave();

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

        if (PlayerPrefs.HasKey("LevelMaxCount"))
        {
            LevelMaxCount = PlayerPrefs.GetInt("LevelMaxCount");
        }
        else
        {
            LevelMaxCount = 0;
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

        GetLevelName();

        LevelMaxCount = LevelMaxCount + 1;

        LevelSettings.CreateObject(LevelName,LastJSON, MountAvalaible, SeaAvalaible, FieldAvalaible, KnightAvalaible,BarbarianAvalaible,MageAvalaible,PriestAvalaible, ShopAvalaible, CrystalAvalaible, SoloAvalaible, SurvivalAvalaible, LevelMaxCount);

        if (LevelName == "")
        {
            return;
        }
        

        string LevelSettingsEnd = JsonUtility.ToJson(LevelSettings);

        //PlayerPrefs.SetString("LastJSON", LastJSON);
        //PlayerPrefs.SetString(LevelMaxCount.ToString(), LevelSettingsEnd);
        //PlayerPrefs.SetString("LevelName", LevelMaxCount);

        string levelsName = PlayerPrefs.GetString("LevelsNames");
        string[] massLevelsName = levelsName.Split('|');

        bool level_exists = false;

        foreach (string ln in massLevelsName)
        {
            if (ln == LevelName)
            {
                level_exists = true;
            }
        }

        if (level_exists == false)
        {
            levelsName = levelsName + "|" + LevelName;
        }

        PlayerPrefs.SetString(LevelName, LevelSettingsEnd);
        PlayerPrefs.SetString("LevelsNames", levelsName);

        //int ml = PlayerPrefs.GetInt("LevelMaxCount");
        //Debug.Log(ml);
        PlayerPrefs.Save();

        //string ab = JsonUtility.ToJson(massiveJSON);
        Debug.Log(LevelSettingsEnd);

        ////string[] words = LastJSON.Split('|');

        ////for (int b = 0; b < words.Length; b++) 
        ////{
        ////    ObjectToJSON bcd = JsonUtility.FromJson<ObjectToJSON>(words[b]);
        ////    Debug.Log(bcd.name);
        ////    Instantiate(floor, bcd.position_, bcd.rotation_);
        ////}

    }
}
public class FieldsObjects
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
public class LevelSave
{
    public string levelname;
    public string FieldsObjects_;

    public string FieldMn;
    public string FieldSe;
    public string FieldFi;

    public string HeroKnight;
    public string HeroBarbarian;
    public string HeroMage;
    public string HeroPriest;

    public string ShopAvai;
    public string CrystalAvai;
    public string SoloAvai;
    public string SurvivalAvai;

    public int LevelCount;

    public void CreateObject(string levelname_, string FieldsObjects__, string FieldMn_, string FieldSe_, string FieldFi_, string HeroKnight_, string HeroBarbarian_, string HeroMage_, string HeroPriest_, string ShopAvai_, string CrystalAvai_, string SoloAvai_, string SurvivalAvai_, int LevelCount_)
    {
        FieldMn = FieldMn_;
        FieldSe = FieldSe_;
        FieldFi = FieldFi_;
        levelname = levelname_;
        FieldsObjects_ = FieldsObjects__;
        HeroKnight = HeroKnight_;
        HeroBarbarian = HeroBarbarian_;
        HeroMage = HeroMage_;
        HeroPriest = HeroPriest_;
        ShopAvai = ShopAvai_;
        CrystalAvai = CrystalAvai_;
        SoloAvai = SoloAvai_;
        SurvivalAvai = SurvivalAvai_;
        LevelCount = LevelCount_;
    }
}
