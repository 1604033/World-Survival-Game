using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopup : MonoBehaviour
{
    public class Dialog
    {
        public string Title =  "Title";
        public string Message = "You are now view the popup message";

    }

    public Action OnConfirmClick;
    private Dialog dialog = new Dialog();
    
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text titleTextUI;
    [SerializeField] private Text messageTextUI;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button cancelButton;
    [SerializeField]private ProgressBar progessBar;

   

    private void Awake()
    {
        cancelButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Hide);
        cancelButton.onClick.AddListener(ShowConfirm);
    }

    public ConfirmPopup SetTitle(string title)
    {
        dialog.Title = title;
        return this;
    } 
    public ConfirmPopup SetMessage(string message)
    {
        dialog.Message = message;
        return this;
    }
    public ConfirmPopup SetProgress(float value, float total, bool showProgress = false)
    {
        progessBar.gameObject.SetActive(true);
        progessBar.SetProgressBar(value, total, showProgress);
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
    }
    public void ShowConfirm()
    {
        Hide();
        OnConfirmClick?.Invoke();
        //var basicPopup = PopupManager.Instance.GetPopup(PopupTypes.Confirmation);
        //this.SetTitle("Delete alert").SetMessage($"You are about to demolish this building. Are you sure you want to continue?").Show();
    }
}
