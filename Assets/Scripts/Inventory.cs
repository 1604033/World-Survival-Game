using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public  Action<ItemData, int> OnInventoryUpdate;

    [System.Serializable] 
    
    /*other
    public static Inventory instance;
    void Awake ()
    {
        instance = this;
    }
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    //other*/
    public class Slot
    {
        public string itemName;
        public int count;
        public int maxAllowed;
        public ItemData itemData;
        public Sprite icon;

        public Slot()
        {
            itemName = "";
            count = 0;
            maxAllowed = 1000;
        }

        public bool IsEmpty
        {
            get
            {
                if (itemName == "" && count == 0)
                {
                    return true;
                }

                return false;
            }
        }

        public bool CanAddItem(string itemName)
        {
            if (this.itemName == itemName && count < maxAllowed)
            {
                return true;
            }

            return false;
        }

        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.itemData = item.data;
            this.icon = item.data.icon;
            count++;
        }
        public void AddItem(Item item, int count)
        {
            this.itemName = item.data.itemName;
            this.itemData = item.data;
            this.icon = item.data.icon;
            this.count += count;
        }

        public void AddItem(ItemData itemData, string itemName, Sprite icon, int maxAllowed, int count)
        {
            this.itemName = itemName;
            this.icon = icon;
            this.itemData = itemData;
            this.count += count;
            this.maxAllowed = maxAllowed;
        }


        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;
                if (count == 0)
                {
                    icon = null;
                    itemName = "";
                    itemData = null;
                }
            }
        }

        public void RemoveAll()
        {
            if(count > 0)
            {
                count = 0;
                icon = null;
                itemName = "";
                itemData = null;
            }
        }

        public bool RemoveItemWithConfirmation(int toRemove)
        {
            if (count >= toRemove)
            {
                count -= toRemove;
                if (count == 0)
                {
                    icon = null;
                    itemName = "";
                    itemData = null;
                }

                return true;
            }

            return false;
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Item item, int count)
    {
        foreach (Slot slot in slots)
        {
            if (slot.itemName == item.data.itemName && slot.CanAddItem(item.data.itemName))
            {   
                slot.AddItem(item, count);
                OnInventoryUpdate?.Invoke(item.data, GetItemCountInTheInventory(item.data));
                return;
                //if(onItemChangedCallback != null)
                //onItemChangedCallback.Invoke();
            }
        }

        foreach (Slot slot in slots)
        {
            if (slot.itemName == "")
            {
                slot.AddItem(item, count);
                OnInventoryUpdate?.Invoke(item.data, GetItemCountInTheInventory(item.data));
                return;
            }
        }
    }
    public void AddSlot(Slot slot, int index)
    {
        slots[index] = slot;
        // slots[index].AddItem(slot.itemData,slot.itemName,slot.icon,slot.maxAllowed, slot.count );
    }
   
    public void Remove(int index)
    {
        slots[index].RemoveItem();

        //if(onItemChangedCallback != null)
        //onItemChangedCallback.Invoke();
    }
    public void Remove(Slot slotParam)
    {
        slotParam.RemoveItem();
        Debug.Log($"Removing {slotParam}");
        //if(onItemChangedCallback != null)
        //onItemChangedCallback.Invoke();
    }

    public void Remove(int index, int numToRemove)
    {
        if (slots[index].count >= numToRemove)
        {
            for (int i = 0; i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }

    public void RemoveItemsFromSlot(ItemData itemData, int count)
    {
        Slot slot = slots.Find(x => x.itemData == itemData);
        int slotIndex = slots.FindIndex(x => x == slot);
        if (slot == null)
        {
            return;
        }

        slot.RemoveItem();
    }

    public int GetItemCountFromSlot(ItemData itemData)
    {
        Slot slot = slots.Find(x => x.itemData == itemData);
        int slotIndex = slots.FindIndex(x => x == slot);
        if (slot == null)
        {
            return 0;
        }

        return slot.count;
    }

    public bool RemoveItemsFromSlotWithConfirmation(List<ItemData> itemDatas, List<int> itemsCounts)
    {
        List<Slot> slotsToremove = new List<Slot>();
        List<int> slotsCountToRemove = new List<int>();
      Debug.Log("Remove method called");
        for (int i = 0; i < itemDatas.Count; i++)
        {
            Slot slot = slots.Find(x => x.itemData == itemDatas[i]);
            int slotIndex = slots.FindIndex(x => x == slot);
            if (slot == null)
            {
                return false;
            }

            if (slot.count < itemsCounts[i])
            {
                return false;
            } 
            slotsToremove.Add(slot);
            slotsCountToRemove.Add(itemsCounts[i]);
        }
        RemoveRawMaterialFromInventoryWithConfirmation(slotsToremove, slotsCountToRemove);
        return true;
    }

    public bool RemoveRawMaterialFromInventoryWithConfirmation(List<Slot> slotWithCount, List<int> slotsCountToRemove)
    {
        for (int i = 0; i < slotWithCount.Count; i++)
        {
            bool  isremoved = slotWithCount[i].RemoveItemWithConfirmation(slotsCountToRemove[i]);
            if (isremoved == false)
            {
                return false;
            }  
        }
        return true;
    }

/*public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if (toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            toSlot.AddItem(fromSlot.itemData, fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed, 1);
            fromSlot.RemoveItem();
        }
    }*/

public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory, int numToMove = 1)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if (toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for(int i = 0; i < numToMove; i++)
            {
              toSlot.AddItem(fromSlot.itemData, fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed, 1);
              fromSlot.RemoveItem();
            }
            
        }
    }

    public int GetItemCountInTheInventory(ItemData itemData)
    {
        return slots.Sum(slot => slot.itemData == itemData ? slot.count :  0);
    }

    public void RemovePermanent(int index)
    {
        //slots[index].RemoveItem();
        slots[index].RemoveAll();
    }
}