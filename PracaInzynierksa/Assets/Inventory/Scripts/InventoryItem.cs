using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    private void Awake()
    {
        Image iconimage = this.GetComponent<Image>();
        iconimage.sprite = this.GetComponent<ItemInfo>().image;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        RemoveFromList(this.gameObject);
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        Debug.Log("Begin");
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        Debug.Log("Dragging");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        AddToList(parentAfterDrag.gameObject);
    }
    public void OnItemClick()
    {
        Debug.Log("TEST");
    }

    private void AddToList(GameObject inventoryItem)
    {
        InventoryManager SlotinventoryManager = inventoryItem.GetComponentInParent<InventoryManager>();
        SlotinventoryManager.itemsList.Add(this.gameObject);
    }

    private void RemoveFromList(GameObject inventoryItem)
    {
        InventoryManager InventoryManager = GetComponentInParent<InventoryManager>();
        InventoryManager.itemsList.Remove(inventoryItem);
    }
}