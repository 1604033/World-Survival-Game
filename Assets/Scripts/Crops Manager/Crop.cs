using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropData cropData;
    //public CropQuality cropQuality;
    public int growthStage;
    public float lastUpdatedTime;
    public float qualityValue;
    public bool IsWatered;
    public int maxGrowthStage;
    public int HydrationLevel;
    public int maxHydrationLevel;
    public float lastwateredTime;
    public bool Fertilized;
    public bool readyForHarvest;
    public int Fertilizerlevel;
    public Vector3 tileOffset = new Vector3(0.5f,0.5f);
    public Vector3Int tilePosition;
    

    private void Start()
    {
        HydrationLevel = 0;
        maxHydrationLevel = 10;
        IsWatered = false;
        growthStage = -1;
        qualityValue = 1.0f;
        lastUpdatedTime = Time.time;
        maxGrowthStage = cropData.growthSprites.Length - 1;
        Fertilized = false;
        Fertilizerlevel = 0;
    }

   public void SetTilePostion(Vector3Int pos)
    {
        tilePosition = pos;
    }
   
}  