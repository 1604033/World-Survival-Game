using System;
using Building_system.Extenstions;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace BuildingSystem.Models
{
    [Serializable]
    public class Buildable
    {
        [field: SerializeField] public Tilemap parentTilemap { get; private set; }
        [field: SerializeField] public BuildableItem BuildableItem { get; private set; }

        [field: SerializeField] public GameObject GameObject { get; private set; }

        [field: SerializeField] public Vector3Int Coordinates { get; private set; }

        public Buildable(BuildableItem item, Vector3Int coords, Tilemap tilemap, GameObject obj = null)
        {
            parentTilemap = tilemap;
            BuildableItem = item;
            Coordinates = coords;
            GameObject = obj;
        }

        public void Destroy()
        {
            if (GameObject != null)
            {
                Object.Destroy(GameObject);
            }
            parentTilemap.SetTile(Coordinates, null);
        }

        public void IterateCollisionSpace(RectInExtenstions.RectAction action)
        {
            BuildableItem.collisionSpace.Iterate(Coordinates, action);
        } public bool IterateCollisionSpace(RectInExtenstions.RectActionBool actionBool)
        {
            return BuildableItem.collisionSpace.Iterate(Coordinates, actionBool);
        }
    }
}
