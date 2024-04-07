using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  public GameObject defaultPopupPrefab; // Assign the default popup prefab in the Unity editor

    private GameObject currentPopup;

    // Singleton pattern for easy access
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("UIManager");
                    instance = go.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    public void ShowPopup(GameObject popupPrefab)
    {
        // Close any existing popup before showing a new one
        ClosePopup();

        // Instantiate and show the new popup
        currentPopup = Instantiate(popupPrefab, Vector3.zero, Quaternion.identity);
        currentPopup.transform.SetParent(transform, false);
    }

    public void ClosePopup()
    {
        // Close the current popup if it exists
        if (currentPopup != null)
        {
            Destroy(currentPopup);
        }
    }
}
