using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManagementHandler : MonoBehaviour, IDataPersistence
{
    public List<GameObject> generatedRecruits;
    public GameObject playerCharacterPreFab;
    public bool recruitsAreGenerated;
    private static CrewManagementHandler instance;

    public static CrewManagementHandler GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this; // Singleton
    }

    public void ResetRecruitsList()
    {
        recruitsAreGenerated = false;
        foreach(GameObject recruit in generatedRecruits)
        {
            Destroy(recruit);
        }
        generatedRecruits.Clear();
    }

    public void LoadData(GameData data)
    {
        this.recruitsAreGenerated = data.recruitsAreGenerated;
        //List
        this.generatedRecruits.Clear();
        foreach (GameDataCharacter dataCharacter in data.generatedRecruits)
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

            this.generatedRecruits.Add(character);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.recruitsAreGenerated = this.recruitsAreGenerated;
        //List
        data.generatedRecruits.Clear();
        foreach (GameObject character in this.generatedRecruits)
        {
            CharacterStats characterinfo = character.GetComponent<CharacterStats>();
            data.generatedRecruits.Add(new GameDataCharacter(characterinfo.characterID, characterinfo.charactername,
                characterinfo.classname, characterinfo.isplayerteam, characterinfo.isalive, characterinfo.inactiveteam,
                characterinfo.maxHealth, characterinfo.health, characterinfo.maxActionPoints, characterinfo.actionPoints,
                characterinfo.strength, characterinfo.endurance, characterinfo.agility, characterinfo.luck,
                characterinfo.inteligence, characterinfo.experience, characterinfo.price, characterinfo.bonusDamage,
                characterinfo.weaponID, characterinfo.armorID, characterinfo.consumableID
                ));
        }
    }
}
