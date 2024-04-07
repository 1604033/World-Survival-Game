using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using BuildingSystem.Models;

namespace BuildingSystem
{
    [RequireComponent(typeof(Tilemap))]
    public class TilemapLayer : MonoBehaviour
    {
        protected Tilemap _tilemap { get; private set; }
        protected TilemapManager _tilemapManager;
        protected Inventory _inventory;
        protected InventoryManager _inventorymanager;
        protected  Player _player;
        protected  Movement _playerMovement;
        protected void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }
        protected void Start()
        { 
	        _player = GameManager.instance.player;
	        _playerMovement = GameManager.instance.player.gameObject.GetComponent<Movement>();
            _tilemapManager = TilemapManager.instance;
            _inventorymanager = _player.inventory;
            _inventory = _inventorymanager.backpack;
        }
        protected bool CanShowPreview(BuildableItem item)
        {
                if (item.Tile != null)
            {              
                //Tiledata tiledata = _tilemapManager.GetTileItemData(item.Tile);
               // if (tiledata != null)
                {          
                   // int count = _inventory.GetItemCountFromSlot(tiledata.itemData);
                   // return count >= tiledata.quantity;
                }
                return false;
            }
                else if (item.GameObject != null)
            {
                if(item.GameObject.TryGetComponent<Building>(out Building building)){ 
                    //if (building.ItemData != null)
                    {
                     // int count = _inventory.GetItemCountFromSlot(building.ItemData);
                   // return count >= building.quantityNeededToConstruct;                                          
                    }                 
                }
            }
            
         return false;
        }
    }
}

