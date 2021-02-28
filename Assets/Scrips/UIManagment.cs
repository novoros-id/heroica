using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagment : MonoBehaviour
{

    Text hearts;
    public Player_ pl;
    public GameObject key;
    
    // Start is called before the first frame update
    void Start()
    {
        hearts = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(pl.key == true)
        {
            key.SetActive(true);
        }

        if (pl.key == false)
        {
            key.SetActive(false);
        }

        hearts.text = pl.leaves.ToString();
    }
}
