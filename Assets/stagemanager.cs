using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stagemanager : MonoBehaviour
{
    //[SerializeField] GameProfileManager GPM;
    [SerializeField] LocationData associated_location;
    
    private readonly GameSceneManager GSM = new();
    public void set_clear()
    {
        // get pin from the still loaded GameDataManager
        // find the pin with the same associated location
        // set the cleared value to true
    }
    public void set_false()
    {
        // get pin from the still loaded GameDataManager
        // find the pin with the same associated location
        // set the cleared value to false
    }

    public void load_worldmap()
    {
        // do any data assigments and switch back to world scene.
        GSM.transition_stage_to_world(associated_location.SceneName);
    }
}
