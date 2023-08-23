using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Item item;
    [Header("UI")]
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    private void Start()
    {
        InitialiseItem(item);
    }

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
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
    }
    public void OnItemClick()
    {
        Debug.Log("TEST");
    }
}