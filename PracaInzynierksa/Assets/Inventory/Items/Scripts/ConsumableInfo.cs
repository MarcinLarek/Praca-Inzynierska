using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableInfo : ItemInfo
{
    public int quantity;
    public int maxQuantity;
    public int boostValue;

    public override void AssignStats(ItemInfo FromItem)
    {
        this.itemId = FromItem.itemId;
        this.itemName = FromItem.itemName;
        this.description = FromItem.description;
        this.image = FromItem.image;
        this.type = FromItem.type;
        this.price = FromItem.price;
        this.owned = FromItem.owned;
        this.equiped = FromItem.equiped;

        ConsumableInfo fromItemConsumableInfo = FromItem.gameObject.GetComponent<ConsumableInfo>();

        if (fromItemConsumableInfo != null)
        {
            this.quantity = fromItemConsumableInfo.quantity;
            this.maxQuantity = fromItemConsumableInfo.maxQuantity;
            this.boostValue = fromItemConsumableInfo.boostValue;
        }
    }
}
