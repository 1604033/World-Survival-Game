using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    public Slot_UI selectedSlot = null;
    public int selectedSlotIndex;

    private void Start()
    {
        // SelectSlot(0);
        // selectedSlotIndex = 0;
        selectedSlot = null;
        selectedSlotIndex = -1;

    }

    
    private void Update() {
        CheckAlphaNumericKeys();
    }

    public void SelectSlot(int index)
    {
        //if(toolbarSlots.Count == 16)
        {
            if(selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = toolbarSlots[index];
            selectedSlotIndex = index;
            selectedSlot.SetHighlight(true);
            Debug.Log("Selected Slot: " + selectedSlot.name);
        }
    }
    public int GetSlotIndex(Slot_UI slot)
    {
        if (toolbarSlots != null) return toolbarSlots.IndexOf(slot);
        return -1;
    }

    private void CheckAlphaNumericKeys()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(0);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(1);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(3);
        }

        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlot(4);
        }

        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectSlot(5);
        }

        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectSlot(6);
        }

        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectSlot(7);
        }

        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectSlot(8);
        }
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            SelectSlot(9);
        }
        if(Input.GetKeyDown(KeyCode.Minus))
        {
            SelectSlot(10);
            Debug.Log("Minus key pressed");
        }
        if(Input.GetKeyDown(KeyCode.Equals))
        {
            SelectSlot(11);
        }
    }
}
