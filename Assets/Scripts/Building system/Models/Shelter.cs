using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shelter : BuildingBase
{
    public List<Pokemon> assignedPokemons = new List<Pokemon>();
    public List<Animal> animals;

    public override void Start()
    {
        base.Start();
        assignedPokemons = new();
        foreach (var animal in animals)
        {
            assignedPokemons.Add(animal.pokemon);
            // Debug.Log("The current animal is " + animal);
        }
    }
    public Animal GetAnimal(Animal animalP)
    {
        var animal = animals.Find(x => x == animalP);
        return animal;
    }
    public Animal GetAnimal(AnimalType type)
    {
        var animal = animals.Find(x => x.animalType == type);
        return animal;
    }
    
}
