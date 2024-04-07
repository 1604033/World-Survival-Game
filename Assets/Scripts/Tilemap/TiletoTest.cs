using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TiletoTest : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
    [SerializeField] private SpriteRenderer detector;
    [SerializeField] private SpriteRenderer preview;
    [SerializeField] private Tilemap tilemap; // Reference to your Tilemap
    [SerializeField] private GridLayout gridLayout; // Reference to the GridLayout component
    private Vector2 referenceSize;
    private void Start()
    {
        referenceSize = (Vector2)preview.bounds.size;
        referenceSize += offset;
        detector.transform.localScale = new Vector3(referenceSize.x, referenceSize.y, 1);
    }

    private void Update()
    {
        if (Input.mousePosition != Vector3.zero)
        {
            // Convert the mouse position to world coordinates
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosWorld.z = 0;
            detector.transform.localScale = new Vector3(referenceSize.x, referenceSize.y, 1);


            // Convert the world coordinates to cell coordinates
            Vector3Int cellPosition = gridLayout.WorldToCell(mousePosWorld);

            // Convert the cell coordinates back to world coordinates to get the center of the cell
            Vector3 cellCenterWorldPosition = gridLayout.CellToWorld(cellPosition) + (gridLayout.cellSize / 2);

            // Adjust the game object's position so that its left bottom corner aligns with the left bottom corner of the grid cell
            transform.position = new Vector3(
                cellCenterWorldPosition.x - (gridLayout.cellSize.x / 2) + (detector.bounds.size.x / 2),
                cellCenterWorldPosition.y - (gridLayout.cellSize.y / 2) + (detector.bounds.size.y / 2),
                0
            );
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(preview.transform.position, preview.bounds.size);
    }
}