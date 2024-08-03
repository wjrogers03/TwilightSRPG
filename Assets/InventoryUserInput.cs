using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class InventoryUserInput : MonoBehaviour
{
    [SerializeField] public InventoryMenuFactory menu_factory;
    [SerializeField] public InventoryDisplay inventoryDisplay;
    [SerializeField] public GameObject menu_pointer;
    [SerializeField] public ScrollRect inventoryScrollContainer;
    [SerializeField] public RectTransform contentPanel;
    [SerializeField] public GameObject topMarker;
    [SerializeField] public GameObject bottomMarker;
    [SerializeField] public float scrollStep = 0.1f;
    [SerializeField] public int cellHeight;
    [SerializeField] public List<GameObject> filterTabs;
    public int filterIndex = 0;
    //scrollView.FocusOnItem(targetItem)
    private Inventory inventory;
    public int currentCell = 0;
    private void Start()
    {
        inventory = Inventory.instance;
        menu_pointer.GetComponent<InventoryMenuSelector>().gameObject.SetActive(true);
    }

    public void move_pointer()
    {
        // assign a destination
        menu_pointer.GetComponent<InventoryMenuSelector>().assign_target(inventoryDisplay.activeCells[currentCell].gameObject);
        // unlock movement
        menu_pointer.GetComponent<InventoryMenuSelector>().onInput();
        inventoryDisplay.update_display(inventoryDisplay.activeCells[currentCell].associated_item);
    }


    void ScrollEdgeCheck()
    {
        // check bottom
        float delta_y_bottom =  menu_pointer.transform.localPosition.y - bottomMarker.transform.localPosition.y;
        float delta_y_top = Math.Abs(menu_pointer.transform.localPosition.y - topMarker.transform.localPosition.y);
        float vnp = inventoryScrollContainer.verticalNormalizedPosition;
        if (delta_y_bottom < cellHeight * 1.25f)
        {
            if (vnp <= 0f)
            {
                inventoryScrollContainer.verticalNormalizedPosition = 0.0f;
                //Debug.Log("A: " + vnp);
            }
            else
            {
                //Debug.Log("B: " + vnp);
                inventoryScrollContainer.verticalNormalizedPosition = vnp - scrollStep;
            }
        }
        if (delta_y_top < cellHeight * 1.1f)
        {
            if (vnp >= 1.0f)
            {
                //Debug.Log("C: " + vnp);
                inventoryScrollContainer.verticalNormalizedPosition = 1.0f;
            }
            else
            {
                //Debug.Log("D: " + vnp);
                inventoryScrollContainer.verticalNormalizedPosition = vnp + scrollStep;
            }
        }
        // check top
    }

    void ScrollTo(Transform target)
    {
        // get the rect transform of the content panel. This will hold the current position
        RectTransform contentRt = contentPanel.GetComponent<RectTransform>();
        Canvas.ForceUpdateCanvases();
        Vector2 offset = (Vector2)inventoryScrollContainer.transform.InverseTransformPoint(contentRt.position) - (Vector2)inventoryScrollContainer.transform.InverseTransformPoint(target.position);
        Vector2 anchor = contentRt.anchoredPosition;
        anchor.y = offset.y;
        contentRt.anchoredPosition = anchor;
    }

    public void move_scrollView()
    {
        //ScrollViewFocusFunctions.FocusOnItem(inventoryScrollContainer, inventoryDisplay.activeCells[currentCell].transform.GetComponent<RectTransform>());
        ScrollViewFocusFunctions.FocusOnItem(inventoryScrollContainer, menu_pointer.transform.GetComponent<RectTransform>());
    }


    void ShowInTabControl()
    {
        // meh
        float normalizePosition = (float)inventoryDisplay.activeCells[currentCell].transform.GetSiblingIndex() / (float)inventoryScrollContainer.content.transform.childCount;
        inventoryScrollContainer.verticalNormalizedPosition = 1 - normalizePosition;
    }

    public void CenterToItem(RectTransform obj)
    {
        float normalizePosition = contentPanel.anchorMin.y - obj.anchoredPosition.y;
        normalizePosition += (float)obj.transform.GetSiblingIndex() / (float)inventoryScrollContainer.content.transform.childCount;
        normalizePosition /= 1000f;
        normalizePosition = Mathf.Clamp01(1 - normalizePosition);
        inventoryScrollContainer.verticalNormalizedPosition = normalizePosition;
        Debug.Log(normalizePosition);
    }

    public void FixedUpdate()
    {
        ScrollEdgeCheck();   
    }

    public void pointer_movement()
    {
        if (inventory == null) {
            Debug.Log("Bad inventory reference");
            return; }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentCell = currentCell - 4;
            if (currentCell < 0)
            {
                currentCell = (inventoryDisplay.activeCells.Count - 1); // wrapping is strange, saving this for later.
            }
            
            move_pointer();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentCell = currentCell + 4;
            if (currentCell >= inventoryDisplay.activeCells.Count)
            {
                currentCell = currentCell%4;
            }
            move_pointer();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentCell = currentCell - 1;
            if (currentCell < 0) { currentCell = inventoryDisplay.activeCells.Count-1; }
            move_pointer();

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentCell = currentCell + 1;
            if (currentCell >= inventoryDisplay.activeCells.Count) { currentCell = 0; }
            move_pointer();
        }
    }

    public void apply_filter(string itemType)
    {
        inventoryDisplay.ApplyFilter(itemType);
        currentCell = 0;
        move_pointer();
    }


    public void InventoryTabControls()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            filterIndex += 1;
            if (filterIndex > (filterTabs.Count-1))
            {
                filterIndex = 0;
            }
            filterTabs[filterIndex].GetComponent<Button>().onClick.Invoke();
            filterTabs[filterIndex].GetComponent<Button>().Select();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            filterIndex -= 1;
            if (filterIndex < 0)
            {
                filterIndex = (filterTabs.Count - 1);
            }
            filterTabs[filterIndex].GetComponent<Button>().onClick.Invoke();
            filterTabs[filterIndex].GetComponent<Button>().Select();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inventoryDisplay.activeCells[currentCell].onSelect();
            menu_factory.CreateMenu();
            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(inventoryDisplay.activeCells[currentCell].associated_item.item_name);
        }
        pointer_movement();
        InventoryTabControls();
        
    }
    private void LateUpdate()
    {
        //move_pointer();
    }
}
