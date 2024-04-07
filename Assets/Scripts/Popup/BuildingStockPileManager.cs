using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuildingStockPileManager : MonoBehaviour
{
    public FoodStockPile currentBuilding;
    private int currentlyDraggedFoodItem ;
    [SerializeField]private GameObject slideOne;
    [SerializeField]private GameObject slideTwo;
    public List<FoodItemDisplay> FoodItemsDisplay = new List<FoodItemDisplay>();
    public List<FoodItemDisplay> InvertoryFoodItemsDisplays = new List<FoodItemDisplay>();
          public class Dialog
            {
                public string Title =  "Title";
                public string Description = "You are now view the popup message";
            }
        
            private Dialog dialog = new Dialog();
        
            //public Action OnCancelOrDemolishClick;
            
            [SerializeField] private GameObject canvas;
            [SerializeField] private Text titleTextUI;
            [SerializeField] private Text messageTextUI;
            [SerializeField] private Button closeButton;
        
            private void Awake()
            {
                closeButton.onClick.RemoveAllListeners();
                closeButton.onClick.AddListener(Hide);
                FoodItemDisplay.OnAddButtonClick += ButtonAddClicked;
            }

            private void ButtonAddClicked()
            {
                slideOne.SetActive(false);
                slideTwo.SetActive(true);
                SetDisplayOfInventory();

            }

            private void OnDisable()
            {
                currentBuilding = null;
                currentlyDraggedFoodItem = -1;
            }

            void RefreshSlots()
            {
                if (currentBuilding != null && currentBuilding.stockedFoodItems.Count > 0)
                    SetFoodItems(currentBuilding.stockedFoodItems, currentBuilding.stockedFoodItemsCount);
            }
            public BuildingStockPileManager SetTitle(string title)
            {
                dialog.Title = title;
                return this;
            } 
            public BuildingStockPileManager SetFoodItems(List<FoodItem> foodItems, List<int> foodItemCount)
            {
                bool foundItemEmpty = false;
                for (int i = 0; i < FoodItemsDisplay.Count && i < foodItems.Count; i++)
                {
                    
                    FoodItemDisplay foodItemDisplay = FoodItemsDisplay[i];
                    FoodItem foodItem = foodItems[i];
                    if (foodItem != null  )
                    {
                    foodItemDisplay.SetDisplay(foodItem, foodItem.Name,foodItem.Icon , foodItemCount[i], i);
                    }
                    else
                    {
                        foodItemDisplay.SetEmpty();
                    }
                }

                foreach (var t in FoodItemsDisplay)
                {
                    if (!t.isFoodItemDisplayOccupied && foundItemEmpty == false)
                    {
                        t.SetButtonActive(true);
                        foundItemEmpty = true;
                    }
                }
                
                return this;
            } 
            public BuildingStockPileManager SetFoodItemsInventory(List<FoodItem> foodItems, List<int> foodItemCount)
            {
                for (int i = 0; i < InvertoryFoodItemsDisplays.Count && i < foodItems.Count; i++)
                {
                    
                    FoodItemDisplay foodItemDisplay = InvertoryFoodItemsDisplays[i];
                    FoodItem foodItem = foodItems[i];
                    if (foodItem != null  )
                    {
                        Debug.Log("Foound an item with type  " + foodItem);
                        foodItemDisplay.SetDisplay(foodItem, foodItem.Name,foodItem.Icon , foodItemCount[i], i);
                    }
                }
                return this;
            } 
            public BuildingStockPileManager SetMessage(string message)
            {
                dialog.Description = message;
                return this;
            }

            public BuildingStockPileManager SetDisplayOfInventory()
            {
             Inventory inventory =  GameManager.instance.inventory;
             List<FoodItem> foodItems = new List<FoodItem>();
             List<int> foodItemsCount = new List<int>();

             foreach (var slot in inventory.slots)
             {
                 
                 if (slot.itemData != null && slot.itemData.type == CollectableType.FoodItem)
                 {
                     foodItems.Add(slot.itemData.FoodItem);
                     foodItemsCount.Add(slot.count);
                 }
             }

             SetFoodItemsInventory(foodItems, foodItemsCount);
                
                return this;
            }

            public void RemoveFoodItem()
            {
               if(currentlyDraggedFoodItem >= 0) currentBuilding.RemoveFoodItem(currentlyDraggedFoodItem);
               RefreshSlots();
               
               Debug.Log("Reset slots");
            }
            
           
            public void Show()
            {
                if (titleTextUI != null) titleTextUI.text = dialog.Title;
                if (messageTextUI != null) messageTextUI.text = dialog.Description;
                canvas.SetActive(true);
            }
            public void Hide()
            {
                Debug.Log("Hide CALLED"); 
                canvas.SetActive(false);
                dialog = new Dialog();
            }
            
}

