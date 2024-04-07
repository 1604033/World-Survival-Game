using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{ 
    [SerializeField] private ConfirmPopup confirmPopup;
    [SerializeField] private BasicPopup basicPopup;
    [SerializeField] private LoggingCampPopup LoggingPopup;
    [SerializeField] private BuildingInfoPopup buildingInfoPopup;
    [SerializeField] private BuildingPokemonPopup buildingPokemon;
    [SerializeField] private BuildingStockPileManager buildingFoodPilePopup;
    [SerializeField] private PowerPlantPoup powerPlantPopup;
    
    [SerializeField] private List<PopupTypes> popupTypesList;
    [SerializeField] private Building activeBuilding;
    public static PopupManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.I))
        // {
        // //confirmPopup.SetTitle("Popup").Show();
        // GetPopupBasic();
        // }
    }

    // Update is called once per frame
    
    
    public ConfirmPopup GetPopupConfirm()
    {
         return confirmPopup;
    }
    public LoggingCampPopup GetPopupLoggingCamp()
    {
         return LoggingPopup;
    }
    public PowerPlantPoup GetPopupPowerPlantPopup()
    {
         return powerPlantPopup;
    }
    public BuildingStockPileManager GetPopupFoodItemPopup()
    {
         return buildingFoodPilePopup;
    }
    public BasicPopup GetPopupBasic()
    {
         return basicPopup;
    }public BuildingPokemonPopup GetPopupPokemon()
    {
         return buildingPokemon;
    }
    public BuildingInfoPopup GetBuildingInfoPopup()
    {
         return buildingInfoPopup;
    }

    public void HideAllPopups()
    {
        basicPopup.Hide();
        confirmPopup.Hide();
    }
}

public enum PopupTypes
{
    Confirmation,
    Base,  
    InfoPopup,  
}


