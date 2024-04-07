using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int numPaddy = 0;
    private Vector2 _vector2;
    public InventoryManager inventory;
    float maxBuildingArea = 2f;

    private Vector3 playerPositionDirection;
    //public Inventory inventory;
    //public Inventory toolbar;

    private void Awake() 
    {
        /*inventory = new Inventory(30);
        toolbar = new Inventory(9);*/
        inventory = GetComponent<InventoryManager>();
       
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = Random.insideUnitCircle * 1.01f;

        //float randX = Random.Range(-1f, 1f);
        //float randY = Random.Range(-1f, 1f);
        

        //Vector3 spawnOffset = new Vector3(randX, randY, 0f).normalized; 

        Item droppedItem = Instantiate (item, spawnLocation + spawnOffset, Quaternion.identity);

        droppedItem.rb2d.AddForce(spawnOffset * .2f, ForceMode2D.Impulse);
    }

    public void DropItem(Item item, int numToDrop)
    {
       for(int i = 0; i < numToDrop; i++)
       {
        DropItem(item);
       }
    }

    // private void Update()
    // {
    //     playerPositionDirection = transform.position;
    //     SetPositionToDisplayPreview();
    //     RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _vector2, maxBuildingArea, LayerMask.GetMask("Building"));
    //     if (raycastHit2D.collider != null)
    //     {
    //         if (raycastHit2D.collider.gameObject.TryGetComponent<Building>(out Building building))
    //         {    
    //             building.CanNowBuild(false);
    //             if (Input.GetMouseButtonDown(0) && building.CanConstructBuilding())
    //             {
    //                 building.ConstructBuilding();
    //             }else if (Input.GetMouseButtonDown(1))
    //             {
    //                 building.DestroyBuilding();
    //             }
    //         }
    //     }
    // }
    // void SetPositionToDisplayPreview()
    // {
    //     if (playerPositionDirection.y == 1f)
    //     {
    //         _vector2 = Vector2.up;
    //     }
    //
    //     if (playerPositionDirection.y == -1f)
    //     {
    //         _vector2 = Vector2.down;
    //     }
    //
    //     if (playerPositionDirection.x == -1f)
    //     {
    //         _vector2 = Vector2.left;
    //     }
    //
    //     if (playerPositionDirection.x == 1f)
    //     {
    //         _vector2 = Vector2.right;
    //     }
    // }
}
