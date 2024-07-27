using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public void load_world()
    {
        GameDataManager.instance.current_scene_category = "world";
        SceneManager.LoadScene("WorldMap");
    }

    public void transition_stage_to_world(string scene_name)
    {
        SceneManager.UnloadSceneAsync(scene_name);
    }

    public void load_scene(string target_scene)
    {
        //DataPersistenceManager.instance.SaveGame();
        // any data that needs to be storred in an intermediary or 
        // otherwise backed up should go here.
        GameDataManager.instance.current_scene_category = "stage";
        SceneManager.LoadScene(target_scene,LoadSceneMode.Additive);
    }
    public void load_worldmap_from_continue()
    {
        //DataPersistenceManager.instance.SaveGame();
        // any data that needs to be storred in an intermediary or 
        // otherwise backed up should go here.
        //GameDataManager.instance.current_scene_category = "worldmap";
        GameDataManager.instance.previous_scene = "title";
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadScene("WorldMap");
    }
}
