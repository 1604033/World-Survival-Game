using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FoodItemDisplay : MonoBehaviour
{
    public static Action OnAddButtonClick;
    //Data
    public FoodItem FoodItem;
    public ItemData Data;
    public int Count;
    public Sprite Icon;
    public string Name;
    public int FoodItemId;
    
    
    //Display
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI countText;
     [SerializeField] private Image displayImage ;
     [SerializeField] private Button buttonAdd;
     public bool isFoodItemDisplayOccupied = false;
    
     private void Awake()
     {
         if (buttonAdd != null)
         {
             buttonAdd.onClick.RemoveAllListeners();
             buttonAdd.onClick.AddListener(AddButtonClick);
         }
     }

     private void AddButtonClick()
     {
        OnAddButtonClick?.Invoke();
     }
    public void SetDisplay(FoodItem foodItem, string displayName, Sprite image, int Count,int id)
    {
        if (name != null) name.text = displayName;
         Name = displayName;
        if (countText != null) countText.text = Count.ToString();
        if (displayImage != null) displayImage.sprite = image;
        SetData(foodItem, displayName, image, Count, id);
        isFoodItemDisplayOccupied = true;
    }
    public void SetEmpty()
    {
        if (name != null) name.text = "";
        if (countText != null) countText.text = "";
        if (displayImage != null) displayImage.sprite = null;

        //Data
          Data = null;
          Count = 0;
          Icon = null;
          Name = null;
          FoodItemId = -1;
          isFoodItemDisplayOccupied = false;
    }

    void SetData(FoodItem foodItem, string displayName, Sprite image, int Count, int id)
    {
        if (foodItem != null)
        {
            FoodItem = foodItem;
            Data = foodItem.data;
            FoodItemId = id;
        }
        this.Count = Count;
        if (image != null) Icon = image;

    }
    
    public void SetButtonActive(bool isActive)
    {
        // if (buttonAdd != null) buttonAdd.gameObject.SetActive(isActive);
        // if (displayImage != null) displayImage.gameObject.SetActive(!isActive);
    }
}
