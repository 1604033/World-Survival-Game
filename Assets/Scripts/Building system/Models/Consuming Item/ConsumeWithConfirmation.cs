using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeWithConfirmation : MonoBehaviour
{
    private CropData _cropData;
    private GameObject instantiatedFruit;
    private SpriteRenderer spriteRenderer;
    public int countToAdd = 1 ;
    public GameObject seedPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        GenerateSeedsFromFruit gen = collision.GetComponent<GenerateSeedsFromFruit>();
        if(player || gen)
        {
            
            Item item = GetComponent<Item>();
            if(item != null)
            {
            //player.inventory.Add("Backpack", item, countToAdd);
            //player.numWood++;
            Destroy(this.gameObject);
            Debug.Log("Proceed a seed ");
            //GameObject seed = Instantiate(seedPrefab, playerTransform.position, Quaternion.identity);
             //GameObject cropPrefab = _cropData.fruitPrefab;
               instantiatedFruit = Instantiate(seedPrefab, gameObject.transform.position, Quaternion.identity);
                spriteRenderer.sprite = null;
                

            }
            
        }
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
