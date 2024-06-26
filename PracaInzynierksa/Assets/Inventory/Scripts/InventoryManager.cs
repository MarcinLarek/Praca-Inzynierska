
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public List<GameObject> itemsList;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryItemPrefabWeapon;
    public GameObject inventoryItemPrefabArmor;
    public GameObject inventoryItemPrefabConsumable;
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
            LoadTraderItemsFromGameHandler();
        }
        else if (isBarterInventory)
        {

        }
        
    }

    private void LoadFromGameHandler()
    {
        InventoryHandler inventoryInstance = InventoryHandler.GetInstance();
        if(inventoryInstance != null)
        {
            foreach (GameObject item in inventoryInstance.inventoryItems)
            {
                if (!item.GetComponent<ItemInfo>().equiped)
                {
                    AddItem(item);
                }
            }
        }
    }

    private void LoadTraderItemsFromGameHandler()
    {
        InventoryHandler inventoryInstance = InventoryHandler.GetInstance();
        if (inventoryInstance != null)
        {
            foreach (GameObject item in inventoryInstance.traderItems)
            {
                AddItem(item);
            }
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
        GameObject itemtospawn;
        switch (item.GetComponent<ItemInfo>().type)
        {
            case ItemInfo.ItemType.Weapon:
                itemtospawn = inventoryItemPrefabWeapon;
                break;
            case ItemInfo.ItemType.Armor:
                itemtospawn = inventoryItemPrefabArmor;
                break;
            case ItemInfo.ItemType.Consumable:
                itemtospawn = inventoryItemPrefabConsumable;
                break;
            default:
                itemtospawn = inventoryItemPrefab;
                break;
        }
        itemtospawn.GetComponent<ItemInfo>().AssignStats(item.GetComponent<ItemInfo>());

        GameObject newItemGo = Instantiate(itemtospawn, slot.transform);
        itemsList.Add(newItemGo);

        if (isMerchantInventory)
        {
            TradeManager.GetInstance().merchantGeneratedItems.Add(newItemGo);
        }

    }
}

