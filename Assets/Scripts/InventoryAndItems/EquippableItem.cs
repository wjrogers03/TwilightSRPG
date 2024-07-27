using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableItem : Item
{


    public Dictionary<string, int> requirements = new Dictionary<string, int>()
    {
        { "constitution", 0 },
        { "vitality", 0 },
        { "wisdom", 0 },
        { "strength", 0 },
        { "dex", 0 },
        { "intelligence", 0 },
    };

    [SerializeField] public int weight;
    [SerializeField] public string equip_slot;
    [SerializeField] public int req_const;
    [SerializeField] public int req_vit;
    [SerializeField] public int req_wis;
    [SerializeField] public int req_str;
    [SerializeField] public int req_dex;
    [SerializeField] public int req_int;
    public int level; // most equipable items will have a level that can be increased with an item
    public string upgrade_material;
    public int upgrade_cost;
    public void refresh_req_dict()
    {
        requirements["constitution"] = req_const;
        requirements["vitality"] = req_vit;
        requirements["wisdom"] = req_wis;
        requirements["strength"] = req_str;
        requirements["dex"] = req_dex;
        requirements["intelligence"] = req_int;
    }

    public void set_upgrade(string material_name, int cost)
    {
        upgrade_material = material_name;
        upgrade_cost = cost;
    }

    public void increase_level()
    {
        level++;
        upgrade_cost++;

    }

}
