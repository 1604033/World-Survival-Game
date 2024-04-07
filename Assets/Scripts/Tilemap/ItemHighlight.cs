using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHighlight : MonoBehaviour
{
    public GameObject highlightPrefab;
    public GameObject highlightGameObject;
    private bool isHighlighted = false;
    public SpriteRenderer spriteRenderer;

    public ColliderDetectionType detectionType;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        if (!isHighlighted && detectionType == ColliderDetectionType.Mouse)
        {
            ShowHighlight();
        }
    }

    private void OnMouseExit()
    {
        if (isHighlighted && detectionType == ColliderDetectionType.Mouse)
        {
            HideHighlight();
        }
    }

    public void ShowHighlight()
    {
        if (highlightPrefab != null && detectionType == ColliderDetectionType.Mouse)
        {
            GameObject highlight = Instantiate(highlightPrefab, transform.position, Quaternion.identity);
            // You may need to adjust the position and size of the highlight here.
            // Ensure that the highlight is rendered on top of the item.
            highlight.transform.SetParent(transform);
            isHighlighted = true;
            Debug.Log("Item highlighted");
        }

        if (detectionType == ColliderDetectionType.Collider)
        {
            highlightGameObject.SetActive(true);
        }
    }

    public void HideHighlight()
    {
        isHighlighted = false;
        if (detectionType == ColliderDetectionType.Mouse)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Highlight")) // Add a "Highlight" tag to the highlight GameObject.
                {
                    Destroy(child.gameObject);
                }
            }
        }

        if (detectionType == ColliderDetectionType.Collider)
        {
            highlightGameObject.SetActive(false);
        }
    }

    public enum ColliderDetectionType
    {
        Collider,
        Mouse,
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out ColliderDetector col))
        {
            HideHighlight();
        }
    }
}