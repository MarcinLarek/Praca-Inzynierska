using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : ItemInfo
{
    public int damageRange;
    public int damageDices;
    public int damageBonus;
    public int accuracy;

    public override void AssignStats(ItemInfo FromItem)
    {
        this.itemId = FromItem.itemId;
        this.itemName = FromItem.itemName;
        this.description = FromItem.description;
        this.image = FromItem.image;
        this.type = FromItem.type;
        this.price = FromItem.price;
        this.owned = FromItem.owned;

        WeaponInfo fromItemWeaponInfo = FromItem.gameObject.GetComponent<WeaponInfo>();

        if(fromItemWeaponInfo != null)
        {
            this.damageRange = fromItemWeaponInfo.damageRange;
            this.damageDices = fromItemWeaponInfo.damageDices;
            this.damageBonus = fromItemWeaponInfo.damageBonus;
            this.accuracy = fromItemWeaponInfo.accuracy;
        }
    }
}
