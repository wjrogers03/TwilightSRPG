using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldMenuFactory : MonoBehaviour
{
    [Header("Reference Objects")]
    [Tooltip("To be set by the objects themselve, visible for debug.")]
    [SerializeField] public LocationPinObect reference_pin;
    [SerializeField] public GameObject player_marker;
    [SerializeField] public Camera main_camera; // doing camera control here seems bad but also like the only place...
    

    [Header("Menu Generation Prefabs")]
    [SerializeField] GameObject titlePrefab;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject menuParent;

    [Header("Button Spacing Settings")]
    [SerializeField] float btnHeight = 0.5f;
    [SerializeField] int x_offset = 2;
    [SerializeField] int y_offset = 2;


    [HideInInspector]
    public List<WorldMenuAction> actions;
    [HideInInspector]
    public string CanvasTitle;

    public Action get_callback(string label)
    {
        Action callback;
        switch (label)
        {
            case "move":
                callback = move;
                break;
            case "enter":
                callback = enter;
                break;
            case "explore":
                callback = enter;
                break;
            case "inspect":
                callback = inspect;
                break;
            case "cancel":
                callback = cancel;
                break;
            default:
                callback = TestCallback;
                break;
        }
        return callback;
    }

    public void clear_canvas()
    {
        actions = new();
        destroy_menu(menuParent);
    }

    public void destroy_menu(GameObject menu_container)
    {
        Button[] buttons = menu_container.GetComponentsInChildren<Button>();
        if (buttons.Length > 0)
        {
            foreach (Button button in buttons)
            {
                DestroyImmediate(button.gameObject);
            }
        }
        // buttons contain tmp_text so the above method finds them, this is just proper ordering.
        TMP_Text[] texts = menu_container.GetComponentsInChildren<TMP_Text>();
        if (texts.Length > 0)
        {
            foreach (TMP_Text obj in texts)
            {
                DestroyImmediate(obj.gameObject);
            }
        }
    }

    public void build_menu()
    {
        GameObject title_text = AddTitle(CanvasTitle,
            reference_pin.gameObject.transform.position.x + x_offset,
            reference_pin.gameObject.transform.position.y + btnHeight);

        int btnCount = 0;

        foreach (WorldMenuAction wma in actions)
        {
            float btn_x = reference_pin.gameObject.transform.position.x + x_offset;
            //float btn_x = x_offset;
            float btn_y = reference_pin.gameObject.transform.position.y - (btnHeight * btnCount)+y_offset;
            //float btn_y = -1f*(btnHeight * y_offset);

            GameObject abutton = AddButton(wma.display_name, btn_x, btn_y, get_callback(wma.callback_name));
            btnCount += 1;
        }
    }

    public GameObject AddTitle(string displaytext, float x, float y)
    {
        GameObject title_text = Instantiate(titlePrefab, menuParent.transform);
        string loc = displaytext; // I don't know why I need to set the string to a new string for this to work...
        title_text.GetComponent<TMPro.TextMeshProUGUI>().text = loc;
        Vector3 placement_position = new Vector3(x, y, 0);
        title_text.transform.position = placement_position;
        return title_text;
    }

    public GameObject AddButton(string displaytext, float x, float y, Action callback)
    {
        GameObject newButton = Instantiate(buttonPrefab, menuParent.transform);
        Vector3 placement_position = new Vector3(x, y, 0);

        newButton.GetComponent<MenuButton>().DisplayText.text = displaytext;

        newButton.GetComponent<Button>().onClick.AddListener(() => callback());

        newButton.transform.position = placement_position;

        return newButton;
    }

    #region Callbacks
    public void move() 
    {
        clear_canvas();
        main_camera.GetComponent<WorldMapCameraController>().FollowPlayer();
        // mark the pin we're moving to as occupied
        reference_pin.onOccupied();
        // move the marker
        player_marker.GetComponent<PlayerMarker>().move_to_next_pin(reference_pin.gameObject.transform.localPosition, this.reference_pin);

        foreach (GameObject connected_pin in reference_pin.connected_locations)
        {
            connected_pin.gameObject.GetComponent<LocationPinObect>().onVacated();
        }
        
        
        

    }
    public void cancel()
    {
        clear_canvas();
    }
    public void inspect()
    {
        print(reference_pin.associated_location.Description);
    }
    public void enter()
    {
        // scene change animations would go here i think.

        clear_canvas();

        GameSceneManager gsm = new();

        StaticDataHandler.instance.location_pin_intermediate = reference_pin;
        StaticDataHandler.instance.location_database_intermediate = reference_pin.LDB;

        gsm.load_scene(reference_pin.associated_location.SceneName);

        
    }
    public void TestCallback()
    {
        print("Test method called");
    }

    public void print(string msg) { Debug.Log(msg);  }
    #endregion
}
