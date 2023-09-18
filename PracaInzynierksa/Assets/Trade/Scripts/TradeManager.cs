using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TradeManager : MonoBehaviour
{
    public GameObject playerInventory;
    public GameObject merchantInventory;
    public GameObject barterInventory;

    public List<GameObject> merchantItems;
    public GameObject GHItemPrefab;

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
        GameObject GHItemSpawn = GHItemPrefab;
        ItemInfo GHItemSpawnInfo = GHItemSpawn.GetComponent<ItemInfo>();
        foreach (GameObject iventoryItem in merchantGeneratedItems)
        {
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
                GameObject ItemToSpawn = GHItemPrefab;
                ItemInfo itemInfo = item.GetComponent<ItemInfo>();
                if (itemInfo.owned)
                {
                    playerInfo.playerMoney += itemInfo.price;
                    itemInfo.owned = false;

                    ItemToSpawn.GetComponent<ItemInfo>().AssignStats(item.GetComponent<ItemInfo>());
                    merchantInv.AddItem(ItemToSpawn);

                    foreach (GameObject GHItem in InventoryHandler.GetInstance().inventoryItems)
                    {
                        if (GHItem.GetComponent<ItemInfo>().itemId == itemInfo.itemId)
                        {
                            InventoryHandler.GetInstance().inventoryItems.Remove(GHItem);
                            Destroy(GHItem);
                            break;
                        }
                    }
                    Destroy(item);
                }
                else
                {
                    playerInfo.playerMoney -= itemInfo.price;
                    itemInfo.owned = true;
                    ItemToSpawn.GetComponent<ItemInfo>().AssignStats(item.GetComponent<ItemInfo>());
                    GameObject SpawnedGHItem = Instantiate(ItemToSpawn, new Vector3(0, 0), Quaternion.identity);
                    InventoryHandler.GetInstance().inventoryItems.Add(SpawnedGHItem);
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
}
