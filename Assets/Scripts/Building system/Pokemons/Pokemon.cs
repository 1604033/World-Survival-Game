using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Pokemon")]
public class Pokemon : ScriptableObject
{
    public string name;
   public Sprite icon;
   public List<BeastAbilities> beastAbilites;

   public enum  BeastAbilities
   {
       Water,
       Heat,
       WorkAtDay,
       WorkAtNight,
   }
   public enum PokemonState
   {
       Idle,
       Working,
       
       
   }
   public enum  BeastShift
   {
       Day,
       Night
   }
}
