using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConsumption : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject seedPrefab;

    // Reference to the player's inventory manager (you need to implement this)
    private InventoryManager inventoryManager;
    public Inventory.Slot slot;

    private void Start()
    {
        // Get a reference to the inventory manager
        inventoryManager = FindObjectOfType<InventoryManager>();
        
    }
    private void Update()
    {
        ConsumeItem();
    }
    public void ConsumeItem()
    {
        // Check if the inventory has the item
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Remove the consumed item from the inventory
            slot.RemoveItem();

            // Instantiate a seed as a byproduct
            Instantiate(seedPrefab, transform.position, Quaternion.identity);
            Debug.Log("It's working");

            // Optionally, play a sound or trigger an animation here
        }
        else
        {
            // Handle the case when the player doesn't have the item
            Debug.Log("You don't have the required item to consume.");
        }
    }
}
