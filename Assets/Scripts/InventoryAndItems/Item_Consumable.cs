using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Consumable Object", menuName = "resources/items/consumable")]
public class Item_Consumable : Item
{
    [SerializeField] public bool overworldConsumable = true;
    [SerializeField] public bool stageConsumable = true;

    public void Awake()
    {
        // there might be a better way to pass through variables than this, but it works, i guess.
        this.set_OverworldConsumable(overworldConsumable);
        this.set_StageConsumable(stageConsumable);
    }

    public void attach_type()
    { 
        type = ItemType.Consumable;
    }
}
