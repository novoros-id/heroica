using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class ScrollView : MonoBehaviour
{
    [SerializeField] RectTransform txtRT;
    [SerializeField] RectTransform contentRT;
    private string lang;

    private void Start()
    {
        if (PlayerPrefs.HasKey("lang"))
        {
            lang = PlayerPrefs.GetString("lang");
        }
        else
        {
            lang = "en";
        }
        if(lang == "ru")
        {
            txtRT = GameObject.Find("level Description_ru").GetComponent<RectTransform>();
        }
        else
        {
            txtRT = GameObject.Find("level Description").GetComponent<RectTransform>();
        }
    }
    // Update is called once per frame
    void Update()
    { 
        var size = contentRT.sizeDelta;
        size.y = txtRT.sizeDelta.y;
        contentRT.sizeDelta = size;
    }
}
