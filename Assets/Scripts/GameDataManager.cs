using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // differs from the GPM in that the GPM is designed to only handle i/o of data.
    // it's my fault the names are so similar, wait until you read how I spelt Object below...
    public static GameDataManager instance { get; private set; }

    public sDict<string, LocationPinObect> world_map_pins = new sDict<string, LocationPinObect>(); // why is this in the GDM AND the LDB.
    public sDict<string, LocationData> location_data_dict = new();
    

    public string previous_scene;

    public string current_scene_category;
    public PlayerDataHandler playerData;



    public void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogWarning("GameDataManager: Found more than one GDM in scene, destroying newest object.");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

}
