using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureContainer : MonoBehaviour
{
    public Action<ItemData, int> OnAddToTreasure;
    public GameObject itemPopupPrefab; // Assign your item popup prefab in the Unity editor
    public Item[] secretItems; // Assign your secret items in the Unity editor

    
    //Todo Remove test fields
    [SerializeField] private ItemData _itemData;
    
    private UIManager uiManager;
    public Inventory PlayerInventory;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void InteractWithContainer()
    {
        ShowItemPopup();
    }

    private void ShowItemPopup()
    {
        GameObject popupInstance = Instantiate(itemPopupPrefab, Vector3.zero, Quaternion.identity);
        ItemPopup itemPopup = popupInstance.GetComponent<ItemPopup>();

        // Set up item slots and button callbacks
        for (int i = 0; i < secretItems.Length; i++)
        {
            int index = i; // Capture the index for the callback
            //itemPopup.SetItemSlot(i, secretItems[i].icon, () => AddItemToInventory(index));
        }

        // Set up the "Add All" button callback
        itemPopup.SetAddAllButton(() => AddAllItemsToInventory());

        uiManager.ShowPopup(popupInstance);
    }

    private void AddItemToInventory(int itemIndex)
    {
        // Add the selected item to the player's inventory
        //PlayerInventory.Instance.AddItem(secretItems[itemIndex]);
        //PlayerInventory.AddItem(secretItems[itemIndex]);
        // Optionally, you can close the item popup after adding the item
        uiManager.ClosePopup();
    }

    private void AddAllItemsToInventory()
    {
        Debug.Log("Finished adding!!----------------");
        OnAddToTreasure?.Invoke(_itemData, 10);
        // Add all secret items to the player's inventory
        foreach (Item item in secretItems)
        {
            //PlayerInventory.Instance.AddItem(item);
        }
        // Close the item popup after adding all items
        uiManager.ClosePopup();
    }
}
