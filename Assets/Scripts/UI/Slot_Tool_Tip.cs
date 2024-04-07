using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mime;
using BuildingSystem;
using BuildingSystem.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot_Tool_Tip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public BuildableItem Item;
    public ItemData itemData;
    public Player player;
    private int count = 1;
    public TextMeshProUGUI quantity;
    private Sprite icon;
    public Inventory inventory;
    public Slot_UI slot;
    public int slotID;
    public Item item;
    
    private BuildingPlacer _buildingPlacer;
    TooltipSystem _tooltipSystem;
    [SerializeField] private Color color;
    [SerializeField]  Image image ;
    private void Start()
    {
        inventory = GameManager.instance.inventory;
        _tooltipSystem = TooltipSystem.Instance;
        _buildingPlacer = FindObjectOfType<BuildingPlacer>();
    }

    public void SetActiveItem()
    {
        if (Item.canOnlyBeOneInstance)
        {
            if (BuildingsManager.Instance.GetBuilding(BuildingType.FoodStockPile) != null)
            {
                Debug.Log("Cannot set data");
                SimulateDisabled(true);
                return;
            }
        }

        if (_buildingPlacer != null)
        {
            _buildingPlacer.SetActiveBuildable(Item);
        }

        
    }

    void OnEnable()
    {
        if (Item.canOnlyBeOneInstance)
        {
            if (BuildingsManager.Instance.GetBuilding(BuildingType.FoodStockPile) != null)
            {
                SimulateDisabled(true);
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
        if (itemData != null)
        {
            string title = itemData.itemName;
            string description = itemData.description;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item != null)
        {
        if (Item.category == BuildingCategory.Basic || Item.category == BuildingCategory.Advanced)
        {
            ShowInfoPopup();
        }
        }
        if(item != null)
          ShowItemPopup();
       
    }

     private void ShowItemPopup()
    {
        _tooltipSystem.Tooltip.SetText(inventory.slots[2].itemName, null , null).Show();
        Debug.Log(inventory.slots[slotID].itemName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PopupManager.Instance.GetBuildingInfoPopup().HidePoup();
        _tooltipSystem.Tooltip.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click here");
        player.inventory.Add("Backpack", item, count);
        icon = itemData.icon;
        Destroy(image);
        itemData = null;
        //Destroy(gameObject);
        Debug.Log("Item added");
        SetActiveItem();
        
    }
}
