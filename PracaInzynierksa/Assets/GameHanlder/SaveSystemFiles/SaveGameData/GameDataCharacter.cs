using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterStats;

[System.Serializable]
public class GameDataCharacter
{

    public int characterID;
    public string charactername;
    public Classes classname;
    public bool isplayerteam;
    public bool isalive;
    public bool inactiveteam;
    public int maxHealth;
    public int health;
    public int maxActionPoints;
    public int actionPoints;
    public int strength;
    public int endurance;
    public int agility;
    public int luck;
    public int inteligence;
    public int experience;
    public int price;
    public int bonusDamage;
    public int weaponID;
    public int armorID;
    public int consumableID;
    
    public GameDataCharacter(
        int characterID,
        string charactername,
        Classes classname,
        bool isplayerteam,
        bool isalive,
        bool inactiveteam,
        int maxHealth,
        int health,
        int maxActionPoints,
        int actionPoints,
        int strength,
        int endurance,
        int agility,
        int luck,
        int inteligence,
        int experience,
        int price,
        int bonusDamage,
        int weaponID,
        int armorID,
        int consumableID)
    {

        this.characterID = characterID;
        this.charactername = charactername;
        this.classname = classname;
        this.isplayerteam = isplayerteam;
        this.isalive = isalive;
        this.inactiveteam = inactiveteam;
        this.maxHealth = maxHealth;
        this.health = health;
        this.maxActionPoints = maxActionPoints;
        this.actionPoints = actionPoints;
        this.strength = strength;
        this.endurance = endurance;
        this.agility = agility;
        this.luck = luck;
        this.inteligence = inteligence;
        this.experience = experience;
        this.price = price;
        this.bonusDamage = bonusDamage;
        this.weaponID = weaponID;
        this.armorID = armorID;
        this.consumableID = consumableID;

        

}

}
