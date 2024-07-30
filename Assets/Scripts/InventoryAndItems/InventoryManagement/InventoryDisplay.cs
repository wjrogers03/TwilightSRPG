using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    [Header("Item displays")]
    [SerializeField] InventoryCell ItemPrefab;
    [SerializeField] GameObject ViewPortContent;
    [SerializeField] GameObject ItemArt_object;
    [SerializeField] GameObject ItemDescription_object;
    [SerializeField] GameObject ItemAddtlInfo_object;

    /// <summary>
    /// revisit this script for better menu generation in the world scene.
    /// </summary>   
    /// 


    
    [Header("Item Cell Spacing")]
    [SerializeField] GameObject position_marker;

    [HideInInspector]
    public List<Item> warehouse_list;
    List<InventoryCell> inventoryCells;
    public void Start()
    {
        warehouse_list = Inventory.instance.warehouse; // the warehouse is sourced on awake.
        foreach (Item item in warehouse_list)
        {
            create_inventory_cell(item);
        }
    }

    public void create_inventory_cell(Item item)
    {
        InventoryCell cell = Instantiate(ItemPrefab, ViewPortContent.transform);
        cell.name = item.name;
        cell.associated_item = item;
        inventoryCells.Add(cell);
    }

    public void update_art_asset(Item item)
    {
        if (item.artL != null)
        {
            ItemArt_object.GetComponent<Image>().sprite = item.artL;
        }
    }
    public void update_description_asset(Item item)
    {
        if (item.item_description != null)
        {
            ItemDescription_object.GetComponent<TMP_Text>().text = item.item_description;
        }
    }

    public void update_item_title_asset(Item item)
    {
        if (item.item_name != null) { }
    }

    public void Update()
    {
        if (Inventory.instance != null)
        {
            if (Inventory.instance.menu_selected_item != null)
            {
                if (Inventory.instance.update_art_assets)
                {
                    Item current_item = Inventory.instance.menu_selected_item;
                    update_art_asset(current_item);
                    update_description_asset(current_item);
                    update_item_title_asset(current_item);
                    Inventory.instance.onUpdateAssets();
                }
                
            }
        }
    }




}
