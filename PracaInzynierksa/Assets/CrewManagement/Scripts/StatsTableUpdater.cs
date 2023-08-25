using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StatsTableUpdater : MonoBehaviour
{
    private CrewManager instance;
    private TextMeshProUGUI moneydisplay;
    private void Awake()
    {
        instance = CrewManager.GetInstance();
        moneydisplay = GameObject.Find("Canvas/MoneyPanel/MoneyCount").GetComponent<TextMeshProUGUI>();

        UpdateTeamCount();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTable();
        MoneyCounterUpdater();
    }
    private void MoneyCounterUpdater()
    {
        moneydisplay.text = PlayerInfo.GetInstance().playerMoney.ToString();
    }

    private void UpdateTable()
    {
        GameObject activeCharacter = CrewManager.GetInstance().activeCharacter;
        if (activeCharacter)
        {
            CharacterStats stats = activeCharacter.GetComponent<CharacterStats>();
            transform.Find("CharacterName").gameObject.GetComponent<TextMeshProUGUI>().text = stats.charactername;
            transform.Find("Points-Health").gameObject.GetComponent<TextMeshProUGUI>().text = stats.maxHealth.ToString();
            transform.Find("Points-ActionPoints").gameObject.GetComponent<TextMeshProUGUI>().text = stats.maxActionPoints.ToString();
            transform.Find("Points-Strength").gameObject.GetComponent<TextMeshProUGUI>().text = stats.strength.ToString();
            transform.Find("Points-Endurance").gameObject.GetComponent<TextMeshProUGUI>().text = stats.endurance.ToString();
            transform.Find("Points-Agility").gameObject.GetComponent<TextMeshProUGUI>().text = stats.agility.ToString();
            transform.Find("Points-Luck").gameObject.GetComponent<TextMeshProUGUI>().text = stats.luck.ToString();
            transform.Find("Points-Inteligence").gameObject.GetComponent<TextMeshProUGUI>().text = stats.inteligence.ToString();

            //To Trzeba przeniesc w inne miejsce pozniej bo tutaj nie pasuje---------
            UpdateTeamCount();
            //-----------------------------------------------------------------------

            switch (stats.classname)
            {
                case (CharacterStats.Classes.DMG):
                    transform.Find("ClassName").gameObject.GetComponent<TextMeshProUGUI>().text = "DPS";
                    break;
                case (CharacterStats.Classes.TANK):
                    transform.Find("ClassName").gameObject.GetComponent<TextMeshProUGUI>().text = "TANK";
                    break;
                case (CharacterStats.Classes.SUPPORT):
                    transform.Find("ClassName").gameObject.GetComponent<TextMeshProUGUI>().text = "SUPPORT";
                    break;
            }

            if (!stats.isplayerteam)
            {
                transform.Find("Price").gameObject.SetActive(true);
                transform.Find("PriceTag").gameObject.SetActive(true);
                transform.Find("Price").gameObject.GetComponent<TextMeshProUGUI>().text = stats.price.ToString();
                transform.Find("PurchaseCharacterButton").gameObject.SetActive(true);
                transform.Find("TeamSwitchButton").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Price").gameObject.SetActive(false);
                transform.Find("PriceTag").gameObject.SetActive(false);
                transform.Find("PurchaseCharacterButton").gameObject.SetActive(false);
                transform.Find("TeamSwitchButton").gameObject.SetActive(true);
                if (activeCharacter.GetComponent<CharacterStats>().inactiveteam == true)
                {
                    transform.Find("TeamSwitchButton").gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Remove from team";
                }
                else
                {
                    transform.Find("TeamSwitchButton").gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Add to team";
                }
            }
        }
    }

    private void UpdateTeamCount()
    {
        transform.Find("ActiveTeam").gameObject.GetComponent<TextMeshProUGUI>().text = $"Characters in Team: {PlayerInfo.GetInstance().CharactersInActiveTeam.Count.ToString()}/{PlayerInfo.GetInstance().teamlimit.ToString()}";
    }
}
