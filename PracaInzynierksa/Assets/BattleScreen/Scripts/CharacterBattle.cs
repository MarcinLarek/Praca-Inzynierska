using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    // W tym pliku obslugujemy postac w czasie walki

    private CharacterVisuals characterVisuals;
    private CharacterStats characterStats;
    private GameObject slectionCircleGameObject;
    private GameObject activeCircleGameObject;
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
        slectionCircleGameObject = transform.Find("SelectionCircle").gameObject;
        activeCircleGameObject = transform.Find("ActiveCircle").gameObject;
        HideActiveCircle();
        HideSelectionCircle();
    }

    //Przelaczamy obecnie wybrana postac
    private void ToggleSelectedCharacter()
    {
        if (battleHandler.selectedCharacter != null)
        {// Sprawdzamy czy juz jakas postac jest wybrana
            if (battleHandler.selectedCharacter == this.gameObject)
            {// Sprawdzamy czy klikamy obecnie wybrana juz postac. Jesli tak to po prostu ja odznaczamy.
                HideSelectionCircle();
                battleHandler.selectedCharacter = null;
            }
            else
            {
                if (this.gameObject.transform.Find("ActiveCircle").gameObject.active)
                {// Sprawdzamy czy wybrana postac to obecnie aktywna postac. Jesli tak to nic nie robimy bo nie wybierzemy obecnie aktywnej.
                    Debug.Log("Aktywna postac");
                }
                else
                {//Jesli jest to nowa postac, podbmieniamy "podsiwetlenie" wybranej postaci i podbmieniamy ja w BattleHandlerze
                    CharacterBattle oldSelectedCharacter = battleHandler.selectedCharacter.GetComponent<CharacterBattle>();
                    oldSelectedCharacter.HideSelectionCircle();

                    battleHandler.selectedCharacter = this.gameObject;
                    ShowSelectionCircle();
                }
            }
        }
        else
        {
            if (this.gameObject.transform.Find("ActiveCircle").gameObject.active)
            {//Podobnie jak wyzej. Sprawdzamy czy wybrana postac to postac aktywna w turze. Jesli tak to nic sie nie dzieje, jesli nie to ustawiamy
                //slected postac na ta w ktora klikamy
                Debug.Log("Aktywna postac");
            }
            else
            {
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
    }

    private void Update()
    {
    }


    public void Attack(CharacterBattle targetCharacterBattle)
    {

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
}
