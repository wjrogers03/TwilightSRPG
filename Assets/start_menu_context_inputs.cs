using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_menu_context_inputs : MonoBehaviour
{

    [SerializeField] WorldMenuManager wm_manager;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) )
        {
            this.gameObject.SetActive(false);
            //wm_manager.interactive_all_menus();
        }
    }
}
