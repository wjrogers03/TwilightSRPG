using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class DefaultData
{

    // ditch this whole thing
    //public string current_scene_name;
    //public List<WorldLocation> world_locations = new List<WorldLocation>();

}


// I feel like I may be reinventing the method I did with my WorldMapLocation scripts but in a weird way
// On the world map I have the location directory. I can put a reference in the DataManager to the LocationDirectory and have that data written
// The data manager references this object like I would want to have it reference the LocationDirectory.
// but why isn't this a game object? Wouldn't we want something easily modifiable?


