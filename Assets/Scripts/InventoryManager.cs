using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();
    public static Action OnHideMainInventory;
    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;
    [Header("Toolbar")]
    public Inventory toolbar; 
    public int toolbarSlotCount;
    [Header("Chestbar")]
    public Inventory chestbar; 
    public int chestbarSlotCount;
    

    public void Awake()
    {
        backpack = new Inventory(backpackSlotCount);
        //toolbar = new Inventory(toolbarSlotCount);
        //chestbar = new Inventory(chestbarSlotCount);
        
        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);
        inventoryByName.Add("Chestbar", chestbar);
    }

    public void Add(string inventoryName, Item item, int count)
    {
        if(inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item, count);
        }
    }

    public Inventory GetInventoryByName(string inventoryName)
    {
        if(inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }
        return null; 
    }

    
}
