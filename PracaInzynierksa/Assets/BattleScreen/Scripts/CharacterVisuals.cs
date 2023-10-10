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
    public Sprite characterSpriteBOSS;

    private Animator attack;
    public Animator animatorController;
    public RuntimeAnimatorController DMGAnimController;
    public RuntimeAnimatorController TANKAnimController;
    public RuntimeAnimatorController SUPAnimController;


    public AnimationClip AttackDmg;
    public AnimationClip AttackSup;
    public AnimationClip AttackTank;
    public AnimationClip Test;

    
    
    public void Start()
    {
        // Pobierz komponent Animator
        Debug.Log("Animator Controllers:");
        Debug.Log("DMGAnimController: " + DMGAnimController);
        Debug.Log("TANKAnimController: " + TANKAnimController);
        Debug.Log("SUPAnimController: " + SUPAnimController);
    }

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
        animatorController = GetComponent<Animator>();
        if (isplayerTeam)
        {
            switch (GetComponent<CharacterStats>().classname)
            {
                case (CharacterStats.Classes.DMG):
                    spriteRenderer.sprite = characterSpriteDPS;
                 animatorController.runtimeAnimatorController = DMGAnimController;
                    break;
                case (CharacterStats.Classes.TANK):
                    spriteRenderer.sprite = characterSpriteTANK;
                  animatorController.runtimeAnimatorController = TANKAnimController;
                    break;
                case (CharacterStats.Classes.SUPPORT):
                    spriteRenderer.sprite = characterSpriteSUPPORT;
                  animatorController.runtimeAnimatorController = SUPAnimController;
                    break;
            }
        }
        else
        {
            switch (GetComponent<CharacterStats>().classname)
            {
                case (CharacterStats.Classes.DMG):
                    spriteRenderer.sprite = characterSpriteDRONE;
                    break;
                case (CharacterStats.Classes.TANK):
                    spriteRenderer.sprite = characterSpriteBOSS;
                    break;
                case (CharacterStats.Classes.SUPPORT):
                    spriteRenderer.sprite = characterSpriteDRONE;
                    break;
            }
        }

        
    }

    public void AttackAnimation (bool isplayerTeam)
    {
        attack = GetComponent<Animator>();
        string layerName = "Base Layer";
        int layerIndex = attack.GetLayerIndex(layerName);

        Debug.Log(Test);
        if (isplayerTeam)
        {
            switch (GetComponent<CharacterStats>().classname)
            {
                case (CharacterStats.Classes.DMG):
                    Debug.Log(AttackDmg);
                    attack.Play("AttackDmg", layerIndex);
                    Debug.Log("Próba udana");
                    break;
                case (CharacterStats.Classes.TANK):
                    Debug.Log(AttackTank);
                    attack.Play("AttackTank", layerIndex);
                    Debug.Log("Próba udana");
                    break;
                case (CharacterStats.Classes.SUPPORT):
                    Debug.Log(AttackSup);
                    attack.Play("AttackSup", layerIndex);
                    Debug.Log("Próba udana");
                    break;
            }
        }
        else
        {
            switch (GetComponent<CharacterStats>().classname)
            {
                case (CharacterStats.Classes.DMG):
                    Debug.Log("Brak animacji");
                    break;
                case (CharacterStats.Classes.TANK):
                    Debug.Log("Brak animacji");
                    break;
                case (CharacterStats.Classes.SUPPORT):
                    Debug.Log("Brak animacji");
                    break;
            }
        }
    }

}
