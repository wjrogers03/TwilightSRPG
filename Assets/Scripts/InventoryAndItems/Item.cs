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
    [SerializeField] public bool consumable;
    [SerializeField] string effect;
    [Header("Art Assets")]
    [SerializeField] public Sprite artM;// art for Inventory menu
    [SerializeField] public Sprite artL;// art for Inventory Inspector
    
    public string unique_guid; // unique guid for persistant data tracking.
    public int quantity;
    

    public void GenerateGuid()
    {
        this.unique_guid = System.Guid.NewGuid().ToString();
    }

}
