using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationMarker : MonoBehaviour
{
    [SerializeField] string scene_to_load;
    [SerializeField] WorldMapController wm_controller;
    [SerializeField] GameProfileManager game_profile_manager;
    [SerializeField] float mouse_over_scale = 1.35f;
    [SerializeField] string menu_to_launch;
    [SerializeField] string location_name;
    [SerializeField] int loction_index;
    private Vector3 base_scale;
    

    private void OnMouseOver()
    {
        Vector3 scaleChange = new Vector3(mouse_over_scale, mouse_over_scale, mouse_over_scale) + base_scale;
        gameObject.transform.localScale = scaleChange;
    }

    private void OnMouseExit()
    {
        gameObject.transform.localScale = base_scale;
        
    }

    void OnMouseDown()
    {
        Vector3 position = gameObject.transform.position;
        // if the current location is not the selected location
        // spawn the movement menu.
        // if the current location IS the selected location
        // spawn the location menu.
        wm_controller.DespawnMenusAll();
        if (location_name != game_profile_manager.location_name)
        {
            wm_controller.launch_move_menu(location_name, position);
        }
        else if (location_name == game_profile_manager.location_name)
        {
            wm_controller.launch_location_menu(location_name, position);
        }
        game_profile_manager.location_name = location_name;
        //wm_controller.SpawnMenu(location_name,position);
        //wm_controller.launch_move_menu(location_name, position);
    }

    private void Awake()
    {
        base_scale = gameObject.transform.localScale;
    }
}
