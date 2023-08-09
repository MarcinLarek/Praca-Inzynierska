using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        StatUpgrade();
    }

    private void StatUpgrade()
    {
        //Szukamy po nazwie ktory przycisk zostal klikniety
        //Nastepnie w zaleznosci od przycisku ulepszamy odpowiadajacy mu atrybut
        //Na koniec ustawiamy atrybut active przycisku do zatwierdzania zmian na true
        //Dopiero po wcisnieciu tego przycisku zapiszemy zmiany
        CharacterStats activeCharacterStats = UpgradeManager.GetInstance().activeCharacter.GetComponent<CharacterStats>();
        switch (this.name) 
        {
            case "Upgrade-Health":
                activeCharacterStats.experience -= activeCharacterStats.maxHealth * 100;
                activeCharacterStats.maxHealth++;
                activeCharacterStats.health = activeCharacterStats.maxHealth;
                break;
            case "Upgrade-ActionPoints":
                activeCharacterStats.experience -= activeCharacterStats.maxActionPoints * 100;
                activeCharacterStats.maxActionPoints++;
                activeCharacterStats.actionPoints = activeCharacterStats.maxActionPoints;
                break;
            case "Upgrade-Strength":
                activeCharacterStats.experience -= activeCharacterStats.strength * 100;
                activeCharacterStats.strength++;
                break;
            case "Upgrade-Endurance":
                activeCharacterStats.experience -= activeCharacterStats.endurance * 100;
                activeCharacterStats.endurance++;
                break;
            case "Upgrade-Agility":
                activeCharacterStats.experience -= activeCharacterStats.agility * 100;
                activeCharacterStats.agility++;
                break;
            case "Upgrade-Luck":
                activeCharacterStats.experience -= activeCharacterStats.luck * 100;
                activeCharacterStats.luck++;
                break;
            case "Upgrade-Inteligence":
                activeCharacterStats.experience -= activeCharacterStats.inteligence * 100;
                activeCharacterStats.inteligence++;
                break;
            default:
                Debug.Log("Something Wrong with upgrade buttons");
                break;

        }
        GameObject.Find("Canvas/StatsPanel/AcceptChangesButton").SetActive(true);
    }
}
