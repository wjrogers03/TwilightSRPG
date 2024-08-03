using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField] public Item associated_item;
    public GameObject factory_reference;

    public void onCreation()
    {
        if (associated_item.artM != null)
        {
            this.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite = associated_item.artM;
            // this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite=associated_item.artM;
        }
    }

    public void onHover()
    {
        if (Inventory.instance != null)
        {
            Inventory.instance.menu_selected_item = associated_item;
            Inventory.instance.onUpdateSelection();
        }
    }

    public void onSelect()
    {
        // do selection actions
        if (Inventory.instance != null)
        {
            Inventory.instance.menu_selected_item = associated_item;
            Inventory.instance.onUpdateSelection();
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        onSelect();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        onHover();
    }
}
