using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class RemoveDic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform itemsPanel;  // Reference to the panel that displays dictionary items
    public Image trashBinImage;   // Reference to the trash bin image

    private Dictionary<string, GameObject> dictionaryItems = new Dictionary<string, GameObject>();

    private GameObject draggedItem;
    private string draggedKey;

    // Example dictionary initialization (you can replace this with your own)
    private void Start()
    {
        // Initialize your dictionary with items
        dictionaryItems.Add("Item1", CreateDictionaryItem("Item1"));
        dictionaryItems.Add("Item2", CreateDictionaryItem("Item2"));
        dictionaryItems.Add("Item3", CreateDictionaryItem("Item3"));
    }

    private GameObject CreateDictionaryItem(string itemName)
    {
        // Create and return a GameObject representing a dictionary item
        // Customize this function to match your actual UI element creation process
        GameObject item = new GameObject(itemName);
        // Add necessary UI components, set their properties, and add to the itemsPanel
        return item;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || !dictionaryItems.ContainsValue(eventData.pointerDrag))
            return;

        draggedItem = eventData.pointerDrag;
        draggedKey = dictionaryItems.FirstOrDefault(x => x.Value == draggedItem).Key;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            draggedItem.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            // Check if the item is dropped onto the trash bin
            if (RectTransformUtility.RectangleContainsScreenPoint(trashBinImage.rectTransform, Input.mousePosition))
            {
                // Remove the item from the dictionary and destroy the GameObject
                dictionaryItems.Remove(draggedKey);
                Destroy(draggedItem);
            }

            draggedItem = null;
            draggedKey = null;
        }
    }
}
