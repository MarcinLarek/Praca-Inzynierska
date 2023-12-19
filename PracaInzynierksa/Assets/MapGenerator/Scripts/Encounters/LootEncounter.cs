using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootEncounter : MonoBehaviour
{
    public List<GameObject> itemsList;
    public GameObject GHItemPrefab;

    public GameObject itemToGive;

    public GameObject popUpPanel;

    private void Awake()
    {
        itemToGive = itemsList[Random.Range(0, itemsList.Count)];
    }

    public void GiveItem()
    {
        GameObject ghItem = Instantiate(GHItemPrefab, new Vector3(0,0), Quaternion.identity);
        ghItem.GetComponent<ItemInfo>().AssignStats(itemToGive.GetComponent<ItemInfo>());
        ghItem.GetComponent<ItemInfo>().itemId = Random.Range(0, 2147483640);
        ghItem.GetComponent<ItemInfo>().owned = true;
        InventoryHandler.GetInstance().inventoryItems.Add(ghItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerMovement playerMovement = GameObject.Find("Player").gameObject.GetComponent<PlayerMovement>();
            if (playerMovement.enableMovement)
            {
                playerMovement.DisableMovement();

                GameObject newPopUpPanel = Instantiate(popUpPanel, new Vector3(8, -13), Quaternion.identity);
                newPopUpPanel.transform.SetParent(GameObject.Find("Canvas").transform);
                newPopUpPanel.GetComponent<RectTransform>().localPosition = new Vector3(8, 13);
                newPopUpPanel.GetComponent<EncounterPopUp>().EncnounterEntry(this.gameObject);
            }
        }
    }
}
