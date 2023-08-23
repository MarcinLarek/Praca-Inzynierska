using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ClassAbilities : MonoBehaviour
{
    private string abilityOneName;
    private string abilityTwoName;
    private string abilityThreeName;

    public GameObject abilityOneButton;
    public GameObject abilityTwoButton;
    public GameObject abilityThreeButton;

    private CharacterStats characterStats;

    private void Awake()
    {
        abilityOneButton = GameObject.Find("Ability 1");
        abilityTwoButton = GameObject.Find("Ability 2");
        abilityThreeButton = GameObject.Find("Ability 3");
        characterStats = this.gameObject.GetComponent<CharacterStats>();
    }

    //Zanim ktos bedzie na mnie krzyczec.
    //Lepszego pomyslu nie mam poki co.
    //Poki co jest limit na 3 mozliwe abilitki ale teoretycznie da sie to rozbudowac.
    //Problem w tym ze mi sie nie chce. Feel free to try.
    //Ale ok, do rzeczy. Dajemy do funkcji nazwe klasy obecnie aktwynej postaci i numer przycisku ktory zostal klikniety
    //Nastepnie przechodzimy switchami przez wszystkie klasy postaci i numery abilitek az znajdziemy to co nas interesuje
    //Rozwiazanie glupie i zle no ale coz...

    public void PrepareButtons(CharacterStats.Classes classtype)
    {
        abilityOneButton = GameObject.Find("Ability 1");
        abilityTwoButton = GameObject.Find("Ability 2");
        abilityThreeButton = GameObject.Find("Ability 3");
        switch (classtype)
        {
            case (CharacterStats.Classes.DMG):
                abilityOneName = "Marksman";
                abilityTwoName = "Blind Fire";
                abilityThreeName = "Command";
                abilityOneButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityOneName;
                abilityTwoButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityTwoName;
                abilityThreeButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityThreeName;
                break;
            case (CharacterStats.Classes.SUPPORT):
                abilityOneName = "Heal Selected";
                abilityTwoName = "Heal All";
                abilityThreeName = "Overheal";
                abilityOneButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityOneName;
                abilityTwoButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityTwoName;
                abilityThreeButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityThreeName;
                break;
            case (CharacterStats.Classes.TANK):
                abilityOneName = "Sacrifice";
                abilityTwoName = "Boost Up";
                abilityThreeName = "Rally up";
                abilityOneButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityOneName;
                abilityTwoButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityTwoName;
                abilityThreeButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityThreeName;
                break;
            default:
                Debug.Log("Wrong Class");
                break;
        }
    }

    public void AbilityDistributor(CharacterStats.Classes classtype, int abilityNumber)
    {
        switch (classtype)
        {
            case CharacterStats.Classes.SUPPORT:
                switch (abilityNumber)
                {
                    case 1:
                        SupportAbilityOne();
                        break;
                    case 2:
                        SupportAbilityTwo();
                        break;
                    case 3:
                        SupportAbilityThree();
                        break;
                }
                break;
            case CharacterStats.Classes.DMG:
                switch (abilityNumber)
                {
                    case 1:
                        DPSAbilityOne();
                        break;
                    case 2:
                        DPSAbilityTwo();
                        break;
                    case 3:
                        DPSAbilityThree();
                        break;
                }
                break;
            case CharacterStats.Classes.TANK:
                switch (abilityNumber)
                {
                    case 1:
                        TankAbilityOne();
                        break;
                    case 2:
                        TankAbilityTwo();
                        break;
                    case 3:
                        TankAbilityThree();
                        break;
                }
                break;
        }
    }

    //Heals ally for 1d10 +3 to his maximum HP. AP cost: 3
    private void SupportAbilityOne()
    {
        BattleHandler battlehandler = BattleHandler.GetInstance();
        int abilityCost = 3;

        if (battlehandler.selectedCharacter != null)//Sprawdzamy czy ktos wybrany
        {
            CharacterStats selectescharacter = battlehandler.selectedCharacter.GetComponent<CharacterStats>();
            if (selectescharacter.isplayerteam == true)//Sprawdzamy czy przeciwnik czy swoj
            {
                if (characterStats.actionPoints >= abilityCost)//Sprawdzamy ilosc Action Pointow
                {
                    if (selectescharacter.health != selectescharacter.maxHealth)//Sprawdzamy czy nie ma juz max HP
                    {
                        int roll = Random.Range(1, 10);
                        int totalheal = roll + 3;
                        Debug.Log($"{characterStats.name} healing {selectescharacter.name} for 1d10+3  - {roll}- Total roll - {totalheal}");
                        selectescharacter.health += totalheal;
                        if (selectescharacter.health > selectescharacter.maxHealth)//Jesli uleczyliscmy o za duzo, zmniejszamy do limitu.
                        {
                            selectescharacter.health = selectescharacter.maxHealth;
                        }
                        characterStats.actionPoints -= abilityCost;
                    }
                    else
                    {
                        Debug.Log("Alredy at full HP");
                    }
                }
                else
                {
                    Debug.Log($"You don't at least {abilityCost} Action Points");
                }
            }
            else
            {
                Debug.Log("Cant heal enemy");
            }
        }
        else
        {
            Debug.Log("Target not selected");
        }

    }
    //Heals all others allies for 2d4 to thier max HP. Ap cost: 4
    private void SupportAbilityTwo()
    {
        BattleHandler battlehandler = BattleHandler.GetInstance();
        int abilitycost = 4;
        if (characterStats.actionPoints >= abilitycost)//Sprawdzamy ilosc Action Pointow
        {
            //Jedziemy po wszystkich postaciach w walce
            foreach (GameObject singlecharacter in battlehandler.charactersListinbattle)
            {
                CharacterStats singlecharacterstats = singlecharacter.GetComponent<CharacterStats>();
                //Sprawdzamy czy:
                //- Postac jest w naszej druzynie
                //- Postac zyje
                //- Postac ma mniej niz maximum HP
                //- Postac nie jest ta samo postacia ktora uzywa zdolnosci
                if (singlecharacterstats.isplayerteam && singlecharacterstats.isalive && singlecharacterstats.health < singlecharacterstats.maxHealth && singlecharacterstats != characterStats)
                {
                    int roll1 = Random.Range(1, 4);
                    int roll2 = Random.Range(1, 4);
                    int totalheal = roll1 + roll2;
                    Debug.Log($"{characterStats.name} healing {singlecharacterstats.name} for 2d4  - {roll1} , {roll2}- Total roll - {totalheal}");
                    singlecharacterstats.health += totalheal;
                    if (singlecharacterstats.health > singlecharacterstats.maxHealth)//Jesli uleczyliscmy o za duzo, zmniejszamy do limitu.
                    {
                        singlecharacterstats.health = singlecharacterstats.maxHealth;
                    }
                }
            }
            characterStats.actionPoints -= abilitycost;
        }
        else
        {
            Debug.Log($"You don't have at least {abilitycost} Action Points");
        }
    }
    //Overheal ally by 8. He needs to be at max hp alredy. AP cost: 5
    private void SupportAbilityThree()
    {
        BattleHandler battlehandler = BattleHandler.GetInstance();
        int abilitycost = 5;
        if (battlehandler.selectedCharacter != null)//Sprawdzamy czy ktos wybrany
        {
            CharacterStats selectescharacter = battlehandler.selectedCharacter.GetComponent<CharacterStats>();
            if (selectescharacter.isplayerteam == true)//Sprawdzamy czy przeciwnik czy swoj
            {
                if (characterStats.actionPoints >= abilitycost)//Sprawdzamy ilosc Action Pointow
                {
                    if (selectescharacter.health == selectescharacter.maxHealth)//Sprawdzamy czy ma max HP
                    {
                        Debug.Log($"{characterStats.name} boost {selectescharacter.name} for 8 hp points");
                        selectescharacter.health += 8;
                        characterStats.actionPoints -= abilitycost;
                    }
                    else
                    {
                        Debug.Log("Not at full HP");
                    }
                }
                else
                {
                    Debug.Log($"You don't have {abilitycost} Action Points");
                }
            }
            else
            {
                Debug.Log("Cant heal enemy");
            }
        }
        else
        {
            Debug.Log("Target not selected");
        }
    }

    //70% of dealing 15 damange. AP cost: 5
    private void DPSAbilityOne()
    {
        BattleHandler battlehandler = BattleHandler.GetInstance();
        int abilityCost = 5;

        if (battlehandler.selectedCharacter != null)//Sprawdzamy czy ktos wybrany
        {
            CharacterStats selectescharacter = battlehandler.selectedCharacter.GetComponent<CharacterStats>();
            if (selectescharacter.isplayerteam == false)//Sprawdzamy czy przeciwnik czy swoj
            {
                if (characterStats.actionPoints >= abilityCost)//Sprawdzamy ilosc Action Pointow
                {

                    int chanceRoll = Random.Range(1, 10);
                    if (chanceRoll <= 7)
                    {
                        Debug.Log($"{characterStats.name} uses Marksman shoot on {selectescharacter.name} and deals 15 damage");
                        selectescharacter.RecieveDamage(15);
                        battlehandler.selectedCharacter.GetComponent<CharacterBattle>().CheckIfKilled(selectescharacter);
                    }
                    else
                    {
                        Debug.Log("Marksman shoot misses!");
                    }

                    characterStats.actionPoints -= abilityCost;
                }
                else
                {
                    Debug.Log($"You don't have {abilityCost} Action Points");
                }
            }
            else
            {
                Debug.Log("Cant shoot teammate");
            }
        }
        else
        {
            Debug.Log("Target not selected");
        }
    }
    //Each enemy has 50% chance of Reciving 1d20 +5 damage. AP cost: 6 
    private void DPSAbilityTwo()
    {
        Debug.Log($"{characterStats.name} uses Blind Fire");
        BattleHandler battlehandler = BattleHandler.GetInstance();
        int abilityCost = 6;
        if (characterStats.actionPoints >= abilityCost)//Sprawdzamy ilosc Action Pointow
        {
            //Jedziemy po wszystkich postaciach w walce
            foreach (GameObject singlecharacter in battlehandler.charactersListinbattle)
            {
                CharacterStats singlecharacterstats = singlecharacter.GetComponent<CharacterStats>();
                //Sprawdzamy czy:
                //- Postac jest w przeciwnej druzynie
                //- Postac zyje
                if (!singlecharacterstats.isplayerteam && singlecharacterstats.isalive)
                {
                    int chanceRoll = Random.Range(1, 10);
                    if (chanceRoll <= 5)
                    {
                        int roll = Random.Range(1, 20);
                        Debug.Log($"Blind Fire success on {singlecharacterstats.name} Rolling 1d20: {roll} Total Damage: {roll+5}");
                        singlecharacterstats.RecieveDamage(roll+5);
                        singlecharacterstats.GetComponent<CharacterBattle>().CheckIfKilled(singlecharacterstats);
                    }
                    else
                    {
                        Debug.Log($"Blind Fire failure on {singlecharacterstats.name}");
                    }
                    

                }
            }
            characterStats.actionPoints -= abilityCost;
        }
        else
        {
            Debug.Log($"You don't have {abilityCost} Action Points");
        }
    }
    //Add +3 damage to next standard attack of selected character. AP cost: 2
    private void DPSAbilityThree()
    {
        int abilityCost = 2;
        BattleHandler battlehandler = BattleHandler.GetInstance();
        Debug.Log($"{characterStats.name} uses Command");
        if (battlehandler.selectedCharacter != null)//Sprawdzamy czy ktos wybrany
        {
            CharacterStats selectescharacter = battlehandler.selectedCharacter.GetComponent<CharacterStats>();
            if (selectescharacter.isplayerteam == true)//Sprawdzamy czy przeciwnik czy swoj
            {
                if (characterStats.actionPoints >= abilityCost)//Sprawdzamy ilosc Action Pointow
                {
                    int roll = Random.Range(1, 10);
                    int totalheal = roll + 3;
                    Debug.Log($"{selectescharacter.name} recive +3 damage for next standard attack");
                    selectescharacter.bonusDamage += 3;
                    characterStats.actionPoints -= abilityCost;
                }
                else
                {
                    Debug.Log($"You don't have {abilityCost} Action Points");
                }
            }
            else
            {
                Debug.Log("Cant command enemy");
            }
        }
        else
        {
            Debug.Log("Target not selected");
        }
    }

    //Suffer 5 damage to heal ally for 0-10 HP. AP cost: 2
    private void TankAbilityOne()
    {
        BattleHandler battlehandler = BattleHandler.GetInstance();
        int abilityCost = 2;
        if (battlehandler.selectedCharacter != null)//Sprawdzamy czy ktos wybrany
        {
            CharacterStats selectescharacter = battlehandler.selectedCharacter.GetComponent<CharacterStats>();
            if (selectescharacter.isplayerteam == true)//Sprawdzamy czy przeciwnik czy swoj
            {
                if (characterStats.actionPoints >= abilityCost)//Sprawdzamy ilosc Action Pointow
                {
                    if (characterStats.health > 5)//Sprawdzamy czy mamy conajmniej 5 HP
                    {
                        int roll = Random.Range(0, 11);
                        Debug.Log($"{characterStats.name} healing {selectescharacter.name} for 0-10 points  - {roll}");
                        selectescharacter.health += roll;
                        if (selectescharacter.health > selectescharacter.maxHealth)//Jesli uleczyliscmy o za duzo, zmniejszamy do limitu.
                        {
                            selectescharacter.health = selectescharacter.maxHealth;
                        }
                        characterStats.actionPoints -= abilityCost;
                        characterStats.health -= 5;
                    }
                    else
                    {
                        Debug.Log("Insufficient ammount of HP");
                    }
                }
                else
                {
                    Debug.Log($"You don't have {abilityCost} Action Points");
                }
            }
            else
            {
                Debug.Log("Cant heal enemy");
            }
        }
        else
        {
            Debug.Log("Target not selected");
        }
    }
    //Heal yourself for 1d10 HP. Overheal possible. AP cost: 4
    private void TankAbilityTwo()
    {
        BattleHandler battlehandler = BattleHandler.GetInstance();
        int abilityCost = 4;
        if (characterStats.actionPoints >= abilityCost)//Sprawdzamy ilosc Action Pointow
        {
            int roll = Random.Range(1, 10);
            Debug.Log($"{characterStats.name} boosting self fo 1d10 HP poiunts. Roll - {roll}");
            characterStats.actionPoints -= abilityCost;
            characterStats.health += roll;
        }
        else
        {
            Debug.Log($"You don't have {abilityCost} Action Points");
        }
    }
    //Give all other allies 0-3 AP. AP cost: 6
    private void TankAbilityThree()
    {
        Debug.Log($"{characterStats} uses Rally up");
        BattleHandler battlehandler = BattleHandler.GetInstance();
        int abilityCost = 6;
        if (characterStats.actionPoints >= abilityCost)//Sprawdzamy ilosc Action Pointow
        {
            //Jedziemy po wszystkich postaciach w walce
            foreach (GameObject singlecharacter in battlehandler.charactersListinbattle)
            {
                CharacterStats singlecharacterstats = singlecharacter.GetComponent<CharacterStats>();
                //Sprawdzamy czy:
                //- Postac jest w naszej druzynie
                //- Postac zyje
                //- Postac nie jest ta samo postacia ktora uzywa zdolnosci
                if (singlecharacterstats.isplayerteam && singlecharacterstats.isalive && singlecharacterstats != characterStats)
                {
                    int roll = Random.Range(0, 3);

                    Debug.Log($"{singlecharacterstats.name} recive 0-3 Action Points. Roll - {roll}");
                    singlecharacterstats.actionPoints += roll;
                }
            }
            characterStats.actionPoints -= abilityCost;
        }
        else
        {
            Debug.Log("You don't have enought Action Points");
        }
    }
}
