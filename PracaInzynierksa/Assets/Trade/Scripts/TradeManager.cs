using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class TradeManager : MonoBehaviour
{
    public GameObject playerInventory;
    public GameObject merchantInventory;
    public GameObject barterInventory;

    public List<GameObject> merchantItems;
    public GameObject GHItemPrefab;
    public GameObject GHItemPrefabWeapon;
    public GameObject GHItemPrefabArmor;
    public GameObject GHItemPrefabConsumable;

    private static TradeManager instance;
    public List<GameObject> merchantGeneratedItems;
    private InventoryHandler handlerInstance;

    public static TradeManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
        handlerInstance = InventoryHandler.GetInstance();
        if (handlerInstance.traderInventoryGenerated == false)
        {
            SpawnMerchantItems();
            handlerInstance.traderInventoryGenerated = true;
        }
    }

    public int CalculateInventoryValue(GameObject inventory)
    {
        InventoryManager inventoryManager = inventory.GetComponent<InventoryManager>();

        int inventoryValue = 0;
        foreach(GameObject item in inventoryManager.itemsList)
        {
            ItemInfo itemInfo = item.GetComponent<ItemInfo>();
            if (itemInfo.owned)
            {
                inventoryValue += itemInfo.price;
            }
            else
            {
                inventoryValue -= itemInfo.price;
            }
        }
        return inventoryValue;
    }

    public void SpawnMerchantItems()
    {
        int itemsToSpawn = Random.Range(5, 20);
        for(int x = 0; x < itemsToSpawn; x++)
        {
            GameObject itemToSpawn = merchantItems[Random.Range(0, merchantItems.Count)];
            merchantInventory.GetComponent<InventoryManager>().AddItem(itemToSpawn);
        }

        GameObject GHItemSpawn;

        foreach (GameObject iventoryItem in merchantGeneratedItems)
        {
            switch(iventoryItem.GetComponent<ItemInfo>().type)
            {
                case ItemInfo.ItemType.Weapon:
                    GHItemSpawn = GHItemPrefabWeapon;
                    break;
                case ItemInfo.ItemType.Armor:
                    GHItemSpawn = GHItemPrefabArmor;
                    break;
                case ItemInfo.ItemType.Consumable:
                    GHItemSpawn = GHItemPrefabConsumable;
                    break;
                default:
                    GHItemSpawn = GHItemPrefab;
                    break;
            }
            ItemInfo GHItemSpawnInfo = GHItemSpawn.GetComponent<ItemInfo>();
            GHItemSpawnInfo.AssignStats(iventoryItem.GetComponent<ItemInfo>());
            GameObject SpawnedGhItem = Instantiate(GHItemSpawn, new Vector3(0,0), Quaternion.identity);
            InventoryHandler.GetInstance().traderItems.Add(SpawnedGhItem);
        }
    }

    private bool BarterCheck()
    {
        int barterValue = CalculateInventoryValue(barterInventory);
        if (barterValue < 0)
        {
            barterValue *= -1;
            if (barterValue > PlayerInfo.GetInstance().playerMoney)
            {
                return false;
            }
        }
        return true;

    }

    public void AcceptTransaction()
    {
        InventoryManager barterInv = barterInventory.GetComponent<InventoryManager>();
        InventoryManager merchantInv = merchantInventory.GetComponent<InventoryManager>();
        InventoryManager playerInv = playerInventory.GetComponent<InventoryManager>();
        PlayerInfo playerInfo = PlayerInfo.GetInstance();
        if (BarterCheck())
        {
            foreach (GameObject item in barterInv.itemsList)
            {
                GameObject ItemToSpawn;
                ItemInfo itemInfo = item.GetComponent<ItemInfo>();
                switch (itemInfo.type)
                {
                    case ItemInfo.ItemType.Weapon:
                        ItemToSpawn = GHItemPrefabWeapon;
                        break;
                    case ItemInfo.ItemType.Armor:
                        ItemToSpawn = GHItemPrefabArmor;
                        break;
                    case ItemInfo.ItemType.Consumable:
                        ItemToSpawn = GHItemPrefabConsumable;
                        break;
                    default:
                        ItemToSpawn = GHItemPrefab;
                        break;
                }

                if (itemInfo.owned)
                {
                    playerInfo.playerMoney += itemInfo.price;
                    itemInfo.owned = false;

                    GameObject GHItem = InventoryHandler.GetInstance().inventoryItems.
                        Find((x) => x.GetComponent<ItemInfo>().itemId == itemInfo.itemId);
                    InventoryHandler.GetInstance().inventoryItems.Remove(GHItem);
                    Destroy(GHItem);

                    GameObject SpawnedGHItem = Instantiate(ItemToSpawn, new Vector3(0, 0), Quaternion.identity);
                    SpawnedGHItem.GetComponent<ItemInfo>().AssignStats(itemInfo);
                    InventoryHandler.GetInstance().traderItems.Add(SpawnedGHItem);
                    merchantInv.AddItem(SpawnedGHItem);
                    Destroy(item);

                }
                else
                {
                    playerInfo.playerMoney -= itemInfo.price;
                    itemInfo.owned = true;

                    ItemToSpawn.GetComponent<ItemInfo>().AssignStats(item.GetComponent<ItemInfo>());
                    GameObject SpawnedGHItem = Instantiate(ItemToSpawn, new Vector3(0, 0), Quaternion.identity);
                    InventoryHandler.GetInstance().inventoryItems.Add(SpawnedGHItem);

                    GameObject Traderitem = InventoryHandler.GetInstance().traderItems.
                        Find((x) => x.GetComponent<ItemInfo>().itemId == itemInfo.itemId);
                    InventoryHandler.GetInstance().traderItems.Remove(Traderitem);
                    Destroy(Traderitem);

                    playerInv.AddItem(ItemToSpawn);
                    Destroy(item);

                }
            }
            barterInv.itemsList.Clear();
        }
        else
        {
            Debug.Log("You don't have enough credits");
        }

    }

    public void ShowObjectInfo(GameObject item)
    {
        this.GetComponent<UiUpdater>().UpdatePanelInfo(item);
    }

}
