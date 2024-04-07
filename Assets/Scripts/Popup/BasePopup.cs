using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    public BasePopup SetTitle(string title)
    {
        Debug.Log("Called the base function");
        return this;
    } 
    protected BasePopup SetMessage(string message)
    {
       
        return this;
    }
    protected BasePopup SetProgress(float value, float total, bool showProgress = false)
    {
        return this;
    }

    protected void Show()
    {
        
    }
    protected void Hide()
    {
       
    }
    protected void ShowConfirm()
    {
    }
    
}
