using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    /*other
    Inventory inventory;
    void Start() {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        
    }
    void UpdateUI ()
    {
        Debug.Log("Hey, Updating");
    }
    //other*/
    //public GameObject inventoryPanel;

    //public Player player;
    public string inventoryName;
    public Item item;
    public UI_Manager ui_manager;

    public List<Slot_UI> slots = new List<Slot_UI>();
    //public Inventory inventory;

    //[SerializeField] private Canvas canvas;

    //private Slot_UI draggedSlot;
    //private Image draggedIcon;
    [SerializeField]public Canvas canvas;
    [SerializeField]public Transform canvasTransform;
    //private bool dragSingle;
    [SerializeField]private Inventory inventory;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
       //inventory =  player.inventory;
       Debug.Log("Start is called: Inventory");
       inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);
        SetupSlots();
        Refresh();
    }

    private void OnEnable()
    {
        if (canvas == null)
        {
            Refresh();
            // canvas = FindObjectOfType<Canvas>();
        }
        //if(slots.Count != 0) Refresh();
    }
    /* void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if(!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Refresh();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }*/

    public void Refresh()
    {
        if(slots.Count == inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if(inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(inventory.slots[i]);

                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
       /* else if(slots.Count == player.toolbar.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if(player.toolbar.slots[i].itemName != "")
                {
                    slots[i].SetItem(player.toolbar.slots[i]);

                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }*/
    }

   /*public void Remove(int slotID)
    {
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(player.inventory.slots[slotID].itemName);
        if(itemToDrop != null)
        {
            player.DropItem(itemToDrop);
            player.inventory.Remove(slotID);
            Refresh();

        }
        
    }*/
   public void Remove()
    {
         Item itemToDrop = GameManager.instance.itemManager.GetItemByName(inventory.slots[UI_Manager.draggedSlot.slotID].itemName);
        if(itemToDrop != null)
        {
            if(UI_Manager.dragSingle)
            {
                GameManager.instance.player.DropItem(itemToDrop);
            inventory.Remove(UI_Manager.draggedSlot.slotID);
            }
            else
            {
            GameManager.instance.player.DropItem(itemToDrop, inventory.slots[UI_Manager.draggedSlot.slotID].count);
            inventory.Remove(UI_Manager.draggedSlot.slotID, inventory.slots[UI_Manager.draggedSlot.slotID].count);
            }
            Refresh();
        }
        UI_Manager.draggedSlot = null;
        
    }

    public void SlotBeginDrag(Slot_UI slot)
    {
        
        UI_Manager.draggedSlot = slot;
        UI_Manager.draggedIcon = Instantiate(UI_Manager.draggedSlot.itemIcon);
        //draggedIcon = Instantiate(draggedSlot.slot.icon);
        //UI_Manager.draggedIcon.transform.SetParent(GameManager.instance.testCanvasTransform);
        UI_Manager.draggedIcon.transform.SetParent(canvas.transform);
        UI_Manager.draggedIcon.raycastTarget = false;
        UI_Manager.draggedIcon.rectTransform.sizeDelta = new Vector2(40, 40);
        MoveToMousePosition(UI_Manager.draggedIcon.gameObject);
        Debug.Log("Start Drag: " + UI_Manager.draggedSlot.name);

    }

    public void SlotDrag()
    {
        /*if (UI_Manager.draggedIcon != null)
        {
            MoveToMousePosition(UI_Manager.draggedIcon.gameObject);
            UI_Manager.draggedIcon.transform.position = Input.mousePosition;
        }*/
        MoveToMousePosition(UI_Manager.draggedIcon.gameObject);

        //Debug.Log("Dragging: " + UI_Manager.draggedSlot.name);
    }

   /* public void SlotEndDrag()
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null; 
        //Remove();
        //draggedIcon = draggedSlot.itemIcon;
        Debug.Log("Done Dragging" + draggedSlot.name);
    }*/

  public void SlotEndDrag()
{
    if (UI_Manager.draggedIcon != null) Destroy(UI_Manager.draggedIcon.gameObject);
    // UI_Manager.draggedIcon = null;
    //Remove();
    Refresh();
    /*if (draggedSlot != null)
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null; 
        Debug.Log("Done Dragging" + draggedSlot.name);
    }*/
}
/*public void SlotEndDrag()
    {
        // Use Physics.Raycast to check if the new instance overlaps with the player GameObject.
        RaycastHit hit;
        if (Physics.Raycast(draggedItemTransform.position, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Remove the item from the inventory and destroy the new instance.
                player.inventory.Remove(draggedSlot.slotID);
                Destroy(draggedItemTransform.gameObject);
            }
        }
        else
        {
            // Destroy the new instance if it was not dropped on the player.
            Destroy(draggedItemTransform.gameObject);
        }
    }*/


    public void SlotDrop(Slot_UI slot)
    {
        if(UI_Manager.dragSingle)
        {
           UI_Manager.draggedSlot.inventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, slot.inventory);
        }
        else
        {
           UI_Manager.draggedSlot.inventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, slot.inventory,
           UI_Manager.draggedSlot.inventory.slots[UI_Manager.draggedSlot.slotID].count);
        }
        Destroy(UI_Manager.draggedIcon.gameObject);
        UI_Manager.draggedIcon = null;
        //Debug.Log("Dropped: " + UI_Manager.draggedSlot.name + " on " + slot.name);
        
        GameManager.instance.activeSlot = inventory.slots[slot.slotID];
        GameManager.instance.uiManager.RefreshAll();
        /*if (slot != null)
        {
            //player.inventory.MoveSlot(draggedSlot.slotID, slot.slotID);
            Refresh();
            // Destroy(UI_Manager.draggedIcon.gameObject);
            // UI_Manager.draggedIcon = null;
            //Debug.Log("Dropped: " + UI_Manager.draggedSlot.name + " on " + slot.name);
            if (UI_Manager.draggedSlot != null)
                UI_Manager.draggedSlot.inventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, slot.inventory);
            GameManager.instance.activeSlot = inventory.slots[slot.slotID];
            GameManager.instance.uiManager.RefreshAll();
            Debug.Log("Drop Function running");
        }*/

        // GameManager.instance.activeSlot = null;

    }

    public void trash(Slot_UI slot)
    {
        /*player.inventory.MoveSlot(draggedSlot.slotID, slot.slotID);*/
        //Refresh();

        Destroy(UI_Manager.draggedIcon.gameObject);
        UI_Manager.draggedIcon = null;
        //Debug.Log("Dropped: " + UI_Manager.draggedSlot.name + " on " + slot.name);
        //UI_Manager.draggedSlot.inventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, slot.inventory);
        //GameManager.instance.activeSlot = inventory.slots[slot.slotID];
        GameManager.instance.uiManager.RefreshAll();
        Debug.Log("Good bye");
    }

    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
        ui_manager.RefreshInventoryUI("Toolbar");
        ui_manager.RefreshInventoryUI("Chestbar");
    }

    void SetupSlots()
    {
        int counter = 0;
        foreach(Slot_UI slot in slots)
        {
            slot.slotID = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
    public void RemovePermanenet(int slotID)
    {
        //player.invent.RemovePermanent(slotID);
        inventory.slots[slotID].RemoveAll();
        Refresh();
    }

    
}
