using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.Models;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        public Action<Quest> OnQuestNextQuestActivated;
        public Action<Quest> OnCompleteQuest;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private BuildingsManager buildingsManager;
        
        //Todo Remove later
        [SerializeField] private TreasureContainer treasureContainer;
        
        //Todo Remove later

        public List<Quest> allQuests;
        public List<Quest> activeQuests;
        
        private void Start()
        {
            _inventory =  GameManager.instance.player.inventory.backpack;
            _inventory.OnInventoryUpdate += CheckForCollectQuest;
            treasureContainer.OnAddToTreasure += CheckForTreasureQuest;
            buildingsManager.OnNewBuildingConstructed += CheckForBuildQuest;
            
            
        }

        public void ActivateQuest(Quest quest)
        {
            if(!activeQuests.Contains(quest))
            activeQuests.Add(quest);
            quest.ChangeState(QuestState.Active);
        }
        
        
        private void CheckForCollectQuest(ItemData item, int count) 
        {
            if(activeQuests.Any())
            {
                var collectQuest = GetTQuest<CollectItemQuest>();
                if(collectQuest != null)
                {
                    if(collectQuest.Check(item, count) )
                    {
                        MarkQuestComplete(collectQuest);
                        if (collectQuest.nextQuest != null)
                        {
                            ActivateQuest(collectQuest.nextQuest);
                            OnQuestNextQuestActivated(collectQuest);
                        }
                    }
                }
            }
        }
        private void CheckForTreasureQuest(ItemData item, int count) 
        {
            if(activeQuests.Any())
            {
                var itemQuest = GetTQuest<TreasureBoxQuest>();
                if(itemQuest != null)
                {
                    if(itemQuest.Check(item, count) )
                    {
                        MarkQuestComplete(itemQuest);
                        if (itemQuest.nextQuest != null)
                        {
                            ActivateQuest(itemQuest.nextQuest);
                            OnQuestNextQuestActivated(itemQuest);
                        }
                    }
                }
            }
        }
        private void CheckForBuildQuest(BuildingType buildingType) 
        {
            if(activeQuests.Any())
            {
                var buildQuest = GetTQuest<BuildQuest>();
                if(buildQuest != null)
                {
                    if(buildQuest.Check(buildingType))
                    {
                        MarkQuestComplete(buildQuest); 
                    }
                }
            }
        }

        private T GetTQuest<T>() where T : Quest
        {
            return activeQuests.OfType<T>().FirstOrDefault();
        }


        private void MarkQuestComplete(Quest quest)
        {
            quest.ChangeState(QuestState.Completed);
            OnCompleteQuest?.Invoke(quest);
            activeQuests.Remove(quest);
        }
    }  
}

