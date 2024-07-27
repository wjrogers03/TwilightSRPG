using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// abandoned for GameProfileManager scriptable object

public class CurrentProfile : MonoBehaviour
{
    // Start is called before the first frame update
    // don't destroy on load?
    // pull in all the user info such as invintory, units, plot/mission progression, location accessibility. 
    // most menus/actions should read and write to the current profile.
    // things like inventory should source from the current profile.


    // another option is to "store the data in a simple non-monobehavior c# object, and keep its static reference"
    // this may be more what I want to do and will possibly be more efficient.
    

    [SerializeField] public string profile_name;
    [SerializeField] public string description;
    [SerializeField] public string icon;
    [SerializeField] public string title;


    [SerializeField] public Inventory inventory;
    [SerializeField] public int location_index;
    

    public Dictionary<string, Unit> Team = new Dictionary<string, Unit>();


    public void add_unit_to_team()
    {
        Unit billy = new Unit();
        billy.name = "billy";
        Team.Add("Billy", billy);
    }

    public void Awake()
    {
        Debug.Log("STOP CLICKING ME.");
    }


}
