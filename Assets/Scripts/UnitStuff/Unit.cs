using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[CreateAssetMenuAttribute]
public class Unit: ScriptableObject
{

    public string unitName;
    public string artS;
    public string artM;
    public string artL;

    public int level;

    // stats
    public int constitution;
    public int vitality;
    public int wisdom;
    public int strength;
    public int dex;
    public int intelligence;


    // resources
    public int base_speed;
    public int bonus_speed;
    public int speed;

    public int base_health;
    public int bonus_health;
    public int health;

    public int base_stamina;
    public int bonus_stamina;
    public int stamina;

    public int base_mana;
    public int bonus_mana;
    public int mana;

    // Equipment references
    public Weapon weapon;
    public Armor armor;
    public OffhandEquip offhand;
    public Accessory accessory1;
    public Accessory accessory2;
    public Accessory accessory3;


    // Item coding
    // 0000 -> 0999 item
    // 1000 -> 1099 weapon
    // 1100 -> 1199 armor
    // 1200 -> 1299 offhand
    // 1300 -> 1399 accessory
    // 2000 -> 2099 skill item
    // 2100 -> 2100 spell item
    // 3000 -> 3099 key item
    // armorer is only used for generation, weapon/eqItem objects will be created and passed between the inventory and the units. Reference them from unit.weapon

    int Calculate_Stamina()
    {
        base_stamina = vitality * 4 + level * 4;
        return base_stamina + bonus_stamina;
    }

    int Calculate_Health()
    {
        base_health = constitution * 4 + level * 4;
        return base_health + bonus_health;
    }

    int Calculate_Speed()
    {
        return base_speed + bonus_speed;
    }

    int Calculate_Mana()
    {
        base_mana = wisdom * 4 + level * 4;
        return base_mana + bonus_mana;
    }

    public void set_base_class_knight()
    {
        constitution = 30;
        stamina = 10;
        vitality = 10;
        wisdom = 10;
        strength = 10;
        dex = 10;
        intelligence = 10;
        level = 1;

        base_speed = 10;
        base_health = 50;
        base_stamina = 10;
        base_mana = 10;
    }

    public void set_base_class_wizard()
    {
        // set base states
        constitution = 10;
        stamina = 10;
        vitality = 10;
        wisdom = 30;
        strength = 10;
        dex = 10;
        intelligence = 10;
        level = 1;

        base_speed = 10;
        base_health = 50;
        base_stamina = 10;
        base_mana = 10;
        
        // set base armor

    }

    public void Calculate_Resources()
    {
        health = Calculate_Health();
        stamina = Calculate_Stamina();
        mana = Calculate_Mana();
        speed = Calculate_Speed();
    }

    public void Set_Armor()
    {
        Debug.Log("Setting Armor");
    }

    public void Set_Weapon()
    {
        Debug.Log("Setting weapon");
    }

    public void Set_Offhand()
    {
        Debug.Log("Setting Offhand");
    }
    public void Set_Accessory1()
    {
        Debug.Log("Setting Accessory 1");
    }
    public void Set_Accessory2()
    {
        Debug.Log("Setting Accessory 2");
    }
    public void Set_Accessory3()
    {
        Debug.Log("Setting Accessory 3");
    }

}
