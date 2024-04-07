using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Animal : MonoBehaviour
{
    public AnimalType animalType;
    public int amountOfFoodConsumption  = 1;
    public int foodLevel  = 0;
    public Pokemon pokemon;

}
public enum AnimalType
{
    Pokemon,
    Cat,
    Dog,
}
