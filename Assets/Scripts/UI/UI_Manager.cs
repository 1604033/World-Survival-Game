using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static Action OnInventoryToggle;
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();
    public GameObject inventoryPanel;
    public List<Inventory_UI>inventoryUIs;
    public static Slot_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;

    public Toolbar_UI ToolbarUI;
    public Chestbar_UI ChestbarUI;

    public void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.L) )
        {
            //ToggleInventoryUI();
            //OnInventoryToggle?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.J) )
        {
            RefreshInventoryUI("Backpack");
            RefreshInventoryUI("Toolbar");
            RefreshInventoryUI("Chestbar");
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
    }

    public void ToggleInventoryUI()
    {
        if(inventoryPanel != null)
        {
        if(!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            RefreshInventoryUI("Backpack");
            RefreshInventoryUI("Toolbar");
            RefreshInventoryUI("Chestbar");
            

        }
        
        else 
        {
            inventoryPanel.SetActive(false);

        }
        }
    }

    public void RefreshInventoryUI(string inventoryName)
    {
        if(inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }

    public void RefreshAll()
    {
        foreach(KeyValuePair<string, Inventory_UI> keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }
    }

    public Inventory_UI GetInventoryUI(string inventoryName)
    {
        if(inventoryUIByName.ContainsKey(inventoryName))
        {
            return inventoryUIByName[inventoryName];
        }

        Debug.LogWarning("There is not inventory ui for" + inventoryName);
        return null;
    }

    void Initialize()
    {
        foreach (Inventory_UI ui in inventoryUIs)
        {
            if(!inventoryUIByName.ContainsKey(ui.inventoryName))
            {
                inventoryUIByName.Add(ui.inventoryName, ui);
            }
        }
    }
}
