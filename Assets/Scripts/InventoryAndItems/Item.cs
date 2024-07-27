using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public string type;
    [SerializeField] public string id;
    [SerializeField] public string artS;// art for field
    [SerializeField] public string artM;// art for Inventory menu
    [SerializeField] public string artL;// art for Inspector
    [SerializeField] public int quantity;
}
