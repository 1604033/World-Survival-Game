using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public Tooltip Tooltip;

    public static TooltipSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UI_Manager.OnInventoryToggle += OnInventoryToggle;
    }

    private void OnInventoryToggle()
    {
        Tooltip.Hide();
    }
}
