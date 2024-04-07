using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
 
public class TrashSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
 
    //public ItemData itemdata;
    public Inventory_UI inventoryui;
    //public List<Slot_UI> slots = new List<Slot_UI>();
    public Slot_UI slot;
    public Item item;
    public Player player;
    public Inventory inventory;
    public GameObject trashAlertUI;
    public Text nameText;
    public Text numberText;
 
    private Text textToModify;
 
    public Sprite trash_closed;
    public Sprite trash_opened;
 
    private Image imageComponent;
 
    Button YesBTN, NoBTN;
 
    GameObject draggedItem
    {
        get
        {
            return UI_Manager.draggedIcon.gameObject;
        }
    }
 
    GameObject itemToBeDeleted;
  
 
 
    public string itemName
    {
        get
        {
            string name = inventory.slots[UI_Manager.draggedSlot.slotID].itemName;
            string toRemove = "(Clone)";
            string result = name.Replace(toRemove, "");
            return result;
        }
    }
 
 
 
    void Start()

    {
        inventory = GameManager.instance.inventory;
        imageComponent = transform.Find("background").GetComponent<Image>();
 
        textToModify = trashAlertUI.transform.Find("Text").GetComponent<Text>();
 
        YesBTN = trashAlertUI.transform.Find("yes").GetComponent<Button>();
        YesBTN.onClick.AddListener(delegate { DeleteItem(); });
 
        NoBTN = trashAlertUI.transform.Find("no").GetComponent<Button>();
        NoBTN.onClick.AddListener(delegate { CancelDeletion(); });
 
    }
 
 
    public void OnDrop(PointerEventData eventData)
    {
        //itemToBeDeleted = DragDrop.itemBeingDragged.gameObject;
        Destroy(UI_Manager.draggedIcon.gameObject);
        StartCoroutine(notifyBeforeDeletion());
        Debug.Log("Item name: " + slot.item.itemName);
        /*if (draggedItem.GetComponent<Item>().isTrashable == true)
        {
            itemToBeDeleted = draggedItem.gameObject;
 
            StartCoroutine(notifyBeforeDeletion());
        }*/
        
    }
 
    IEnumerator notifyBeforeDeletion()
    {
        trashAlertUI.SetActive(true);
        nameText.text = inventory.slots[UI_Manager.draggedSlot.slotID].itemName;
        numberText.text = inventory.slots[UI_Manager.draggedSlot.slotID].count.ToString();
        textToModify.text = "Throw away this " + inventory.slots[UI_Manager.draggedSlot.slotID].itemName + "?";
        yield return new WaitForSeconds(1f);
    }
 
    public void CancelDeletion()
    {
        //imageComponent.sprite = trash_closed;
        trashAlertUI.SetActive(false);
    }
 
    public void DeleteItem()
    {
        //imageComponent.sprite = trash_closed;
        //DestroyImmediate(UI_Manager.draggedIcon.gameObject);
        inventoryui.RemovePermanenet(UI_Manager.draggedSlot.slotID);
        //InventorySystem.Instance.ReCalculeList();
        //Inventory_UI.Refresh();
        //CraftingSystem.Instance.RefreshNeededItems();
        trashAlertUI.SetActive(false);
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        {
            Debug.Log("pointer enter" + UI_Manager.draggedSlot.slotID);
            
            //Debug.Log(GameManager.instance.itemManager.GetItemByName(inventory.slots[UI_Manager.draggedSlot.slotID].itemName));
            Debug.Log(inventory.slots[UI_Manager.draggedSlot.slotID].itemName);
            Debug.Log(inventory.slots[UI_Manager.draggedSlot.slotID].count);
        }
        /*if (draggedItem != null && draggedItem.GetComponent<Item>().isTrashable == true)
        {
            imageComponent.sprite = trash_opened;
        }*/
       
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointer exit");
        /*if (draggedItem != null && draggedItem.GetComponent<Item>().isTrashable == true)
        {
            imageComponent.sprite = trash_closed;
        }*/
    }
 
}
