using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
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

        TradeManager tradeManager = TradeManager.GetInstance();
        if (tradeManager != null)
        {
            tradeManager.ShowObjectInfo(this.gameObject);
        }

        Debug.Log("Begin");
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        AddToList(parentAfterDrag.gameObject);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        TradeManager tradeManager = TradeManager.GetInstance();
        if(tradeManager != null)
        {
            tradeManager.ShowObjectInfo(this.gameObject);
        }
    }

    private void AddToList(GameObject inventoryItem)
    {
        InventoryManager SlotinventoryManager = inventoryItem.GetComponentInParent<InventoryManager>();
        if (SlotinventoryManager != null)
        {
            SlotinventoryManager.itemsList.Add(this.gameObject);
        }
    }

    private void RemoveFromList(GameObject inventoryItem)
    {
        InventoryManager InventoryManager = GetComponentInParent<InventoryManager>();
        InventoryManager.itemsList.Remove(inventoryItem);
    }
}