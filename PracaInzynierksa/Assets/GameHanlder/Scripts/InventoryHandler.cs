using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour, IDataPersistence
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

    public void LoadData(GameData data)
    {
        this.traderInventoryGenerated = data.traderInventoryGenerated;
        //List
        //this.traderItems = data.traderItems;
        //this.inventoryItems = data.inventoryItems;
    }

    public void SaveData(ref GameData data)
    {
        data.traderInventoryGenerated = this.traderInventoryGenerated;
        //List
        //data.traderItems = this.traderItems;
        //data.inventoryItems = this.inventoryItems;
    }
}
