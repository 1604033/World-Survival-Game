using System;
using System.Collections;
using System.Collections.Generic;
using BuildingSystem.Models;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public static BuildingsManager Instance;
    private AnimalsManager AnimalsManager;
    public Action<BuildingType> OnNewBuildingConstructed;
    
   [SerializeField] List<BuildingBase>  buildings;
   [SerializeField] List<BuildingBase>  buildingsUnderConstruction;
   bool isThereAbuildingUnderConstruction = false;

   private void Awake()
   {
       Instance = this;
   }

   private void Start()
   {
       AnimalsManager = AnimalsManager.Instance;
   }

   public BuildingBase  GetBuilding(BuildingBase building)
   {
      BuildingBase buildingBase = buildings.Find(b => b == building);
       return buildingBase;
   }
   public  BuildingBase  GetBuilding(BuildingType buildingType)
    {
        if (buildings != null)
        {
            BuildingBase buildingBase = buildings.Find(b => b.buildableItem.Type == buildingType);
            return buildingBase;
        }
        return null;

    }
   public void  DeleteBuilding(BuildingBase building)
   {
       buildings.Remove(building);
   }
   public void  RemoveCompleteBuilding(BuildingBase building)
   {
       if(buildingsUnderConstruction.Contains(building))
        buildingsUnderConstruction.Remove(building);
       AnimalsManager.ControlPlayerCompanion(IsThereABuildingsUnderConstruction());

   }
  public void  AddBuilding(BuildingBase building, BuildingType buildingType)
    {
        buildings.Add(building);
        OnNewBuildingConstructed?.Invoke(buildingType);
    }
  public void  AddBuildingUnderConstruction(BuildingBase building)
    {
        if (!buildingsUnderConstruction.Contains(building))
        {
            buildingsUnderConstruction.Add(building);
            AnimalsManager.ControlPlayerCompanion(IsThereABuildingsUnderConstruction());
        }

    }

    public bool IsThereABuildingsUnderConstruction()
    {
        return !(buildingsUnderConstruction.Count > 0);
    }
  
}
