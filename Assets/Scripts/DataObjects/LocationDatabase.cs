using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class LocationDatabase : MonoBehaviour, IDataPersistence
{
    [Header("Game Manager Links")]
    [SerializeField]
    GameProfileManager GPM;
    [SerializeField]
    GameDataManager GDM;


    // gather all the potential location scripts.
    [HideInInspector]
    public sDict<string, LocationData> location_objects_dict = new sDict<string, LocationData>();
    [HideInInspector]
    public List<LocationData> location_objects_list;

    [Header("General Settings")]
    [SerializeField]
    public bool newGameSet;
    public string new_game_init_location = "DungeonMap_COTD";

    [Header("Location Pin Objects")]
    public sDict<string, LocationPinObect> world_map_pins = new sDict<string, LocationPinObect>();
    [HideInInspector]
    public List<Tuple<string, string>> completed_connections = new();

    // Note:
    // The location_object_dict contains the current instance of the object in world. This is represented in the serialzed output
    // as a series of numbers, but between runs of the game, these numbers become meaningless.
    //
    //
    // The keys in the location_objects_dict can be used to reference an associated worldmap pin which is stored in a dictionary by location GUID


    public void Awake()
    {

        List<LocationData> location_objects_list = FindAllLocationDataAssets(); // does not change over the course of a game.
        location_objects_dict = BuildLocationsDictionary(location_objects_list);

        if (newGameSet != true)
        {
            reset_location_new_game();
            this.newGameSet = true;
        }



        GPM.pass_location_dict(location_objects_dict);
        send_world_pins_to_gdm();
    }

    public void send_location_dict_to_gdm()
    {
        if (location_objects_dict.Keys.Count() < 1)
        {
            Debug.LogWarning("LocationDatabase: Sending an empty location_dict to the GDM, check the order of operations");
        }

        GDM.location_data_dict = location_objects_dict;
    }
    private void send_world_pins_to_gdm()
    {
        if (world_map_pins.Count() < 1)
        {
            Debug.LogWarning("LocationDatabase: Sending an empty world_map_pins object to the GDM, check the order of operations.");
        }
        GameDataManager.instance.world_map_pins = world_map_pins;

        
    }

    private sDict<string, LocationData> BuildLocationsDictionary(List<LocationData> input_list)
    {
        // this may not even be needed, but figured i wouldn't muck around with too much here...
        sDict<string, LocationData> _outputDict = new sDict<string, LocationData>();
        foreach (LocationData _location in input_list)
        {
            if (_location.id == null)
            {
                _location.GenerateGuid();
            }
            if (_location != null)
            {
                _outputDict.Add(_location.id, _location);
            }
        }
        return _outputDict;
    }



    private List<LocationData> FindAllLocationDataAssets()
    {
        Dictionary<string, LocationData> _tempDict = new Dictionary<string, LocationData>();
        
        IEnumerable<LocationData> locationDataObjects = Resources.LoadAll<LocationData>("LocationDatas");

        return new List<LocationData>(locationDataObjects);
    }

    #region Data Persistence
    void IDataPersistence.LoadData(GameData gameData)
    {
        // grab most recent instances of all the objects.
        // Remember to only load IF those objects are already in existence.
        sDict<string, LocationPinObect.PersistantInfo> _holding;

        _holding = gameData.worldmap_persistance;

        foreach (KeyValuePair<string, LocationPinObect.PersistantInfo> pair in _holding)
        {
            Debug.Log("Assiging: " + pair.Key);
            world_map_pins[pair.Key].locationInfo = pair.Value;
        }
        

    }

    void IDataPersistence.SaveData(GameData gameData)
    {
        // Serialize the persistant data for writing
        sDict<string, LocationPinObect.PersistantInfo> _writable = new();
        foreach (KeyValuePair<string, LocationPinObect> pair in world_map_pins)
        {
            Debug.Log(pair.Value.associated_location.SceneName);
            _writable.Add(pair.Key, pair.Value.locationInfo);
            
        }
        // update the gameData object.
        gameData.newGameSet = newGameSet;
        gameData.worldmap_persistance = _writable;

    }
    #endregion


    public void push_location_info_to_objects()
    { }
    public void pull_location_info_from_objects()
    { }
    public void reset_location_new_game()
    {
        Debug.Log("New game instantiated, lets reset all the locations to locked");
        foreach (KeyValuePair<string, LocationPinObect> kvp in world_map_pins)
        {
            if (kvp.Value.associated_location.SceneName == new_game_init_location)
            {
                kvp.Value.locationInfo.clear = false;
                kvp.Value.locationInfo.available = true;
                kvp.Value.locationInfo.accessible = true;
            }
            else
            {
                Debug.Log("Locking: " + kvp.Key);
                kvp.Value.locationInfo.clear = false;
                kvp.Value.locationInfo.available = false;
                kvp.Value.locationInfo.accessible = false;
            }
        }

    }
}
