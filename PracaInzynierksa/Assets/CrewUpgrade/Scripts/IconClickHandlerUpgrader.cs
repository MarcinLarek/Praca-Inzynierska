using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconClickHandlerUpgrader : MonoBehaviour
{
    private GameObject statsTable;
    private void Awake()
    {
        statsTable = GameObject.Find("Canvas/StatsPanel");
    }

    private void OnMouseDown()
    {
        //Jesli tabela ze statystykami jest ukryta (Nie bylo wczesniej wybranej zadnej postaci) to teraz ja odkrywamy
        if (!statsTable.activeSelf)
        {
            statsTable.SetActive(true);
        }
        UpgradeManager.GetInstance().activeCharacter = this.gameObject;
        UpgradeManager.GetInstance().LoadCharacterPortrait();
    }
}
