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

public class ItemToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    public BuildableItem Item;
    public ItemData itemData;
    public Player player;
    public Item item;
    private int count = 1;
    public TextMeshProUGUI quantity;
    private Sprite icon;
    
    private BuildingPlacer _buildingPlacer;
    TooltipSystem _tooltipSystem;
    [SerializeField] private Color color;
    [SerializeField]  Image image ;

    private void Start()
    {
        _tooltipSystem = TooltipSystem.Instance;
        _buildingPlacer = FindObjectOfType<BuildingPlacer>();
        player = GameManager.instance.player;
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
        if (Item != null && Item.canOnlyBeOneInstance)
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
        if(itemData != null)
          ShowItemPopup();
       
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
        Debug.Log("click here");
        player.inventory.Add("Backpack", item, count);
        icon = itemData.icon;
        Destroy(image);
        itemData = null;
        //Destroy(gameObject);
        Debug.Log("Item added");
        SetActiveItem();
        
    }
    public void AddItem()
    {
        //Debug.Log("click here");
        player.inventory.Add("Backpack", item, count);
        icon = itemData.icon;
        itemData = null;
        Destroy(image);
        count = 0;
        //quantity.text = '0'.ToString();
        //quantity.SetText("0");
        //Debug.Log(quantity);
        
        //quantity = '0';
        //Destroy(gameObject);
        Debug.Log("Item added");
    }
    public void AddItem1()
    {
        //Debug.Log("click here");
        player.inventory.Add("Backpack", item, count);
        icon = itemData.icon;
        count = 0;
        itemData = null;
        Destroy(image);
        //Destroy(gameObject);
        Debug.Log("Item added");
    }
     public void AddItem2()
    {
        //Debug.Log("click here");
        player.inventory.Add("Backpack", item, count);
        icon = itemData.icon;
        itemData = null;
        Destroy(image);
        //Destroy(gameObject);
        Debug.Log("Item added");
    }
     public void AddItem3()
    {
        //Debug.Log("click here");
        player.inventory.Add("Backpack", item, count);
        icon = itemData.icon;
        itemData = null;
        Destroy(image);
        //Destroy(gameObject);
        Debug.Log("Item added");
    }
}
