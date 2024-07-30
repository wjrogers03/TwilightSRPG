using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Material Object", menuName = "resources/items/material")]
public class Item_Material : Item
{
    public void attach_type()
    {
        type = ItemType.Equipment;
    }
}
