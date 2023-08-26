using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconClickHandler : MonoBehaviour
{
    private GameObject statsTable;
    private void Awake()
    {
        statsTable = GameObject.Find("Canvas/StatsTable");
    }

    private void OnMouseDown()
    {
        CrewManager.GetInstance().activeCharacter = this.gameObject;
        //Jesli tabela ze statystykami jest ukryta (Nie bylo wczesniej wybranej zadnej postaci) to teraz ja odkrywamy
        if (!statsTable.activeSelf)
        {
            statsTable.SetActive(true);
        }
    }

}
