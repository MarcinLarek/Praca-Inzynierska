using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour
{
    //Tutaj sie wstawi obsluge animacji tekstur itp

    private SpriteRenderer spriteRenderer;

    public Sprite characterSpriteDPS;
    public Sprite characterSpriteTANK;
    public Sprite characterSpriteSUPPORT;
    public Sprite characterSpriteDRONE;


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
        if (isplayerTeam)
        {
            switch (GetComponent<CharacterStats>().classname)
            {
                case (CharacterStats.Classes.DMG):
                    spriteRenderer.sprite = characterSpriteDPS;
                    break;
                case (CharacterStats.Classes.TANK):
                    spriteRenderer.sprite = characterSpriteTANK;
                    break;
                case (CharacterStats.Classes.SUPPORT):
                    spriteRenderer.sprite = characterSpriteSUPPORT;
                    break;
            }
        }
        else
        {
            spriteRenderer.sprite = characterSpriteDRONE;
        }

        
    }

}
