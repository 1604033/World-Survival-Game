using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField]private Collider2D _collider2D;
    [SerializeField]private List<Interactable> interactablesInRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactablesInRange.ForEach((intractable) =>
                intractable.Interact()
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable[] intractables = other.GetComponents<Interactable>();
        foreach (var intractable in intractables)
        {
            if (!interactablesInRange.Contains(intractable))
                interactablesInRange.Add(intractable);  
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable[] intractables = other.GetComponents<Interactable>();
        foreach (var intractable in intractables)
        {
            if (interactablesInRange.Contains(intractable))
                interactablesInRange.Remove(intractable);  
        }
    }
}