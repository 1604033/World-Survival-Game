using System.Collections.Generic;
using Building_system;
using BuildingSystem.Models;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BuildingSystem
{
    public class PreviewLayer : TilemapLayer
    {
        [SerializeField] private BuildingTilemap BuildingTilemap;

        [SerializeField] private SpriteRenderer _previewSpriteRenderer;
        [SerializeField] private SpriteRenderer _buildingPreviewSpriteRenderer;
        [SerializeField] private GameObject monsterPreviewGameObject;
        [SerializeField] private GameObject loggingCampTreeDetectionArea;

        private Vector3 _playerMoveDirection;
        [SerializeField] private Color highlightColor = new Color(0.22f, 0.686f, 0.863f, 1f);
        [SerializeField] private Tilemap _tilemap;

        private Dictionary<Vector3Int, Color> initialColorAndTile = new Dictionary<Vector3Int, Color>();
        private Vector3 InitialMousePosition;
        private string treeTag = "Tree";
        [SerializeField] private float loggingCampdetectionRadius = 7f;


        private Collider2D[] previouslyHighlightedGameObjects;

        private Camera mainCamera;
//Grid
        [SerializeField] float gridCubeWidth;
        [SerializeField] private float gridCubeHeight;

        private Vector3 boundsCenter;
        private Vector3 boundsSize;

        private Vector3 highlightedPositionCenter;

        void Start()
        {
            BuildingPlacer.OnBuildInputClicked += BuildInputClicked;
            mainCamera = Camera.main;
        }

        private void BuildInputClicked()
        {
            UndoHighlightTiles();
            ClearPreview();
        }

        public void ShowPreview(BuildableItem item, Vector3 worldPosition, Vector3 mousePosition, bool isvalid,
            Transform buildingCenter = null)
        {
            if (InitialMousePosition == mousePosition
               )
            {
                return;
            }

            bool canShowPreview = CanShowPreview(item);
            var position = _buildingPreviewSpriteRenderer.gameObject.transform.position;

            // if (canShowPreview)
            if (item != null)
            {
                if (item.category == BuildingCategory.Basic || item.category == BuildingCategory.Advanced)
                {
                    _previewSpriteRenderer.enabled = false;
                    monsterPreviewGameObject.SetActive(false);
                    // var mouseVector2d = new Vector3(mousePosition.x, mousePosition.y, 0f);
                    // mouseVector2d = Vector3Int.RoundToInt(mouseVector2d);
                    _buildingPreviewSpriteRenderer.enabled = true;
                    // _buildingPreviewSpriteRenderer.gameObject.transform.position = mouseVector2d;
                    _buildingPreviewSpriteRenderer.sprite = item.previewSprite;
                    _buildingPreviewSpriteRenderer.color =
                        isvalid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
                }
                else
                {
                    _buildingPreviewSpriteRenderer.enabled = false;
                    _previewSpriteRenderer.enabled = true;
                    monsterPreviewGameObject.SetActive(true);
                    // _previewSpriteRenderer.transform.position = _tilemap.WorldToCell(coords) + _tilemap.cellSize / 2f;
                    GetPositionToDisplayPreview(worldPosition, item.OffsetFromPlayer4D);
                    //_previewSpriteRenderer.gameObject.LookAt(Camera.main.transform.position);
                    _previewSpriteRenderer.sprite = item.previewSprite;
                    _previewSpriteRenderer.color = isvalid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
                }
            }
            else
            {
                ClearPreview();
            }

            if (item.Type == BuildingType.Logging)
            {
                loggingCampTreeDetectionArea.SetActive(true);

                // LoggingCampShowRadar(_buildingPreviewSpriteRenderer.transform.position);  
            }

            InitialMousePosition = mousePosition;
        }

        public Vector3 GetSpritePreviewPosition()
        {
            if (_previewSpriteRenderer.enabled)
            {
                return _previewSpriteRenderer.transform.position;
            }

            return _buildingPreviewSpriteRenderer.transform.position;
        }


        public void ShowPreviewMove(BuildableItem item, Vector3 worldPosition, bool isvalid)
        {
            var coords = _tilemap.WorldToCell(worldPosition);
            _previewSpriteRenderer.enabled = true;
            _previewSpriteRenderer.transform.position = _tilemap.WorldToCell(coords) + _tilemap.cellSize / 2f;
            _previewSpriteRenderer.sprite = item.previewSprite;
            _previewSpriteRenderer.color = isvalid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        }

        public bool HighlightTiles(Vector3 mousePos, BuildableItem activeBuildingItem)
        {
            Vector2Int offset = activeBuildingItem.collisionVector;

            Vector3Int mouseGridPos =
                _tilemap.WorldToCell(_buildingPreviewSpriteRenderer.gameObject.transform.position +
                                     activeBuildingItem.TileOffset);
            if (activeBuildingItem != null && !activeBuildingItem.needsAnotherBuildingToConstruct)
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
                        Vector3Int tilePos = new Vector3Int(x, y, mouseGridPos.z);
                        _tilemap.SetTileFlags(tilePos, TileFlags.None);
                        Color tileColor = _tilemap.GetColor(tilePos);
                        _tilemap.SetColor(tilePos, highlightColor);
                        if (!initialColorAndTile.ContainsKey(tilePos))
                        {
                            initialColorAndTile.Add(tilePos, tileColor);
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

        private void CalculateGrid(Vector3 mousePosition)
        {
            // Convert mouse position to world position
            Vector3 worldPosition =mainCamera.ScreenToWorldPoint(mousePosition);

            // Snap to grid
            Vector3Int gridPosition = _tilemap.WorldToCell(worldPosition);
            Vector3 snappedPosition = _tilemap.CellToWorld(gridPosition);

            // Align the SpriteRenderer
            Vector3 spriteSize = _buildingPreviewSpriteRenderer.bounds.size;
            Vector3 alignedPosition = snappedPosition + new Vector3(spriteSize.x / 2, spriteSize.y / 2, 0);

            // Update the GameObject's position
            _buildingPreviewSpriteRenderer.transform.position = alignedPosition;
        }
        void UndoHighlightTiles()
        {
            // foreach (var CoordColor in initialColorAndTile)
            // {
            //     _tilemap.SetTileFlags(CoordColor.Key, TileFlags.None);
            //     _tilemap.SetColor(CoordColor.Key, CoordColor.Value);
            // }
            //BuildingTilemap.UnhighlightBlock();
        }


        public void ClearPreview()
        {
            if (_previewSpriteRenderer != null) _previewSpriteRenderer.enabled = false;
            if (_buildingPreviewSpriteRenderer != null) _buildingPreviewSpriteRenderer.enabled = false;
            if (loggingCampTreeDetectionArea != null) loggingCampTreeDetectionArea.SetActive(false);
        }

        void GetPositionToDisplayPreview(Vector3 playerPosition, Vector4 offset)
        {
            //GetPositionToDisplayMonster(playerPosition, offset);
            var objectPosition = offset * 2;
            _playerMoveDirection = _playerMovement.direction;
            if (_playerMoveDirection.y == 1)
            {
                _previewSpriteRenderer.gameObject.transform.position =
                    playerPosition + new Vector3(0f, objectPosition.w, 0f);
            }

            if (_playerMoveDirection.y == -1)
            {
                _previewSpriteRenderer.gameObject.transform.position =
                    playerPosition + new Vector3(0f, -objectPosition.z, 0f);
            }

            if (_playerMoveDirection.x == -1)
            {
                _previewSpriteRenderer.gameObject.transform.position =
                    playerPosition + new Vector3(-objectPosition.x, 0F, 0f);
            }

            if (_playerMoveDirection.x == 1)
            {
                _previewSpriteRenderer.gameObject.transform.position =
                    playerPosition + new Vector3(objectPosition.y, 0f, 0f);
            }
        }

        // void GetPositionToDisplayMonster(Vector3 playerPosition, Vector4 offset)
        // {
        //     _playerMoveDirection = _playerMovement.direction;
        //     if (_playerMoveDirection.y == 1)
        //     {
        //         monsterPreviewGameObject.transform.position = playerPosition + new Vector3(0f, offset.w, 0f);
        //     }
        //
        //     if (_playerMoveDirection.y == -1)
        //     {
        //         monsterPreviewGameObject.transform.position = playerPosition + new Vector3(0f, -offset.z, 0f);
        //     }
        //
        //     if (_playerMoveDirection.x == -1)
        //     {
        //         monsterPreviewGameObject.transform.position = playerPosition + new Vector3(-offset.x, 0F, 0f);
        //     }
        //
        //     if (_playerMoveDirection.x == 1)
        //     {
        //         monsterPreviewGameObject.transform.position = playerPosition + new Vector3(offset.y, 0f, 0f);
        //     }
        // }

        // void LoggingCampShowRadar(Vector3 position)
        // {
        //
        //     // foreach (Collider2D collider in previouslyHighlightedGameObjects)
        //     // {
        //     //     GameObject detectedObject = collider.gameObject;
        //     //     if (detectedObject.CompareTag(treeTag))
        //     //     {
        //     //         if(detectedObject.TryGetComponent(out ItemHighlight itemHighlight))
        //     //         {
        //     //             itemHighlight.HideHighlight();
        //     //         }
        //     //     }
        //     // }
        //
        //     // previouslyHighlightedGameObjects = colliders;
        //
        //
        //     Collider2D[] colliders = Physics2D.OverlapCircleAll(position, loggingCampdetectionRadius);
        //     foreach (Collider2D collider in colliders)
        //     {
        //         GameObject detectedObject = collider.gameObject;
        //         if (detectedObject.CompareTag(treeTag))
        //         {
        //             if(detectedObject.TryGetComponent(out ItemHighlight itemHighlight))
        //             {
        //                 itemHighlight.ShowHighlight();
        //                 Debug.Log("Detected object: " + detectedObject.name);
        //
        //             }
        //         }
        //     }
        //
        //    
        // }
        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawWireSphere(_buildingPreviewSpriteRenderer.transform.position, loggingCampdetectionRadius);
        // }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(boundsCenter, boundsSize);
        }
    }
}