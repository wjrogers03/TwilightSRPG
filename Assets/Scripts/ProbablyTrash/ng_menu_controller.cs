using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ng_menu_controller : MonoBehaviour
{
    [SerializeField] GameProfileManager ProfileManager;

    [SerializeField] TMP_InputField input_name;
    [SerializeField] TMP_Dropdown starting_class;
    [SerializeField] TMP_Dropdown body_type;

    [SerializeField] TMP_Text text_const;
    [SerializeField] TMP_Text text_vital;
    [SerializeField] TMP_Text text_wis;
    [SerializeField] TMP_Text text_str;
    [SerializeField] TMP_Text text_dex;
    [SerializeField] TMP_Text text_int;
    [SerializeField] TMP_Text text_spd;
    
    [SerializeField] UnityEngine.UI.Image img_class;
    [SerializeField] UnityEngine.UI.Image img_offhand;
    [SerializeField] UnityEngine.UI.Image img_weapon;
    [SerializeField] UnityEngine.UI.Image img_armor;
    [SerializeField] UnityEngine.UI.Image img_accessory;
    [SerializeField] UnityEngine.UI.Image img_other;

    //[SerializeField] UnityEngine.Sprite knight_art;
    [SerializeField] Sprite knight_art;
    [SerializeField] Sprite wizard_art;
    [SerializeField] Sprite archer_art;
    [SerializeField] Sprite knight_armor_art;
    [SerializeField] Sprite knight_weapon_art;
    [SerializeField] Sprite knight_offhand_art;

    // Start is called before the first frame update
    private void Awake()
    {
        ProfileManager.add_billy_to_team();
        Unit ref_unit = ProfileManager.Team["Billy"];
        update_stats(ref_unit);
    }
    void update_stats(Unit reference_unit)
    {
        Debug.Log("Updating stats");
        text_const.text = reference_unit.constitution.ToString();
        text_vital.text = reference_unit.vitality.ToString();
        text_wis.text = reference_unit.wisdom.ToString();
        text_str.text = reference_unit.strength.ToString();
        text_dex.text = reference_unit.dex.ToString();
        text_int.text = reference_unit.intelligence.ToString();
        text_spd.text = reference_unit.base_speed.ToString();
    }

    public void accept_character_creation()
    {
        //int dd_value = starting_class.value;
        //string dd_text = starting_class.options[dd_value].text;
        Unit player = new Unit();
        player.name = input_name.text;
        update_starting_class(player);
        ProfileManager.Team["player"] = player;
    }


    public void update_reference_class()
    {
        Unit unit = ProfileManager.Team["Billy"];
        

        int dd_value = starting_class.value;
        string dd_text = starting_class.options[dd_value].text;
        Debug.Log(dd_text);
        if (dd_text == "Knight")
        {
            Debug.Log("assigning knight.");
            unit.set_base_class_knight();
            img_class.sprite = knight_art;
            img_offhand.enabled = true;
            img_offhand.sprite = knight_offhand_art;
            img_weapon.enabled = true;
            img_weapon.sprite = knight_weapon_art;
            img_armor.enabled = true;
            img_armor.sprite = knight_armor_art;
            img_accessory.enabled = false;
            img_other.enabled = false;
        }
        else if (dd_text == "Wizard")
        {
            Debug.Log("assigning wiz.");
            unit.set_base_class_wizard();
            img_class.sprite = wizard_art;
            img_offhand.enabled = false;
            //img_offhand.sprite = knight_offhand_art;
            img_weapon.enabled = false;
            //img_weapon.sprite = knight_weapon_art;
            img_armor.enabled = false;
            //img_armor.sprite = knight_armor_art;
            img_accessory.enabled = false;
            img_other.enabled = false;
        }
        else if (dd_text == "Archer")
        {
            Debug.Log("assigning wiz.");
            //ref_unit.set_base_class_wizard();
            img_class.sprite = archer_art;
            img_offhand.enabled = false;
            //img_offhand.sprite = knight_offhand_art;
            img_weapon.enabled = false;
            //img_weapon.sprite = knight_weapon_art;
            img_armor.enabled = false;
            //img_armor.sprite = knight_armor_art;
            img_accessory.enabled = false;
            img_other.enabled = false;
        }
        else
        {
            Debug.Log("Other Class");
        }
        update_stats(unit);
    }

    public void update_starting_class(Unit unit)
    {
        if (unit == null)
        {
            unit = ProfileManager.Team["Billy"];
        }

        int dd_value = starting_class.value;
        string dd_text = starting_class.options[dd_value].text;
        Debug.Log(dd_text);
        if (dd_text == "Knight")
        {
            Debug.Log("assigning knight.");
            unit.set_base_class_knight();
            img_class.sprite = knight_art;
            img_offhand.enabled = true;
            img_offhand.sprite = knight_offhand_art;
            img_weapon.enabled = true;
            img_weapon.sprite = knight_weapon_art;
            img_armor.enabled = true;
            img_armor.sprite = knight_armor_art;
            img_accessory.enabled = false;
            img_other.enabled = false;
        }
        else if (dd_text == "Wizard")
        {
            Debug.Log("assigning wiz.");
            unit.set_base_class_wizard();
            img_class.sprite = wizard_art;
            img_offhand.enabled = false;
            //img_offhand.sprite = knight_offhand_art;
            img_weapon.enabled = false;
            //img_weapon.sprite = knight_weapon_art;
            img_armor.enabled = false;
            //img_armor.sprite = knight_armor_art;
            img_accessory.enabled = false;
            img_other.enabled = false;
        }
        else if (dd_text == "Archer")
        {
            Debug.Log("assigning wiz.");
            //ref_unit.set_base_class_wizard();
            img_class.sprite = archer_art;
            img_offhand.enabled = false;
            //img_offhand.sprite = knight_offhand_art;
            img_weapon.enabled = false;
            //img_weapon.sprite = knight_weapon_art;
            img_armor.enabled = false;
            //img_armor.sprite = knight_armor_art;
            img_accessory.enabled = false;
            img_other.enabled = false;
        }
        else
        {
            Debug.Log("Other Class");
        }
        update_stats(unit);
    }

}
