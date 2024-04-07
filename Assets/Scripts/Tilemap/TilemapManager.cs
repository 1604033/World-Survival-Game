using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public static TilemapManager instance;
    public Tilemap interactableMap;
    public Tilemap cropsTileMap;
    public Tilemap fertilizerTilemap;
    public Tile HighlightTile;
    [SerializeField] Tile plowedTile;
    [SerializeField] Tile plantedTile;
    [SerializeField] Tile WateredTile;
    [SerializeField] Tile fertilizedTile;
    [SerializeField] GameObject player;
    private Inventory.Slot activeItemSlot;
    GameObject cropPrefab;
    GameObject seedPrefab;
    public GameObject monosterPrefab;
    private Vector3 playerPosition;
    private Vector3Int playerMouseGridPos;
    private Inventory inventory;
    private Vector3 mousePos;
    public float maxInteractionDistance = 10f;
    public List<Tiledata> Tiledatas;
    public Dictionary<TileBase, Tiledata> tileswithdata;

    public List<TileBase> farmTiles;

    Vector3 previousMousePosition = Vector3.zero;
    // private CropManager cropManager;
    //public GameObject seedPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        tileswithdata = new Dictionary<TileBase, Tiledata>();

        foreach (var tiledata in Tiledatas)
        {
            // Debug.Log(tiledata );
            foreach (var tile in tiledata.tile)
            {
                Debug.Log($"Added into: " + tile.name);
                tileswithdata.Add(tile, tiledata);
            }
        }


        //inventory = player.GetComponent<Player>().inventory;
        playerPosition = player.transform.position;
        playerMouseGridPos = Vector3Int.RoundToInt(Input.mousePosition);
    }

    private void Start()
    {
        // cropManager = GameManger.instance.CropManager;
        //activeItemSlot = GameManager.instance.uiManager.ToolbarUI.selectedSlot;
    }

    private void Update()
    {
        InterractWithTile(MousePosToGridPos());
    }

    private Vector3 MousePosToGridPos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerMouseGridPos = interactableMap.WorldToCell(mousePos);
        return mousePos;
    }

    public bool IsTilePloughed(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);
        if (tile != null)
        {
            if (tile.name == "plowedTile")
            {
                return true;
            }
        }

        return false;
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);
        return farmTiles.Contains(tile);
    }

    public void SetInteracted(Vector3Int postiton)
    {
        Debug.Log("Interact called");
        interactableMap.SetTile(postiton, plowedTile);
    }

    public void SetTileHarvested(Vector3Int postiton)
    {
        cropsTileMap.SetTile(postiton, null);
        interactableMap.SetTile(postiton, null);
        fertilizerTilemap.SetTile(postiton, null);
    }

    void InterractWithTile(Vector3 mousePosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            int slotId = 0;
            if (GameManager.instance != null)
            {
                if (GameManager.instance.uiManager != null)
                {
                    if (GameManager.instance.uiManager.ToolbarUI != null)
                    {
                        slotId = GameManager.instance.uiManager.ToolbarUI.selectedSlotIndex;
                        if (slotId > 0)
                            activeItemSlot = GameManager.instance?.player?.inventory?.GetInventoryByName("Toolbar")
                                ?.slots[slotId];
                    }
                }
            }

            if (slotId == 0)
            {
                if (Vector3.Distance(mousePosition, player.transform.position) > maxInteractionDistance)
                {
                    return;
                }

                if (IsInteractable(playerMouseGridPos))
                {
                    SetInteracted(playerMouseGridPos);
                    //inventory.RemoveItemsFromSlot(GameManager.instance.activeSlot.);
                }

                return;
            }


            if (activeItemSlot.count == 0)
            {
                return;
            }

            if (IsTilePloughed(playerMouseGridPos))
            {
                SetTilePlanted(playerMouseGridPos);
            }
        }
    }

    private void SetTilePlanted(Vector3Int postiton)
    {
        PlantSeed(postiton);
    }

    void PlantSeed(Vector3Int postiton)
    {
        TileBase tile = cropsTileMap.GetTile(postiton);
        ItemData itemData = activeItemSlot.itemData;
        int seedCount = activeItemSlot.count;
        cropPrefab = itemData.prefab;
        if (tile == null && itemData.type == CollectableType.Seed && seedCount > 0)
        {
            activeItemSlot.count--;
            Crop crop = cropPrefab.GetComponent<Crop>();
            CropBehaviour cropBehaviour = cropPrefab.GetComponent<CropBehaviour>();
            Vector3 tempPos = (Vector3)postiton + crop.tileOffset;
            Instantiate(cropPrefab, tempPos, Quaternion.identity);
            if (crop != null)
            {
                crop.SetTilePostion(postiton);
                cropsTileMap.SetTile(postiton, plantedTile);
                CropManager.instance.AddCrop(crop, postiton);
            }
        }
    }

    public void SetTileWatered(Vector3Int position)
    {
        Debug.Log("POSITION" + position + WateredTile);
        interactableMap.SetTile(position, WateredTile);
    }

    public void SetTileFertilized(Vector3Int position)
    {
        fertilizerTilemap.SetTile(position, fertilizedTile);
    }

    public void SetFertilizedTileEmpty(Vector3Int position)
    {
        fertilizerTilemap.SetTile(position, null);
    }


    public Tiledata GetTileItemData(TileBase tile)
    {
        Debug.Log($"Looking for: {tile}");
        Tiledata tiledata = tileswithdata[tile];
        return tiledata;
    }
}