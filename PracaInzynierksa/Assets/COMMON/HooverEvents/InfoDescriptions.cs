using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoDescriptions 
{
    //STATS
    public static string labelHp = "Health";
    public static string descriptionHp = "Amount of your character Health Points. If it reach 0, you lost this character and " +
        "all its equipment";

    public static string labelAP = "Action Points";
    public static string descriptionAP = "Amount of your character Action Points. It determine what, and how many action can your " +
        "character take in one turn in the battle. Various actions cost various amount of AP";

    public static string labelStr = "Strength";
    public static string descriptionStr = "Determines your character's physical strength. Currently it has no real function";

    public static string labelEnd = "Endurance";
    public static string descriptionEnd = "Endurance determines your ability to resist damage. Combines with Armor's " +
        "Stopping Power to negate the amount of damage your character will take";

    public static string labelAgi = "Agility";
    public static string descriptionAgi = "Agility determines your character's ability to attack. In battle, you test your agility " +
        $"roll against the enemy's agility roll. That is: {Environment.NewLine} Agility + 1d10 + Weapon Accuracy";

    public static string labelLuck = "Luck";
    public static string descriptionLuck = "Luck increases your ability to land a critical hit during battle. " +
        $"If you roll a 10 on your Agility attack dice, you make a Luck check: " +
        $"{Environment.NewLine}Luck + 1d10 - Enemy's luck" +
        $"{Environment.NewLine} If your Luck check is 10 or higher, you double your damage.";

    public static string labelInt = "Inteligence";
    public static string descriptionInt = "intelligence determines the amount of healing for Support Class abilities";

    //CLASSES
    public static string labelClassDMG = "DPS";
    public static string descriptionClassDMG = "This class focuses on dealing damage. It receives normal damage on hit. " +
        $"{Environment.NewLine}DPS abilities: {Environment.NewLine}" +
        $"{Environment.NewLine}Marksman - 70% of dealing 15 damage. AP cost: 5 {Environment.NewLine}" +
        $"{Environment.NewLine}Blind Fire -  Each enemy has 50% chance of Reciving 1d20 +5 damage. AP cost: 6 {Environment.NewLine}" +
        $"{Environment.NewLine}Command - Add +3 damage to next standard attack of selected character. AP cost: 2 {Environment.NewLine}";

    public static string labelClassSUPPORT = "SUPPORT";
    public static string descriptionClassSUPPORT = "This class focuses on healing other characters damage. " +
        "It's stopping power is reduced by 2." +
        $"{Environment.NewLine}Support abilities: {Environment.NewLine}" +
        $"{Environment.NewLine}Heal Selected - Heals ally for 1d10 +INT to his maximum HP. AP cost: 3 {Environment.NewLine}" +
        $"{Environment.NewLine}Heal All -  Heals all others allies for 2d4 + INT/2 to thier max HP. Ap cost: 4 {Environment.NewLine}" +
        $"{Environment.NewLine}Overheal - Overheal ally by 8. He needs to be at max hp alredy. AP cost: 5 {Environment.NewLine}";

    public static string labelClassTANK = "TANK";
    public static string descriptionClassTANK = "This class focuses on tanking damage and helping other characters." +
        "It's stopping power is increased by 3." +
        $"{Environment.NewLine}Tank abilities: {Environment.NewLine}" +
        $"{Environment.NewLine}Sacrifice - Suffer 5 damage to heal ally for 0-10 HP. AP cost: 2 {Environment.NewLine}" +
        $"{Environment.NewLine}Boost Up -  Heal yourself for 1d10 HP. Overheal possible. AP cost: 4 {Environment.NewLine}" +
        $"{Environment.NewLine}Rally up - Give all other allies 0-3 AP. AP cost: 6 {Environment.NewLine}";

}
