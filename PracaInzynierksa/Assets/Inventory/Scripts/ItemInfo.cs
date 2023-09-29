using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemInfo : MonoBehaviour
{
    public int itemId = 0;
    public string itemName;
    public string description;
    public Sprite image;
    public ItemType type;
    public int price;
    public bool owned;
    public bool equiped;

    private void Awake()
    {
        if(itemId == 0)
        {
            itemId = Random.Range(0, 2147483640);
        }
    }

    public virtual void AssignStats(ItemInfo FromItem)
    {
        this.itemId = FromItem.itemId;
        this.itemName = FromItem.itemName;
        this.description = FromItem.description;
        this.image = FromItem.image;
        this.type = FromItem.type;
        this.price = FromItem.price;
        this.owned = FromItem.owned;
        this.equiped = FromItem.equiped;
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


