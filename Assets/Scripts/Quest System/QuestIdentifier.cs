using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    [System.Serializable]
    public class ThreeDimensionalItem
    {
        public Quest Quest;
        public List<string> Coordinates;

    }  

public class QuestIdentifier : MonoBehaviour
{
   [SerializeField] private ThreeDimensionalItem ThreeDimensionalItem;
}
}