using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tiledata")]

public class Tiledata : ScriptableObject
{
    public TileBase[] tile;
    public ItemData itemData;
    public  Dictionary<ItemData, int> RawMaterials;
    public int quantity;
}

