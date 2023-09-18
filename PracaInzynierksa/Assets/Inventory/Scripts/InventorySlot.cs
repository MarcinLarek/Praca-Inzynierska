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
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (inventoryItem != null)
        {
            if (
            (inventoryManager.isPlayerInventory && inventoryItem.gameObject.GetComponent<ItemInfo>().owned)
            ||
            (inventoryManager.isBarterInventory)
            ||
            (inventoryManager.isMerchantInventory && inventoryItem.gameObject.GetComponent<ItemInfo>().owned == false)
           )
            {
                if (transform.childCount == 0)
                {
                    inventoryItem.parentAfterDrag = transform;

                }
            }
            else
            {
                Debug.Log("Wrong Inventory");
            }
        }      
    }
}
