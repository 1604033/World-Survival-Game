using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuestSystem;
using Subtegral.DialogueSystem.DataContainers;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class QuestsDialogueLinker : MonoBehaviour
{
    [SerializeField] public QuestElement _questElement;
    [SerializeField] public DialogueInteract _dialogueInteract;
    [SerializeField] public List<Quest> RequiredQuests;
    [SerializeField] public List<Quest> Quests;
    [SerializeField] public List<string> DialogueUUId;
    [SerializeField] public DialogueContainer DialogueContainer;
    public bool isQuestCompleted = false;
    [SerializeField] public QuestManager _questManager;


    private void Start()
    {
        if(_questManager == null)
        _questManager = GameManager.instance.QuestManager;
        _questManager.OnCompleteQuest += OnCompleteQuest;
    }

    private void OnCompleteQuest(Quest quest)
    {
        if (RequiredQuests.Contains(quest))
        {
            if (RequiredQuests.All(q => q.Check(QuestState.Completed)))
            {
                Quests.First().ChangeState(QuestState.Pending);
            }
        }
    }

    public virtual (DialogueContainer, string) UpdateDialogue()
    {
        Quest quest = new Quest();
        string nodeGUid = "";

        if (Quests.Any((q) => q.Check(QuestState.Active) || q.Check(QuestState.Pending) ))
        {
            foreach (var q in Quests)
            {
                if (q.Check(QuestState.Active) || q.Check(QuestState.Pending))
                {
                    quest = q;
                    var index = Quests.IndexOf(quest);
                    nodeGUid = DialogueUUId[index];
                    return (DialogueContainer, nodeGUid);

                }

            }   
        }

      if( Quests.Any((q) => q.Check(QuestState.ReadyToStart)))
          return (null, null);

        var lastQuest = Quests.Last();
        isQuestCompleted = true;
        lastQuest.ChangeState(lastQuest.State);
        return (DialogueContainer, DialogueUUId.Last());
    }
    
    
}