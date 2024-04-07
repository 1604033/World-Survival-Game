using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropBehaviour : MonoBehaviour
{
    [SerializeField] Crop crop;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D _Collider2D;
    float minDistanceTowater;
    private bool IsHarvested;
    private CropData _cropData;
    private TilemapManager _tilemapManager;
    private Vector3 positionFloat;
    private float _timeToDehydrate = 10f;
    private GameObject instantiatedFruit;

    public float delay = 10f;
    private float timer;
    public Vector3Int TileVector3Int;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        minDistanceTowater = 2f;
        _Collider2D = GetComponent<BoxCollider2D>();
        IsHarvested = false;
        _cropData = crop.cropData;
    }

    public void Start()
    {
        
        _tilemapManager = TilemapManager.instance;
        CropManager.instance.OnCropGrow += CropGrowOnNewDay;
        TileVector3Int =   Vector3Int.RoundToInt(gameObject.transform.position - new Vector3(0.5f, 0.4f, 0f));
        //CropManager.instance.OnResetWaterLevel += ResetWaterLevel;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            CropGrowOnNewDay();
            timer = 0;
        }
    }

    void CropGrowOnNewDay()
    {
        if (crop.growthStage < crop.maxGrowthStage)
        {
            if (crop.HydrationLevel > 0)
            {
                crop.growthStage++;
                crop.readyForHarvest = crop.growthStage == crop.maxGrowthStage;
                StartCoroutine(ResetWaterLevel());
                UpdateCropSprite();
            }
        }else if (crop.growthStage == crop.maxGrowthStage )
        {
            StartCoroutine(ResetWaterLevel());
        }
    }

    private void UpdateCropSprite()
    {
        spriteRenderer.sprite = crop.cropData.growthSprites[crop.growthStage];
        UpdateCollider();
    }

    public void Harvest()
    {
        if (crop.growthStage >= crop.maxGrowthStage)
        {
            if (IsHarvested)
            {
                Vector3 positionFloat = gameObject.transform.position - new Vector3(0.5f, 0.4f, 0f);
                Vector3Int position = Vector3Int.RoundToInt(positionFloat);
                _tilemapManager.SetTileHarvested(position);

                Item fruitItem = instantiatedFruit.GetComponent<Item>();

                if (fruitItem != null)
                {
                    if (crop.Fertilized)
                    {
                        //GameManager.instance.player.inventory.Add("Backpack" , fruitItem, 3);
                        GameManager.instance.player.inventory.Add("Toolbar", fruitItem, 3);
                    }
                    else
                    {
                        GameManager.instance.player.inventory.Add("Toolbar", fruitItem, 1);
                    }

                    Destroy(gameObject);
                    Destroy(instantiatedFruit);
                }
            }
            else
            {
                GameObject fruitPrefab = _cropData.fruitPrefab;
                instantiatedFruit = Instantiate(fruitPrefab, gameObject.transform.position, Quaternion.identity);
                IsHarvested = true;
                Destroy(gameObject);
            }
        }
    }

    public void UpdateCollider()
    {
        Bounds spriteBounds = spriteRenderer.bounds;
        _Collider2D.size = spriteBounds.size;
        _Collider2D.offset = spriteBounds.center - transform.position;
    }

    public void WaterCrop(Vector3Int tilePos)
    {
        if (crop.HydrationLevel + 1 < crop.maxHydrationLevel && !crop.readyForHarvest)
        {
            crop.HydrationLevel++;
            crop.IsWatered = true;
            _tilemapManager.SetTileWatered(tilePos);
        }
    }

    IEnumerator  ResetWaterLevel()
    {
       
        _tilemapManager.SetInteracted(TileVector3Int);
        crop.IsWatered = false;
        crop.HydrationLevel = 0;
        yield return null;
    }

    public bool NeedsWatering()
    {
        if (Time.time - crop.lastwateredTime >= _timeToDehydrate)
        {
            return true;
        }

        return false;
    }

    public bool NeedsFertilizer()
    {
        if (crop.growthStage == -1 && !crop.Fertilized)
        {
            return true;
        }

        return false;
    }

    public void FertilizeCrop(Vector3Int position)
    {
        if (NeedsFertilizer())
        {
            crop.Fertilized = true;
            _tilemapManager.SetTileFertilized(position);
        }
    }
}