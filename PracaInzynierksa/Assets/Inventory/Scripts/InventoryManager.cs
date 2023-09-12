
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public List<GameObject> itemsList;
    public GameObject inventoryItemPrefab;
    public bool isPlayerInventory;
    public bool isMerchantInventory;
    public bool isBarterInventory;

    private void Awake()
    {
        if (isPlayerInventory)
        {
            LoadFromGameHandler();
        }
        else if (isMerchantInventory)
        {

        }
        else if (isBarterInventory)
        {

        }
        
    }

    private void LoadFromGameHandler()
    {
        foreach( GameObject item in InventoryHandler.GetInstance().inventoryItems)
        {
            AddItem(item);
        }
    }

    public void AddItem(GameObject item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                Debug.Log("spawned");
                return;
            }
        }
    }
    void SpawnNewItem(GameObject item, InventorySlot slot)
    {
        GameObject itemtospawn = inventoryItemPrefab;
        itemtospawn.GetComponent<ItemInfo>().AssignStats(item.GetComponent<ItemInfo>());

        GameObject newItemGo = Instantiate(itemtospawn, slot.transform);
        itemsList.Add(newItemGo);

    }
}

