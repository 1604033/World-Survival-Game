using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightTile : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase highlightTile;
    public Tilemap previousTileCoordinate;
    public Tilemap highlightMap;


    private void Update()
    {
        /*Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(cursorWorldPosition);

        Vector3Int playerCellPosition = tilemap.WorldToCell(transform.position);

        if (cellPosition != playerCellPosition)
        {
            tilemap.SetTile(playerCellPosition, null); // Clear previous highlight
            tilemap.SetTile(cellPosition, highlightTile); // Highlight new cell
        }
        else
        {
            tilemap.SetTile(playerCellPosition, highlightTile); // Highlight player's cell
        }*/
        
        //set the previously highlighted tile back to null
     /*Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
     Vector3Int tileCoordinate = highlightMap.WorldToCell(mouseWorldPos);
 
 	if(tileCoordinate != previousTileCoordinate ){
         highlightMap.SetTile(previousTileCoordinate, null);
 	    highlightMap.SetTile(tileCoordinate,highlightTile);
         previousTileCoordinate = tileCoordinate; 	
     }
        
        if(previousTileCoordinate != null)
        {
            highlightMap.SetTile(previousTileCoordinate, null);
        }

        //TileBase highlightTile = gravelTile;

        //We need to highlight the tile on mousover. First get the curent position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCoordinate = highlightMap.WorldToCell(mouseWorldPos);
        //store the current position for the next frame
        previousTileCoordinate = tileCoordinate;
        //set the current tile to the highlighted version
        highlightMap.SetTile(tileCoordinate, highlightTile);*/
    }
}

