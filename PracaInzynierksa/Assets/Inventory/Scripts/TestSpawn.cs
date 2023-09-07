using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject itemPrefab;

    public void PickupItem()
    {
        inventoryManager.AddItem(itemPrefab);
        Debug.Log("spawning");
    }
}
