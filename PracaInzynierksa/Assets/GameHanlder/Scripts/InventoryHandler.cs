using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    private static InventoryHandler instance;
    public static InventoryHandler GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this; // Singleton
    }

    public List<GameObject> inventoryItems;
    public bool traderInventoryGenerated;
    public List<GameObject> traderItems;

    public void ClearMerchantInventory()
    {
        foreach(GameObject item in traderItems)
        {
            Destroy(item);
        }
        traderItems.Clear();
        traderInventoryGenerated = false;
    }

    

}
