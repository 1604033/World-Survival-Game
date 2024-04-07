using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeA : MonoBehaviour
{
    public static ConsumeA instance;
    [SerializeField] GameObject player;
    private Inventory.Slot activeItemSlot;
     GameObject cropPrefab;
     GameObject seedPrefab;
     public GameObject monosterPrefab;
     public GameObject smilefacePrefab;
     public float Lifetime = 3.0f;
    private Vector3 playerPosition;
    private Vector3Int playerMouseGridPos;
    private Inventory inventory;
    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        } 
        playerPosition = player.transform.position;  
        playerMouseGridPos = Vector3Int.RoundToInt(Input.mousePosition);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activeItemSlot = GameManager.instance.activeSlot;
        //InterractWithTile(MousePosToGridPos());
        ConsumeFruit();
    }
    public void ConsumeFruit()
    {
       if(Input.GetKeyDown(KeyCode.L))
        {
             if (activeItemSlot.count == 0)
            {
                Debug.Log("There are no any Fruit in activeslot");
            }
            else
            {
               monosterPrefab = Instantiate(monosterPrefab, player.transform.position, Quaternion.identity);
               smilefacePrefab = Instantiate(smilefacePrefab, player.transform.position, Quaternion.identity);
               StartCoroutine(DestroyAfterTime());
                StartCoroutine(DestroyAfterTime2());
               //yield return new WaitForSeconds(Lifetime);
               //Destroy(monosterPrefab);
               //monosterPrefab.gameObject.active = true ;
               //yield WaitForSeconds ();
               //monosterPrefab.gameObject.active = false ;
               Debug.Log("The Fruit process seed ");
               ItemData itemData = activeItemSlot.itemData;
               activeItemSlot.count--;
               //pos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
               //seedPrefab = itemData.seedPrefab;
               Instantiate(itemData.seedPrefab, player.transform.position, Quaternion.identity);
               Debug.Log("Seed added");
            }
             //Destroy(this.gameObject);
        //spriteRenderer.sprite = null;
        // Implement logic to add the item to the inventory
        // You can access your player's inventory system here

        // Hide the notification panel
        //notificationPanel.SetActive(false);
        }
    }
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(Lifetime);
        
        // Destroy the GameObject after the specified time
        Destroy(monosterPrefab);
        //Destroy(smilefacePrefab);
        //DestroyAfterTime(smilefacePrefab);
    }
    IEnumerator DestroyAfterTime2()
    {
        yield return new WaitForSeconds(4);
        
        // Destroy the GameObject after the specified time
        //Destroy(monosterPrefab);
        Destroy(smilefacePrefab);
        //DestroyAfterTime(smilefacePrefab);
    }
}
