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

    public bool ReduceQuantity(int ammount)
    {
        if(quantity > 0)
        {
            this.quantity -= ammount;
            if (quantity == 0)
            {
                this.price = 10;
            }
            else if(quantity < maxQuantity / 2)
            {
                this.price = price / 2;
            }
            return true;
        }
        else
        {
            Debug.Log("You don't have more uses");
            return false;
        }
    }
}
