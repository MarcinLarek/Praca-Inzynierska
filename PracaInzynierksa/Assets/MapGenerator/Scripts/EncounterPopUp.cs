using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncounterPopUp : MonoBehaviour
{
    private GameObject encounter;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Value;
    public Image Image;

    public void EncnounterEntry(GameObject encounter)
    {
        this.encounter = encounter;
        ItemInfo encounterItemInfo = this.encounter.GetComponent<LootEncounter>().itemToGive.GetComponent<ItemInfo>();
        Title.text = "You Found: " + encounterItemInfo.itemName;
        Value.text = encounterItemInfo.price.ToString();
        Image.sprite = encounterItemInfo.image;

    }

    public void TakeItemButton()
    {
        encounter.GetComponent<LootEncounter>().GiveItem();
        Destroy(encounter);
        Destroy(this.gameObject);
    }

    public void ThrowItemButton()
    {
        Destroy(encounter);
        Destroy(this.gameObject);
    }
}
