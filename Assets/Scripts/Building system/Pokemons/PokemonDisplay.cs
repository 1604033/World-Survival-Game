using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Image pokemonImage ;
    public Pokemon pokemon;
    

    public void SetDisplay(string displayName, Sprite image, Pokemon pokemon)
    {
        Debug.Log($"The display name is {displayName}");
        this.pokemon = pokemon;
        name.text = displayName;
        pokemonImage.sprite = image;
    }

    

}
