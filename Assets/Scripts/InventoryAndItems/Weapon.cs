using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] public string art_equipped;
    public string equip_slot = string.Empty; // might not be a necessary field
    public int weight = 0;
    // This may be better set in Item, as armor is going to extend item as well and these keys will need to match 100p
    public Dictionary<string, int> damage = new Dictionary<string, int>()
    {
        {"strike", 0 },
        {"pierce", 0 },
        {"slash", 0 },
        {"magic", 0 },
        {"fire", 0 },
        {"lightning", 0 },

    };
    [SerializeField] public int damage_strike = 0;
    [SerializeField] public int damage_pierce = 0;
    [SerializeField] public int damage_slash = 0;
    [SerializeField] public int damage_magic = 0;
    [SerializeField] public int damage_fire = 0;
    [SerializeField] public int damage_lightning = 0;

    // required stats
    public int constitution = 0;
    public int vitality = 0;
    public int wisdom = 0;
    public int strength = 0;
    public int dex = 0;
    public int intelligence = 0;


    public void fill_damage_dictionary()
    {
        damage["strike"] = damage_strike;
        damage["pierce"] = damage_pierce;
        damage["slash"] = damage_slash;
        damage["magic"] = damage_magic;
        damage["fire"] = damage_fire;
        damage["lightning"] = damage_lightning;
    }

    private void Awake()
    {
        fill_damage_dictionary();
    }

    public Dictionary<string, int> get_damage()
    {
        return damage;
    }

    public bool check_stat_requirements(Unit unit)
    {
        // This feels grossly inefficient, if any of these var names change in the future this just falls apart.
        // is it possible in c# to create an iterable holder like dictionaries in python
        // should the unit stats be public Dictionary<string, int> stats = new Dictionary<string, int>();
        // that would allow methods like this to iterate through matching keys. 
        if (unit == null)
        {
            return false;
        }
        if (unit.constitution < constitution) { return false; };
        if (unit.vitality < vitality) { return false; };
        if (unit.wisdom < wisdom) { return false; };
        if (unit.strength < strength) { return false; };
        if (unit.dex < dex) { return false; };
        if (unit.intelligence < intelligence) { return false; };
        return true;
    }
}
