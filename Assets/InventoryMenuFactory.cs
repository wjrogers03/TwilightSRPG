using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuFactory : MonoBehaviour
{
    [SerializeField] GameObject menuPrefab;
    [SerializeField] GameObject menuParent;

    private GameObject activeMenu;

    private List<GameObject> created_menues = new();

    public Dictionary<string, Action> defaultOptions = new();

    public void Start()
    {
        Options();
    }

    public void CreateMenu()
    {
        if (created_menues.Count > 0)
        {
            foreach (GameObject menu in created_menues) { Destroy(menu); }
        }
        Inventory inv = Inventory.instance;
        GameObject pum = Instantiate(menuPrefab, menuParent.transform);
        created_menues.Add(pum);
        this.activeMenu = pum;
        if (inv.menu_selected_item == null)
        {
            Debug.LogWarning("No menu selected item to reference");
            return;
        }

        string celltype = inv.menu_selected_item.WhatType();
        pum.GetComponent<popupMenu>().menu_title = inv.menu_selected_item.item_name;
        
        if (celltype == "equipment")
        {
            Debug.Log(pum.GetComponent<popupMenu>().menu_options);
            pum.GetComponent<popupMenu>().menu_options.Add("equip", defaultOptions["equip"]);
            pum.GetComponent<popupMenu>().menu_options.Add("test", defaultOptions["test"]);
        }
        else if (celltype == "consumable")
        {
            if (inv.menu_selected_item.OverworldConsumable)
            {
                pum.GetComponent<popupMenu>().menu_options.Add("use", defaultOptions["use"]);
            }
            pum.GetComponent<popupMenu>().menu_options.Add("test", defaultOptions["test"]);
        }
        else
        {
            Debug.Log("no menu to construct");
            Destroy(this.activeMenu); // i realize this should go at the front of createmenu but w/e, lazy right now.
            return;
        }
        pum.GetComponent<popupMenu>().menu_options.Add("cancel", defaultOptions["cancel"]);
        pum.GetComponent<popupMenu>().ConstructMenu();

    }
    
    // returns of the callbacks, similar to the cludgy switch/case in the world menu factory script.
    public void Options()
    {
        this.defaultOptions["test"] = testCallback;
        this.defaultOptions["use"] = useCallback;
        this.defaultOptions["equip"] = equipCallback;
        this.defaultOptions["cancel"] = cancelCallback;
    }

    #region callbacks
    public void testCallback()
    {
        Debug.Log("The inventory test callback was called!");
    }

    public void cancelCallback()
    {
        Destroy(this.activeMenu);
    }

    public void useCallback()
    {
        Inventory inv = Inventory.instance;
        Debug.Log("Attempting to use the item" + inv.menu_selected_item.item_name);
    }

    public void equipCallback()
    {
        Inventory inv = Inventory.instance;
        Debug.Log("Attempting to equip the item: " + inv.menu_selected_item.item_name);
    }
    #endregion



}
