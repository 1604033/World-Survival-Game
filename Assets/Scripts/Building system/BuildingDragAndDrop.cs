using UnityEngine;
using BuildingSystem;
using BuildingSystem.Models;
public class BuildingDragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startPosition;
    private Vector3 offset;
    private BuildingPlacer buildingPlacer;
    private PreviewLayer previewLayer;

    public delegate void DragStartHandler();
    public event DragStartHandler OnDragStart;

    public delegate void DragEndHandler();
    public event DragEndHandler OnDragEnd;

    void Start()
    {
        buildingPlacer = FindObjectOfType<BuildingPlacer>();
        previewLayer = FindObjectOfType<PreviewLayer>();
        OnDragStart += HandleDragStart;
        OnDragEnd += HandleDragEnd;
    }

    void OnMouseDown()
    {
        isDragging = true;
        startPosition = transform.position;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        if (OnDragStart != null)
        {
            OnDragStart.Invoke();
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z) + offset;

            if (TryGetComponent<Building>(out Building _building))
            {
               if (_building.buildableItem != null)
               {
                Debug.Log("Building method called");
                previewLayer.ShowPreviewMove(_building.buildableItem, mousePosition, true);
                //buildingPlacer.PlaceBuildingItem(_building.buildableItem, mousePosition );
               }
      
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (OnDragEnd != null)
        {
            OnDragEnd.Invoke();
        }
    }

    void HandleDragStart()
    {
        Debug.Log("Drag started");
    }

    void HandleDragEnd()
    {
        Debug.Log("Drag ended");
    }
}

