using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //To plik od statysytk i informacji postaci. Te statystyki bedzie sie przenosic miedzy scenami
    public class Stats
    {
        public int maxHealth;
        public int maxActionPoints;
        public int strength;
        public int endurance;
        public int agility;
        public int luck;
        public int inteligence;
    }

    public string charactername;
    public Classes classname;
    public Stats stats;
    public bool isplayerteam;
    public enum Classes
    {
        DMG,
        SUPPORT,
        TANK,
    }
    
    public Character(string name, Classes classname, Stats? stats )
    {
        this.charactername = name;
        this.classname = classname;
        if( stats != null )
        {
            this.stats = stats;
        }
        else
        {
            this.stats.maxHealth = 40;
            this.stats.maxActionPoints = 10;
            this.stats.strength = 5;
            this.stats.endurance = 5;
            this.stats.agility = 5;
            this.stats.luck = 5;
            this.stats.inteligence = 5;
        }
        
    }

}
