using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trashbin : MonoBehaviour
{
    public Inventory inventory;
    public Item item;
    public List<Slot_UI> slots = new List<Slot_UI>();
    public Slot_UI slot;

    private void Start()
    {
        inventory = GameManager.instance.player.inventory.backpack;
    }

    public void MouseClicked (int slotId)
    {
        //Instead of mouse click it is MouseDrop in the event trigger
        
        Debug.Log("MouseClicked with Id: " + UI_Manager.draggedSlot.slotID);
        //inventory.RemovePermanent(UI_Manager.draggedSlot.slotID);
        // Check if the item dropped into the trash bin is an inventory item
        //Item item = other.GetComponent<Item>();

        // if (item != null)
        // {
        //     // If it is an inventory item, delete it from the inventory
        //     Destroy(UI_Manager.draggedIcon.gameObject);
        //     UI_Manager.draggedIcon = null;
        //     //Destroy(draggedIcon.gameObject);
        //     //draggedIcon = null; 
        //     Debug.Log("Destrioying");
        //     UI_Manager.draggedSlot.inventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, slot.inventory);
        //     //GameManager.instance.activeSlot = inventory.slots[slot.slotID];
        //     GameManager.instance.uiManager.RefreshAll();
        //     //inventory.RemovePermanent(GameManager.instance.activeSlot.slotID);
        //     //inventory.Remove(1);
        //     // Destroy the GameObject that represents the inventory item
        //     Destroy(other.gameObject);
        // }
    }
}
