using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*public class TileHighlighter : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float tileHighlightDistance = 1.5f; // Distance within which tiles should be highlighted
    public Material highlightMaterial;
    public Tile highlightTile; // The material to use for highlighting

    private Transform lastHighlightedTile; // To keep track of the last highlighted tile

    private void Update()
    {
        // Cast a ray from the cursor position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Transform tileTransform = hit.transform;

            // Check if the hit tile is within the highlight distance from the player
            if (Vector3.Distance(playerTransform.position, tileTransform.position) <= tileHighlightDistance)
            {
                // If the hit tile is different from the last highlighted tile, update the highlight
                if (tileTransform != lastHighlightedTile)
                {
                    // Remove highlight from the previous tile
                    RemoveHighlight();

                    // Highlight the current tile
                    HighlightTile(tileTransform);
                }
            }
            else
            {
                // If the hit tile is outside the highlight distance, remove highlight
                RemoveHighlight();
            }
        }
        else
        {
            // If no tile is hit, remove highlight
            RemoveHighlight();
        }
    }

    // Function to highlight a tile
    private void HighlightTile(Transform tileTransform)
    {
        Renderer tileRenderer = tileTransform.GetComponent<Renderer>();

        if (tileRenderer != null)
        {
            // Store the original material of the tile
            Material originalMaterial = tileRenderer.material;

            // Apply the highlight material
            tileRenderer.material = highlightMaterial;

            // Store the last highlighted tile
            lastHighlightedTile = tileTransform;
        }
    }

    // Function to remove highlight from a tile
    private void RemoveHighlight()
    {
        if (lastHighlightedTile != null)
        {
            Renderer tileRenderer = lastHighlightedTile.GetComponent<Renderer>();

            if (tileRenderer != null)
            {
                // Restore the original material
                tileRenderer.material = tileRenderer.sharedMaterial;
            }

            // Clear the last highlighted tile
            lastHighlightedTile = null;
        }
    }
}*/

/*public class TileHighlighter : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase highlightTile; // The tile to use for highlighting
    public Color highlightColor = Color.green; // Color to tint the highlight

    private Vector3Int lastHoveredTilePosition;

     void Start()
    {
     tilemap =  GetComponent<Tilemap>();
    }

    private void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Convert the mouse position to tile coordinates
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
        
        // Check if the mouse position is over a different tile than before
        if (cellPosition != lastHoveredTilePosition)
        {
            // Remove the highlight from the previous tile
            tilemap.SetTile(lastHoveredTilePosition, null);
            
            // Set the new highlight on the current tile
            tilemap.SetTile(cellPosition, highlightTile);
            
            // Tint the highlighted tile with the desired color
            tilemap.SetColor(cellPosition, highlightColor);
            
            // Update the last hovered tile position
            lastHoveredTilePosition = cellPosition;
        }
    }
}

public class TileHighlighter : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase highlightTile;

    private TilemapRenderer tilemapRenderer;
    private Vector3Int previousTilePosition;

    private void Start()
    {
        tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
    }

    private void Update()
    {
        // Get the cursor position in world coordinates
        Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cursorTilePos = tilemap.WorldToCell(cursorWorldPos);

        // Check if the cursor is over the tilemap
        if (tilemap.HasTile(cursorTilePos))
        {
            // Highlight the current tile
            tilemap.SetTile(cursorTilePos, highlightTile);

            // Remove the highlight from the previous tile
            if (previousTilePosition != cursorTilePos)
            {
                tilemap.SetTile(previousTilePosition, null);
                previousTilePosition = cursorTilePos;
            }

            // Show the tilemap renderer (in case it's hidden)
            tilemapRenderer.enabled = true;
        }
        else
        {
            // Hide the tilemap renderer when the cursor is not over a tile
            tilemapRenderer.enabled = false;
        }
    }
}*/

public class TileHighlighter : MonoBehaviour
{
    public Tilemap tilemap; // Reference to the Tilemap you want to highlight.
    public TileBase highlightTile;
    public TileBase previousTile; // The tile you want to use for highlighting.
    public float highlightRadius = 2.0f; // The radius around the player to highlight tiles.

    private Vector3Int lastTilePosition; // Stores the position of the last highlighted tile.

    private void Update()
    {
        // Get the mouse position in world coordinates.
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Convert the mouse position to tile coordinates.
        Vector3Int mouseTilePos = tilemap.WorldToCell(mouseWorldPos);
        
        // Calculate the distance between the player and the mouse cursor.
        float distanceToMouse = Vector3Int.Distance(mouseTilePos, tilemap.WorldToCell(transform.position));
        
        // Check if the cursor is within the highlight radius.
        if (distanceToMouse <= highlightRadius)
        {
            // Check if the tile position has changed or if there was no previous highlighted tile.
            if (mouseTilePos != lastTilePosition)
            {
                // Remove the highlight from the previous tile.
                if (tilemap.GetTile(lastTilePosition) == highlightTile)
                {
                    tilemap.SetTile(lastTilePosition, previousTile);
                }

                // Highlight the current tile.
                previousTile = tilemap.GetTile(mouseTilePos);
                tilemap.SetTile(mouseTilePos, highlightTile);

                // Update the last highlighted tile position.
                lastTilePosition = mouseTilePos;
            }
        }
        else
        {
            // Remove the highlight from the previous tile when outside the highlight radius.
            if (tilemap.GetTile(lastTilePosition) == highlightTile)
            {
                tilemap.SetTile(lastTilePosition, previousTile);
            }
        }
    }
}







