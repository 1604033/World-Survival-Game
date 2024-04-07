using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopUp : MonoBehaviour
{
    public ItemData itemdata;
    public Text nameText;
    public Text descriptionText;
    public Text price;
    public Text type;
    public string popupMessage;
    public GameObject popupBox;
    
    private bool isHovering;

    private void Start()
    {
        if (popupBox != null) popupBox.SetActive(false);
    }

    /*public void SetUp(string name, string description)
    {
        nameText.text = name;
        descriptionText.text = description;
    }*/

    private void OnMouseEnter()
    {
        isHovering = true;
        ShowPopup();
    }

    private void OnMouseExit()
    {
        isHovering = false;
        HidePopup();
    }

    private void ShowPopup()
    {
        if (isHovering)
        {
            popupBox.SetActive(true);
            nameText.text = itemdata.itemName;
            Debug.Log(nameText);
            //type.text = itemdata.itemtype;
            price.text = itemdata.price;
            // Display the popup message in the popup box
            // You can use UI Text component to show the message
            // For example:
            // popupBox.GetComponentInChildren<Text>().text = popupMessage;
        }
    }

    private void HidePopup()
    {
        popupBox.SetActive(false);
    }

    /*void OnMouseOver() 
    {
        nameText.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);
        //nameText.itemdata.SetActive(true);
        //descriptionText.itemdata.SetActive(true);
    }

    void OnMouseExit()
    {
        nameText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
        //nameText.itemdata.SetActive(false);
        //descriptionText.itemdata.SetActive(false);
    }*/
}
