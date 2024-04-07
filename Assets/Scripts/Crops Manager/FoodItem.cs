using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Food Item")]
public class FoodItem : ScriptableObject
{
    public ItemData data;
    public string Name;
    public Sprite Icon;
}
