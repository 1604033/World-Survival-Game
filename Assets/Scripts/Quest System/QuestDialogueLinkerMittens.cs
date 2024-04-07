using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;

namespace QuestSystem
{
    
public class QuestDialogueLinkerMittens : QuestsDialogueLinker
{
    private bool isFirstInteract;
   public override (DialogueContainer, string) UpdateDialogue()
    {
        
        Quest quest = new Quest();
        string nodeGUid = "";

        // if (Quests.Any((q) => q.Check(QuestState.Active) ||q.Check(QuestState.Pending) ))
        // {
        //     foreach (var q in Quests)
        //     {
        //         if (q.Check(QuestState.Active))
        //         {
        //             quest = q;
        //             var index = Quests.IndexOf(quest);
        //             nodeGUid = DialogueUUId[index];
        //             return (DialogueContainer, nodeGUid);
        //
        //         }
        //
        //     }   
        // }
        //
        // if( Quests.Any((q) => q.Check(QuestState.ReadyToStart)))
        //     return (null, null);
        var questL = Quests[0];
        if (questL.Check(QuestState.Completed) )
        {
            nodeGUid = DialogueUUId[2];
            questL.ChangeState(questL.State);
            isQuestCompleted = true;
            return (DialogueContainer, nodeGUid);  
        }
         if(questL.Check(QuestState.Active) && isFirstInteract )
        {
            
            nodeGUid = DialogueUUId[1];
            questL.ChangeState(questL.State);
            
            return (DialogueContainer, nodeGUid);
        }
         if(questL.Check(QuestState.Active) ||questL.Check(QuestState.Pending) )
        {
            isFirstInteract = true;
            nodeGUid = DialogueUUId[0];
            questL.ChangeState(questL.State);
            return (DialogueContainer, nodeGUid);
        }
          

        var lastQuest = Quests.Last();
        isQuestCompleted = true;
        lastQuest.ChangeState(lastQuest.State);
        return (DialogueContainer, DialogueUUId.Last());    
    }
}
}