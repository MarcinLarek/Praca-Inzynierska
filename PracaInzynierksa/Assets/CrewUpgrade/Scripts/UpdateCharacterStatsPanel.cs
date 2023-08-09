using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UpdateCharacterStatsPanel : MonoBehaviour
{
    private UpgradeManager instance;

    private void Awake()
    {
        instance = UpgradeManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTable();
        LvlUpButtonSwitch();
    }

    private void UpdateTable()
    {
        GameObject activeCharacter = UpgradeManager.GetInstance().activeCharacter;
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
            transform.Find("Points-Experience").gameObject.GetComponent<TextMeshProUGUI>().text = stats.experience.ToString();

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
        }
    }

    private void LvlUpButtonSwitch()
    {
        GameObject activeCharacter = UpgradeManager.GetInstance().activeCharacter;

        if (activeCharacter)
        {
            CharacterStats activeCharacterStats = UpgradeManager.GetInstance().activeCharacter.GetComponent<CharacterStats>();

            //Health
            if (activeCharacterStats.experience < activeCharacterStats.maxHealth * 100)
            {
                transform.Find("Upgrade-Health").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Upgrade-Health").gameObject.SetActive(true);
            }
            //ActionPoints
            if (activeCharacterStats.experience < activeCharacterStats.maxActionPoints * 100)
            {
                transform.Find("Upgrade-ActionPoints").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Upgrade-ActionPoints").gameObject.SetActive(true);
            }
            //Strength
            if (activeCharacterStats.experience < activeCharacterStats.strength * 100)
            {
                transform.Find("Upgrade-Strength").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Upgrade-Strength").gameObject.SetActive(true);
            }
            //Endurance
            if (activeCharacterStats.experience < activeCharacterStats.endurance * 100)
            {
                transform.Find("Upgrade-Endurance").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Upgrade-Endurance").gameObject.SetActive(true);
            }
            //Agility
            if (activeCharacterStats.experience < activeCharacterStats.agility * 100)
            {
                transform.Find("Upgrade-Agility").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Upgrade-Agility").gameObject.SetActive(true);
            }
            //Luck
            if (activeCharacterStats.experience < activeCharacterStats.luck * 100)
            {
                transform.Find("Upgrade-Luck").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Upgrade-Luck").gameObject.SetActive(true);
            }
            //Inteligence
            if (activeCharacterStats.experience < activeCharacterStats.inteligence * 100)
            {
                transform.Find("Upgrade-Inteligence").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Upgrade-Inteligence").gameObject.SetActive(true);
            }
        }

    }
}
