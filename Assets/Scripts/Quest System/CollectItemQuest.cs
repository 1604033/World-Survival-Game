using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Data/Quests/Collect Quest")]

    public class CollectItemQuest : Quest
    {
        public List<ItemData> Items;
        public List<int> Count;

        public override bool Check(ItemData item, int count)
        {
            {
                var itemIndex = Items.IndexOf(item);
      
                if(itemIndex != -1 && Count[itemIndex] >= count)
                {
                    return true;
                }
            }
            return false;
        }
    }

   
}

