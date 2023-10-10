using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataConsumable : GameDataItem
{
    public int quantity;
    public int maxQuantity;
    public int boostValue;
    public GameDataConsumable(int itemId, string itemName, string description, Sprite image, ItemInfo.ItemType type,
        int price, bool owned, bool equiped, int quantity, int maxQuantity, int boostValue
        ) : base(itemId, itemName, description, image, type, price, owned, equiped)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.description = description;
        this.image = image;
        this.type = type;
        this.price = price;
        this.owned = owned;
        this.equiped = equiped;
        this.quantity = quantity;
        this.maxQuantity = maxQuantity;
        this.boostValue = boostValue;
    }
}
