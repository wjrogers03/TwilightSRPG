using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Dictionary<string, Item> stock = new Dictionary<string, Item>();
    
    public void add_item(Item item)
    {
        if (stock.ContainsKey(item.name))
        {
            stock[item.name].quantity = stock[item.name].quantity + 1;
        }
        else {
            stock.Add(item.name, item);
        }
        
    }

    public void remove_item(Item item)
    {
        if (stock.ContainsKey(item.name))
        {
            stock[item.name].quantity = stock[item.name].quantity - 1;

            if (stock[item.name].quantity == 0)
            {
                stock.Remove(item.name);
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
