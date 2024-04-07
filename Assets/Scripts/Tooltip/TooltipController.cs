using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    TooltipSystem _tooltipSystem;
    [SerializeField] private String tooltipDisplayTitle;
    [SerializeField] private String tooltipDisplayContent;
    [SerializeField]private MethodOfTrigger methodOfTrigger;
    void Start()
    {
        _tooltipSystem = TooltipSystem.Instance;
    }

   public void  OnPointerEnter(PointerEventData eventData)
    {
        if (methodOfTrigger == MethodOfTrigger.HoverTrigger)
        {
            _tooltipSystem.Tooltip.SetText(tooltipDisplayTitle, tooltipDisplayContent, null).Show();
        }  
    }

   public void  OnPointerExit (PointerEventData eventData)
    {
        if (methodOfTrigger == MethodOfTrigger.HoverTrigger)
        {
            _tooltipSystem.Tooltip.Hide();
 
        }  
    } 
   public void  OnPointerClick(PointerEventData eventData)
    {
      
        if (methodOfTrigger == MethodOfTrigger.ClickTrigger)
        {
            _tooltipSystem.Tooltip.SetText(tooltipDisplayTitle, tooltipDisplayContent, null).Show();
        }  
    }
    
    
    
    enum MethodOfTrigger
    {
     ClickTrigger,
     HoverTrigger,
    }
    
}
