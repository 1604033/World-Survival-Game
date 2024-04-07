using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PowerPlant : BuildingBase
{
    public List<Pokemon> assignedPokemons;
    public List<Pokemon> assignedPokemonsNight;
    public List<GameObject> AssignedPokemonsGameObjects;
    private DayTimeController.WorkingShifts currentShift;
    public PowerPlantState powerPlantState = PowerPlantState.Idle;

    private StockPile _stockPile;
    public int timeForProduction = 3;
    private int maxAllowedBeast = 2;

    public int woodInputStock = 1;
    private int woodConsumptionPerHour = 1;

    [SerializeField] private ItemData woodItem;

    private float heatInput;
    private float waterInput;
    public float powerCapacity;
    public float powerOutputPerHour = 1;
    public float powerOutputModifier = 1;

    void Start()
    {
        base.Start();
        _stockPile = StockPile.Instance;
        DayTimeController.Instance.HourChanged += GeneratePower;
        DayTimeController.Instance.WorkingShiftChanged += ModifyOutputMultiplier;
        woodInputStock = _stockPile.GetItemCount(woodItem);
    }


    private void Update()
    {
        woodInputStock = _stockPile.GetItemCount(woodItem);
        // if (powerPlantState == PowerPlantState.Running)
        // {
        //     if (!StockPile.Instance.CanDecrementCount(woodItem, woodConsumptionPerHour) || !CheckBeastsAssigned())
        //     {
        //         powerPlantState = PowerPlantState.Idle;
        //     }
        // }
        // else if (powerPlantState == PowerPlantState.Idle)
        // {
        //     if (StockPile.Instance.CanDecrementCount(woodItem, woodConsumptionPerHour)
        //         && CheckBeastsAssigned())
        //     {
        //         powerPlantState = PowerPlantState.Running;
        //     }
        // }
    }

    public void AssignPokemon(Pokemon pokemon, List<Pokemon.BeastAbilities> beastAbilites)
    {
        if (beastAbilites.Contains(Pokemon.BeastAbilities.WorkAtDay))
        {
            if (beastAbilites.Contains(Pokemon.BeastAbilities.Heat)
                || beastAbilites.Contains(Pokemon.BeastAbilities.Heat)
               )
            {
                if (!assignedPokemons.Contains(pokemon))
                    assignedPokemons.Add(pokemon);
            }

            ModifyOutputMultiplier(DayTimeController.WorkingShifts.Day);
        }
        else
        {
            if (beastAbilites.Contains(Pokemon.BeastAbilities.WorkAtNight))
            {
                if (beastAbilites.Contains(Pokemon.BeastAbilities.Heat)
                    || beastAbilites.Contains(Pokemon.BeastAbilities.Water)
                   )
                {
                    if (!assignedPokemonsNight.Contains(pokemon))
                        assignedPokemonsNight.Add(pokemon);
                }
            }

            ModifyOutputMultiplier(DayTimeController.WorkingShifts.Night);
        }
    }

    void ModifyOutputMultiplier(object sender, DayTimeController.WorkingShifts workingShift)
    {
        currentShift = workingShift;
        ModifyOutputMultiplier(workingShift);
        SetThePowerPlantState();
    }

    void ModifyOutputMultiplier(DayTimeController.WorkingShifts workingShift)
    {
        if (workingShift == DayTimeController.WorkingShifts.Day)
        {
            int waterPokemons = assignedPokemons.Count(p => p.beastAbilites.Contains(Pokemon.BeastAbilities.Water));
            int firePokemons = assignedPokemons.Count(p => p.beastAbilites.Contains(Pokemon.BeastAbilities.Heat));

            if (waterPokemons == 1 && firePokemons == 1)
            {
                powerOutputModifier = 1;
            }
        }

        if (workingShift == DayTimeController.WorkingShifts.Night)
        {
            int waterPokemons =
                assignedPokemonsNight.Count(p => p.beastAbilites.Contains(Pokemon.BeastAbilities.Water));
            int firePokemons = assignedPokemonsNight.Count(p => p.beastAbilites.Contains(Pokemon.BeastAbilities.Heat));

            if (waterPokemons == 1 && firePokemons == 1)
            {
                powerOutputModifier = 1;
            }
        }
    }

    bool CheckBeastsAssigned()
    {
        if (currentShift == DayTimeController.WorkingShifts.Day)
        {
            if (assignedPokemons.Count < 2)
            {
                return false;
            }

            int waterPokemons = assignedPokemons.Count(p => p.beastAbilites.Contains(Pokemon.BeastAbilities.Water));
            int firePokemons = assignedPokemons.Count(p => p.beastAbilites.Contains(Pokemon.BeastAbilities.Heat));
            return firePokemons > 0 && waterPokemons > 0;
        }

        if (currentShift == DayTimeController.WorkingShifts.Night)
        {
            if (assignedPokemonsNight.Count < 2)
            {
                return false;
            }

            int waterPokemons =
                assignedPokemonsNight.Count(p => p.beastAbilites.Contains(Pokemon.BeastAbilities.Water));
            int firePokemons = assignedPokemonsNight.Count(p => p.beastAbilites.Contains(Pokemon.BeastAbilities.Heat));
            return firePokemons > 0 && waterPokemons > 0;
        }

        return true;
    }

    private void GeneratePower(object sender, int e)
    {
        if (powerPlantState == PowerPlantState.Running)
        {
            float powerGenerated = powerOutputPerHour * powerOutputModifier;
            _stockPile.DecrementCount(woodItem, woodConsumptionPerHour);
            powerCapacity += powerGenerated;
        }

        SetThePowerPlantState();
    }

    void SetThePowerPlantState()
    {
        if (!StockPile.Instance.CanDecrementCount(woodItem, woodConsumptionPerHour) || !CheckBeastsAssigned())
        {
            powerPlantState = PowerPlantState.Idle;
        }
        else
        {
            powerPlantState = PowerPlantState.Running;
        }
    }

    public enum PowerPlantState
    {
        Running,
        Idle
    }
}