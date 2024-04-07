using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
    public GameObject prefab;
    public GameObject stackPrefab;
    public CollectableType type;
    public GameObject seedPrefab;
    public String description;
    public String price;
    public FoodItem FoodItem;
}
public enum CollectableType
{
    NONE, Wood, Stone, Seed, Fruit, Trash,FoodItem, 
}
