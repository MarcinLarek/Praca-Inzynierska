using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour
{
    //Tutaj sie wstawi obsluge animacji tekstur itp

    private SpriteRenderer spriteRenderer;
    public Sprite playerTeamSprite;
    public Sprite enemyTeamSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Poki co po prostu zmieniamy kolor spirta. To sie zas wywali i dorobi funkcje wcyztujace teksutry i animacje.
    public void ChangeSpriteColor(Color newColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
    }

    public void ChangeSpriteImage(bool isplayerTeam)
    {
        if (spriteRenderer != null)
        {
            if (isplayerTeam)
            {
                spriteRenderer.sprite = playerTeamSprite;
            }
            else
            {
                spriteRenderer.sprite = enemyTeamSprite;
            }
        }
    }

}
