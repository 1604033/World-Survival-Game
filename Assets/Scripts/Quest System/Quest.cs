using System;
using System.Collections.Generic;
using BuildingSystem.Models;
using UnityEngine;

namespace QuestSystem
{
    public  class Quest : ScriptableObject
    {
        public Action<QuestState> OnstateChange;
        public string UniqueId;
        public string Name;
        public QuestState State;
        public QuestState DefaultState;
        public Quest nextQuest; 
        //To add to a new class [Requirements]
        public Quest previousQuest;
        public ThreeDimensionalItem QuestDialogueUUIds;
        
        
        
        public void ChangeState(QuestState state)
        {
            State = state;
            OnstateChange?.Invoke(state);
        }

        public virtual bool Check(QuestState state)
        {
          return State == state;  
        }public virtual bool Check(ItemData itemData, int count)
        {
          return  false;
        }
        public virtual bool Check(BuildingType building)
        {
            return false;
        }

        
    }
}

