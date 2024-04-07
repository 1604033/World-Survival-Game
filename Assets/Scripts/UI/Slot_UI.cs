using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_UI : MonoBehaviour
{
    public int slotID;
    public Inventory inventory;
    public Image itemIcon;
    public ItemData item;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI quantityText;
    [SerializeField] private GameObject highlight;

    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.itemName;
            quantityText.text = slot.count.ToString();
            item = slot.itemData;
            if(TryGetComponent(out ItemToolTip toolTip))
            {
                toolTip.itemData = slot.itemData;
            }
        }
        
    }
    
    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = " ";
        item = null;
        if(TryGetComponent(out ItemToolTip toolTip))
            {
                toolTip.itemData = null;
            }
    }

    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
        
    }

    public void DropAction()
    {
        if (GameManager.instance.activeSlot != null && item == null)
        {
            if (GameManager.instance.activeSlot.count > 0) 
            {
                Inventory.Slot newSlot =  GameManager.instance.activeSlot;
                GameManager.instance.inventory.AddSlot(newSlot, slotID);
                PopupManager.Instance.GetPopupFoodItemPopup().RemoveFoodItem();
                SetItem(GameManager.instance.activeSlot);
                Debug.Log("Drop slot ui");
                // oldSlot.AddItem(newSlot.itemData, newSlot.itemData.itemName, newSlot.icon, 99, newSlot.count);
            }

        }
    }
}
