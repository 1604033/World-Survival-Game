using System;
using System.Collections;
using System.Collections.Generic;
using BuildingSystem;
using BuildingSystem.Models;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{

    public Action OnFinishedConstruction;
    public List<int> itemsNeedCounts;
    public List<ItemData> itemsDatasNeededToConstruct;
    public List<GameObject> scatteredRawMaterials;
    public List<GameObject> originalScatteredRawMaterials;
    public List<Transform> buildingTransformPoints;
    public List<Sprite> houseSprites;
    public List<Vector3Int> worldCoordinates;
    
    public BuilderController builderController;
    [SerializeField]public FloatingProgressBar _floatingProgressBar;
    
    public float _previousColorOpacity = 1f;
    [Range(0, 100)] public float constructionLevel = 0f;
    public float maxconstructionLevel = 100f;
    public int constructionStep = 0;
    public int maxconstructionStep = 3;
    public int countToRemove;
    
    public Transform scatterPoint;
    public GameObject finalBuilding;
    public GameObject secondStageBuilding;
    public GameObject initialBuilding;
    public GameObject monsterToConstruct;
    public GameObject interiourGameObject;
    
    public Color _color;
    public ParticleSystem particleSystem;
    
    public SpriteRenderer BuildingSpriteRenderer;
    public SpriteRenderer signPostSpriteRenderer;

    public BuildableItem buildableItem;
    public ItemData ItemData;
    TilemapLayer tilemapLayer;
    public ConstructionLayer _constructionLayer;
    public BuildingPlacer buildingPlacer;
   
    public bool IsFullyCompleted = false;
    
    public virtual void Start()
    {
        buildingPlacer = FindObjectOfType<BuildingPlacer>();
        _constructionLayer = BuildingSystemManager.Instance.ConstructionLayer;
        itemsDatasNeededToConstruct = buildableItem.itemsDatasNeededToConstruct;
        itemsNeedCounts = buildableItem.itemsNeedCounts;
    }

    public virtual void FixedUpdate()
    {
        if (_floatingProgressBar != null && _floatingProgressBar.gameObject.activeSelf)
        {
            _floatingProgressBar.SetProgressBar(constructionLevel, maxconstructionLevel);
        }
    }

    public void CalculateConstructionLevel(float increment)
    {
        if (constructionLevel < maxconstructionLevel)
        {
            constructionLevel += increment;
        }
        
    }
    
    public virtual void Awake()
    {
        scatteredRawMaterials = new List<GameObject>();
    }

    public virtual bool CanConstructBuilding()
    {
        return constructionLevel < maxconstructionLevel;
    }

    public virtual void CanNowBuild(bool canBuild)
    {
        buildingPlacer.SetCanBuild(canBuild);
    }

    public virtual void ConstructBuilding()
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
                OnFinishedConstruction?.Invoke();
                ConsumeRawMaterials();
            }
        }
    }

    public virtual void DestroyBuilding()
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

    public virtual void ConsumeRawMaterials()
    {
        foreach (var rawMaterial in scatteredRawMaterials)
        {
            Destroy(rawMaterial);
        }

        scatteredRawMaterials.Clear();
    }

    public virtual void UpdateProgress(float progress, float total)
    {
        _floatingProgressBar.SetProgressBar(progress, total);
        
    }
    public virtual void AddInstantiatedGameObject(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return;
        }

        scatteredRawMaterials.Add(gameObject);
    }

    public virtual void EnableRawMaterialsColliders()
    {
        foreach (var gameobject in scatteredRawMaterials)
        {
            if (gameobject.TryGetComponent<Collider2D>(out Collider2D collider2D))
            {
                collider2D.enabled = true;
            }
        }
    }

    public virtual void DeleteRawMaterials(float factor)
    {
        float counter = scatteredRawMaterials.Count * factor;
        for (int i = 0; i < (int)counter; i++)
        {
            GameObject scatterItem = scatteredRawMaterials[i];
            Destroy(scatterItem);
        }
    }


    public virtual bool ScatterRawMaterials(float factor)
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

