using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterInventorySlot : MonoBehaviour, IDropHandler
{ 
    public bool isWeaponSlot;
    public bool isArmorSlot;
    public bool isConsumableSlot;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (transform.childCount < 1)
        {
            ItemInfo inventoryItemInfo = inventoryItem.gameObject.GetComponent<ItemInfo>();
            switch (inventoryItemInfo.type)
            {
                case ItemInfo.ItemType.Weapon:
                    if (isWeaponSlot)
                    {
                        HandleDrop(inventoryItem);
                    }
                    break;
                case ItemInfo.ItemType.Armor:
                    if (isArmorSlot)
                    {
                        HandleDrop(inventoryItem);
                    }
                    break;
                case ItemInfo.ItemType.Consumable:
                    if (isConsumableSlot)
                    {
                        HandleDrop(inventoryItem);
                    }
                    break;
                default:
                    Debug.Log("Can't eqiup this item");
                    break;
            }

        }
    }

    private void HandleDrop(InventoryItem inventoryItem)
    {
        inventoryItem.parentAfterDrag = transform;
        CharacterInventoryManager.GetInstance().EquipItem(inventoryItem.gameObject);
    }

}
