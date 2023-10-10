using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataArmor : GameDataItem
{
    public int stopingPower;
    public bool balistic;
    public bool shields;
    public GameDataArmor(int itemId, string itemName, string description, Sprite image, ItemInfo.ItemType type,
        int price, bool owned, bool equiped, int stopingPower, bool balistic, bool shields
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
        this.stopingPower = stopingPower;
        this.balistic = balistic;
        this.shields = shields;
    }
}
