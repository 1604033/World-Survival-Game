using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BuildingSystem;
using BuildingSystem.Models;


public class ItemPopup : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler 
{
   
    public Player player;
    public Item item;
    private int count = 1;
    public Transform itemSlotParent; // Assign the parent object of your item slots in the Unity editor
    public Button addAllButton; // Assign your "Add All" button in the Unity editor

    private Button[] itemButtons;

    private void Awake()
    {
        itemButtons = itemSlotParent.GetComponentsInChildren<Button>();
        // Assign click callbacks to item buttons
        for (int i = 0; i < itemButtons.Length; i++)
        {
            int index = i; // Capture the index for the callback
            itemButtons[i].onClick.AddListener(() => OnItemButtonClick(index));
        }

        // Assign click callback to "Add All" button
        addAllButton.onClick.AddListener(OnAddAllButtonClick);
    }

    public void SetItemSlot(int index, Sprite itemIcon, UnityEngine.Events.UnityAction onClickCallback)
    {
        // Set the item icon and click callback for the specified item slot
        if (index >= 0 && index < itemButtons.Length)
        {
            Image buttonImage = itemButtons[index].GetComponent<Image>();
            buttonImage.sprite = itemIcon;
            itemButtons[index].onClick.AddListener(onClickCallback);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Player player = collision.GetComponent<Player>();
        //SetActiveItem();
        player.inventory.Add("Backpack", item, count);
        Destroy(gameObject);
        Debug.Log("Item added");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointer exit");
    }
    public void SetAddAllButton(UnityEngine.Events.UnityAction onClickCallback)
    {
        // Set the click callback for the "Add All" button
        addAllButton.onClick.AddListener(onClickCallback);
    }

    private void OnItemButtonClick(int index)
    {
        // Handle individual item button click if needed
    }

    private void OnAddAllButtonClick()
    {
        // Handle "Add All" button click if needed
    }

}
