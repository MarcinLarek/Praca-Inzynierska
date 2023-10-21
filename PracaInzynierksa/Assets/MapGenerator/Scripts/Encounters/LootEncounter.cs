using System.Collections;
using System.Collections.Generic;
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
        InventoryHandler.GetInstance().inventoryItems.Add(ghItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject newPopUpPanel = Instantiate(popUpPanel, new Vector3(8, -13), Quaternion.identity);
            newPopUpPanel.transform.SetParent(GameObject.Find("Canvas").transform);
            newPopUpPanel.GetComponent<RectTransform>().localPosition = new Vector3(8, 13);
            newPopUpPanel.GetComponent<EncounterPopUp>().EncnounterEntry(this.gameObject);
        }
    }
}
