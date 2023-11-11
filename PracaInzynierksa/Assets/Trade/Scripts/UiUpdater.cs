using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiUpdater : MonoBehaviour
{
    public GameObject transactionValue;
    private TextMeshProUGUI transactionValueText;

    public GameObject InfoPanel;
    public GameObject InfoPanelImage;
    public GameObject InfoPanelItemName;
    public GameObject InfoPanelItemDescription;
    public GameObject InfoPanelPrice;
    public TextMeshProUGUI Stats;


    private void Awake()
    {
        transactionValueText = transactionValue.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        TransactionValueUpdate();
    }

    private void TransactionValueUpdate()
    {
        TradeManager instance = TradeManager.GetInstance();
        int transactionValue = instance.CalculateInventoryValue(instance.barterInventory);
        if (transactionValue >= 0)
        {
            transactionValueText.color = new Color(0f, 0.7176470588235294f, 0.01568627450980392f);
        }
        else
        {
            transactionValueText.color = new Color(0.7215686274509804f, 0f, 0.0784313725490196f);
        }
        transactionValueText.text = transactionValue.ToString();
    }

    public void UpdatePanelInfo(GameObject item)
    {
        if (!InfoPanel.activeSelf)
        {
            InfoPanel.SetActive(true);
        }
        ItemInfo itemInfo = item.GetComponent<ItemInfo>();
        InfoPanelImage.GetComponent<Image>().sprite = itemInfo.image;
        InfoPanelItemName.GetComponent<TextMeshProUGUI>().text = itemInfo.itemName;
        InfoPanelItemDescription.GetComponent<TextMeshProUGUI>().text = itemInfo.description;
        InfoPanelPrice.GetComponent<TextMeshProUGUI>().text = itemInfo.price.ToString();
        switch (itemInfo.type)
        {
            case ItemInfo.ItemType.Weapon:
                WeaponInfo weaponInfo = item.GetComponent<WeaponInfo>();
                Stats.text = $"Damage: {weaponInfo.damageDices}d{weaponInfo.damageRange}";
                if (weaponInfo.damageBonus > 0) Stats.text += $"+{weaponInfo.damageBonus}";
                Stats.text += $"{Environment.NewLine}Accuracy: +{weaponInfo.accuracy}";
                break;
            case ItemInfo.ItemType.Armor:
                ArmorInfo armorInfo = item.GetComponent<ArmorInfo>();
                Stats.text = $"Stopping Power: {armorInfo.stopingPower}";
                break;
            case ItemInfo.ItemType.Consumable:
                ConsumableInfo consumableInfo = item.GetComponent<ConsumableInfo>();
                Stats.text = $"Action: Heal {Environment.NewLine}" +
                    $"Boost Value: {consumableInfo.boostValue} {Environment.NewLine}" +
                    $"Uses: {consumableInfo.quantity}/{consumableInfo.maxQuantity}";
                break;
            default:
                Stats.text = "";
                break;
        }
    }
}
