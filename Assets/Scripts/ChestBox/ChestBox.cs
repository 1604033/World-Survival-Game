using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestBox : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject notificationPanel;
    public Player player;
    public Slot_UI slot;
    void Start()
    {
        notificationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //notificationPanel.SetActive(false);
        //activeItemSlot = GameManager.instance.activeSlot;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        //GenerateSeedsFromFruit gen = collision.GetComponent<GenerateSeedsFromFruit>();
        // Set the text to describe the item and the action
        //notificationText.text = "Do you want to consume " + itemDescription + "?";

        // Show the notification panel
        notificationPanel.SetActive(true);
       
    }
   
    public void OnYesButtonClick()
    {
       
               Debug.Log("The Fruit process seed ");
               //ItemData itemData = activeItemSlot.itemData;
               //activeItemSlot.count--;
               //pos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
               //seedPrefab = itemData.seedPrefab;
               //Instantiate(itemData.seedPrefab, player.transform.position, Quaternion.identity);
               Debug.Log("Seed added");
            
        // Implement logic to consume the item
        // You can access your player's inventory system here

        // Hide the notification panel
        notificationPanel.SetActive(false);
    }

    public void OnNoButtonClick()
    {
         //Destroy(this.gameObject);
         Debug.Log("The collectable no ");
        //instantiatedFruit = Instantiate(seedPrefab, gameObject.transform.position, Quaternion.identity);
        //spriteRenderer.sprite = null;
        // Implement logic to add the item to the inventory
        // You can access your player's inventory system here

        // Hide the notification panel
        notificationPanel.SetActive(false);
    }
    public void AddallItem()
    {
        Debug.Log("SubhanAllah");
       for(int i = 0; i<4; i++)
       {
        
       }
    }
}
