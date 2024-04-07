using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace QuestSystem
{
    public class QuestElement : Interactable
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] public Quest quest;
        [SerializeField] public List<Quest> Quests;


        private void Awake()
        {
            foreach (var quest1 in Quests)
            {
                quest1.ChangeState(quest1.DefaultState);
            }
        }

        private void Start()
        {
            if (questManager == null)
                questManager = GameManager.instance.QuestManager;

            questManager.OnQuestNextQuestActivated += UpdateQuest;
        }

        private void UpdateQuest(Quest quest)
        {
            if (this.quest == quest)
            {

             
              if ( quest.nextQuest!= null)
              {
                  this.quest = this.quest.nextQuest;
              }
            }
        }

        public override void Interact()
        {
            if (quest.Check(QuestState.Pending))
                questManager.ActivateQuest(quest);
        }
    }
}