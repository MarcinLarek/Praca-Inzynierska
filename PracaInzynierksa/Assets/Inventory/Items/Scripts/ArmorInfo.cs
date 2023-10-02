using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorInfo : ItemInfo
{
    public int stopingPower;
    public bool balistic;
    public bool shields;
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

        ArmorInfo fromItemArmorInfo = FromItem.gameObject.GetComponent<ArmorInfo>();

        if (fromItemArmorInfo != null)
        {
            this.stopingPower = fromItemArmorInfo.stopingPower;
            this.balistic = fromItemArmorInfo.balistic;
            this.shields = fromItemArmorInfo.shields;
        }
    }
}
