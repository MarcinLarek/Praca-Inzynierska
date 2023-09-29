using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconClickHandlerUpgrader : MonoBehaviour, IPointerClickHandler
{
    private GameObject statsTable;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Jesli tabela ze statystykami jest ukryta (Nie bylo wczesniej wybranej zadnej postaci) to teraz ja odkrywamy
        if (!statsTable.activeSelf)
        {
            statsTable.SetActive(true);
        }
        UpgradeManager.GetInstance().activeCharacter = this.gameObject;
        UpgradeManager.GetInstance().LoadCharacterPortrait();
        UpgradeManager.GetInstance().LoadCharacterEquipment();
    }

    private void Awake()
    {
        statsTable = GameObject.Find("Canvas/StatsPanel");
    }

}
