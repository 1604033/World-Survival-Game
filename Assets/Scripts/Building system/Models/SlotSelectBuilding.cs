using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using BuildingSystem;
using BuildingSystem.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotSelectBuilding : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public BuildableItem Item;
    //public ItemData Item;
    
    public ItemData itemData;
    private BuildingPlacer _buildingPlacer;
    TooltipSystem _tooltipSystem;
    [SerializeField] private Color color;
    [SerializeField] Image image;
    private TabsNavigation TabsNavigation;

    private void Start()
    {
        _tooltipSystem = TooltipSystem.Instance;
        TabsNavigation = GameManager.instance.tabNavigation;
        _buildingPlacer = FindObjectOfType<BuildingPlacer>();
    }

    public void SetActiveItem()
    {
        if (Item != null)
        {
            if (Item.canOnlyBeOneInstance && Item.Type == BuildingType.FoodStockPile)
            {
                if (BuildingsManager.Instance.GetBuilding(BuildingType.FoodStockPile) != null)
                {
                    SimulateDisabled(true);
                    return;
                }
            }

            if (_buildingPlacer != null)
            {
                _buildingPlacer.SetActiveBuildable(Item);
            }
        }
    }

    void OnEnable()
    {
        if (Item != null)
        {
            if (Item.canOnlyBeOneInstance)
            {
                if (BuildingsManager.Instance.GetBuilding(BuildingType.FoodStockPile) != null)
                {
                    SimulateDisabled(true);
                }
            }
        }
    }

    public void SimulateDisabled(bool state)
    {
        if (image != null)
        {
            image.color = state ? color : Color.white;
        }
    }

    private void ShowInfoPopup()
    {
        if (Item != null)
        {
            string description = Item.Description;
            string title = Item.Name;
            List<string> items = new List<string>();
            List<string> quantities = new List<string>();
            List<string> listText = new List<string>();

            for (int i = 0; i < Item.itemsDatasNeededToConstruct.Count; i++)
            {
                items.Add(Item.itemsDatasNeededToConstruct[i].itemName);
                quantities.Add(Item.itemsNeedCounts[i].ToString());
                listText.Add($"{Item.itemsDatasNeededToConstruct[i].itemName}  {Item.itemsNeedCounts[i].ToString()}x");
            }

            // PopupManager.Instance.GetBuildingInfoPopup().SetDescription(description).SetMaterials(items, quantities).SetTitle(title).Show(Input.mousePosition);
            _tooltipSystem.Tooltip.SetText(title, description, listText).Show();
        }
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Item != null)
        {
            if (Item.category == BuildingCategory.Basic || Item.category == BuildingCategory.Advanced)
            {
                ShowInfoPopup();
            }
        }

        if (TryGetComponent(out Slot_UI slotUI))
        {
            itemData = slotUI.item;
        }

        if (itemData != null) ShowItemPopup();
    }

    private void ShowItemPopup()
    {
        _tooltipSystem.Tooltip.SetText(itemData.itemName, itemData.description, null).Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PopupManager.Instance.GetBuildingInfoPopup().HidePoup();
        _tooltipSystem.Tooltip.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetActiveItem();
        TabsNavigation.ToggleTab(2);
        PopupManager.Instance.GetBuildingInfoPopup().HidePoup();
        //Hide The main Inventory
        InventoryManager.OnHideMainInventory?.Invoke();
        _tooltipSystem.Tooltip.Hide();

    }
}