using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    private Trashbin trashBin;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite stackSprite;
    public int count = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        GenerateSeedsFromFruit gen = collision.GetComponent<GenerateSeedsFromFruit>();
        if (player || gen)
        {
            Item item = GetComponent<Item>();
            if (item != null)
            {
                player.inventory.Add("Backpack", item, count);
                Destroy(gameObject);
            }
        }
    }

    public void IncrementCount(int toAdd, int maxAllowed)
    {
        int sum = count + toAdd;
        int incrementedCount = Math.Min(sum, maxAllowed);
        count = incrementedCount;
        //Change to stack
        if (spriteRenderer != null) spriteRenderer.sprite = stackSprite;
    }
    /*private void Start()
    {
        // Find the TrashBin GameObject in the scene by its tag (you can set the tag in the Unity Inspector)
        trashBin = GameObject.FindGameObjectWithTag("Trashbin").GetComponent<Trashbin>();
    }*/

    // Function to delete the item when dropped into the trash bin
    public void DeleteItem()
    {
        //trashBin.AddToTrash(gameObject);
        gameObject.SetActive(false);
    }

    private void OnRightClick()
    {
        // Show the description in a dialog box.
        Debug.Log("Right clicked!");
        //UnityEngine.UI.DialogBox.Show(Description);
    }
}