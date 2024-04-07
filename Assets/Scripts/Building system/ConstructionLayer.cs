using System;
using System.Collections;
using System.Collections.Generic;
using Building_system;
using Building_system.Extenstions;
using BuildingSystem.Models;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace BuildingSystem
{
    public class ConstructionLayer : TilemapLayer

    {
        public Action OnBuildingStart;
        private Dictionary<Vector3Int, Buildable> _buildables = new();
        private List<Vector3Int> coordinates = new();
        [SerializeField] private TileBase fenceTile;
        [SerializeField] private TileBase gateTile;
        private Vector3 previousMousePosition;

        private Vector3 detectionCenter;
        private Vector3 boxSize;
        private Vector2 boundsSize;
        private float rawMaterialsOffset = 0.7f;
        [SerializeField] private CollisonLayer _collisonLayer;
        private BuildingBase activeBuilding;
        public BuildableItem activeBuildingItem;
        [SerializeField] Tilemap _backgroundTilemap;
        private Vector2 boundsCenter;
        private string allowedColliderTag = "AllowedCollider";

        public void Build(Vector3 worldPosition, BuildableItem item)
        {
            GameObject itemObj;
            if (item != null)
            {
                activeBuildingItem = item;
                var coords = _tilemap.WorldToCell(worldPosition);
                if (item.Tile != null)
                {
                    List<ItemData> data = new List<ItemData>();
                    List<int> count = new List<int>();
                    Tiledata tiledata = _tilemapManager.GetTileItemData(item.Tile);
                    if (tiledata != null)
                    {
                        data.AddRange(item.itemsDatasNeededToConstruct);
                        count.AddRange(item.itemsNeedCounts);
                        bool isRemoved = _inventory.RemoveItemsFromSlotWithConfirmation(data, count);
                        if (isRemoved)
                        {
                            _tilemap.SetTile(coords, item.Tile);

                            if (item.occupiesMoreThanOneTile)
                            {
                                SetBlockOfTilesOccupied(coords, item.collisionVector, null);
                            }
                            else
                            {
                                SetTileOccupied(coords, null);
                            }
                        }
                    }
                }

                if (item.GameObject != null)
                {
                    //if (item.GameObject.TryGetComponent<BuildingBase>(out BuildingBase BuildingBase))
                    {
                        {
                            bool isRemoved =
                                _inventory.RemoveItemsFromSlotWithConfirmation(item.itemsDatasNeededToConstruct,
                                    item.itemsNeedCounts);
                            if (isRemoved)
                            {
                                itemObj = Instantiate(item.GameObject, coords , quaternion.identity);
                                if (itemObj.TryGetComponent(out BuildingBase instantiatedBuilding))
                                {
                                    BuildingsManager.Instance.AddBuilding(instantiatedBuilding, item.Type);
                                    BuildingsManager.Instance.AddBuildingUnderConstruction(instantiatedBuilding);
                                    instantiatedBuilding.constructionStep = 1;
                                    if (instantiatedBuilding.monsterToConstruct != null)
                                    {
                                        Vector3 position = _player.transform.position +
                                                           new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),
                                                               0f);
                                        //
                                        GameObject builder = Instantiate(instantiatedBuilding.monsterToConstruct,
                                            position,
                                            Quaternion.identity);
                                        var builderController = builder.GetComponentInChildren<BuilderController>();
                                        if (builderController != null)
                                        {
                                            Debug.Log(
                                                "Builder available"); //builderController.Move(instantiatedBuilding.buildingTransformPoints[0].position);
                                            builderController.MoveBuilderAcrossPoints(
                                                instantiatedBuilding.buildingTransformPoints, instantiatedBuilding);
                                        }
                                    }

                                    if (instantiatedBuilding.scatterPoint != null)
                                    {
                                        ScatterRawMaterials(item.itemsDatasNeededToConstruct,
                                            item.itemsNeedCounts,
                                            instantiatedBuilding.scatterPoint.position, instantiatedBuilding);
                                        Debug.Log($"Instantiated BuildingBase is: " + instantiatedBuilding);
                                    }

                                    if (item.occupiesMoreThanOneTile)
                                    {
                                        SetBlockOfTilesOccupied(coords, item.collisionVector, instantiatedBuilding);
                                    }
                                    else
                                    {
                                        SetTileOccupied(coords, instantiatedBuilding);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        private bool IsRectOccupied(Vector3Int coordinates, RectInt rect)
        {
            return rect.Iterate(coordinates, tilecoords => _buildables.ContainsKey(tilecoords));
        }

        public void SetBlockOfTilesOccupied(Vector3 mousePos, Vector2Int offset, BuildingBase BuildingBase)
        {
            Vector3Int mouseGridPos = _tilemap.WorldToCell(mousePos);
            for (int xOffset = -offset.x; xOffset < offset.x; xOffset++)
            {
                for (int yOffset = -offset.y; yOffset < offset.y; yOffset++)
                {
                    Vector3Int tilePos = new Vector3Int(mouseGridPos.x + xOffset, mouseGridPos.y + yOffset,
                        mouseGridPos.z);
                    coordinates.Add(tilePos);
                    if (BuildingBase != null) BuildingBase.worldCoordinates.Add(tilePos);
                }
            }
        }

        public void SetTileOccupied(Vector3Int tilePos, BuildingBase BuildingBase)
        {
            if (BuildingBase != null) BuildingBase.worldCoordinates.Add(tilePos);
            coordinates.Add(tilePos);
        }

        public bool IsAreaBlockEmpty(Vector3 mousePos, BuildableItem active)
        {
            Vector2Int offset = active.collisionVector;
            Vector3Int mouseGridPos = _tilemap.WorldToCell(mousePos);

            if (activeBuildingItem != null && !activeBuildingItem.needsAnotherBuildingToConstruct)
            {
                for (int x = mouseGridPos.x - offset.x; x <= mouseGridPos.x + offset.x; x++)
                {
                    for (int y = mouseGridPos.y - offset.y; y <= mouseGridPos.y + offset.y; y++)
                    {
                        Vector3Int tilePos = new Vector3Int(x, y, mouseGridPos.z);
                        if (_backgroundTilemap != null)
                        {
                            // TileBase tile = _backgroundTilemap.GetTile(tilePos);
                            // Debug.Log(" The tile at +  " + tilePos + " is " + tile);
                            // if (active.canBePlacedInSpecificTiles &&
                            //     !activeBuildingItem.specificTilesToBePlacedUpon.Contains(tile))
                            // {
                            //     return false;
                            // }
                        }

                        bool hasATile = coordinates.Contains(tilePos);
                        if (hasATile)
                        {
                            return false;
                        }
                    }
                }
            }

            if (activeBuildingItem != null && activeBuildingItem.needsAnotherBuildingToConstruct)
            {
                Debug.Log($"Method called to check mine");
                Vector3 offsetP = new Vector3(activeBuildingItem.collisionVector.x,
                    activeBuildingItem.collisionVector.y, activeBuildingItem.collisionVector.x);
                boxSize = new Vector3(offsetP.x, offsetP.y, offsetP.x);
                detectionCenter = mousePos;
                Collider2D[] colliders = Physics2D.OverlapBoxAll(mousePos, boxSize / 2, 0f);
                if (activeBuildingItem.Type == BuildingType.Mine)
                {
                    foreach (Collider2D collider in colliders)
                    {
                        GameObject detectedObject = collider.gameObject;
                        if (detectedObject.TryGetComponent(out MinePit minepit))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            return true;
        }

        public bool IsAreaEmptyNoGameObjects(Vector2 mousePos, Vector2 bounds, Vector2 offset)
        {
            Vector2 boundsSize = bounds;
            this.boundsSize = boundsSize;
            Vector2 boundsCenter = mousePos ;
            this.boundsCenter = boundsCenter;
            boundsCenter += offset;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(boundsCenter, boundsSize, 0);
            if (colliders.Length > 2)
            {
                return false;
            }
            return true;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boundsCenter, boundsSize);
        }

        public void ClearConstructedArea(List<Vector3Int> buildingCoordinates)
        {
            foreach (var coord in buildingCoordinates)
            {
                coordinates.Remove(coord);
            }
        }

        public bool IsAreaEmpty(Vector3 mousePosition)
        {
            Vector3Int mouseGridPos = _tilemap.WorldToCell(mousePosition);
            return !coordinates.Contains(mouseGridPos);
        }

        public void ScatterRawMaterials(List<ItemData> itemDatas, List<int> itemCounts, Vector3 coord,
            BuildingBase parentBuilding = null)
        {
            for (int i = 0; i < itemDatas.Count; i++)
            {
                {
                    if (itemDatas[i].stackPrefab != null)
                    {
                        GameObject instantiatedRawMaterial =
                            Instantiate(itemDatas[i].stackPrefab, coord, Quaternion.identity);
                        if (instantiatedRawMaterial.TryGetComponent(out Collider2D collider2D))
                        {
                            collider2D.enabled = false;
                        }

                        if (TryGetComponent(out Collectable collectable))
                        {
                            collectable.IncrementCount(itemCounts[i] - 1, 99);
                        }

                        if (parentBuilding != null) parentBuilding.AddInstantiatedGameObject(instantiatedRawMaterial);
                    }
                }
            }
        }
    }
}