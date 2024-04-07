using System;
using System.Collections;
using System.Collections.Generic;
using BuildingSystem.Models;
using QuestSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemManager itemManager;
    public UI_Manager uiManager;
    public QuestManager QuestManager;
    public DialogueManager DialogueManager;
    public Player player;
    public Transform canvas;
    public Transform testCanvasTransform;
    public GameObject ItemPopUpPrefab;
    private GameObject currentdescriptionbox = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        itemManager = GetComponent<ItemManager>();
        uiManager = GetComponent<UI_Manager>();

        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        inventory = player.inventory.backpack;

    }

    public void DisplayDescriptionBox(string itemname, string itemdescription, Vector2 buttonPos)
    {
        if(currentdescriptionbox != null)
        {
            Destroy(currentdescriptionbox.gameObject);
        }
        currentdescriptionbox = Instantiate(ItemPopUpPrefab,buttonPos,Quaternion.identity,canvas);
        //currentdescriptionbox.GetComponent<ItemPopUp>().SetUp(itemname, itemdescription);
    }

    public void DestroyDescriptionBox()
    {
        if(currentdescriptionbox != null)
        {
            Destroy(currentdescriptionbox.gameObject);
        }
    }

    public TilemapManager TilemapManager;
    public Inventory.Slot activeSlot;
    public Inventory inventory;
    public TabsNavigation tabNavigation;
}
