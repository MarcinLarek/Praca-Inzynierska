using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler

{
    public Image image;
    public Color selectedColor, notSelectedColor;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryManager inventoryManager = GetComponentInParent<InventoryManager>();
        if (inventoryManager.isPlayerInventory || inventoryManager.isBarterInventory)
        {
            if (transform.childCount == 0)
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

                //AddToList(inventoryItem);

                if (inventoryItem != null)
                {
                    inventoryItem.parentAfterDrag = transform;
                }
            }
        }
        else
        {
            Debug.Log("Wrong Inventory");
        }
        
    }

    private void AddToList(InventoryItem inventoryItem)
    {
        InventoryManager SlotinventoryManager = GetComponentInParent<InventoryManager>();
        SlotinventoryManager.itemsList.Add(inventoryItem.gameObject);
    }

}
