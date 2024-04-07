using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData_Display : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemData itemdata;
    public Text nameText;
    public Text descriptionText;
    public Text itemType;
    public Text priceText;
    public Text sellPrice;
    public Text consumable;
    public Image spritelogo;
    /*private void Start()
    {
        //Debug.Log(itemdata.itemName);
    }*/

    /*private void OnMouseEnter() {
      Debug.Log("Description: " + itemdata.description);
      nameText.text = itemdata.itemName;
      descriptionText.text = itemdata.description;
      //itemType.text = itemdata.itemType;
      //consumable.text = itemdata.consumable;
      //priceText.text = itemdata.price.ToString();
      //sellPrice.text = itemdata.sellValue.ToString();
      spritelogo.sprite = itemdata.icon;
      
    }

    private void OnMouseExit() {
        //Debug.Log("Product prize =" + itemdata.price);
        nameText.text = "";
        descriptionText.text = "";
        itemType.text = "";
        consumable.text = "";
        priceText.text = "";
        sellPrice.text = "";
        spritelogo.sprite = null;
    }*/
    private void OnMouseDown() {
        Debug.Log("Right clicked!");
    }

}
