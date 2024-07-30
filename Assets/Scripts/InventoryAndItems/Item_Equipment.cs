using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "resources/items/equipment")]
public class Item_Equipment : Item
{
    public void attach_type()
    {
        type = ItemType.Equipment;
    }
}
