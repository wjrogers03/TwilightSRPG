using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public Item associated_item;


    public void onSelect()
    {
        if (Inventory.instance != null)
        {
            Inventory.instance.menu_selected_item = associated_item;
            Inventory.instance.onUpdateSelection();
        }
        Debug.Log(associated_item.item_description);
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        onSelect();
    }
}
