using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    /// <summary>
    /// 
    /// GameData is used as the persistant handler of data. 
    /// 
    /// </summary>

    public bool newGameSet;

    public PlayerDataHandler.PersistantInfo playerInfo;
    public sDict<string, LocationPinObect.PersistantInfo> worldmap_persistance;


    public GameData()
    {
        newGameSet = false;
        playerInfo = new PlayerDataHandler.PersistantInfo();
        worldmap_persistance = new sDict<string, LocationPinObect.PersistantInfo>();
    }
}
