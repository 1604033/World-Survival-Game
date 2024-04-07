using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BuildingInteraction : MonoBehaviour
{
    [SerializeField] private BuildingBase _building;
    [SerializeField] private FoodStockPile _foodStockPile;
    [SerializeField] private Shelter _shelter;
    [SerializeField] private LoggingCamp _loggingCamp;
    [SerializeField] private PowerPlant _powerPlant;

    [SerializeField] private float minimumDistanceToInteract = 5f;

    private float buildingConstructionPercent;

    private ConfirmPopup ConfirmPopup;
    private BasicPopup basicPopup;
    private Sprite highlightSprite;

    private void Start()
    {
        ConfirmPopup = PopupManager.Instance.GetPopupConfirm();
        basicPopup = PopupManager.Instance.GetPopupBasic();

        ConfirmPopup.OnConfirmClick = OnClickYes;
        basicPopup.OnCancelOrDemolishClick = OnDemolishClick;
    }


    private void ShowBasicPopup()
    {
        float buildingConstructionPercent = _building.constructionLevel;
        Debug.Log(buildingConstructionPercent);
        BasicPopup basicPopup = PopupManager.Instance.GetPopupBasic();
        basicPopup
            .SetTitle($"Construction Progress")
            .SetMessage("BuildingBase construction is going on as scheduled")
            .SetProgress(_building.constructionLevel, _building.maxconstructionLevel, true)
            .Show();
    }

    private void ShowLoggingCampPopup()
    {
        LoggingCampPopup loggingCampPopup = PopupManager.Instance.GetPopupLoggingCamp();
        loggingCampPopup
            .SetTitleAndDescription("Logging camp", "Generates logs from timber over a period of time")
            .SetInputAndOutputAndLoggingCamp($"This building has a stock of {_loggingCamp.inputStockCount}", $"This building outputs is {_loggingCamp.outputCount} woods over a period of {_loggingCamp.timeForProduction} seconds", _loggingCamp)
            .SetPokemonsDisplay(_loggingCamp.assignedPokemons)
            .Show();
        Debug.Log("Logging camp created");
    }
private void ShowPowerPlantPopup()
    {
        PowerPlantPoup powerPlantPoup = PopupManager.Instance.GetPopupPowerPlantPopup();
        powerPlantPoup
            .SetTitleAndDescription("Power Plant", "Generates power from Beasts energy over a period of time")
            .SetPokemonsDisplay(_powerPlant.assignedPokemons, _powerPlant.assignedPokemonsNight)
            .Show();
    }

    private void BuildingShowPokemonPopup()
    {
        BuildingPokemonPopup buildingPokemon = PopupManager.Instance.GetPopupPokemon();
        buildingPokemon.SetPokemonsDisplay(_shelter.assignedPokemons).SetTitle(_shelter.name)
            .SetMessage(_shelter.buildableItem.Description).Show();
    }

    private void BuildingShowFoodItems()
    {
        BuildingStockPileManager buildingStockPile = PopupManager.Instance.GetPopupFoodItemPopup();
        buildingStockPile.SetFoodItems(_foodStockPile.stockedFoodItems, _foodStockPile.stockedFoodItemsCount)
            .SetTitle(_foodStockPile.buildableItem.Name).SetMessage(_foodStockPile.buildableItem.Description).Show();
        buildingStockPile.currentBuilding = _foodStockPile;
    }

    private void OnClickYes()
    {
        _building.DestroyBuilding();
    }

    private void OnDemolishClick()
    {
        if (_building.constructionLevel < 100f)
        {
            ConfirmPopup.SetTitle("Cancel alert")
                .SetMessage(
                    $"You are about to cancel this BuildingBase construction. Are you sure you want to continue?")
                .Show();
        }
        else
        {
            ConfirmPopup.SetTitle("Demolish alert")
                .SetMessage($"You are about to demolish this BuildingBase. Are you sure you want to continue?").Show();
        }
    }

    public void OnMouseDown()
    {

        if(Vector3.Distance(GameManager.instance.player.transform.position, transform.position)> minimumDistanceToInteract) 
            return;

        PlayerLookAtSign();

        if (!_building.IsFullyCompleted)
        {
            ShowBasicPopup();
            return;
        }

        if (_foodStockPile != null)
        {
            BuildingShowFoodItems();
            return;
        }

        if (_shelter != null)
        {
            BuildingShowPokemonPopup();
            return;
        }

        if (_loggingCamp != null)
        {
            ShowLoggingCampPopup();
        }
        if (_powerPlant != null)
        {
            ShowPowerPlantPopup();
        }
    }

    private void PlayerLookAtSign()
    {
        Debug.Log("PlayerLookAtSign called");
        Transform playerTransform = GameManager.instance.player.transform;
        float angle = Mathf.Atan2(transform.position.y - playerTransform.position.y, transform.position.x - playerTransform.position.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 90) * 90;
        playerTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}