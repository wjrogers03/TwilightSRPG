using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Consumable Object", menuName = "resources/items/consumable")]
public class Item_Consumable : Item
{
    public void attach_type()
    { 
        type = ItemType.Consumable;
    }
}
