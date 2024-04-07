using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryFoodStockPileInterface : MonoBehaviour
{
    [SerializeField]private BuildingStockPileManager _buildingStockPileManager;
    [SerializeField] private FoodItem _foodItem;
    [SerializeField] private FoodItemDisplay display;
    
    public void DroppedItem()
    {
        Inventory.Slot activeSlot = GameManager.instance.activeSlot;
        if (activeSlot != null) Debug.Log("Active slot  not null");
        if (activeSlot.itemData != null)  Debug.Log("Active slot  not null");
        if (activeSlot.itemData.FoodItem != null) Debug.Log("Active slot  not null"); 
        _buildingStockPileManager.currentBuilding.Add(GameManager.instance.activeSlot.itemData.FoodItem,GameManager.instance.activeSlot.count);
        GameManager.instance.player.inventory.GetInventoryByName("Backpack").Remove(activeSlot);
    }
    
    public  void Ondrag()
    {
        Inventory.Slot slot = new Inventory.Slot();
        slot.itemData = display.Data;
        slot.count = display.Count;
        slot.icon = display.Icon;
        slot.itemName = display.Name;
        slot.maxAllowed = 99;
        GameManager.instance.activeSlot = slot;
    }
    
    
    public void DragEnd(PointerEventData eventData){
        GameObject objectHit = eventData.pointerCurrentRaycast.gameObject;
        
        if (objectHit.TryGetComponent(out Slot_UI slotUI))
        {
            Debug.Log("The slotId: " + slotUI.slotID);
        }

        Debug.Log("The slotId: " + slotUI.slotID);

    }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     GameObject objectHit = eventData.pointerCurrentRaycast.gameObject;
    //     if (objectHit != null) Debug.Log("Found the slot " + objectHit.name);
    //
    //     Slot_UI slotUI = objectHit.GetComponent<Slot_UI>();
    //     // if (objectHit.TryGetComponent(out Slot_UI slotUI))
    //     // {
    //     //     Debug.Log("The slotId: " + slotUI.slotID);
    //     // }
    //
    //     if (slotUI != null) Debug.Log("Found the slot " + slotUI);
    // }
}