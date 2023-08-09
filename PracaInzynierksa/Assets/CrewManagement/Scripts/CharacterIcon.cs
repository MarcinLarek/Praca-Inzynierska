using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterIcon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite IconDPS;
    public Sprite IconTank;
    public Sprite IconSupport;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Wczytujemy grafike ikony przypisanej do klasy
    public void SetIcon()
    {
        switch (this.GetComponent<CharacterStats>().classname)
        {
            case (CharacterStats.Classes.DMG):
                spriteRenderer.sprite = IconDPS;
                break;
            case (CharacterStats.Classes.TANK):
                spriteRenderer.sprite = IconTank;
                break;
            case (CharacterStats.Classes.SUPPORT):
                spriteRenderer.sprite = IconSupport;
                break;
        }
        ToggleActiveTeamVisuals();
    }

    //Zmieniamy wyglad Ikonek postaci ktore zostaly dodane do druzyny 
    //Obecnie po prostu zmieniamy kolor na zielony. Pozniej trzeba zrobic cos innego
    public void ToggleActiveTeamVisuals()
    {
        if (this.GetComponent<CharacterStats>().inactiveteam)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
