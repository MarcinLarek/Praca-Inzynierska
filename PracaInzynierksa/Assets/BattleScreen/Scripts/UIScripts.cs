using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    private BattleHandler battlehandler;
    public GameObject ActionPointsDisplay;

    private int attackCost = 2;
    private int givePointCost = 2;

    private void Awake()
    {
        battlehandler = BattleHandler.GetInstance();
    }

    private void Update()
    {
        ActionPointsDisplay.GetComponent<TextMeshProUGUI>().text = $"Action Points: {battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints}/{battlehandler.activeCharacter.GetComponent<CharacterStats>().maxActionPoints}";
    }

    public void AttackButton()
    {
        if (battlehandler.selectedCharacter != null)
        {
            if (battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints < attackCost)
            {
                Debug.Log($"You don't have at least {attackCost} action points");
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
                    battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints -= attackCost;
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
        if (battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints < givePointCost)
        {
            Debug.Log($"You don't have at least {givePointCost} action points");
        }
        else
        {
            battlehandler.activeCharacter.GetComponent<CharacterStats>().actionPoints -= givePointCost;
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
