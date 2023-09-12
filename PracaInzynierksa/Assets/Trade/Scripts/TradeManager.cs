using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{



    public GameObject playerInventory;
    public GameObject merchantInventory;
    public GameObject barterInventory;
    private static TradeManager instance;
    public static TradeManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
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

}
