using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconClickHandler : MonoBehaviour, IPointerClickHandler
{
    private GameObject statsTable;

    public void OnPointerClick(PointerEventData eventData)
    {
        CrewManager.GetInstance().activeCharacter = this.gameObject;
        //Jesli tabela ze statystykami jest ukryta (Nie bylo wczesniej wybranej zadnej postaci) to teraz ja odkrywamy
        if (!statsTable.activeSelf)
        {
            statsTable.SetActive(true);
        }
    }

    private void Awake()
    {
        statsTable = GameObject.Find("Canvas/StatsTable");
    }

}
