using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "resources/items/equipment")]
public class Item_Equipment : Item
{
    [SerializeField] public int item_level = 1;
    [SerializeField] public float level_scaling = 0.1f;
    [Serializable]
    public struct WeaponProps
    {
        public int physical;
        public int magical;
        public int fire;
        public int cold;
        public int lightning;
        public int light;
        public int dark;
    }
    [Tooltip("Weapon offsensive stats")]
    [SerializeField] public WeaponProps weaponProps;

    // contains how much damage is actually sent using the weapon.
    // this is calculated and shouldn't be in the inspector.
    public struct WeaponDamage
    {
        public int physical;
        public int magical;
        public int fire;
        public int cold;
        public int lightning;
        public int light;
        public int dark;
    }
    public WeaponDamage weaponDamage;
    
    [Serializable]
    public struct ArmorProps
    {
        public int physical;
        public int magical;
        public int fire;
        public int light;
        public int ice;
        public int dark;
    }
    [Tooltip("Armor defensive properties")]
    [SerializeField] public ArmorProps armorProps;

    [Serializable]
    public struct RequiredStats
    {
        public int strength;
        public int dexterity;
        public int intelligence;
        public int faith;
    }
    [Tooltip("Stats required for full effect of equipment.")]
    [SerializeField] public RequiredStats reqs;

    
    [Serializable]
    public struct BonusStats
    {
        public int health;
        public int mana;
        public int stamina;
        public int strength;
        public int constitution;
        public int vitality;
        public int mind;
        public int dexterity;
    }
    [Tooltip("If required stats are met, the bonus stats are applied to the unit")]
    [SerializeField] public BonusStats bonusStats;

    [Serializable]
    public struct StatScaling
    {
        public float strength;
        public float dexterity;
        public float intelligence;
        public float faith;
    }
    [Tooltip("Determines the damage bonus from specific stats")]
    [SerializeField] public StatScaling statScales;

    private struct UnitPlaceholder
    {
        public int unit_level;
        public int strength;
        public int dexterity;
        public int intelligence;
        public int faith;
    }
    UnitPlaceholder _unit;

    public float CalculateStatContribution()
    {
        float _fromStrength = statScales.strength * _unit.strength;
        float _fromDex = statScales.dexterity * _unit.dexterity;
        float _fromInt = statScales.intelligence * _unit.intelligence;
        float _fromFaith = statScales.faith * _unit.faith;
        return _fromStrength + _fromDex + _fromInt + _fromFaith;
    }

    public float StatCheck()
    {

        if (_unit.strength >= reqs.strength) { return 1f; }
        else if (_unit.dexterity >= reqs.dexterity) { return 1f; }
        else if (_unit.intelligence >= reqs.intelligence) { return 1f; }
        else if (_unit.faith >= reqs.faith) { return 1f; }
        else { return 0.25f; }
    }

    public void CalcWeaponDamage(Unit unit)
    {
        float statCheck = StatCheck();
        float scaling_modifier = this.level_scaling * this.item_level + CalculateStatContribution();
        weaponDamage.physical = (int)(statCheck * weaponProps.physical * (1 + scaling_modifier) + 2f * unit.level);
        weaponDamage.fire = (int)(statCheck * weaponProps.fire * (1 + scaling_modifier) + 2f * unit.level);
        weaponDamage.cold = (int)(statCheck * weaponProps.cold * (1 + scaling_modifier) + 2f * unit.level);
        weaponDamage.lightning = (int)(statCheck * weaponProps.lightning * (1 + scaling_modifier) + 2f * unit.level);
        weaponDamage.light = (int)(statCheck * weaponProps.light * (1 + scaling_modifier) + 2f * unit.level);
        weaponDamage.dark = (int)(statCheck * weaponProps.dark * (1 + scaling_modifier) + 2f * unit.level);
    }

    public void attach_type()
    {
        type = ItemType.Equipment;
    }
}