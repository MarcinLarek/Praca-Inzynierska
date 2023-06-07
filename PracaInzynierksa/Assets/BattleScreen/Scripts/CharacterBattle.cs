using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    private CharacterBase characterBase;
    private State state;
    private Vector3 slideTargetPosition;
    private Action onSlideComplete;
    private enum State
    {
        Idle,
        Sliding,
        Busy,
    }

    private void Awake()
    {
        characterBase = GetComponent<CharacterBase>();
        state = State.Idle;
    }

    //W tej funkcji ustawiamy wyglad spritow w odpowiednich druzynach. Animacje, odwrocenie, wyglad, tekstury itp.
    //Pozniej sie to oczywiscie bardziej rozbuduje.
    public void Setup(bool isPlayerTeam)
    {
        if (isPlayerTeam)
        {
            characterBase.ChangeSpriteColor(Color.blue);
        }
        else
        {
            characterBase.ChangeSpriteColor(Color.red);
        }
    }

    private void Update()
    {
     switch(state)
        {
            case State.Idle:
                break;
            case State.Busy:
                break;
            case State.Sliding:
                float slideSpeed = 10f;
                transform.position += (slideTargetPosition - GetPosition()) * slideSpeed * Time.deltaTime;
                float reachedDistance = 1f;
                if (Vector3.Distance(GetPosition(), slideTargetPosition) < reachedDistance)
                {
                    //arrived at Slide Target Position
                    transform.position = slideTargetPosition;
                    onSlideComplete();
                }
                break;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackComplete)
    {
        Vector3 startingPosition = GetPosition();
        Vector3 slideTargetPosition = targetCharacterBattle.GetPosition() + (GetPosition() - targetCharacterBattle.GetPosition()).normalized *10f;

        //Podchodzimy do typa co go chcemy ciachnac
        SlideToPosition(slideTargetPosition, () =>
        {
            state = State.Busy;
            // Implementujemy atak

            // Wracamy na start
            SlideToPosition(startingPosition, () =>
            {
                state = State.Idle; 
                onAttackComplete(); //koncyzmy atak
            });
        });
    }

    private void SlideToPosition(Vector3 slideTargetPosition, Action onSlideComplete)
    {
        this.slideTargetPosition = slideTargetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
    }
}
