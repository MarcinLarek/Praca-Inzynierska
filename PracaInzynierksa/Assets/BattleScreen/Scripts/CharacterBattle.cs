using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    // W tym pliku obslugujemy postac w czasie walki
    private CharacterBase characterBase;
    private Character character;
    private State state;
    private GameObject slectionCircleGameObject;
    private enum State
    {
        Idle,
        Sliding,
        Busy,
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        characterBase = GetComponent<CharacterBase>();
        character = GetComponent<Character>();
        state = State.Idle;
        slectionCircleGameObject = transform.Find("SelectionCircle").gameObject;
        HideSelectionCircle();
    }

    //W tej funkcji ustawiamy wyglad spritow w odpowiednich druzynach. Animacje, odwrocenie, wyglad, tekstury itp.
    //Pozniej sie to oczywiscie bardziej rozbuduje.
    public void Setup(bool isPlayerTeam)
    {
        characterBase.ChangeSpriteImage(isPlayerTeam);
    }

    private void Update()
    {
    }


    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackComplete)
    {

    }

    public void HideSelectionCircle()
    {
        slectionCircleGameObject.SetActive(false);
    }

    public void ShowSelectonCircle()
    {
        slectionCircleGameObject.SetActive(true);
    }
}
