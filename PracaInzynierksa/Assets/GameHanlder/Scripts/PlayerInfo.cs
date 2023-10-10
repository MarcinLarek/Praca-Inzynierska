using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IDataPersistence
{

    private static PlayerInfo instance;

    public static PlayerInfo GetInstance()
    {
        return instance;
    }
    //Prefab GameObjectu przechowywujacego informacje miedzy scenami o pojedynczej postaci
    public GameObject playerCharacterPreFab;
    //Lista wszystkich zrekrutowanych postaci
    public List<GameObject> CharactersInActiveTeam;
    //Lista postaci ktore bierzemy na misje
    public List<GameObject> RecruitedCharacters;
    //Waluta i statysytki zmieniajace sie po ulepszeniu statku
    public int playerMoney;
    public int recruitslimit = 2;
    public int crewlimit = 5;
    public int teamlimit = 3;
    public bool betterrecruits = false;

    private void Awake()
    {
        instance = this; // Singleton
    }

    public void LoadData(GameData data)
    {
        this.playerMoney = data.playerMoney;
        this.recruitslimit = data.recruitslimit;
        this.crewlimit = data.crewlimit;
        this.teamlimit = data.teamlimit;
        this.betterrecruits = data.betterrecruits;
        //Lists
        this.CharactersInActiveTeam.Clear();
        this.RecruitedCharacters.Clear();
        foreach (GameDataCharacter dataCharacter in data.RecruitedCharacters)
        {
            GameObject character = Instantiate(playerCharacterPreFab);
            CharacterStats characterinfo = character.GetComponent<CharacterStats>();
            characterinfo.characterID = dataCharacter.characterID;
            characterinfo.charactername = dataCharacter.charactername;
            characterinfo.classname = dataCharacter.classname;
            characterinfo.isplayerteam = dataCharacter.isplayerteam;
            characterinfo.isalive = dataCharacter.isalive;
            characterinfo.inactiveteam = dataCharacter.inactiveteam;
            characterinfo.maxHealth = dataCharacter.maxHealth;
            characterinfo.health = dataCharacter.health;
            characterinfo.maxActionPoints = dataCharacter.maxActionPoints;
            characterinfo.actionPoints = dataCharacter.actionPoints;
            characterinfo.strength = dataCharacter.strength;
            characterinfo.endurance = dataCharacter.endurance;
            characterinfo.agility = dataCharacter.agility;
            characterinfo.luck = dataCharacter.luck;
            characterinfo.inteligence = dataCharacter.inteligence;
            characterinfo.experience = dataCharacter.experience;
            characterinfo.price = dataCharacter.price;
            characterinfo.bonusDamage = dataCharacter.bonusDamage;
            characterinfo.weaponID = dataCharacter.weaponID;
            characterinfo.armorID = dataCharacter.armorID;
            characterinfo.consumableID = dataCharacter.consumableID;

            if (characterinfo.inactiveteam)
            {
                this.CharactersInActiveTeam.Add(character);
            }

            this.RecruitedCharacters.Add(character);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.playerMoney = this.playerMoney;
        data.recruitslimit = this.recruitslimit;
        data.crewlimit = this.crewlimit;
        data.teamlimit = this.teamlimit;
        data.betterrecruits = this.betterrecruits;
        //Lists
        data.RecruitedCharacters.Clear();
        foreach (GameObject character in this.RecruitedCharacters)
        {
            CharacterStats characterinfo = character.GetComponent<CharacterStats>();
            data.RecruitedCharacters.Add(new GameDataCharacter(characterinfo.characterID, characterinfo.charactername,
                characterinfo.classname, characterinfo.isplayerteam, characterinfo.isalive, characterinfo.inactiveteam,
                characterinfo.maxHealth, characterinfo.health, characterinfo.maxActionPoints, characterinfo.actionPoints,
                characterinfo.strength, characterinfo.endurance, characterinfo.agility, characterinfo.luck,
                characterinfo.inteligence, characterinfo.experience, characterinfo.price, characterinfo.bonusDamage,
                characterinfo.weaponID, characterinfo.armorID, characterinfo.consumableID
                ));
        }
    }
}
