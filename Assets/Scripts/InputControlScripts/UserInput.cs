using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] GameObject start_menu_canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            //manager.uninteractive_all_menus();
            start_menu_canvas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            //manager.view_full_map();
        }
        
        if (Input.GetKeyUp(KeyCode.M))
        {
            //manager.view_focus_map();
        }
    }
}
