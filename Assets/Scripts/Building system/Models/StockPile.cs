    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class StockPile : BuildingBase
    {
        public static StockPile Instance;
        
        public Dictionary<ItemData, int> stockData;
       public List<ItemData> stockDataList ;
        public List<int> stockDataCount ;
        private int maximumStock;
        public override void Awake()
        {
            base.Awake();
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void IncrementCount(ItemData itemData, int count = 1)
        {
            if (stockDataList.Contains(itemData))
            {
                int index = stockDataList.IndexOf(itemData);
                if (itemData != null) 
                    if((stockDataCount[index] = count ) <= maximumStock)stockDataCount[index] += count;
            }
        }
        
        public void DecrementCount(ItemData itemData, int count = 1)
        {
           if (stockDataList.Contains(itemData))
            {
                int index = stockDataList.IndexOf(itemData);
                if (itemData != null) 
                    if((stockDataCount[index] - count ) > 0 )stockDataCount[index] -= count;
            }
            
        }

        public bool CanDecrementCount(ItemData itemData, int count = 1)
        {
           if (stockDataList.Contains(itemData))
            {
                int index = stockDataList.IndexOf(itemData);
                if (itemData != null) 
                    if((stockDataCount[index] - count ) > 0)return true;
            }

            return false;
        }

        public int GetItemCount(ItemData itemData)
        {
            
            if (stockDataList.Contains(itemData))
            {
                int index = stockDataList.IndexOf(itemData);
                    return stockDataCount[index];
            }
            return 0;
        }
        
        






    }
    
