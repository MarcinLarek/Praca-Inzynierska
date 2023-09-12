using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiUpdater : MonoBehaviour
{
    public GameObject playerMoney;
    private TextMeshProUGUI playerMoneyText;

    public GameObject transactionValue;
    private TextMeshProUGUI transactionValueText;

    private void Awake()
    {
        playerMoneyText = playerMoney.GetComponent<TextMeshProUGUI>();
        transactionValueText = transactionValue.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        playerMoneyText.text = PlayerInfo.GetInstance().playerMoney.ToString();
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
}
