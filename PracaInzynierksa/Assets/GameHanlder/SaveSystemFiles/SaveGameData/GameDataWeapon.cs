using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataWeapon : GameDataItem
{
    public int damageRange;
    public int damageDices;
    public int damageBonus;
    public int accuracy;
    public GameDataWeapon(int itemId, string itemName, string description, Sprite image, ItemInfo.ItemType type,
        int price, bool owned, bool equiped, int damageRange, int damageDices, int damageBonus, int accuracy
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
        this.damageRange = damageRange;
        this.damageDices = damageDices;
        this.damageBonus = damageBonus;
        this.accuracy = accuracy;


    }
}
