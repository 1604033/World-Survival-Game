using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BasicPopup : MonoBehaviour
{
    public class Dialog
    {
        public string Title =  "Title";
        public string Message = "You are now view the popup message";

    }

    private Dialog dialog = new Dialog();

    public Action OnCancelOrDemolishClick;
    
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text titleTextUI;
    [SerializeField] private Text messageTextUI;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button cancelButton;
    [SerializeField]private ProgressBar progessBar;
    [FormerlySerializedAs("Instance")] public  BasicPopup @this;

    private void Awake()
    {
        closeButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Hide);
        cancelButton.onClick.AddListener(ShowConfirm);
    }

    public BasicPopup SetTitle(string title)
    {
        Debug.Log("Called the inherit function");
        dialog.Title = title;
        return this;
    } 
    public BasicPopup SetMessage(string message)
    {
        dialog.Message = message;
        return this;
    }
    public BasicPopup SetProgress(float value, float total, bool showProgress = false)
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
        OnCancelOrDemolishClick?.Invoke();
        // ConfirmPopup confirmPopup = PopupManager.Instance.GetPopupConfirm();
        // confirmPopup.SetTitle("Delete alert").SetMessage($"You are about to destroy this house. Are you sure you want to continue?").Show();
    }
}
