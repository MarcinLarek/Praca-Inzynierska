using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : MonoBehaviour
{
    public Sprite IconDPS;
    public Sprite IconTank;
    public Sprite IconSupport;
    public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    //Wczytujemy grafike ikony przypisanej do klasy
    public void SetIcon()
    {
        switch (this.GetComponent<CharacterStats>().classname)
        {
            case (CharacterStats.Classes.DMG):
                image.sprite = IconDPS;
                break;
            case (CharacterStats.Classes.TANK):
                image.sprite = IconTank;
                break;
            case (CharacterStats.Classes.SUPPORT):
                image.sprite = IconSupport;
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
            image.color = Color.green;
        }
        else
        {
            image.color = Color.white;
        }
    }
}
