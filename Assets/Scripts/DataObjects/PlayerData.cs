using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenuAttribute]
public class PlayerData : ScriptableObject, IDataPersistence
{
    

    [Serializable]
    public struct PersistantInfo
    {
        public string name;
        public int deathcount;
        public int currency;
        public string world_location;
    }
    [SerializeField]
    public PersistantInfo persistantInfo;

    public void Awake()
    {
        Debug.Log("Is this even called");
    }

    #region Get-Set passthroughs for Persistant Info variables. Not sure when or where I'd need them.
    public string get_name() { return persistantInfo.name; }
    public void set_name(string _name) { persistantInfo.name = _name; }
    public int get_deathcount() { return persistantInfo.deathcount; }
    public void set_deathcount(int _val) { persistantInfo.deathcount = _val; }
    public int get_currency() { return persistantInfo.currency; }
    public void set_currency(int _val) { persistantInfo.currency = _val; }

    #endregion

    void IDataPersistence.LoadData(GameData gameData)
    {
        //persistantInfo = gameData.playerInfo;
    }

    void IDataPersistence.SaveData(GameData gameData)
    {
        //gameData.playerInfo = persistantInfo;
    }
}
