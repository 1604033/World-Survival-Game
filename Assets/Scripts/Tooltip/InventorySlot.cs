using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int slotID;  // Set this in the inspector or dynamically when creating slots

    private SlotTooltip tooltip;
    private static Dictionary<int, ItemData> slotItemDataMap = new Dictionary<int, ItemData>();

    private void Start()
    {
        tooltip = FindObjectOfType<SlotTooltip>();
        if (tooltip == null)
        {
            Debug.LogError("Tooltip not found in the scene!");
        }
        else
        {
            tooltip.HideTooltip();
        }
    }

    public void SetItemData(ItemData newItemData)
    {
        if (!slotItemDataMap.ContainsKey(slotID))
        {
            slotItemDataMap.Add(slotID, newItemData);
        }
        else
        {
            slotItemDataMap[slotID] = newItemData;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotItemDataMap.ContainsKey(slotID))
        {
            tooltip.ShowTooltip(slotItemDataMap[slotID].itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
