using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
   public static GameObject ItemData;
   public ItemData data;
    public Text nameText;
    public Text descriptionText;
    public Text itemType;
    public Text priceText;
    public bool isTrashable;
    //public Text sellPrice;
    //public Text consumable;
    public Image spritelogo;

   [HideInInspector] public Rigidbody2D rb2d; 

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnMouseDown() {
        Debug.Log("Right clicked!");
        nameText.text = data.itemName;
      descriptionText.text = data.description;
      //itemType.text = data.itemtype;
      priceText.text = data.price;
      spritelogo.sprite = data.icon;
    }
}
