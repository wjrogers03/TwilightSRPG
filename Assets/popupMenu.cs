using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class popupMenu : MonoBehaviour
{
    [SerializeField] GameObject menuItemContainer;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Sprite background_image;
    [SerializeField] public string menu_title;
    [SerializeField] public Dictionary<string, Action> menu_options = new();


    
    // list to hold all the buttons after being instantiated
    List<string> menu_items;

    // list of elements to reenable once 
    List<string> returnElements;



    public void ConstructMenu()
    {
        foreach (KeyValuePair<string,Action> option in menu_options)
        {
            GameObject btn = Instantiate(buttonPrefab, menuItemContainer.transform);
            btn.GetComponent<subMenuButton>().DisplayText.text = option.Key;
            btn.GetComponent<subMenuButton>().callback = option.Value;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
