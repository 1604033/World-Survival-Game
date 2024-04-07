using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace BuildingSystem.Models
{
    [CreateAssetMenu(menuName = "Building/New Building Item", fileName = "New building item")]
    public class BuildableItem : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        [SerializeField] private Vector2 TemporaryVectorCollision;
        [FormerlySerializedAs("Type")] [SerializeField]
        public BuildingCategory category;

        [SerializeField] public BuildingType Type;
        [SerializeField] public List<TileBase> specificTilesToBePlacedUpon;

        public bool has2NdStageGameObject;
        public bool hasInteriorGameObject;
        public bool needsAnotherBuildingToConstruct;
        public bool canOnlyBeOneInstance;
        public bool canBePlacedInSpecificTiles;


        public GameObject baseBuilding;
        public List<int> itemsNeedCounts;
        public List<ItemData> itemsDatasNeededToConstruct;
        [field: SerializeField] public TileBase Tile { get; private set; }
        [field: SerializeField] public Sprite previewSprite { get; private set; }
        [field: SerializeField] public Sprite UIicon { get; private set; }

        [field: SerializeField] public GameObject GameObject { get; private set; }

        [field: SerializeField] public Vector3 OffsetFromPlayer { get; private set; }

        [field: SerializeField] public Vector4 OffsetFromPlayer4D { get; private set; }

        [field: SerializeField] public Vector3 TileOffset { get; private set; }

        [field: SerializeField] public bool occupiesMoreThanOneTile { get; private set; }

        [field: SerializeField] public RectInt collisionSpace { get; private set; }

        [field: SerializeField] public Vector2Int collisionVector { get; private set; }
    }

    public enum BuildingCategory
    {
        Basic,
        Advanced,
        Auxiliary,
    }

    public enum BuildingType
    {
        Cabin,
        Shelter,
        Mine,
        Logging,
        FoodStockPile,
        PowerPlant,
        StockPile
    }
}