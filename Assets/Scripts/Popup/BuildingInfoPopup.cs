using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuildingInfoPopup : MonoBehaviour
{
    public class Dialog
    {
        public string BuildingTitle =  "Title";
        public string Description = "You are now view the popup message";
        public List<string> BuildingMaterials = new List<string>();
    }

    private Dialog dialog = new Dialog();

    
    [SerializeField] private GameObject canvas;
    [FormerlySerializedAs("titleTextUI")] [SerializeField] private Text buildingNameTextUI;
    [FormerlySerializedAs("messageTextUI")] [SerializeField] private Text descriptionTextUI; 
    [SerializeField] private Text quantityTextUI;

    

    public BuildingInfoPopup SetTitle(string title)
    {
        dialog.BuildingTitle = title;
        return this;
    } 
    public BuildingInfoPopup SetDescription(string message)
    {
        dialog.Description = message;
        return this;
    } 
    public BuildingInfoPopup SetMaterials(List<string> materials,List<string> quantities)
    {

        for (int i = 0; i < materials.Count; i++)
        {
         dialog.BuildingMaterials.Add($"{materials[i] + " " + quantities[i] + " x" }");            
        }
        return this;
    }
    

    public void Show(Vector3 mousePosition  )
    {
        Debug.Log("The position is " + mousePosition);
        transform.position = mousePosition;
        buildingNameTextUI.text = dialog.BuildingTitle;
        descriptionTextUI.text = dialog.Description;
        quantityTextUI.text = dialog.BuildingMaterials[0];
        
        canvas.SetActive(true);
    }
    
    public void HidePoup()
    {
        canvas.SetActive(false);
        dialog = new Dialog();
    }
    
}
