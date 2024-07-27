using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    private GameSceneManager gameSceneManager = new();
    [Header("Menu Button Links")]
    [SerializeField] private Button new_game_button;
    [SerializeField] private Button continue_game_button;
    [SerializeField] private Button exit_game_button;

    #region Menu Actions
    public void new_game_selected()
    {
        DisableAllMenuButtons();
        DataPersistenceManager.instance.NewGame(); // instantiates new game data. 
        gameSceneManager.load_world();
    }

    public void continue_game_selected()
    {
        DisableAllMenuButtons();
        // just load the scene, this will trigger OnSceneLoaded() from the DataPersistenceManager and populate all the reference scripts which have onSceneLoad triggers.
        gameSceneManager.load_worldmap_from_continue();
    }

    public void exit_game_selected()
    {
        DisableAllMenuButtons();
        Debug.Log("Title Menu : Exit()");
    }
    #endregion

    private void EnableAllMenuButtons()
    {
        new_game_button.interactable = true;
        continue_game_button.interactable = true;
        exit_game_button.interactable = true;
    }

    private void DisableAllMenuButtons()
    {
        new_game_button.interactable = false;
        continue_game_button.interactable = false;
        exit_game_button.interactable = false;
    }

}