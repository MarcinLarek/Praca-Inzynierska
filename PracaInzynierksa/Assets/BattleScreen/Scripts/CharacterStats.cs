using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStats : MonoBehaviour
{
    //To plik od statysytk i informacji postaci. Te statystyki bedzie sie przenosic miedzy scenami


    public string charactername;
    public Classes classname;
    public bool isplayerteam;
    public bool isalive = true;
    public int maxHealth;
    public int health;
    public int maxActionPoints;
    public int actionPoints;
    public int strength;
    public int endurance;
    public int agility;
    public int luck;
    public int inteligence;
    public enum Classes
    {
        DMG,
        SUPPORT,
        TANK,
    }

    public int CalculateDamage()
    {
        int damage = 0;
        switch (classname)
        {
            case Classes.DMG:
                int DMGroll1 = Random.Range(1, 10);
                int DMGroll2 = Random.Range(1, 10);
                damage = DMGroll1 + DMGroll2 + 4;
                Debug.Log($"{charactername} rolling 2d10+4  - {DMGroll1}, {DMGroll2} - Total roll - {damage}");
                break;
            case Classes.SUPPORT:
                int SUProll1 = Random.Range(1, 8);
                int SUProll2 = Random.Range(1, 8);
                damage = SUProll1 + SUProll2;
                Debug.Log($"{charactername} rolling 2d8  - {SUProll1}, {SUProll2} - Total roll - {damage}");
                break;
            case Classes.TANK:
                int TANroll1 = Random.Range(1, 4);
                int TANroll2 = Random.Range(1, 4);
                int TANroll3 = Random.Range(1, 4);
                damage = TANroll1 + TANroll2 + TANroll3;
                Debug.Log($"{charactername} rolling 3d4  - {TANroll1}, {TANroll2}, {TANroll3} - Total roll - {damage}");
                break;
        }
        Debug.Log(charactername + " deals " + damage + " damage");
        return damage;
    }

    public void RecieveDamage(int damage)
    {
        int finaldamage = damage;
        switch (classname)
        {
            case Classes.DMG:
                finaldamage -= endurance;
                break;
            case Classes.SUPPORT:
                finaldamage -= (endurance - 2);
                break;
            case Classes.TANK:
                finaldamage -= (endurance + 4);
                break;
        }
        if (finaldamage < 0) finaldamage = 0;
        this.health -= finaldamage;
        Debug.Log(charactername + " blocks " + (damage - finaldamage) + " out of " + damage + " total damage");

    }

}
