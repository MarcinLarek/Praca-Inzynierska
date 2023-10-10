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
    public void EquipItem(GameObject item)
    {
        UpgradeManager upgradeManager = UpgradeManager.GetInstance();

        ItemInfo itemInfo = item.GetComponent<ItemInfo>();
        itemInfo.equiped = true;
        switch (itemInfo.type)
        {
            case ItemInfo.ItemType.Weapon:
                upgradeManager.activeCharacter.GetComponent<CharacterStats>().weaponID = itemInfo.itemId;
                break;
            case ItemInfo.ItemType.Armor:
                upgradeManager.activeCharacter.GetComponent<CharacterStats>().armorID = itemInfo.itemId;
                break;
            case ItemInfo.ItemType.Consumable:
                upgradeManager.activeCharacter.GetComponent<CharacterStats>().consumableID = itemInfo.itemId;
                break;
            default:
                Debug.Log("Wrong Item");
                break;
        }

    }

    public void UnequipItem(GameObject item)
    {
        CharacterStats activeCharacterInfo = UpgradeManager.GetInstance().activeCharacter.GetComponent<CharacterStats>();
        switch (item.GetComponent<ItemInfo>().type)
        {
            case ItemInfo.ItemType.Weapon:
                activeCharacterInfo.weaponID = 0;
                break;
            case ItemInfo.ItemType.Armor:
                activeCharacterInfo.armorID = 0;
                break;
            case ItemInfo.ItemType.Consumable:
                activeCharacterInfo.consumableID = 0;
                break;
            default:
                Debug.Log("Something broke with unequiping");
                break;
        }
        item.GetComponent<ItemInfo>().equiped = false;
    }

}
