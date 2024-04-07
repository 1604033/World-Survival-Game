using System;
using BuildingSystem.Models;
using Unity.VisualScripting;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingPlacer : MonoBehaviour
    {
        public event Action ActiveBuildableChanged;
        public static event Action OnBuildInputClicked;
        [field: SerializeField] public BuildableItem ActiveBuildingItem { get; private set; }
        [SerializeField] private float _maxbuildingarea = 1f;
        [SerializeField] private ConstructionLayer _constructionLayer;
        private bool canShowPreview = false;
        [SerializeField] private PreviewLayer preview;
        public Camera mainCamera;
        public Vector3 playerOffset = new(.5f, .5f, .5f);
        bool canBuild;
        private Vector3 previousHighlightedPosition;
        [SerializeField]private BuildingTilemap buildingTilemap;

        private void Update()
        {
            if (
                _constructionLayer == null
            )
            {
                preview.ClearPreview();
                return;
            }

            ;
            if (_constructionLayer == null) return;
            if (ActiveBuildingItem == null || !canBuild) return;
            var playerPosition = gameObject.transform.position;
            var mousePositionWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 previewPosition = preview.GetSpritePreviewPosition();
            bool isAreaEmpty = ActiveBuildingItem.occupiesMoreThanOneTile 
            
                ? (
                    // _constructionLayer.IsAreaBlockEmpty(previewPosition,  ActiveBuildingItem ) &&
                    buildingTilemap.IsAreaEmpty(previewPosition,  ActiveBuildingItem.collisionVector ) &&
                    _constructionLayer.IsAreaEmptyNoGameObjects(previewPosition, ActiveBuildingItem.collisionVector, ActiveBuildingItem.TileOffset)
                )
                : (
                    _constructionLayer.IsAreaEmpty(previewPosition) &&
                    _constructionLayer.IsAreaEmptyNoGameObjects(previewPosition, new Vector2(1f, 1f), ActiveBuildingItem.TileOffset)
                );

            if (ActiveBuildingItem.occupiesMoreThanOneTile)
            {
                preview.ShowPreview(ActiveBuildingItem, playerPosition, mousePositionWorld,
                    isAreaEmpty);
            }
            else
            {
                preview.ShowPreview(ActiveBuildingItem, playerPosition, mousePositionWorld,
                    isAreaEmpty);
            }


            if (Input.GetMouseButtonDown(0))
            {
                OnBuildInputClicked?.Invoke();
                if (canBuild && isAreaEmpty)
                {
                    _constructionLayer.Build(previewPosition, ActiveBuildingItem);
                    if (ActiveBuildingItem.category == BuildingCategory.Basic ||
                        ActiveBuildingItem.category == BuildingCategory.Advanced)
                    {
                        canBuild = false;
                    }

                    ActiveBuildingItem = null;
                    preview.ClearPreview();
                    
                }
            }

            previousHighlightedPosition = previewPosition;
        }

        public void SetActiveBuildable(BuildableItem buildableItem)
        {
            ActiveBuildingItem = buildableItem;
            _constructionLayer.activeBuildingItem = buildableItem;
            ActiveBuildableChanged?.Invoke();
            canBuild = true;
        }

        public void SetCanBuild(bool _canBuild)
        {
            canBuild = _canBuild;
        }
    }
}