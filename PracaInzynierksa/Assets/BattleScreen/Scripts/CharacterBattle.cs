using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Random = UnityEngine.Random;


public class CharacterBattle : MonoBehaviour
{
    // W tym pliku obslugujemy postac w czasie walki

    private CharacterVisuals characterVisuals;
    private CharacterStats characterStats;
    private WeaponInfo weaponInfo;

    private GameObject slectionCircleGameObject;
    private GameObject activeCircleGameObject;
    private GameObject healthbar;
    private Animator animatorr;

    BattleHandler battleHandler = BattleHandler.GetInstance();

    //Gdy klikamy na postac wywolywana jest ta funkcja.
    //Teoretycznie to i ToggleSelectedCharacter() powinno byc w BattleHandlerze ale nie wiem jak to zrobic.
    //Wiec zrobilem to tutaj tak troche na brute force
    void OnMouseDown()
    {
        ToggleSelectedCharacter();
    }

    private void Awake()
    {
        characterVisuals = GetComponent<CharacterVisuals>();
        characterStats = GetComponent<CharacterStats>();
        weaponInfo = GetComponent<WeaponInfo>();
        slectionCircleGameObject = transform.Find("SelectionCircle").gameObject;
        activeCircleGameObject = transform.Find("ActiveCircle").gameObject;
        healthbar = transform.Find("HealthBar").gameObject;
        HideActiveCircle();
        HideSelectionCircle();
    }

    private void Start()
    {
        animatorr = GetComponent<Animator>();
    }
    private void Update()
    {
        if(characterStats.health > 0)
        {
            healthbar.GetComponent<TextMeshPro>().text = characterStats.health + "/" + characterStats.maxHealth;
        }
        else
        {
            healthbar.GetComponent<TextMeshPro>().text = "";
        }
        
    }

    //Przelaczamy obecnie wybrana postac
    public void ToggleSelectedCharacter()
    {
        slectionCircleGameObject = transform.Find("SelectionCircle").gameObject;
        activeCircleGameObject = transform.Find("ActiveCircle").gameObject;

        if (battleHandler.selectedCharacter != null)
        {// Sprawdzamy czy juz jakas postac jest wybrana
            if (battleHandler.selectedCharacter == this.gameObject)
            {// Sprawdzamy czy klikamy obecnie wybrana juz postac. Jesli tak to po prostu ja odznaczamy.
                HideSelectionCircle();
                battleHandler.selectedCharacter = null;
            }
            else
            {
                if (this.gameObject.transform.Find("ActiveCircle").gameObject.activeSelf)
                {// Sprawdzamy czy wybrana postac to obecnie aktywna postac. Jesli tak to nic nie robimy bo nie wybierzemy obecnie aktywnej.
                    Debug.Log("Aktywna postac");
                }
                else
                {//Jesli jest to nowa postac, podbmieniamy "podsiwetlenie" wybranej postaci i podbmieniamy ja w BattleHandlerze
                    CharacterBattle oldSelectedCharacter = battleHandler.selectedCharacter.GetComponent<CharacterBattle>();
                    oldSelectedCharacter.HideSelectionCircle();


                    if (characterStats.isalive == false)
                    {
                        Debug.Log("Martwa postac");
                        return;
                    }
                    battleHandler.selectedCharacter = this.gameObject;
                    ShowSelectionCircle();
                }
            }
        }
        else
        {
            if (this.gameObject.transform.Find("ActiveCircle").gameObject.activeSelf)
            {//Podobnie jak wyzej. Sprawdzamy czy wybrana postac to postac aktywna w turze. Jesli tak to nic sie nie dzieje, jesli nie to ustawiamy
                //slected postac na ta w ktora klikamy
                Debug.Log("Aktywna postac");
            }
            else
            {

                if (characterStats.isalive == false)
                {
                    Debug.Log("Martwa postac");
                    return;
                }
                battleHandler.selectedCharacter = this.gameObject;
                slectionCircleGameObject.SetActive(true);
            }
        }


        
    }

    //W tej funkcji ustawiamy wyglad spritow w odpowiednich druzynach. Animacje, odwrocenie, wyglad, tekstury itp.
    //Pozniej sie to oczywiscie bardziej rozbuduje.
    public void Setup(bool isPlayerTeam)
    {
        characterVisuals.ChangeSpriteImage(isPlayerTeam);
        if(characterStats.isalive == false)
        {
            characterVisuals.ChangeSpriteColor(Color.red);
        }
    }

    public void HideSelectionCircle()
    {
        slectionCircleGameObject.SetActive(false);
    }

    public void ShowSelectionCircle()
    {
        slectionCircleGameObject.SetActive(true);
    }

    public void HideActiveCircle()
    {
        activeCircleGameObject.SetActive(false);
    }

    public void ShowActiveCircle()
    {
        activeCircleGameObject.SetActive(true);
    }

    //////////
    //COMBAT//
    //////////

    public void Attack(CharacterBattle targetCharacterBattle)
    {
        animatorr.SetTrigger("IsShooting"); 
        characterVisuals.AttackAnimation(targetCharacterBattle);
        CharacterStats targetStats = targetCharacterBattle.GetComponent<CharacterStats>();

        Debug.Log(characterStats.charactername + " attacks " + targetStats.charactername + " with " + weaponInfo.itemName);
        int attackRoll = Random.Range(1, 11);
        int attackscore = characterStats.agility + attackRoll + weaponInfo.accuracy;

        int defenceRoll = targetStats.agility + Random.Range(1, 11);
        if(attackscore > defenceRoll)
        {
            Debug.Log($"Attack succed! Rolled {attackscore} aginst {defenceRoll}");
            int damage = CalculateDamage();
            if(attackRoll == 10)
            {
                int luckcheck = Random.Range(1, 11) + characterStats.luck - targetStats.luck;
                Debug.Log($"Rolled 10 for atack. Rolling {luckcheck} for critical");
                if (luckcheck >= 10)
                {
                    Debug.Log("Criical! Double damage!");
                    damage *= 2;
                }
            }
            targetCharacterBattle.animatorr.SetTrigger("IsHit");
            targetStats.RecieveDamage(damage);
            CheckIfKilled(targetStats);
        }
        else
        {
            Debug.Log($"Attack fails! Rolled {attackscore} aginst {defenceRoll}");
            targetCharacterBattle.animatorr.SetTrigger("IsAvoiding");
        }
       
    }

    private int CalculateDamage()
    {
        int damage = 0;
        int dice = weaponInfo.damageDices;
        int range = weaponInfo.damageRange;
        int bonus = weaponInfo.damageBonus;
        for(int i = 0; i < dice; i++)
        {
            damage += Random.Range(1, range);
        }
        damage += bonus;
        Debug.Log($"Rolling {dice}d{range}+{bonus} for damage. Result: {damage}");
        return damage;
    }

    public void CheckIfKilled(CharacterStats targetCharacterStats)
    {
        if(targetCharacterStats.health <= 0)
        {
            //Dead(targetCharacterBattle);
            targetCharacterStats.animatorr.SetTrigger("IsDead");
            targetCharacterStats.isalive = false;
            targetCharacterStats.gameObject.GetComponent<CharacterVisuals>().ChangeSpriteColor(Color.red);
            //Dodajemy expa za zabicie przeciwnika
            BattleHandler.GetInstance().activeCharacter.GetComponent<CharacterStats>().experience += targetCharacterStats.experience;

            if (targetCharacterStats.isplayerteam)
            {
                // U W A G A
                // Poki co szukamy po imieniu. Bedzie problem jesli 2 postaci beda mialy takie samo imie.
                // Pozniej trzeba dodac jaki unikalny identyfikator.
                PlayerInfo playerInfo = PlayerInfo.GetInstance();
                GameObject deadCharacter = playerInfo.CharactersInActiveTeam.Find((x) => x.GetComponent<CharacterStats>().charactername == targetCharacterStats.charactername);
                playerInfo.CharactersInActiveTeam.Remove(deadCharacter);
                playerInfo.RecruitedCharacters.Remove(deadCharacter);
                Destroy(deadCharacter);

                BattleHandler battleHandler = BattleHandler.GetInstance();
                GameObject deadCharacterBattle = battleHandler.charactersListinbattle.Find((x) => x.GetComponent<CharacterStats>().charactername == targetCharacterStats.charactername);
                battleHandler.charactersListinbattle.Remove(deadCharacterBattle);
            }


        }
    }

    public void Dead(CharacterBattle targetCharacterBattle)
    {
        targetCharacterBattle.animatorr.SetTrigger("IsDead");
    }

    //to chyba b�dzie do wywalenia, ale zobaczymy.
    /*public void AttackAnimationTrigger(bool isPlayerTeam)
    {
        characterVisuals.AttackAnimation(isPlayerTeam);
    }
    */
}
