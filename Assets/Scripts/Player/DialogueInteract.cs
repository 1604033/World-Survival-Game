using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuestSystem;
using Subtegral.DialogueSystem.DataContainers;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueInteract : Interactable
{
    [SerializeField] private QuestsDialogueLinker QuestsDialogueLinker;
    [SerializeField] private Pokemon character;

    private void Start()
    {
    }

    public override void Interact()
    {
        // _quest = GetComponent<QuestElement>().quest;
        // var dialogue1 =  _dialogueContainers[0];
        // if (!_quest.Check(QuestState.Completed))
        // {
        //     GameManager.instance.DialogueManager.StartDialogue(dialogue1).Show();  
        // }
        // else
        // {
        //     bool quest = !_questElement.Quests.Any(_quest => !_quest.Check(QuestState.Completed));
        //     if (quest)
        //     {
        //     }
        // }
        //
        var dialogueWithNode = QuestsDialogueLinker.UpdateDialogue();
        if (dialogueWithNode.Item1 != null && dialogueWithNode.Item2 != null)
            GameManager.instance.DialogueManager
                .StartDialogue(dialogueWithNode.Item1, character, dialogueWithNode.Item2).Show();
    }
}