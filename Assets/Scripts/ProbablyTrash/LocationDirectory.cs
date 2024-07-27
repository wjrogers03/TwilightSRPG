using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationDirectory : MonoBehaviour
{
    // depricated version
    // update with new information from StageDirectory in file saving example.
    [Header("Game Manager Links")]
    [SerializeField] GameProfileManager manager;
    [Header("Locations List Generation")]
    [Tooltip("List of prefab location asset scripts. !!Not stage scenes.!!")]
    [SerializeField] List<GameObject> location_objects = new List<GameObject>();
    
    
    // This may be better served as a scriptable object with all of the prefab locations, tied to a specific index, instead of just added
    // in situ to a list of gameobjects. It would at the very least, make the editor simpler.
    private void Awake()
    {
        //manager.pass_location_objects(location_objects);
    }
}
