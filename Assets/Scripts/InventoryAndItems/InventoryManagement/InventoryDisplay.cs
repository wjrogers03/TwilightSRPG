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
    [SerializeField] GameObject ItemTitle_object;
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

    public List<InventoryCell> activeCells = new();
    

    public void Start()
    {
        warehouse_list = Inventory.instance.warehouse; // the warehouse is sourced on awake.
        // eventually this warehouse reference will be switched to inventory.stock
        foreach (Item item in warehouse_list)
        {
            create_inventory_cell(item);
        }
        Inventory.instance.menu_selected_item = warehouse_list[0];
        update_art_asset(warehouse_list[0]);
        update_description_asset(warehouse_list[0]);
        update_item_title_asset(warehouse_list[0]);
        Inventory.instance.onUpdateAssets();
        position_marker.GetComponent<InventoryMenuSelector>().assign_target(activeCells[0].gameObject);
    }

    public void cell_toggle(string itemType)
    {
        // I think the enum overcomplicates things here, itemType should really have been a string...
        var refer = ItemType.Default;
        if (itemType == "consumable")
        {
            refer = ItemType.Consumable;
        }
        else if (itemType == "equipment")
        {
            refer = ItemType.Equipment;
        }
        else if (itemType == "material")
        {
            refer = ItemType.Material;
        }
        else
        {
            Debug.Log("No reference type passed to cell_toggle");
            return;
        }

        foreach (InventoryCell cell in inventoryCells)
        {
            if (cell.associated_item.type == refer)
            {
                cell.gameObject.SetActive(true);
                activeCells.Add(cell);
            }
            else
            {
                cell.gameObject.SetActive(false);
            }
        }
    }

    public void ApplyFilter(string itemtype)
    {
        activeCells.Clear();
        if (itemtype == "all")
        {
            foreach (InventoryCell cell in inventoryCells)
            {
                cell.gameObject.SetActive(true);
                activeCells.Add(cell);
            }
        }
        else
        {
            cell_toggle(itemtype);
        }
    }

    public void update_display(Item item)
    {
        update_art_asset(item);
        update_description_asset(item);
        update_item_title_asset(item);
        Inventory.instance.onUpdateAssets(); // why is this in the Inventory?
    }

    public void create_inventory_cell(Item item)
    {
        InventoryCell cell = Instantiate(ItemPrefab, ViewPortContent.transform);
        cell.name = item.name;
        cell.associated_item = item;
        cell.onCreation();
        inventoryCells.Add(cell);
        activeCells.Add(cell);
    }

    public void update_art_asset(Item item)
    {
        if (item.artL != null)
        {
            ItemArt_object.GetComponent<Image>().sprite = item.artL;
        }
        else if (item.artM != null)
        {
            ItemArt_object.GetComponent<Image>().sprite = item.artM;
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
        if (item.item_name != null)
        {
            ItemTitle_object.GetComponent<TMP_Text>().text = item.item_name;
        }
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
