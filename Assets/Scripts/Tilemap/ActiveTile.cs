using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActiveTile : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject activeIcon;
    private Vector3Int tilePos;
    public TilemapManager tilemapManager;
    private Tilemap cropsTilemap;
    public float maxInteractionDistance = 10.05f;
    private Camera mainCamera;

    private void Awake()
    {
        cropsTilemap = tilemapManager.cropsTileMap;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        bool distanceAllowed = Vector3.Distance(mousePos, GameManager.instance.player.transform.position) >
                               maxInteractionDistance;
        if (tilemap == null || distanceAllowed || !tilemap.HasTile(tilemap.WorldToCell(mousePos)))
        {
            activeIcon.SetActive(false);
            return;
        }

        tilePos = tilemap.WorldToCell(mousePos);
        var tileCenter = tilemap.GetCellCenterWorld(tilePos);
        activeIcon.SetActive(true);
        activeIcon.transform.position = tileCenter + new Vector3(0, 0f, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            Toolbar_UI toolbarUI = GameManager.instance.uiManager.ToolbarUI;
            Collider2D[] colliders = Physics2D.OverlapPointAll(activeIcon.transform.position);
            foreach (var collider in colliders)
            {
                {
                    if (collider.gameObject.TryGetComponent(out CropBehaviour cropBehaviour))
                    {
                        if (toolbarUI != null)
                        {
                            if (toolbarUI.selectedSlotIndex == 1)
                            {
                                {
                                    cropBehaviour.WaterCrop(tilePos);
                                }
                            }

                            if (toolbarUI.selectedSlotIndex == 2)
                            {
                                {
                                    cropBehaviour.Harvest();
                                }
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D collider = Physics2D.OverlapPoint(activeIcon.transform.position);
            if (collider != null)
            {
                GameObject gameObject = collider.gameObject;
                CropBehaviour cropBehaviour = gameObject.GetComponent<CropBehaviour>();
                cropBehaviour.FertilizeCrop(tilePos);
            }
        }
    }

    private void OnMouseEnter()
    {
        activeIcon.SetActive(true);
        CropBehaviour cropBehaviour = gameObject.GetComponent<CropBehaviour>();
        cropBehaviour.FertilizeCrop(tilePos);
        Debug.Log("mouse enter");
    }

    private void OnMouseExit()
    {
        activeIcon.SetActive(false);
    }
}