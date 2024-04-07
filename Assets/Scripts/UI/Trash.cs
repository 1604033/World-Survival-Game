using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Trash : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    public GameObject AlertUI;
    private Text textToModify;
    public Sprite trash_closed;
    public Sprite trash_opened;

    GameObject draggedItem
    {
        get
        {
            return null;
        }
    }
    GameObject itemToBeDelete;
    public string itemName
    {
        get
        {
            string name = itemToBeDelete.name;
            string toRemove = "(Clone)";
            string result = name.Replace(toRemove, "");
            return result;
        }
    }
    void Start()
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        itemToBeDelete = draggedItem.gameObject;
    }

    // Update is called once per frame
    
}
