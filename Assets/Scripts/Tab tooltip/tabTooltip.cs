using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tabTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   public GameObject panel; // Reference to the panel containing the text
    public Vector2 offset = new Vector2(10f, 10f); // Offset for the panel position

    void Start()
    {
        // Set the panel to be initially inactive
        panel.SetActive(false);
    }

    // Called when the mouse pointer enters the GameObject's collider
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show the panel when hovering
        panel.SetActive(true);

        // Set the position of the panel based on the button's position
        RectTransform buttonRectTransform = GetComponent<RectTransform>();
        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();

        Vector3 panelPosition = buttonRectTransform.position + new Vector3(offset.x, offset.y, 0f);
        panelRectTransform.position = panelPosition;
    }

    // Called when the mouse pointer exits the GameObject's collider
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the panel when not hovering
        panel.SetActive(false);
    }
}
