using BuildingSystem;
using BuildingSystem.Models;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Building_system
{
    public class CollisonLayer : TilemapLayer
    {

        [SerializeField] private TileBase _collisionTileBase;

        public void SetCollision(Buildable buildable, bool value)
        {
            var tile = value ? _collisionTileBase : null;
            buildable.IterateCollisionSpace(tileCoords => _tilemap.SetTile(tileCoords, tile));
        }
    }
}
