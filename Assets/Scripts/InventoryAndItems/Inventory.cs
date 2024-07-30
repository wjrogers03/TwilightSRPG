using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }
    public List<Item> warehouse = new();
    public Dictionary<string, Item> stock = new Dictionary<string, Item>();
    public Item menu_selected_item;
    public bool update_art_assets = true;


    public void onUpdateAssets()
    {
        update_art_assets=false;
    }

    public void onUpdateSelection()
    {
        update_art_assets = true;
    }

    private List<Item> FindAllItemDataAssets()
    {
        List<Item> items = new List<Item>();

        IEnumerable<Item> itemDataObjects = Resources.LoadAll<Item>("Items");

        return new List<Item>(itemDataObjects);
    }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("InventoryManager: Found more than one InventoryManager in scene, destroying newest object.");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        warehouse = FindAllItemDataAssets();
    }

    public void add_item(Item item)
    {
        if (stock.ContainsKey(item.item_name))
        {
            stock[item.item_name].quantity = stock[item.item_name].quantity + 1;
        }
        else {
            stock.Add(item.item_name, item);
        }
        
    }

    public void remove_item(Item item)
    {
        if (stock.ContainsKey(item.item_name))
        {
            stock[item.item_name].quantity = stock[item.item_name].quantity - 1;

            if (stock[item.item_name].quantity == 0)
            {
                stock.Remove(item.item_name);
            }
        }
        else
        {
            Debug.Log("Item not in inventory");
        }

        
    }

    public void get_items()
    {
        Debug.Log(stock);
    }

}
