using System;
using System.Collections;
using System.Collections.Generic;
using BuildingSystem;
using UnityEngine;

public class BuildingSystemManager : MonoBehaviour
{
    public static BuildingSystemManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public BuildingPlacer BuildingPlacer;
    public ConstructionLayer ConstructionLayer;
    public PreviewLayer PreviewLayer;
    public BuildingSelector BuildingSelector;
    
}
