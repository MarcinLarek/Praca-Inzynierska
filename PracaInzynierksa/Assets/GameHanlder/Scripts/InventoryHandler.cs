using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static ItemInfo;

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

    public GameObject itemPrefab;
    public GameObject weaponPrefab;
    public GameObject armorPrefab;
    public GameObject consumablePrefab;

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
        this.inventoryItems.Clear();
        this.traderItems.Clear();
        foreach (GameDataWeapon item in data.listWeapons)
        {
            GameObject weapon = Instantiate(weaponPrefab);
            WeaponInfo weaponInfo = weapon.GetComponent<WeaponInfo>();
            weaponInfo.itemId = item.itemId;
            weaponInfo.itemName = item.itemName;
            weaponInfo.description = item.description;
            weaponInfo.image = item.image;
            weaponInfo.type = item.type;
            weaponInfo.price = item.price;
            weaponInfo.owned = item.owned;
            weaponInfo.equiped = item.equiped;
            weaponInfo.damageRange = item.damageRange;
            weaponInfo.damageDices = item.damageDices;
            weaponInfo.damageBonus = item.damageBonus;
            weaponInfo.accuracy = item.accuracy;
            if (weaponInfo.owned)
            {
                this.inventoryItems.Add(weapon);
            }
            else
            {
                this.traderItems.Add(weapon);
            }
        }
        foreach (GameDataArmor itemArmor in data.listArmors)
        {
            GameObject armor = Instantiate(armorPrefab);
            ArmorInfo armorInfo = armor.GetComponent<ArmorInfo>();
            armorInfo.itemId = itemArmor.itemId;
            armorInfo.itemName = itemArmor.itemName;
            armorInfo.description = itemArmor.description;
            armorInfo.image = itemArmor.image;
            armorInfo.type = itemArmor.type;
            armorInfo.price = itemArmor.price;
            armorInfo.owned = itemArmor.owned;
            armorInfo.equiped = itemArmor.equiped;
            armorInfo.stopingPower = itemArmor.stopingPower;
            armorInfo.balistic = itemArmor.balistic;
            armorInfo.shields = itemArmor.shields;
            if (armorInfo.owned)
            {
                this.inventoryItems.Add(armor);
            }
            else
            {
                this.traderItems.Add(armor);
            }
        }
        foreach (GameDataConsumable itemConsumable in data.listConsumables)
        {
            GameObject consumable = Instantiate(consumablePrefab);
            ConsumableInfo consumableInfo = consumable.GetComponent<ConsumableInfo>();
            consumableInfo.itemId = itemConsumable.itemId;
            consumableInfo.itemName = itemConsumable.itemName;
            consumableInfo.description = itemConsumable.description;
            consumableInfo.image = itemConsumable.image;
            consumableInfo.type = itemConsumable.type;
            consumableInfo.price = itemConsumable.price;
            consumableInfo.owned = itemConsumable.owned;
            consumableInfo.equiped = itemConsumable.equiped;
            consumableInfo.quantity = itemConsumable.quantity;
            consumableInfo.maxQuantity = itemConsumable.maxQuantity;
            consumableInfo.boostValue = itemConsumable.boostValue;
            if (consumableInfo.owned)
            {
                this.inventoryItems.Add(consumable);
            }
            else
            {
                this.traderItems.Add(consumable);
            }
        }
        foreach (GameDataItem item in data.listItems)
        {
            GameObject spawnedItem = Instantiate(itemPrefab);
            ItemInfo spawnedItemInfo = spawnedItem.GetComponent<ItemInfo>();
            spawnedItemInfo.itemId = item.itemId;
            spawnedItemInfo.itemName = item.itemName;
            spawnedItemInfo.description = item.description;
            spawnedItemInfo.image = item.image;
            spawnedItemInfo.type = item.type;
            spawnedItemInfo.price = item.price;
            spawnedItemInfo.owned = item.owned;
            spawnedItemInfo.equiped = item.equiped;
            if (spawnedItemInfo.owned)
            {
                this.inventoryItems.Add(spawnedItem);
            }
            else
            {
                this.traderItems.Add(spawnedItem);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.traderInventoryGenerated = this.traderInventoryGenerated;
        //List
        data.listItems.Clear();
        data.listArmors.Clear();
        data.listWeapons.Clear();
        data.listConsumables.Clear();
        foreach (GameObject item in this.inventoryItems)
        {
            ItemInfo itemInfo = item.GetComponent<ItemInfo>();
            switch (itemInfo.type)
            {
                case ItemType.Weapon:
                    WeaponInfo weaponInfo = item.GetComponent<WeaponInfo>();
                    data.listWeapons.Add(new GameDataWeapon(weaponInfo.itemId, weaponInfo.itemName, weaponInfo.description,
                        weaponInfo.image, weaponInfo.type, weaponInfo.price, weaponInfo.owned, weaponInfo.equiped, 
                        weaponInfo.damageRange, weaponInfo.damageDices, weaponInfo.damageBonus, weaponInfo.accuracy
                ));
                    break;
                case ItemType.Armor:
                    ArmorInfo armorInfo = item.GetComponent<ArmorInfo>();
                    data.listArmors.Add(new GameDataArmor(armorInfo.itemId, armorInfo.itemName, armorInfo.description,
                        armorInfo.image, armorInfo.type, armorInfo.price, armorInfo.owned, armorInfo.equiped,
                        armorInfo.stopingPower, armorInfo.balistic, armorInfo.shields
                ));
                    break;
                case ItemType.Consumable:
                    ConsumableInfo consumableInfo = item.GetComponent<ConsumableInfo>();
                    data.listConsumables.Add(new GameDataConsumable(consumableInfo.itemId, consumableInfo.itemName, consumableInfo.description,
                        consumableInfo.image, consumableInfo.type, consumableInfo.price, consumableInfo.owned, consumableInfo.equiped,
                        consumableInfo.quantity, consumableInfo.maxQuantity, consumableInfo.boostValue
                ));
                    break;
                default:
                    data.listItems.Add(new GameDataItem(itemInfo.itemId, itemInfo.itemName, itemInfo.description,
                        itemInfo.image, itemInfo.type, itemInfo.price, itemInfo.owned, itemInfo.equiped
                ));
                    break;
            }
        }

        foreach (GameObject item in this.traderItems)
        {
            ItemInfo itemInfo = item.GetComponent<ItemInfo>();
            switch (itemInfo.type)
            {
                case ItemType.Weapon:
                    WeaponInfo weaponInfo = item.GetComponent<WeaponInfo>();
                    data.listWeapons.Add(new GameDataWeapon(weaponInfo.itemId, weaponInfo.itemName, weaponInfo.description,
                        weaponInfo.image, weaponInfo.type, weaponInfo.price, weaponInfo.owned, weaponInfo.equiped,
                        weaponInfo.damageRange, weaponInfo.damageDices, weaponInfo.damageBonus, weaponInfo.accuracy
                ));
                    break;
                case ItemType.Armor:
                    ArmorInfo armorInfo = item.GetComponent<ArmorInfo>();
                    data.listArmors.Add(new GameDataArmor(armorInfo.itemId, armorInfo.itemName, armorInfo.description,
                        armorInfo.image, armorInfo.type, armorInfo.price, armorInfo.owned, armorInfo.equiped,
                        armorInfo.stopingPower, armorInfo.balistic, armorInfo.shields
                ));
                    break;
                case ItemType.Consumable:
                    ConsumableInfo consumableInfo = item.GetComponent<ConsumableInfo>();
                    data.listConsumables.Add(new GameDataConsumable(consumableInfo.itemId, consumableInfo.itemName, consumableInfo.description,
                        consumableInfo.image, consumableInfo.type, consumableInfo.price, consumableInfo.owned, consumableInfo.equiped,
                        consumableInfo.quantity, consumableInfo.maxQuantity, consumableInfo.boostValue
                ));
                    break;
                default:
                    data.listItems.Add(new GameDataItem(itemInfo.itemId, itemInfo.itemName, itemInfo.description,
                        itemInfo.image, itemInfo.type, itemInfo.price, itemInfo.owned, itemInfo.equiped
                ));
                    break;
            }
        }
    }
}
