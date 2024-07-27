using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_menu_processing : MonoBehaviour
{
    [SerializeField] string party_management_scene;
    [SerializeField] string inventory_management_scene;
    [SerializeField] string journal_management_scene;
    [SerializeField] string data_management_scene;
    [SerializeField] string options_management_scene;
    [SerializeField] string travel_management_scene;

    [SerializeField] GameObject start_menu_canvas;
    [SerializeField] LocationDatabase location_database; // it seems silly to be linking to LDB so frequently...


    // intended to be used in the world menu
    // will reference world menu stuff
    // just build another for different scenes
    // get over the dnr. smile.
    public void Awake()
    {
        start_menu_canvas.gameObject.SetActive(false);
    }



    public void change_scene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    public void open_party_manager() { change_scene(party_management_scene); }
    public void open_inventory_manager() { change_scene(inventory_management_scene); }
    public void open_journal() { change_scene(journal_management_scene); }
    public void open_data_management() { change_scene(data_management_scene); }

    public void onSaveGame()
    {
        DataPersistenceManager.instance.SaveGame();
    }

    public void onLoadGame()
    {
        DataPersistenceManager.instance.LoadGame();
        onResetPins();
    }

    public void onResetPins()
    {
        foreach (LocationPinObect pin in location_database.world_map_pins.Values)
        {
            pin.update_pin_status();
        }
    }

    public void open_fast_travel() { change_scene(travel_management_scene); }
    public void close_menu() { start_menu_canvas.gameObject.SetActive(false); }
}
