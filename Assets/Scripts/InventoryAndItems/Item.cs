using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
    Material,
    Default
}

[CreateAssetMenuAttribute]
public abstract class Item : ScriptableObject
{
    [Header("Basic Information")]
    [SerializeField] public string item_name;
    [TextArea(20,20)]
    [SerializeField] public string item_description;
    [SerializeField] public ItemType type;
    [SerializeField] public string item_id; // 5 digit lookup number
    [SerializeField] public bool consumable; // no longer needed
    [SerializeField] string effect;
    [Header("Art Assets")]
    [SerializeField] public Sprite artM;// art for Inventory menu
    [SerializeField] public Sprite artL;// art for Inventory Inspector
    
    public string unique_guid; // unique guid for persistant data tracking.
    public int quantity;

    public bool OverworldConsumable = false;
    public bool StageConsumable = false;


    public void set_OverworldConsumable(bool val)
    {
        this.OverworldConsumable = val;
    }
    public void set_StageConsumable(bool val)
    {
        this.StageConsumable = val;
    }
    public string WhatType()
    {
        if (type == ItemType.Consumable) { return "consumable"; }
        if (type == ItemType.Equipment) { return "equipment"; }
        if (type == ItemType.Material) { return "material"; }
        else { return "default"; }
    }
    public void GenerateGuid()
    {
        this.unique_guid = System.Guid.NewGuid().ToString();
    }

}
