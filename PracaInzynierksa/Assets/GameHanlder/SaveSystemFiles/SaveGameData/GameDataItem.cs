using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemInfo;

[System.Serializable]
public class GameDataItem
{
    public int itemId = 0;
    public string itemName;
    public string description;
    public Sprite image;
    public ItemType type;
    public int price;
    public bool owned;
    public bool equiped;

    public GameDataItem(int itemId, string itemName, string description, Sprite image, ItemType type, int price, bool owned, bool equiped)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.description = description;
        this.image = image;
        this.type = type;
        this.price = price;
        this.owned = owned;
        this.equiped = equiped;
    }
}
