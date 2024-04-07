using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Crop Data", menuName = "Crop Data")]
public class CropData : ScriptableObject
{
    public string cropName;
    public string description;
    public float growthTime;
    public Sprite[] growthSprites;
    public Sprite harvestSprite;
    public Item item;
    //public Seed seed;
    public GameObject fruitPrefab;
    private GameObject seedPrefab;
}

