using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingProgressBar : MonoBehaviour
{
    public Slider slide;
    
       public void SetProgressBar( float progress, float total )
       {
          float value = progress / total;
          slide.value = value;
         
       }

    // Update is called once per frame
    void Update()
    {
        
    }
}
