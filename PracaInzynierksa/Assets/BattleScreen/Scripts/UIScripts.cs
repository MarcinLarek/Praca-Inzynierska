using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    private BattleHandler battlehandler;

    private void Awake()
    {
        battlehandler = BattleHandler.GetInstance();
    }
    public void AttackButton()
    {
        if (battlehandler.selectedCharacter != null)
        {
            if (battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints < 3)
            {
                Debug.Log("You don't have at least 3 action points");
            }
            else
            {
                if (battlehandler.selectedCharacter.GetComponent<CharacterStats>().isplayerteam && battlehandler.activeCharacter.GetComponent<CharacterStats>().isplayerteam)
                {
                    Debug.Log("Cant shoot teammates");
                }
                else
                {
                    battlehandler.activeCharacter.GetComponent<CharacterBattle>().Attack(battlehandler.selectedCharacter.GetComponent<CharacterBattle>());
                    battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints -= 3;
                }
            }
        }
        else
        {
            Debug.Log("Nie wybrano celu");
        }
    }

    public void EndTurnButton()
    {
        battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints = battlehandler.activeCharacter.GetComponent<CharacterStats>().maxActionPoints;
        battlehandler.EndTurn();
    }

    public void GiveActionPointButton()
    {
        if (battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints < 3)
        {
            Debug.Log("You don't have at least 3 action points");
        }
        else
        {
            battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints -= 3;
            battlehandler.selectedCharacter.GetComponent<CharacterStats>().actionPoints += 1;
        }

    }

    public void AbilitiOneButton()
    {
        battlehandler.activeCharacter.GetComponent<ClassAbilities>().AbilityDistributor(battlehandler.activecharacterclass, 1);
    }

    public void AbilitTwoButton()
    {
        battlehandler.activeCharacter.GetComponent<ClassAbilities>().AbilityDistributor(battlehandler.activecharacterclass, 2);
    }

    public void AbilitiThreeButton()
    {
        battlehandler.activeCharacter.GetComponent<ClassAbilities>().AbilityDistributor(battlehandler.activecharacterclass, 3);
    }
}
