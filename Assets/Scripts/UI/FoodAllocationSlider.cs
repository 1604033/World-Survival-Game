using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FoodAllocationSlider : MonoBehaviour
{
    public static Action<int> OnFoodRatioAllocationChanged;
    [SerializeField] private List<Sprite> moodSprites;
    [SerializeField] private List<Sprite> foodQuantitySprites;
    [SerializeField] private List<String> moodNames;
    
    [SerializeField] private TextMeshProUGUI moodNameDisplay;
    [SerializeField] private Image moodImageDisplay;
    [SerializeField] private Image foodQuantityImageDisplay;
    [SerializeField]private Slider slider;
    private void Awake()
    {
        if (slider != null) slider.onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(float value)
    {
        int sliderValue = Mathf.RoundToInt(value);
        OnFoodRatioAllocationChanged.Invoke(sliderValue);
        SetSliderDisplayInfo(sliderValue);
    }

    private void SetSliderDisplayInfo(int value)
    {
        Sprite sprite = null;
        Sprite foodSprite = null;
        String name =  "";
    switch(value)
        {
            case -2:
                sprite = moodSprites[0];
                foodSprite = foodQuantitySprites[0];
                name = moodNames[0];
                break;
        
            case -1:
                sprite = moodSprites[1]; 
                foodSprite = foodQuantitySprites[1];
                name = moodNames[1];   
                break;

            case 0:
                sprite = moodSprites[2];
                foodSprite = foodQuantitySprites[2];
                name = moodNames[2];
                break; 
        
            case 1:
                sprite = moodSprites[3];
                foodSprite = foodQuantitySprites[3];
                name = moodNames[3];
                break;

            case 2:    
                sprite = moodSprites[4];
                foodSprite = foodQuantitySprites[4];
                name = moodNames[4];
                break;
        }

        if (moodImageDisplay != null)
            if (sprite != null)
                moodImageDisplay.sprite = sprite; 
        if (foodQuantityImageDisplay != null)
            if (foodSprite != null)
                foodQuantityImageDisplay.sprite = foodSprite;
         if(moodNameDisplay != null) moodNameDisplay.text = name;
         
    }
}