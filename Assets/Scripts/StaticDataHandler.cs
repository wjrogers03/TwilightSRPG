using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataHandler: MonoBehaviour
{
    // Holds data for interscene reference and data passing.
    // if an object exists in the base scene but not in the additive scene
    // then the data can be placed here prior to scene load, 
    // and picked up in the added scene.

    // this is not data persistance IO, similar concept except for between scenes.

    [SerializeField] bool debug_messaging = true;

    public static StaticDataHandler instance;

    public Dictionary<string, GameObject> stage_data_intermediary = new();

    // eh, maybe we should have specific intermediaries like this

    public LocationPinObect location_pin_intermediate;
    public LocationDatabase location_database_intermediate;
    public PlayerDataHandler pdh_intermediate;

    // then in the scene change ensure that all needed reference objects are attached.
    // this will allow the scripts in the stage scene to have proper references in the IDE.
    
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("StaticDataHandler: Found more than one DPM in scene, destroying newest object.");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public Dictionary<string, GameObject> pull_to_stage_scene()
    {
        if (debug_messaging)
        {
            print("Pulling data into scene:");
            foreach (KeyValuePair<string, GameObject> kvp in stage_data_intermediary)
            {
                print(">" + kvp.Key);
            }
            print("are available to be accessed in the local scene object.");
            print("Understand that you may access these through SDH.instance.stage_data_intermediary as well.");
        }
        return stage_data_intermediary;
    }

    private void print(string msg)
    {
        Debug.Log(msg);
    }

}
