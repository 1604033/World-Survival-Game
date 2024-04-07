using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxInteraction : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public float interactionRadius = 2f;
    public GameObject popup; // Reference to the popup UI element
    public GameObject player; // Reference to the player GameObject

    private bool isPlayerInside = false;

    private void Update()
    {
        if (isPlayerInside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Handle the interaction when the player presses the E key
                OpenPopup();
            }
            /*else if(Input.OnPointerClick(PointerEventData eventData))
            {
                OpenPopup();
            }*/
            else if (Input.GetMouseButtonDown(0))
            {
                // Check if the player clicked on the box to show the popup
                CheckForClick();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInside = false;
            ClosePopup();
        }
    }

    private void CheckForClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            // Clicked on the box, show the popup
            OpenPopup();
            Debug.Log("yeah!!Alhamdulillah");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MashaAllah");
    }

    private void OpenPopup()
    {
        if (popup != null)
        {
            popup.SetActive(true);
        }
    }

    private void ClosePopup()
    {
        if (popup != null)
        {
            popup.SetActive(false);
        }
    }
}
