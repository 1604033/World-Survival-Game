using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar :  MonoBehaviour {
   public Slider slide;
   public TextMeshProUGUI progressText;

   public void SetProgressBar( float progress, float total,  bool showText = false)
   {
      float value = progress / total;
      float percent = value * 100f;
      int percentageWhole = (int)percent;
      slide.value = value;
      if (showText)
      {
         progressText.text = percentageWhole.ToString()  + "%";
      }
   }
}
