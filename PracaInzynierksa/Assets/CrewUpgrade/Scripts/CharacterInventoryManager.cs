using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryManager : MonoBehaviour
{
    private static CharacterInventoryManager instance;

    public static CharacterInventoryManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    public void EquipItem(int itemId)
    {
        UpgradeManager upgradeManager = UpgradeManager.GetInstance();
        GameObject item = null;
        foreach(GameObject GHitem in InventoryHandler.GetInstance().inventoryItems)
        {
            if (GHitem.GetComponent<ItemInfo>().itemId == itemId)
            {
                item = GHitem;
                break;
            }
        }
        ItemInfo itemInfo = item.GetComponent<ItemInfo>();
        switch (itemInfo.type)
        {
            case ItemInfo.ItemType.Weapon:
                upgradeManager.activeCharacter.GetComponent<CharacterStats>().weaponID = itemId;
                break;
            case ItemInfo.ItemType.Armor:
                upgradeManager.activeCharacter.GetComponent<CharacterStats>().armorID = itemId;
                break;
            case ItemInfo.ItemType.Consumable:
                upgradeManager.activeCharacter.GetComponent<CharacterStats>().consumableID = itemId;
                break;
            default:
                Debug.Log("Wrong Item");
                break;
        }

    }
}
