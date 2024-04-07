using System;
using System.Collections.Generic;
using BuildingSystem;
using UnityEngine;
using BuildingSystem.Models;
using UnityEngine.Serialization;


public class Building : MonoBehaviour
{
    // public  Dictionary<ItemData, int> RawMaterials = new ();
    private List<int> itemsNeedCounts;
    private List<ItemData> itemsDatasNeededToConstruct;
    public List<GameObject> scatteredRawMaterials;
    public List<GameObject> originalScatteredRawMaterials;
    public List<Transform> buildingTransformPoints;
    public List<Sprite> houseSprites;
    public BuilderController builderController;
    private float _previousColorOpacity = 1f;
    public List<Vector3Int> worldCoordinates;
    [SerializeField]private FloatingProgressBar _floatingProgressBar;
    public Transform scatterPoint;
    public GameObject finalBuilding;
    public GameObject secondStageBuilding;
    public GameObject initialBuilding;
    public List<Pokemon> assignedPokemons = new List<Pokemon>();
    public List<FoodItem> stockedFoodItems = new List<FoodItem>();
    public List<int> stockedFoodItemsCount = new List<int>();
    
    [FormerlySerializedAs("InteriourGameObject")] [FormerlySerializedAs("initialBuilding")]
    public GameObject interiourGameObject;

    public GameObject monsterToConstruct;


    public ParticleSystem particleSystem;
    private Color _color;
    public SpriteRenderer BuildingSpriteRenderer;
    public SpriteRenderer signPostSpriteRenderer;

    public BuildableItem buildableItem;
    public ItemData ItemData;
    TilemapLayer tilemapLayer;
    private ConstructionLayer _constructionLayer;
    private BuildingPlacer buildingPlacer;
    [Range(0, 100)] public float constructionLevel = 0f;
    public float maxconstructionLevel = 100f;
    public int constructionStep = 0;
    public int maxconstructionStep = 3;
    private int countToRemove;
    private bool IsFullyCompleted = false;

    private void Start()
    {
        buildingPlacer = FindObjectOfType<BuildingPlacer>();
        _constructionLayer = BuildingSystemManager.Instance.ConstructionLayer;
        itemsDatasNeededToConstruct = buildableItem.itemsDatasNeededToConstruct;
        itemsNeedCounts = buildableItem.itemsNeedCounts;
    }

    private void FixedUpdate()
    {
        if (_floatingProgressBar != null && _floatingProgressBar.gameObject.activeSelf)
        {
            _floatingProgressBar.SetProgressBar(constructionLevel, maxconstructionLevel);
        }
    }

    private void Awake()
    {
        scatteredRawMaterials = new List<GameObject>();
    }

    public bool CanConstructBuilding()
    {
        return constructionLevel < maxconstructionLevel;
    }

    public void CanNowBuild(bool canBuild)
    {
        buildingPlacer.SetCanBuild(canBuild);
    }

    public void ConstructBuilding()
    {
        Debug.Log($"Constructing Building called");

        if (constructionStep < maxconstructionStep)
        {
            constructionStep += 1;
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
            else
            {
                Debug.Log("Particle system stopped");
                particleSystem.Play();
            }

            if (constructionStep == 2)
            {
                if (buildableItem.has2NdStageGameObject)
                {
                    if (secondStageBuilding != null) secondStageBuilding.SetActive(true);
                    finalBuilding.SetActive(false);
                }
                else
                {
                    finalBuilding.SetActive(true);
                    if (BuildingSpriteRenderer != null) BuildingSpriteRenderer.sprite = houseSprites[0];
                }
                initialBuilding.SetActive(false);
            }

            if (constructionStep == maxconstructionStep)
            {
                // _previousColorOpacity = 1f;
                // BuildingSpriteRenderer.color = Color.white;
                // signPostSpriteRenderer.color = Color.white;
                if (buildableItem.has2NdStageGameObject)
                {
                    secondStageBuilding.SetActive(false);
                }

                if (buildableItem.hasInteriorGameObject)
                {
                    interiourGameObject.SetActive(true);
                }

                BuildingSpriteRenderer.sprite = houseSprites[1];
                signPostSpriteRenderer.gameObject.SetActive(true);
                if (_floatingProgressBar != null) _floatingProgressBar.gameObject.SetActive(false);

                if (finalBuilding != null) finalBuilding.SetActive(true);
                IsFullyCompleted = true;
                ConsumeRawMaterials();
            }
        }
    }

    public void DestroyBuilding()
    {
        Debug.Log("DestroyBuilding called");
        if (constructionLevel < 100f)
        {
            //bool  hasScattered = ScatterRawMaterials(itemsDatasNeededToConstruct, itemsNeedCounts, scatterPoint.position);
            EnableRawMaterialsColliders();
            _constructionLayer.ClearConstructedArea(worldCoordinates);
            //ScatterRawMaterials(1f);
        }

        if (constructionLevel >= 100f)
        {
            //bool  hasScattered = ScatterRawMaterials(itemsDatasNeededToConstruct, itemsNeedCounts, scatterPoint.position);
            //_constructionLayer.ClearConstructedArea(worldCoordinates);
            Debug.Log("Scatter Method called");
            ScatterRawMaterials(0.5f);
        }

        if (builderController != null) Destroy(builderController.transform.parent.gameObject);
        PopupManager.Instance.HideAllPopups();
        Destroy(gameObject);
    }

    private void ConsumeRawMaterials()
    {
        foreach (var rawMaterial in scatteredRawMaterials)
        {
            Destroy(rawMaterial);
        }

        scatteredRawMaterials.Clear();
    }

    public void UpdateProgress(float progress, float total)
    {
        _floatingProgressBar.SetProgressBar(progress, total);
        
    }
    public void AddInstantiatedGameObject(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return;
        }

        scatteredRawMaterials.Add(gameObject);
    }

    private void EnableRawMaterialsColliders()
    {
        foreach (var gameobject in scatteredRawMaterials)
        {
            if (gameobject.TryGetComponent<Collider2D>(out Collider2D collider2D))
            {
                collider2D.enabled = true;
            }
        }
    }

    private void DeleteRawMaterials(float factor)
    {
        float counter = scatteredRawMaterials.Count * factor;
        for (int i = 0; i < (int)counter; i++)
        {
            GameObject scatterItem = scatteredRawMaterials[i];
            Destroy(scatterItem);
        }
    }


    private bool ScatterRawMaterials(float factor)
    {
        const float spread = 0.7f;
        for (var i = 0; i < itemsDatasNeededToConstruct.Count; i++)
        {
            float count = itemsNeedCounts[i] * factor;
            int counter = (int)count;
            Debug.Log("The count is" + counter);
            for (int j = 0; j < counter; j++)
            {
                Vector3 position = scatterPoint.position;
                position.x += spread * UnityEngine.Random.value - spread / 2;
                position.y += spread * UnityEngine.Random.value - spread / 2;
                GameObject instantiatedRawMaterial =
                    Instantiate(itemsDatasNeededToConstruct[i].prefab, position, Quaternion.identity);
            }
        }

        return true;
    }
}