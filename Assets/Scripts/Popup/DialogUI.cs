using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public class Dialog
    {
        public string Title =  "Title";
        public string Message = "You are now view the popup message";

    }

    private Dialog dialog = new Dialog();
    
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text titleTextUI;
    [SerializeField] private Text messageTextUI;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button cancelButton;
    [SerializeField]private UnityEngine.UIElements.ProgressBar progessBar;
    [FormerlySerializedAs("Instance")] public  DialogUI @this;

    private void Awake()
    {
        cancelButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Hide);
        cancelButton.onClick.AddListener(ShowConfirm);
    }

    public DialogUI SetTitle(string title)
    {
        dialog.Title = title;
        return this;
    } 
    public DialogUI SetMessage(string message)
    {
        dialog.Message = message;
        return this;
    }
    public DialogUI SetProgress(float value, float total, bool showProgress = false)
    {
        //progessBar.gameObject.SetActive(true);
        //progessBar.SetProgressBar(value, total, showProgress);
        return this;
    }

    public void Show()
    {
        titleTextUI.text = dialog.Title;
        messageTextUI.text = dialog.Message;
       canvas.SetActive(true);
    }
    public void Hide()
    {
        canvas.SetActive(false);
        dialog = new Dialog();
    }public void ShowConfirm()
    {
        //DialogUI dialogUI = PopupManager.Instance.GetPopup(PopupTypes.Confirmation);
        //dialogUI.SetTitle("Delete alert").SetMessage($"You are about to destroy this house. Are you sure you want to continue?").Show();
    }
}
