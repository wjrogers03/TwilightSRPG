using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName ="GameProfileManager", menuName ="ProfileManager", order = 0)]
public class GameProfileManager : ScriptableObject
{
    // Start is called before the first frame update
    // pull in all the user info such as invintory, units, plot/mission progression, location accessibility. 
    // most menus/actions should read and write to the current profile.
    // things like inventory should source from the current profile.


    // another option is to "store the data in a simple non-monobehavior c# object, and keep its static reference"
    // this may be more what I want to do and will possibly be more efficient.

    // a lot of this is going to get stripped away into different scritps and processes as we go.

    // boilerplate
    public string profile_name;
    
    
    
    
    
    // Location book keeping
    [SerializeField] public string current_location_guid = "63ac74cc-ea24-437e-a3d4-883cfb5fe9cf";
    public string selected_location_guid; // perspective
    public string location_name; //
    public Vector3 world_map_location; // does this need to exist here for any reason other than being accessed in WMM

    public sDict<string, LocationData> location_data_dict;
    public Dictionary<string, Unit> Team = new Dictionary<string, Unit>();

    [Header("Data Managers")]
    public Inventory inventory;


    private readonly GameSceneManager GSM = new();


    #region Worldmap Position Info
    public void pass_location_dict(sDict<string, LocationData> location_dict)
    {
        location_data_dict = location_dict;
    }

    public string get_location_name()
    {
        // new version using location_data_dict
        // this needs to be handled in world menu manager. 
        if (location_data_dict.ContainsKey(current_location_guid))
        {
            string _locationName = location_data_dict[selected_location_guid].Name;
            return _locationName;
        }
        else
        {
            Debug.LogWarning("location not in location_data_dict");
            Debug.LogWarning(current_location_guid);
            Debug.LogWarning(location_data_dict.Keys);
            string _locationName = "dungeon_cotd";
            return _locationName;
        }    
        
    }

    public List<string> apply_location_plot_filter(List<string> _iopts)
    {
        return _iopts;
    }

    public List<string> get_location_options(string location_guid)
    {
        List<string> _opts = new List<string>();
        if (current_location_guid != selected_location_guid)
        {
            _opts.Add("move");
            _opts.Add("cancel");
        }
        else
        {
            //_opts = location_obj_list[location_id].GetComponent<WorldMapLocation>().options;
            _opts = location_data_dict[location_guid].options;
            _opts = apply_location_plot_filter(_opts);
        }
        return _opts;
    }

    public bool is_stage_available(string location_guid)
    {
        return location_data_dict[location_guid].available;
    }

    public bool is_stage_clear(string location_guid)
    {
        return location_data_dict[location_guid].clear;
    }
    public void move_location_to_selected()
    {
        this.current_location_guid = selected_location_guid;
    }

    public void move_character(string location1, string location2)
    {
        Debug.Log("moving character from " + location1 + " to " + location2);
    }
    #endregion
    
    
    #region Scene Changing Functions
    // These functions are handled here because I assume there will be some amount of data that needs to be handled.
    // since this class has links to all the data structures it seems like a good place. 
    public void change_scene_to_worldmap()
    {
        // generalized passthrough, inefficient but w/e.
        GSM.load_world();
    }

    public void change_scene_to_location()
    {
        string _scene = location_data_dict[current_location_guid].SceneName;

        
        if (string.IsNullOrEmpty(_scene))
        {
            return;
        }

        GSM.load_scene(_scene);
    }
    #endregion


    #region Unit and Team.
    public void add_billy_to_team()
    {
        Unit billy = new Unit();
        billy.name = "Billy";
        billy.set_base_class_knight();
        Team.Add("Billy", billy);
    }

    public void whose_on_team()
    {
        foreach (var kvp in Team.ToArray())
        {
            Debug.Log(Team[kvp.Key].name);
        }
    }

    #endregion


    #region Monobehavior methods
    public void OnEnable()
    {
        //add_billy_to_team();
    }
    #endregion


    #region Testing Methods


    public void echo()
    {
        Debug.Log("STOP CLICKING ME.");
    }
    #endregion


}
