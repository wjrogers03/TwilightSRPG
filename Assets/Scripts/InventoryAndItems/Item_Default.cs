using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object", menuName = "resources/items/default")]
public class Item_Default : Item
{
    public void attach_type()
    { 
        type = ItemType.Default;
    }
}
