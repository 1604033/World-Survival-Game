using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TabsNavigation : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject[] subPanels;
    public TabButtonUI[] subButtons;


    private void Start()
    {
        InventoryManager.OnHideMainInventory += HideMainPanel;
    }
    // private void Start()
    // {
    //     for (int i = 0; i < subButtons.Length; i++)
    //     {
    //         subButtons[i].uiButton.onClick.AddListener(() =>
    //         {
    //             ToggleTab(i);
    //         });
    //     }
    // }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) ToggleTab(0);
        else if (Input.GetKeyDown(KeyCode.B)) ToggleTab(1);
        else if (Input.GetKeyDown(KeyCode.C)) ToggleTab(2);
        else if (Input.GetKeyDown(KeyCode.M)) ToggleTab(3);
        else if (Input.GetKeyDown(KeyCode.J)) ToggleTab(4);
        else if (Input.GetKeyDown(KeyCode.U)) ToggleTab(5);
        else if (Input.GetKeyDown(KeyCode.Escape)) ToggleTab(6);
    }

    void HideMainPanel()
    {
        mainPanel.SetActive(false);
    }
    public void ToggleTab(int tabIndex)
    {
        for (var index = 0; index < subPanels.Length; index++)
        {
            var subPanel = subPanels[index];
           var button= subButtons[index];
            if (index == tabIndex)
            {
                bool state = !subPanel.activeInHierarchy;
                subPanel.SetActive(state);
                mainPanel.SetActive(state);
                button.GetComponent<Image>().color = Color.white;
            }
            else
            {
                subPanel.SetActive(false);
                button.GetComponent<Image>().color = Color.gray;

            }
        }
    }

    
}