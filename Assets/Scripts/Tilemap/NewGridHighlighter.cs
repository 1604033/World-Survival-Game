using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class NewGridHighlighter : MonoBehaviour
{
    [SerializeField] private SceneObstacles sceneObstacles;    
    public Tilemap tilemap;
    public Tilemap highlightTilemap;
    public Tilemap collsionTilemap;
    public Tile highlightTile;
    public Tile collisonTile;
    public Camera mainCam;
    private Dictionary<Vector3Int, bool> IsTileOccupied = new Dictionary<Vector3Int, bool>();
    private HashSet<Vector3Int> highlightedTiles = new HashSet<Vector3Int>();
    private Vector3Int previousMousePos = new Vector3Int();
    [SerializeField] private LayerMask obstacleLayer;
    
    private int range = 5; // Define the range for highlighting

    private void Start()
    {
        mainCam = Camera.main;
        LayerMask.GetMask("Obstacle");
    }

    private void Update()
    {
        Vector3Int currentMousePos = GetMousePosition();
        if (currentMousePos != previousMousePos)
        {
            // Unhighlight the previous highlighted tiles
            UnhighlightBlock(previousMousePos, range, range);
        
            // Highlight the new tiles
            HighlightBlock(currentMousePos, range, range);
        
            // Update the previous mouse position
            previousMousePos = currentMousePos;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int tilepos = GetMousePosition();
            IsTileOccupied.TryAdd(tilepos, true);
        }
       if(Input.GetKeyDown(KeyCode.N)) BakeScene();
       if(Input.GetKeyDown(KeyCode.Q)) OutPutScene();
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        return tilemap.WorldToCell(mouseWorldPos);
    }

    public void HighlightBlock(Vector3Int center, int sizeX, int sizeY)
    {
        Vector3Int startPos = center - new Vector3Int(sizeX / 2, sizeY / 2, 0);
        Vector3Int endPos = center + new Vector3Int(sizeX / 2, sizeY / 2, 0);

        for (int x = startPos.x; x <= endPos.x; x++)
        {
            for (int y = startPos.y; y <= endPos.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                Vector3 worldPos = highlightTilemap.CellToWorld(tilePos); // Convert grid position to world position
                worldPos.z = 0; // Ensure the z position is correct for your scene
                if (!highlightedTiles.Contains(tilePos))
                {
                    highlightTilemap.SetTile(tilePos, highlightTile);
                    collsionTilemap.SetTile(tilePos, collisonTile);
                    highlightedTiles.Add(tilePos);
                    SetCollisions(tilePos);
                }
               
            }
        }
    }


    void SetCollisions(Vector3Int tilePos)
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
    public void UnhighlightBlock(Vector3Int center, int sizeX, int sizeY)
    {
        Vector3Int startPos = center - new Vector3Int(sizeX / 2, sizeY / 2, 0);
        Vector3Int endPos = center + new Vector3Int(sizeX / 2, sizeY / 2, 0);

        for (int x = startPos.x; x <= endPos.x; x++)
        {
            for (int y = startPos.y; y <= endPos.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (highlightedTiles.Contains(tilePos))
                {
                    highlightTilemap.SetTile(tilePos, null);
                    collsionTilemap.SetTile(tilePos, null);
                    highlightedTiles.Remove(tilePos);
                }
            }
        }
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
                isOccupied = Physics2D.OverlapBox(center, new Vector2(1,1), 0, obstacleLayer);

                // If the tile is occupied, add it to the SceneObstacleData dictionary
                if (isOccupied)
                {
                    sceneObstacles.SceneObstacleData.TryAdd(tilePos, true);
                }
            }
        }
    } 
    
    
    
    void OutPutScene()
    {
        Debug.Log($"Total obstacles: { sceneObstacles.SceneObstacleData.Count }");

        foreach (var entry in sceneObstacles.SceneObstacleData)
        {
            Debug.Log($"{entry.Key}");
        }

       
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        //sceneObstacles.LoadData();

    }
}
