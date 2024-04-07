using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Item))]
public class NotificationAlert : MonoBehaviour
{
    public GameObject notificationPanel;
    public Text notificationText;
    public CropData _cropData;
    public Player player;
    public GameObject instantiatedFruit;
    public SpriteRenderer spriteRenderer;
    public int countToAdd = 1 ;
    public GameObject seedPrefab;

    public void Start()
    {
        // Hide the notification panel at the start
        notificationPanel.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        GenerateSeedsFromFruit gen = collision.GetComponent<GenerateSeedsFromFruit>();
        // Set the text to describe the item and the action
        //notificationText.text = "Do you want to consume " + itemDescription + "?";

        // Show the notification panel
        notificationPanel.SetActive(true);
       
    }
   
    public void OnYesButtonClick()
    {
        //Player player = collision.GetComponent<Player>();
        //GenerateSeedsFromFruit gen = collision.GetComponent<GenerateSeedsFromFruit>();
         //if(player || gen)
        {
            
            Item item = GetComponent<Item>();
            if(item != null)
            {
            player.inventory.Add("Backpack", item, countToAdd);
            //player.numWood++;
            Destroy(this.gameObject);
            Debug.Log("The collectable yes ");
            //GameObject seed = Instantiate(seedPrefab, playerTransform.position, Quaternion.identity);
             //GameObject cropPrefab = _cropData.fruitPrefab;
            }
            
        }
        // Implement logic to consume the item
        // You can access your player's inventory system here

        // Hide the notification panel
        notificationPanel.SetActive(false);
    }

    public void OnNoButtonClick()
    {
         Destroy(this.gameObject);
         Debug.Log("The collectable no ");
        instantiatedFruit = Instantiate(seedPrefab, gameObject.transform.position, Quaternion.identity);
        //spriteRenderer.sprite = null;
        // Implement logic to add the item to the inventory
        // You can access your player's inventory system here

        // Hide the notification panel
        notificationPanel.SetActive(false);
    }
}
