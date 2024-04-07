using System;
using System.Collections;
using System.Linq;
using Subtegral.DialogueSystem.DataContainers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    private Action OnTabPressed;
    [SerializeField] private DialogueContainer dialogue;
    
    [SerializeField] private TextMeshProUGUI dialogueText;
    [FormerlySerializedAs("characterSpriteRenderer")] [SerializeField] private Image characterImage;
    [SerializeField] private Button choicePrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private GameObject dialogueContainer;

    [SerializeField] private float typingSpeed = 0.5f;
    [SerializeField] private float waitTimeForNextDialog = 10f;
    [SerializeField] private float waitTimeToCloseDialogScreen = 10f;
    private string narrativeDataGUID;

    private float waitTime = 5f;
    private bool isTabPressed;
    private bool isTyping;

    public DialogueManager StartDialogue(DialogueContainer dialogue, Pokemon character, string nodeStart = "")
    {
        this.dialogue = dialogue;
        if (nodeStart.Any() )
        {
            if (!isTyping)
            {
                ProceedToNarrative(nodeStart);
                SetCharacterSprite(character.icon);
                isTyping = true;  
            }
            
        }
        else
        {
            var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
            ProceedToNarrative(narrativeData.TargetNodeGUID);
            SetCharacterSprite(character.icon);

        }


        return this;
    }

    public DialogueManager Show()
    {
        dialogueContainer.SetActive(true);
        return this;
    }

    public DialogueManager Hide()
    {
        dialogueContainer.SetActive(false);
        return this;
    }

    private void SetCharacterSprite(Sprite sprite)
    {
        if (characterImage != null) characterImage.sprite = sprite;
    }
    private void Start()
    {
        // var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
        // ProceedToNarrative(narrativeData.TargetNodeGUID);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isTabPressed = true;
            Invoke(nameof(ResetTabClick), typingSpeed);
        }
    }

    private void ResetTabClick()
    {
        isTabPressed = false;
    }

    private void ProceedToNarrative(string narrativeDataGUID)
    {
        this.narrativeDataGUID = narrativeDataGUID;
        var text = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).DialogueText;
        var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
        //dialogueText.text = ProcessProperties(text);
        SetTextOvertime(ProcessProperties(text));
        // var buttons = buttonContainer.GetComponentsInChildren<Button>();
        // for (int i = 0; i < buttons.Length; i++)
        // {
        //     Destroy(buttons[i].gameObject);
        // }
        //
        // {
        //     if (choices.Count() > 1)
        //     {
        //         // foreach (var choice in choices)
        //         // {
        //         //     var button = Instantiate(choicePrefab, buttonContainer);
        //         //     button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
        //         //     button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGUID));
        //         // }
        //     }
        // }
    }

    private string ProcessProperties(string text)
    {
        foreach (var exposedProperty in dialogue.ExposedProperties)
        {
            text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
        }

        return text;
    }

    private void SetTextOvertime(string text)
    {
        StopCoroutine(nameof(TypeDialog));
        StartCoroutine(TypeDialog(text, CheckIfCanLoadNextNode));
    }

    private IEnumerator TypeDialog(string text, UnityAction callback)
    {
        dialogueText.text = "";
        foreach (var character in text.ToCharArray())
        {
            dialogueText.text += character;
            if (isTabPressed)
            {
                dialogueText.text = text;
                
                break;
            }

            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(waitTimeForNextDialog);
        Debug.Log("Callback called!");
        callback?.Invoke();
    }


    private void CheckIfCanLoadNextNode()
    {
        var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID).ToList();
        if (choices.Count() == 1)
        {
            foreach (var choice in choices)
            {
                Debug.Log(choice.TargetNodeGUID);
                ProceedToNarrative(choice.TargetNodeGUID);
            }
        }
        else
        {
            isTyping = false;
            Invoke(nameof(Hide), waitTimeToCloseDialogScreen);
        }
    }
}