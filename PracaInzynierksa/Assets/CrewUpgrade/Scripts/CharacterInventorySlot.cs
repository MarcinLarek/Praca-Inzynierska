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

        inventoryItem.parentAfterDrag = transform;
        CharacterInventoryManager.GetInstance().EquipItem(inventoryItem.gameObject);
    }

}
