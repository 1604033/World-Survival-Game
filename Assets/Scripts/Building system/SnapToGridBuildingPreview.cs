using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Building_system
{
    public class SnapToGridBuildingPreview : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] public SpriteRenderer sr;
        private Vector3 previousMousePos;
        private void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            if (sr.enabled && previousMousePos != mousePos)
            {
                CalculateGrid(mousePos);
                previousMousePos = mousePos;
            }
        }

        public void CalculateGrid(Vector3 mousePosition)
        {
            // Convert mouse position to world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Snap to grid
            Vector3Int gridPosition = _tilemap.WorldToCell(worldPosition);
            Vector3 snappedPosition = _tilemap.CellToWorld(gridPosition);

            // Align the SpriteRenderer
            Vector3 spriteSize = sr.bounds.size;
            Vector3 alignedPosition = snappedPosition + new Vector3(spriteSize.x / 2, 0, 0);

            // Update the GameObject's position
            gameObject.transform.position = alignedPosition;
        }


        public Vector3 GetSpriteCenter()
        {
            // Calculate the center of the sprite
            return sr.bounds.center;

        }

    }
}