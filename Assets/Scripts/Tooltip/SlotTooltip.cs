using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotTooltip : MonoBehaviour
{
    public Text itemNameText;

    public void ShowTooltip(string itemName)
    {
        gameObject.SetActive(true);
        itemNameText.text = itemName;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
