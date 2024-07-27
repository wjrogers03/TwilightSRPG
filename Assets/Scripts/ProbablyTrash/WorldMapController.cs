using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapController : MonoBehaviour
{
    //[SerializeField] GameProfileManager game_profile_manager;

    [SerializeField] float offset_x;
    [SerializeField] float offset_y;
    [SerializeField] GameObject button_container_layer1;
    [SerializeField] GameObject button_container_layer2;
    [SerializeField] GameObject button_container_layer3;
    [SerializeField] GameObject button_container_layer4;
    public void launch_world_map_menu()
    {

    }

    public void SourceLocationOptions(string Location)
    {
        // reads the LUT to identify the options available for this location
    }

    public List<string> MovementOptions()
    {
        List<string> _opts = new List<string>();
        _opts.Add("Move");//move is a GameProfileManager method that will change the characters position from their current to the new position. This is only available when the player's position is not one selected.
        _opts.Add("Cancel");
        return _opts;
    }

    public List<string> LocationOptions()
    {
        List<string> _opts = new List<string>();
        _opts.Add("Enter Map"); //EnterMap is a GameProfileManager method which will change the scene, to the scene associated with this location. Available only when present
        _opts.Add("Hail Bobert"); // CharacterConvo("Bobert") is a WorldMapController method which will launch the dialogue and shop menu for Bobert. CharacterConvo will refrence the GPM to determain available options
        _opts.Add("Cancel");
        return _opts;
    }

    public void SpawnMenu(string location, Vector3 marker_position, List<string> options) {
        // this might need to be in a parent menu object that world_map_menu, dungeon_map_menu, and battle_stage_menu inheret. 
        //int NumberOfOptions = 2;
        //List<string> options = new List<string>(); // This options list should be returned via a LUT search.
        // the options below should be added to a list based on the lut search. 
        //options.Add("Move"); //move is a GameProfileManager method that will change the characters position from their current to the new position. This is only available when the player's position is not one selected.
        //options.Add("Enter Map"); //EnterMap is a GameProfileManager method which will change the scene, to the scene associated with this location. Available only when present
        //options.Add("Speak with Bobert"); // CharacterConvo("Bobert") is a WorldMapController method which will launch the dialogue and shop menu for Bobert. CharacterConvo will refrence the GPM to determain available options
        //options.Add("Move (battle)"); //battle_move(target) is a unit action. // all methods in the battle scene should be unit actions, initially started via a call from the GameClock.
        //options.Add("Attack"); // battle_attack(target) is a Unit method.
        //options.Add("Cancel"); // this is a WorldMapController option which will call the destroy all buttons method.
        float btnCount = 0;
        float btnHeight = 0.5f; // this seems really small and i'm expected it to not be a standard value...
        
        GameObject title_text = gameObject.GetComponent<GeneralMenuMain>().AddTitle(location, marker_position.x + 2, marker_position.y + btnHeight);

        foreach (var opt in options)
        {
            GameObject abutton = gameObject.GetComponent<GeneralMenuMain>().AddButton(opt, marker_position.x+2, marker_position.y-(btnHeight * btnCount),0);
            btnCount += 1;
        }
    }

    public void DespawnMenusAll()
    {
        DespawnMenu(button_container_layer1);
        DespawnMenu(button_container_layer2);
        DespawnMenu(button_container_layer3);
        DespawnMenu(button_container_layer4);
    }

    public void DespawnMenu(GameObject button_container)
    {
        Button[] buttons = button_container.GetComponentsInChildren<Button>();
        if (buttons.Length > 0)
        {
            foreach (Button button in buttons)
            {
                DestroyImmediate(button.gameObject);
            }
        }
        // buttons contain tmp_text so this filter still finds them...
        TMP_Text[] texts = button_container.GetComponentsInChildren<TMP_Text>();
        if (texts.Length > 0)
        {
            foreach (TMP_Text obj in texts)
            {
                DestroyImmediate(obj.gameObject);
            }
        }
    }

    public void launch_move_menu(string location, Vector3 menu_position)
    {
        List<string> opts = MovementOptions();
        SpawnMenu(location, menu_position, opts);
    }

    public void launch_location_menu(string location, Vector3 menu_position)
    {
        List<string> opts = LocationOptions();
        SpawnMenu(location, menu_position, opts);
    }


    public void SetFocus(GameObject ui_element)
    {
        
    }
}
