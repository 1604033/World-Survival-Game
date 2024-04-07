using UnityEngine;
using UnityEngine.UI;

public class TabButtonUI : MonoBehaviour
{
    public Color normalColor = Color.gray;
    public Color selectedColor = Color.white;
    public Button uiButton ;
    public Image uiImage ;

    public void SetActiveColor(bool isActive)
    {
        uiImage.color = isActive ? selectedColor : normalColor;
        
    }

    
    

   
}
