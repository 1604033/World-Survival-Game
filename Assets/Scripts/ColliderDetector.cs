using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
    private string treeTag = "Tree";
    // HashSet<Collider2D> collidersInRange = new HashSet<Collider2D>();

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(treeTag) && gameObject.activeSelf )
        {
            if(other.TryGetComponent(out ItemHighlight itemHighlight))
            {
                itemHighlight.HideHighlight();
                // if (collidersInRange.Contains(other)) collidersInRange.Remove(other);
               // Debug.Log("The colliders in range are:" + collidersInRange.Count);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(treeTag) && gameObject.activeSelf )
        {
            if(other.TryGetComponent(out ItemHighlight itemHighlight))
            {
                itemHighlight.ShowHighlight();
                // collidersInRange.Add(other);
                Debug.Log("The colliders in range are:" + itemHighlight);
            }
        }
    }
}
