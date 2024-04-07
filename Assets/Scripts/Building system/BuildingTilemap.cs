using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Building_system;
using BuildingSystem;
using BuildingSystem.Models;
using UnityEditor.U2D;

public class BuildingTilemap : MonoBehaviour
{
    [SerializeField] private SceneObstacles sceneObstacles;
    [SerializeField] private BuildingPlacer _buildingPlacer;
    public Tilemap tilemap;
    public Tilemap highlightTilemap;
    public Tilemap collsionTilemap;
    public Tile highlightTile;
    public Tile collisonTile;
    public Camera mainCam;
    private Dictionary<Vector3Int, bool> IsTileOccupied = new Dictionary<Vector3Int, bool>();
    private HashSet<Vector3Int> highlightedTiles = new HashSet<Vector3Int>();
    private Vector3 previousMousePos = new Vector3Int();
    [SerializeField] private LayerMask obstacleLayer;

    private int range = 8; // Define the range for highlighting
    private int buildsize = 5; // Define the range for highlighting
    [SerializeField] private Vector3 highlightedPositionCenter;
    private SnapToGridBuildingPreview snapToGridBuilding;
//TEST VAR TO DELETE
    [SerializeField] private GameObject previewLayerGameObject;
    private Vector3 center;
   [SerializeField] public Vector3 buildingOffset;
    private Vector2 bounds;

    private void Start()
    {
        mainCam = Camera.main;
        LayerMask.GetMask("Obstacle");
        BuildingPlacer.OnBuildInputClicked += UnhighlightBlock;
        snapToGridBuilding =
            previewLayerGameObject.GetComponent<SnapToGridBuildingPreview>();
    }

    private void Update()

    {
        Vector3Int currentMousePosGrid = GetCellPositionWorld(snapToGridBuilding.transform.position );
     // Calculate the center of the object
        if (currentMousePosGrid != previousMousePos && _buildingPlacer.ActiveBuildingItem != null)
        {
            Vector2Int buildingArea = _buildingPlacer.ActiveBuildingItem.collisionVector;
            // Unhighlight the previous highlighted tiles
            UnhighlightBlock();

            // Highlight the new tiles
            bool isAreaEmpty = IsAreaEmptyNoGameObjects(currentMousePosGrid,buildingArea
                );
            ;
            HighlightTiles(currentMousePosGrid, buildingArea, true,isAreaEmpty);
            HighlightTiles(currentMousePosGrid, buildingArea + new Vector2Int(2, 2));


            // Update the previous mouse position
            previousMousePos = currentMousePosGrid;
        }

        if (Input.GetKeyDown(KeyCode.N)) BakeScene();
        if (Input.GetKeyDown(KeyCode.Q)) OutPutScene();
    }
  
      
    Vector3Int GetCellPosition(Vector3 mousePosition)
    {
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(mousePosition);
        return tilemap.WorldToCell(mouseWorldPos);
    }
Vector3Int GetCellPositionWorld(Vector3 worldPosition)
    {
        return tilemap.WorldToCell(worldPosition);
    }


    void SetCollisions(Vector3Int tilePos, bool isBuildingArea, bool isAreaEmpty = false)
    {
        if (!isBuildingArea)
        {
            if (sceneObstacles.SceneObstacleData.GetValueOrDefault(tilePos))
            {
                collsionTilemap.RemoveTileFlags(tilePos, TileFlags.LockColor);
                collsionTilemap.SetColor(tilePos, Color.red);
            }
            else
            {
                collsionTilemap.RemoveTileFlags(tilePos, TileFlags.LockColor);
                collsionTilemap.SetColor(tilePos, Color.blue);
            }
        }
        else
        {
            if (sceneObstacles.SceneObstacleData.GetValueOrDefault(tilePos) || !isAreaEmpty)
            {
                collsionTilemap.RemoveTileFlags(tilePos, TileFlags.LockColor);
                collsionTilemap.SetColor(tilePos, Color.red);
            }
            else
            {
                collsionTilemap.RemoveTileFlags(tilePos, TileFlags.LockColor);
                collsionTilemap.SetColor(tilePos, Color.green);
            }
        }
    }

    public void UnhighlightBlock()
    {
        foreach (var tilePos in highlightedTiles)
        {
            highlightTilemap.SetTile(tilePos, null);
            collsionTilemap.SetTile(tilePos, null);
            //highlightedTiles.Remove(tilePos);
        }

        highlightedTiles = new HashSet<Vector3Int>();
    }
// public void UnhighlightBlock(Vector3Int center)
//     {
//         int sizeX = range;
//         int sizeY = range;
//         Vector3Int startPos = center - new Vector3Int(sizeX / 2, sizeY / 2, 0);
//         Vector3Int endPos = center + new Vector3Int(sizeX / 2, sizeY / 2, 0);
//
//         for (int x = startPos.x; x <= endPos.x; x++)
//         {
//             for (int y = startPos.y; y <= endPos.y; y++)
//             {
//                 Vector3Int tilePos = new Vector3Int(x, y, 0);
//                 if (highlightedTiles.Contains(tilePos))
//                 {
//                     highlightTilemap.SetTile(tilePos, null);
//                     collsionTilemap.SetTile(tilePos, null);
//                     highlightedTiles.Remove(tilePos);
//                 }
//             }
//         }
//     }
//

    //Building area highlight
    public bool HighlightTiles(Vector3 mousePos, Vector2Int offset, bool isBuildingArea = false, bool isAreaEmpty = false)
    {
        // Vector2Int offset = activeBuildingItem.collisionVector;

        Vector3Int mouseGridPos = tilemap.WorldToCell(mousePos);
        //if (activeBuildingItem != null && !activeBuildingItem.needsAnotherBuildingToConstruct)
        {
            int squareSizeX = offset.x;
            int squareSizeY = offset.y;

            // Calculate loop limits based on specified square sizes
            int startX = mouseGridPos.x - squareSizeX / 2;
            int endX = mouseGridPos.x + (squareSizeX + 1) / 2;
            int startY = mouseGridPos.y - squareSizeY / 2;
            int endY = mouseGridPos.y + (squareSizeY + 1) / 2;

            float centerX = 0;
            float centerY = 0;
            int count = 0;

            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    highlightTilemap.CellToWorld(tilePos);
                    if (!highlightedTiles.Contains(tilePos))
                    {
                        highlightTilemap.SetTile(tilePos, highlightTile);
                        collsionTilemap.SetTile(tilePos, collisonTile);
                        highlightedTiles.Add(tilePos);
                        SetCollisions(tilePos,isBuildingArea, isAreaEmpty);
                    }

                    // Calculate the center of the highlighted tiles
                    centerX += x;
                    centerY += y;
                    count++;
                }
            }

            // Calculate the average to find the center
            centerX /= count;
            centerY /= count;
            highlightedPositionCenter = new Vector3(centerX, centerY, mouseGridPos.z);
        }

        return true;
    }


    public bool IsAreaEmpty(Vector3 mousePos, Vector2Int offset)
    {
        Vector3Int mouseGridPos = tilemap.WorldToCell(mousePos);
        {
            int squareSizeX = offset.x;
            int squareSizeY = offset.y;

            // Calculate loop limits based on specified square sizes
            int startX = mouseGridPos.x - squareSizeX / 2;
            int endX = mouseGridPos.x + (squareSizeX + 1) / 2;
            int startY = mouseGridPos.y - squareSizeY / 2;
            int endY = mouseGridPos.y + (squareSizeY + 1) / 2;

            float centerX = 0;
            float centerY = 0;
            int count = 0;

            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    highlightTilemap.CellToWorld(tilePos);
                    if (sceneObstacles.SceneObstacleData.ContainsKey(tilePos))
                        return false;

                    // Calculate the center of the highlighted tiles
                    centerX += x;
                    centerY += y;
                    count++;
                }
            }

            centerX /= count;
            centerY /= count;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(center, bounds);
    }

    void BakeScene()
    {
        // Use the cellBounds property to get the bounds of the tilemap
        BoundsInt bounds = tilemap.cellBounds;
        bool isOccupied = false;
        Vector3 center;
        Vector3Int tilePos;
        // Iterate over each cell in the bounds
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                tilePos = new Vector3Int(x, y, 0);
                center = tilemap.GetCellCenterWorld(tilePos);
                isOccupied = Physics2D.OverlapBox(center, new Vector2(1, 1), 0, obstacleLayer);

                // If the tile is occupied, add it to the SceneObstacleData dictionary
                if (isOccupied)
                {
                    sceneObstacles.SceneObstacleData.TryAdd(tilePos, true);
                }
            }
        }

        sceneObstacles.SaveData();
    }

    public bool IsAreaEmptyNoGameObjects(Vector3 mousePos, Vector2 boundsInterior)
    {
        Collider2D[] colliders = new Collider2D[5];
        Vector2 boundsSize = boundsInterior;
        Vector2 boundsCenter = mousePos;
        bounds = boundsInterior;
        center = mousePos;

        var size = Physics2D.OverlapBoxNonAlloc(boundsCenter, boundsSize, 0, colliders);
        if (size > 2)

        {
            Debug.Log("Found colliders: " + colliders.Length);
            return false;
        }

        Debug.Log("No colliders: " + colliders.Length);

        return true;
    }

    void OutPutScene()
    {
        Debug.Log($"Total obstacles: {sceneObstacles.SceneObstacleData.Count}");

        foreach (var entry in sceneObstacles.SceneObstacleData)
        {
            Debug.Log($"{entry.Key}");
        }

        sceneObstacles.LoadData();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        sceneObstacles.LoadData();
    }
}