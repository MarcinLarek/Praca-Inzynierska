using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite image;
    public ItemType type;
    public int price;

    public void AssignStats(ItemInfo FromItem)
    {
        this.itemName = FromItem.itemName;
        this.description = FromItem.description;
        this.image = FromItem.image;
        this.type = FromItem.type;
        this.price = FromItem.price;
    }
    public enum ItemType
    {
        Loot,
        Consumable,
        Weapon,
        Armor,
        Special
    }
}


