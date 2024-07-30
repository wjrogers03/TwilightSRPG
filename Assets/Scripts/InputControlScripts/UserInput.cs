using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    [SerializeField] GameObject start_menu_canvas;
    [SerializeField] PinSelector pin_selector;
    [SerializeField] GameObject player_marker;
    [SerializeField] GameObject location_menu_selector;
    [SerializeField] WorldMenuFactory world_menu_factory;
    [SerializeField] Camera main_camera;

    private string selection_layer = "worldmap";

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

        switch (selection_layer)
        {
            case "worldmap":
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    // get pin
                    GameObject loc = pin_selector.current_pin.GetComponent<LocationPinObect>().connectedLocations.location_right;
                    if (loc != null)
                    {
                        pin_selector.current_pin = loc.gameObject;
                        pin_selector.move_to_next_pin(pin_selector.current_pin);

                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    GameObject loc = pin_selector.current_pin.GetComponent<LocationPinObect>().connectedLocations.location_left;
                    if (loc != null)
                    {
                        pin_selector.current_pin = loc.gameObject;
                        pin_selector.move_to_next_pin(pin_selector.current_pin);
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameObject loc = pin_selector.current_pin.GetComponent<LocationPinObect>().connectedLocations.location_up;
                    if (loc != null)
                    {
                        pin_selector.current_pin = loc.gameObject;
                        pin_selector.move_to_next_pin(pin_selector.current_pin);
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GameObject loc = pin_selector.current_pin.GetComponent<LocationPinObect>().connectedLocations.location_down;
                    if (loc != null)
                    {
                        pin_selector.current_pin = loc.gameObject;
                        pin_selector.move_to_next_pin(pin_selector.current_pin);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (pin_selector.current_pin != null)
                    {
                        pin_selector.current_pin.GetComponent<LocationPinObect>().OnMouseDown();
                        location_menu_selector.SetActive(true);
                        List<GameObject> btns = world_menu_factory.GetComponent<WorldMenuFactory>().active_buttons;
                        if (location_menu_selector.GetComponent<LocationMenuSelector>().current_button == null)
                        {
                            location_menu_selector.GetComponent<LocationMenuSelector>().current_button = btns[0];
                        }
                        location_menu_selector.GetComponent<LocationMenuSelector>().current_button = btns[0];
                        location_menu_selector.GetComponent<LocationMenuSelector>().assign_destination(btns[0].transform.localPosition);
                        //location_menu_selector.GetComponent<LocationMenuSelector>().onInput();
                        location_menu_selector.GetComponent<LocationMenuSelector>().jump_to_destination();
                        this.selection_layer = "location";
                        // set the location selector to active.
                    }
                }
                break;

            case "location":
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    // get button
                    List<GameObject> btns = world_menu_factory.GetComponent<WorldMenuFactory>().active_buttons;
                    GameObject current_btn = location_menu_selector.GetComponent<LocationMenuSelector>().current_button;

                    int destination_index = world_menu_factory.GetComponent<WorldMenuFactory>().fetch_index_of_button(current_btn) - 1;

                    if (destination_index < 0)
                    {
                        destination_index = btns.Count - 1;
                    }

                    GameObject nextbutton = btns[destination_index];
                    //Debug.Log(nextbutton.name);
                    location_menu_selector.GetComponent<LocationMenuSelector>().current_button = nextbutton;
                    location_menu_selector.GetComponent<LocationMenuSelector>().assign_destination(nextbutton.transform.localPosition);
                    location_menu_selector.GetComponent<LocationMenuSelector>().onInput();
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    // moving down is stepping UP the list.
                    List<GameObject> btns = world_menu_factory.GetComponent<WorldMenuFactory>().active_buttons;
                    GameObject current_btn = location_menu_selector.GetComponent<LocationMenuSelector>().current_button;

                    int destination_index = world_menu_factory.GetComponent<WorldMenuFactory>().fetch_index_of_button(current_btn)+1;
                    if (destination_index > (btns.Count - 1))
                    { 
                        destination_index = 0;
                    }

                    GameObject nextbutton = btns[destination_index];
                    //Debug.Log(nextbutton.name);
                    
                    location_menu_selector.GetComponent<LocationMenuSelector>().current_button = nextbutton;
                    location_menu_selector.GetComponent<LocationMenuSelector>().assign_destination(nextbutton.transform.localPosition);
                    
                    location_menu_selector.GetComponent<LocationMenuSelector>().onInput();
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    location_menu_selector.GetComponent<LocationMenuSelector>().current_button.GetComponent<Button>().onClick.Invoke();
                    this.selection_layer = "worldmap";
                    world_menu_factory.GetComponent<WorldMenuFactory>().clear_canvas();
                    location_menu_selector.SetActive(false);
                    //Debug.Log("Selecting menu action: ");
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    this.selection_layer = "worldmap";
                    world_menu_factory.GetComponent<WorldMenuFactory>().clear_canvas();
                    location_menu_selector.SetActive(false);
                    // close location menu
                    // set the location menu selector to inactive
                }
                break;
        }


    }
}
