using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace QuestSystem
{
    public class QuestIcon : MonoBehaviour
    {
        [SerializeField] private List<Quest> _quests;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<Sprite> questIcons;

        [SerializeField]
        private QuestsDialogueLinker questsDialogueLinker;

        private void Start()
        {
            _quests = questsDialogueLinker.Quests;
            _quests.ForEach((quest) =>
            {
                quest.OnstateChange += OnQuestStateChange;
                OnQuestStateChange(quest.State);
            });
        }

        private void OnQuestStateChange(QuestState state)
        {
            bool areAllQuestsCompleted = _quests.All(quest => quest.Check(QuestState.Completed));
            bool isAnyActive = _quests.Any(quest => quest.Check(QuestState.Active));
            bool isAnyPending = _quests.Any(quest => quest.Check(QuestState.Pending));
            if (areAllQuestsCompleted && !questsDialogueLinker.isQuestCompleted)
            {
                spriteRenderer.sprite = questIcons[0];

                return;
            }

            if (isAnyActive || questsDialogueLinker.isQuestCompleted)
            {
                spriteRenderer.sprite = null;
                return;
            }

            if (isAnyPending)
            {
                spriteRenderer.sprite = questIcons[0];
            }
        }
    }
}