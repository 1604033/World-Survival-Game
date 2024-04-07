    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class FoodStockPile : BuildingBase 
    {
        
        public List<FoodItem> stockedFoodItems = new List<FoodItem>(10);
        public List<int> stockedFoodItemsCount = new List<int>(10);
        private List<Animal> _animals;
        int currentFoodRatio;
        public void Add(FoodItem foodItem, int count)
        { 
            int emptyIndex = 0;
           bool HasFoundEmpty = false;
           for (int i = 0; i < stockedFoodItems.Count && HasFoundEmpty==false ; i++)
           {
               if (stockedFoodItems[i] == null)
               {
                   emptyIndex = i;
                   HasFoundEmpty = true;
               }
           }

            if (emptyIndex == -1) return;
            
           //FoodItem foodItemEmptySlot = stockedFoodItems[emptyIndex];
          if (HasFoundEmpty)
           {
               Debug.Log($"Added foodItem at {stockedFoodItems[emptyIndex]} ");
               stockedFoodItems[emptyIndex] = foodItem;
               stockedFoodItemsCount[emptyIndex] = count;
           }

          BuildingShowFoodItems();

        }

        public override void Start()
        {
            base.Start();
             DayTimeController.Instance.OnTimeForAnimalsToEat += TimeToEat;
             FoodAllocationSlider.OnFoodRatioAllocationChanged += UpdateFoodRatio;
        }

        private void UpdateFoodRatio(int value)
        {
            switch (value)
            {
                case -2:
                    currentFoodRatio = -100;
                    break;
                case -1:
                    currentFoodRatio = -50;
                    break;
                case 0:
                    currentFoodRatio = 0;
                    break;
                case 1:
                    currentFoodRatio = 50;
                    break;
                case 2:
                    currentFoodRatio = 100;
                    break;
                
                
            }
        }

        private void TimeToEat()
        {
            Invoke(nameof(FeedAnimals) , 40f);
        }

        private void BuildingShowFoodItems()
        {
            BuildingStockPileManager buildingStockPile =  PopupManager.Instance.GetPopupFoodItemPopup();
            buildingStockPile.SetFoodItems(stockedFoodItems, stockedFoodItemsCount);

        }
        public bool RemoveFoodItem(int index)
        {
            if (stockedFoodItems.Count == 0) 
                return false;
            if (index >= 0 && index < stockedFoodItems.Count) 
                stockedFoodItems[index] = null;
            if (index >= 0 && index < stockedFoodItemsCount.Count) 
                stockedFoodItemsCount[index] = 0; 

            return true;

        } 
        public bool RemoveFoodItem(int index, int count)
        {
            if (stockedFoodItems.Count == 0) 
                return false;

            if (index >= 0 && index < stockedFoodItemsCount.Count)
            {
                int slotCount = stockedFoodItemsCount[index];
                if (slotCount >= count)
                {
                    stockedFoodItemsCount[index] -= count;
                }

                if (count == 0)
                {
                    stockedFoodItems[index] = null;  
                    stockedFoodItemsCount[index] =  0;
                }

            } 
          return true;
        }

        void FeedAnimals()
        {
           _animals = AnimalsManager.Instance.GetAllAnimals(AnimalType.Pokemon);
         
           foreach (var animal in _animals)
           {
               float foodAllocated = ((100 + currentFoodRatio) / 100f) * animal.amountOfFoodConsumption;
                RemoveFoodItem(0, (int)foodAllocated);
               if (animal != null) animal.foodLevel = (int)foodAllocated;
               Debug.Log("Feeding " + foodAllocated);
           }
        }
        
    }
    
