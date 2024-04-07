using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestbar_UI : MonoBehaviour
{[SerializeField] private List<Slot_UI> chestbarSlots = new List<Slot_UI>();

    public Slot_UI selectedSlot;
    public int selectedSlotIndex;

    private void Start()
    {
        SelectSlot(0);
        selectedSlotIndex = 0;
    }

    
    private void Update() {
        //CheckAlphaNumericKeys();
    }

    public void SelectSlot(int index)
    {
        //if(toolbarSlots.Count == 16)
        {
            if(selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = chestbarSlots[index];
            selectedSlotIndex = index;
            selectedSlot.SetHighlight(true);
            Debug.Log("Selected Slot: " + selectedSlot.name);
        }
    }
    public int GetSlotIndex(Slot_UI slot)
    {
        if (chestbarSlots != null) return chestbarSlots.IndexOf(slot);
        return -1;
    }

    
}
