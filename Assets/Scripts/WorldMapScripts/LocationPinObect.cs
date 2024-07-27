using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using static System.Collections.Specialized.BitVector32;

[Serializable]
public class LocationPinObect : MonoBehaviour
{
    [Header("Game Managers")]
    [SerializeField] WorldMenuManager WMM;
    [SerializeField] public LocationDatabase LDB;
    [SerializeField] public WorldMenuFactory WMFactory;
    

    [Header("physical components")]
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject pin;
    [SerializeField] float mouse_over_scale = 1.35f;
    [SerializeField] Color clear_color = Color.green;
    [SerializeField] Color not_clear_color = Color.red;

    [Header("location information")]
    [SerializeField] public LocationData associated_location;
    [SerializeField] public List<GameObject> connected_locations = new();
    [SerializeField] public bool player_occupied = false;

    #region Exploring struct storage for iterative serialization

    [Serializable]
    public struct PersistantInfo
    {
        // only game changable properties will go in here, such as clear, available, visible, etc.
        public string location_name;
        public bool clear;      // has been completed
        public bool available;  // can be selected
        public bool accessible; // is visible
    }
    [Header("Persistent Info")]
    [SerializeField]
    public PersistantInfo locationInfo;
    #endregion

    [SerializeField] public string pin_guid;
    [ContextMenu("Generate Pin GUID")] // needed for tracking current position.
    public void GenerateGUID()
    {
        this.pin_guid = System.Guid.NewGuid().ToString();
    }


    [Header("Menu Builder Options")]
    [SerializeField] List<WorldMenuAction> menu_actions = new();


    [HideInInspector]
    private string location_guid; // This is sourced on awake(). 
    private Vector3 base_scale = new Vector3(1,1,1);
    [ContextMenu("Grab Associated Location GUID")]
    private void GrabGuid()
    {
        location_guid = associated_location.id;
    }
    [ContextMenu("Clear Associated Location GUID")]
    private void ClearGuid()
    {
        location_guid = "";
    }

    public void onOccupied()
    {
        this.player_occupied = true;
        GameDataManager.instance.playerData.persistantInfo.world_location_id = location_guid;
        //Debug.Log(this.player_occupied);
    }

    public void onVacated()
    {
        this.player_occupied = false;
        //Debug.Log(this.player_occupied);
    }



    private void OnMouseOver()
    {
        Vector3 scaleChange = new Vector3(mouse_over_scale, mouse_over_scale, mouse_over_scale) + base_scale;
        sphere.transform.localScale = scaleChange;
    }

    private void OnMouseExit()
    {
        sphere.transform.localScale = base_scale;

    }

    public void OnMouseDown()
    {
        // this needs to be replaced with a OnSelected() method that can be called from any interraction. 
        // update selected pin position in WMM
        WMFactory.clear_canvas();
        WMFactory.reference_pin = this;
        WMFactory.CanvasTitle = this.associated_location.name; // this also may not be needed since i'm passing the pin.
        // I'm playing around with switches just as a learning exercise, if/else is probably better with how linear this logic is.
        List<WorldMenuAction> _tempAction = new();
        switch (this.player_occupied)
        {
            case true:
                foreach (WorldMenuAction action in this.menu_actions)
                {
                    if (action.callback_name != "move") { _tempAction.Add(action); }
                }
                break;
            case false:
                switch (this.locationInfo.available)
                {
                    case true:
                        foreach (WorldMenuAction action in this.menu_actions)
                        {
                            if (action.callback_name == "move") { _tempAction.Add(action); }
                            if (action.callback_name == "inspect") { _tempAction.Add(action); }
                            if (action.callback_name == "cancel") { _tempAction.Add(action); }
                        }
                        break;
                    case false:
                        foreach (WorldMenuAction action in this.menu_actions)
                        {
                            if (action.callback_name == "inspect") { _tempAction.Add(action); }
                            if (action.callback_name == "cancel") { _tempAction.Add(action); }
                        }
                        break;
                }
                break;
        }
        WMFactory.actions = _tempAction;

        WMFactory.build_menu();
    }

    public Tuple<string, string> flip_connection_order(Tuple<string, string> loc_pairing)
    {
        Tuple<string, string> outTuple = new Tuple<string, string>(loc_pairing.Item2, loc_pairing.Item1);
        return outTuple;
    }

    public void connect_the_dots()
    {
        // idk why, but i can't get this method to work from outside of the class

        string this_guid = this.pin_guid;

        foreach (GameObject connected_pin in this.connected_locations)
        {
            if (!connected_pin.gameObject.GetComponent<LocationPinObect>().associated_location.available)
            {
                continue;
            }
            string connected_guid = connected_pin.GetComponent<LocationPinObect>().pin_guid;
            
            Tuple<string,string> check_tuple = new Tuple<string,string>(this_guid, connected_guid);
            if (LDB.completed_connections.Contains(flip_connection_order(check_tuple)) || LDB.completed_connections.Contains(check_tuple))
            {
                Debug.Log("already connected");
            }
            else
            {
                LDB.completed_connections.Add(check_tuple);
                // create child to hold the line
                GameObject lineChild = new GameObject();
                // set this pin to be the childs parent
                lineChild.transform.SetParent(this.transform);
                // create a line renderer on the child.
                var lineobject = lineChild.AddComponent<LineRenderer>();
                lineobject.material = new Material(Shader.Find("Sprites/Default"));
                lineobject.gameObject.layer = (5);
                lineobject.startWidth = 0.1f;
                lineobject.endWidth = 0.1f;
                Color tempcolor = Color.white;
                tempcolor.a = 0.25f;
                lineobject.startColor = tempcolor;
                lineobject.endColor = tempcolor;
                // set the end positions of the line
                //    lineobject.SetPosition(0, this.gameObject.transform.GetChild(1).transform.position);
                lineobject.SetPosition(0, this.gameObject.transform.GetChild(0).transform.position);
                lineobject.SetPosition(1, connected_pin.gameObject.transform.GetChild(0).transform.position);
            }
        }


        //if (true) {return; }

        //// this keeps breaking, there has to be a better way.
        //if (this.connected_locations.Count == 0) { return; }
        
        //foreach (GameObject connected_pin in this.connected_locations)
        //{
            
        //    //// create child to hold the lines.
        //    GameObject child = new GameObject();
        //    // set child to parent of the current pin.
        //    child.transform.SetParent(this.transform);
        //    // Create the linerenderer
        //    var lineobject = child.AddComponent<LineRenderer>();
        //    // set the shader & layer (for visibility)
        //    lineobject.material = new Material(Shader.Find("Sprites/Default"));
        //    lineobject.gameObject.layer = (5);
        //    // set the end positions of the line from 
        //    // the current pin
        //    lineobject.SetPosition(0, this.gameObject.transform.GetChild(1).transform.position);
        //    // the location_pin from the connected locations dictionary.
        //    lineobject.SetPosition(1, connected_pin.gameObject.transform.GetChild(1).transform.position);
        //    lineobject.startWidth = 0.1f;
        //    lineobject.endWidth = 0.1f;
        //    Color tempcolor = Color.white;
        //    tempcolor.a = 0.25f;
        //    lineobject.startColor = tempcolor;
        //    lineobject.endColor = tempcolor;
        //}
    
    }



    public void update_pin_status()
    {
        // wmm should have an refresh_pins() method which will call the update_pin_status() method for each pin in the gdm.pin_dict.
        // Set activity based on accessible call.
        if (locationInfo.accessible == false)
        {
            // the location is not accessible, and we will set the associated pin to false.
            this.gameObject.SetActive(false);
        }
        else
        {
            // the location is available
            // set the scaling (I don't know why but a scale of 1,1,1 doesn't work well from inspector and has to be attached here too...

            sphere.transform.localScale = base_scale;
            // Set color and connection based on Clear status.
            if (locationInfo.clear)
            {
                // the pin is clear
                this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = clear_color;
                // If the pin is connected to any other pins, connect the dots. 
                if (connected_locations.Count > 0)
                {
                    connect_the_dots();
                }
                else
                {
                    //Debug.Log(associated_location.Name + "No Connected Locations to Link.");
                }
            }
            else
            {
                // The Pin is not clear, set it's color to red.
                this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = not_clear_color;
            }
        }
    }

    private void Awake()
    {
        location_guid = associated_location.id;

        // check if the pin is in the GDM
        Debug.Log("world_map_pin order: 2");
        //if (GameDataManager.instance.world_map_pins.ContainsKey(location_guid))
        //{
        //    // why would the pin be in the GDM when the pin wakes up?
        //    // If awake happens before scene load, then it shouldn't be.
        //    // update the pin status
        //    this.locationInfo = GameDataManager.instance.world_map_pins[location_guid].locationInfo; // how is this not circular? I need to rethink my entire data persistence strat.
        //    update_pin_status();
        //}
        


        // Attach to LocationDatabase (Passes to LDB which then passes to GDM... why not just pass to GDM?
        // LDB loads next after this.
        if (!LDB.world_map_pins.ContainsKey(location_guid))
        {
            LDB.world_map_pins.Add(location_guid, this);
        }

        //startup_occupation_check(); // why does this break?
    }


    public void startup_occupation_check()
    {
        // ironically, not to be called during Awake...
        //GameDataManager GDM = GameDataManager.instance;
        Debug.Log("Checking if the player is supposed to be here or not...");
        string where_the_player_is = GameDataManager.instance.playerData.persistantInfo.world_location_id;
        if (associated_location.id == where_the_player_is)
        {
            Debug.Log("The player should be here, and this should only appear once in the log");
            Debug.Log(where_the_player_is);
            this.player_occupied = true;
        }
        else
        {
            this.player_occupied = false;
        }
    }

    private void Start()
    {
        // Start() comes AFTER Awake().

        //startup_occupation_check();

        update_pin_status();
    }

    //private void Update()
    //{
    //    update_pin_status();
    //}
}
