using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


[CreateAssetMenu(fileName = "ArmoryManager", menuName = "ArmoryManager", order = 0)]
public class Armory : ScriptableObject
{
    public Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
    
    public Dictionary<string, OffhandEquip> offhands = new Dictionary<string, OffhandEquip>();
    
    public Dictionary<string, Armor> armors = new Dictionary<string, Armor>();
    
    public Dictionary<string, Accessory> accessories = new Dictionary<string, Accessory>();


    public void build_battle_armory(List<Unit> units)
    {
        /// build a reference sheet for all weapons, all armor, spells, skills to be used during the battle.
        foreach (Unit unit in units)
        {
            // build weapon id list
            // build armor id list
            // build accessory id list
            // build offhand id list
            Debug.Log(unit.weapon);
        }
    }


    public void read_equipable_tables()
    {
        // puts all the armor and weapon tables into memory. This is all just text so it doesn't take up more than a few kb.
        read_armor_table();
        read_weapon_table();
        read_offhand_table();
        read_accessory_table();
    }



    private void read_armor_table() { }
    private void read_weapon_table() { }
    private void read_accessory_table() { }
    private void read_offhand_table() { }

    public Armor source_armor(string ItemID)
        { return armors[ItemID]; }
    
    public Weapon source_weapon(string ItemID)
    {
        return weapons[ItemID]; }

    public Accessory source_Acc(string ItemID)
    {
        return accessories[ItemID];
    }

    public void populate_armory()
    {
        populate_weapons();
        populate_armor();
        populate_offhand();
        populate_accessories();
    }

    public void populate_weapons()
    {
        //Weapon sword01 = new Weapon();
        ////Weapon sword01 = this.AddComponent<Weapon>();
        //sword01.damage_slash = 8;
        //sword01.weight = 5;
        //sword01.ItemName = "Long Sword";
        //sword01.req_str = 8;
        //sword01.refresh_req_dict();
        //sword01.refresh_damage_dictionary();
        //weapons["0001"] = sword01;
    }

    public void populate_offhand()
    {
        //OffhandEquip offhand01 = new OffhandEquip();
        //OffhandEquip offhand01 = this.AddComponent<OffhandEquip>();
        //offhand01.ItemName = "shield";
        //offhand01.weight = 2;
        //offhand01.req_str = 4;
        //offhand01.refresh_req_dict();


    }

    public void populate_armor()
    {
        //Armor armor01 = new Armor();
        //Armor armor01 = this.AddComponent<Armor>();  
        //armor01.ItemName = "Cloth Armor";
        //armor01.req_str = 3;
        //armor01.weight = 3;
        //armor01.refresh_req_dict();
    }

    public void populate_accessories()
    {
        //Accessory accessory01 = this.AddComponent<Accessory>();
        //accessory01.ItemName = "Ring1";
        //accessory01.weight = 1;

    }


}
