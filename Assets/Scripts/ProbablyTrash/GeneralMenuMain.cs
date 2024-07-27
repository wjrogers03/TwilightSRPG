using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralMenuMain : MonoBehaviour
{
    [SerializeField] GameProfileManager game_profile_manager;
    [SerializeField] GameObject titlePrefab;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject buttonParent;


    // menu prefab should be constructed of several components
    // a top cap of the menu and the first button
    // n middle segments containing a button
    // n bottom caps

    private void Awake()
    {
        Debug.Log("GMM exists!?");
    }

    public Action id_to_callback(int callback_id)
    {
        if (callback_id == 0) { return MoveCharacter; } // calls the MoveCharacter action from the GPM
        if (callback_id == 1) { return CancelMenu; } // closes all open menues
        return TestCallback;
    }

    public GameObject AddButton(string displaytext, float x, float y, int callback_id)
    {
        GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
        Vector3 placement_position = new Vector3(x, y, 0);

        newButton.GetComponent<MenuButton>().DisplayText.text = displaytext;
        SetCallback(newButton, callback_id);
        
        newButton.transform.position = placement_position;

        return newButton;
    }

    public GameObject AddTitle(string displaytext, float x, float y)
    {
        GameObject title_text = Instantiate(titlePrefab, buttonParent.transform);
        string loc = displaytext; // I don't know why I need to set the string to a new string for this to work...
        title_text.GetComponent<TMPro.TextMeshProUGUI>().text = loc;
        Vector3 placement_position = new Vector3(x, y, 0);
        title_text.transform.position = placement_position; 
        return title_text;
    }

    // All of the menu callbacks go here. The callbacks will be associated with a Callback ID passed to AddButton
    public void SetCallback(GameObject button, int callback_id)
    {
        List<Action> callbacks = new List<Action>();
        callbacks.Add(MoveCharacter);
        button.GetComponent<Button>().onClick.AddListener(()=>callbacks[callback_id]());
    }
    public void TestCallback()
    {
        Debug.Log("Test");
    }

    public void MoveCharacter()
    {
        // current position and selected position are handled in GPM, so there is no need to pass info here.
        Debug.Log("MOVE CALLBACK");
    }

    public void ChatWith(string character)
    {
        Debug.Log("Opening Chat Menu for " + character);
    }

    public void CancelMenu()
    {
    }
    public void InspectLocation(string location) { }
    // get number of options for menu
    // set size of menu
    // add buttons for options
}
