using System.Collections;
using System.Collections.Generic;
using BuildingSystem.Models;
using UnityEngine;

namespace QuestSystem
{

[CreateAssetMenu(menuName = "Data/Quests/Build Quest")]
    public class BuildQuest : Quest
    {
        [SerializeField]private List<BuildingType> Buildings;
        
        public override bool Check(BuildingType buildingType)
        {
            return Buildings.Contains(buildingType);
        }
    }
}
