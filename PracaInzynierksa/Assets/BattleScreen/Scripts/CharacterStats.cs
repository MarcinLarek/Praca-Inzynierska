using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //To plik od statysytk i informacji postaci. Te statystyki bedzie sie przenosic miedzy scenami


    public string charactername;
    public Classes classname;
    public bool isplayerteam;
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
    


}
