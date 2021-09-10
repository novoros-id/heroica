using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class level_script : MonoBehaviour
{

    public Button Lvl1Button_;
    public Button Lvl2Button_;
    public Button Lvl3Button_;
    public Button Lvl4Button_;
    public Button Lvl5Button_;
    public Button Lvl6Button_;
    public Button Lvl7Button_;
    public Button Lvl8Button_;
    public Button Lvl9Button_;
    public Button Lvl10Button_;
    public Button Lvl11Button_;
    public Button Lvl12Button_;
    public Button Lvl13Button_;
    public Button Lvl14Button_;
    public Button Lvl15Button_;

    public Dictionary<string, Button> mas_Button = new Dictionary<string, Button>();

    // Start is called before the first frame update
    private void Awake()
    {
        mas_Button.Add("Test",Lvl1Button_);
        mas_Button.Add("Level2", Lvl2Button_);
        mas_Button.Add("Level3", Lvl3Button_);
        mas_Button.Add("Level4", Lvl4Button_);
        mas_Button.Add("Level5_", Lvl5Button_);
        mas_Button.Add("Level6", Lvl6Button_);
        mas_Button.Add("Level7", Lvl7Button_);
        mas_Button.Add("Level8", Lvl8Button_);
        mas_Button.Add("Level9", Lvl9Button_);
        mas_Button.Add("Level10", Lvl10Button_);
        mas_Button.Add("Level11", Lvl11Button_);
        mas_Button.Add("Level12", Lvl12Button_);
        mas_Button.Add("Level13", Lvl13Button_);
        mas_Button.Add("Level14",Lvl14Button_);
        mas_Button.Add("Level15", Lvl15Button_);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        // Считаем все переменные из сохранненых данных
       
        GameObject cam = GameObject.Find("Directional Light");
        Main mScript = cam.GetComponent<Main>();


        foreach (var item in mas_Button)
        {
            string name_save = mScript.get_level_complete_name(item.Key);

            if (PlayerPrefs.HasKey(name_save))
            {
                int level_save = PlayerPrefs.GetInt(name_save);

                if (level_save == 1)
                {
                    item.Value.interactable = true;
                }
                else
                {
                    item.Value.interactable = false;
                }
            }
            else
            {
                item.Value.interactable = false;
            }

        }

        // и установим видимость, на следующий после отсутвия видимости и сразу прервемся

        foreach (var item in mas_Button)
        {
            if (item.Value.interactable == false)
            {
                item.Value.interactable = true;
                break;
            }

        }
    }
}
