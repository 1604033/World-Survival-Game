using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AnimalsManager : MonoBehaviour
{
    public static AnimalsManager Instance;
    private BuildingsManager buildingsManager;
    public List<Animal> Animals;
    public List<Pokemon> allPokemons;
    [SerializeField]private GameObject playerCompanion;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        buildingsManager = BuildingsManager.Instance;
    }

    public void AddAnimal(Animal animal)
    {
        if (animal != null)
            if (Animals != null)
                if (!Animals.Contains(animal))
                    Animals.Add(animal);
    }

    public Animal GetAnimal(Animal animal)
    {
        if (animal != null)
            if (Animals != null)
                if (Animals.Contains(animal))
                    return Animals.Find(b => b == animal);
        return null;
    }

    public Animal GetAnimal(AnimalType animalType)
    {
        if (Animals != null)
        {
            return Animals.Find(b => b.animalType == animalType);
        }

        return null;
    }

    [ItemCanBeNull]
    public List<Animal> GetAllAnimals(AnimalType animalType)
    {
        if (Animals != null)
        {
            return Animals.FindAll(b => b.animalType == animalType);
        }

        return null;
    }

    public List<Pokemon> GetAllPokemons()
    {
            List<Pokemon> pokemons = new List<Pokemon>();
            List<Animal> animals = Animals.FindAll(b => b.animalType == AnimalType.Pokemon);
            foreach (var animal in animals)
            {
                pokemons.Add(animal.pokemon);
            }

        return pokemons;
    }


    public void AddPokemon(Pokemon pokemon)
    {
        if (!allPokemons.Contains(pokemon))
        {
            allPokemons.Add(pokemon);
        }
    }

    public void ControlPlayerCompanion(bool isActive)
    {
        playerCompanion.SetActive(isActive);
    }
}