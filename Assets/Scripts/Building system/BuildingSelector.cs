using System;
using System.Collections.Generic;
using BuildingSystem.Models;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingSelector : MonoBehaviour
    {
   
        [SerializeField]
        private List <BuildableItem> _buildableItems ;
        
        [SerializeField]
        private BuildingPlacer _buildablePlacer ;
        
        private int activeBuildableIndex;

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.N))
        //     {
        //         // if (activeBuildableIndex == _buildableItems.Count)
        //         // {
        //         //     activeBuildableIndex = 0;
        //         //     _buildablePlacer.SetActiveBuildable(null);
        //         // }
        //        // else
        //         {
        //             NextItem();
        //         }
        //     }
        // }
        //
        // private void NextItem()
        // {
        //    
        //     activeBuildableIndex = (activeBuildableIndex + 1) % _buildableItems.Count;
        //     _buildablePlacer.SetActiveBuildable(_buildableItems[activeBuildableIndex]);
        // }
        public void SetBuildingItem(BuildableItem item)
        {
            if (_buildableItems.Contains(item))
            {
                _buildablePlacer.SetActiveBuildable(item);
            }
        }
    
       
    }
}
