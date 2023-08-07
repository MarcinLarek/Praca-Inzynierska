using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{

    public GameObject CharacterTemplate;
    public GameObject activeCharacter;
    public GameObject characterPortrait;
    public Sprite portraitDPS;
    public Sprite portraitTank;
    public Sprite portraitSupport;
    private static UpgradeManager instance;
    public static UpgradeManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        GeneratePlayerCharacters();
    }

    private void AssignStats(CharacterStats toCharacter, CharacterStats fromCharacter)
    {
        toCharacter.charactername = fromCharacter.charactername;
        toCharacter.classname = fromCharacter.classname;
        toCharacter.isplayerteam = fromCharacter.isplayerteam;
        toCharacter.isalive = fromCharacter.isalive;
        toCharacter.maxHealth = fromCharacter.maxHealth;
        toCharacter.health = fromCharacter.health;
        toCharacter.maxActionPoints = fromCharacter.maxActionPoints;
        toCharacter.actionPoints = fromCharacter.actionPoints;
        toCharacter.strength = fromCharacter.strength;
        toCharacter.endurance = fromCharacter.endurance;
        toCharacter.agility = fromCharacter.agility;
        toCharacter.luck = fromCharacter.luck;
        toCharacter.inteligence = fromCharacter.inteligence;
        toCharacter.experience = fromCharacter.experience;
        toCharacter.price = fromCharacter.price;
        toCharacter.inactiveteam = fromCharacter.inactiveteam;
    }

    private void GeneratePlayerCharacters()
    {
        Vector3 position = new Vector3(-140, 70);
        foreach (GameObject playerCharacter in PlayerInfo.GetInstance().RecruitedCharacters)
        {
            GameObject character = CharacterTemplate;
            GameObject spawnedCharacter = Instantiate(character, position, Quaternion.identity);
            position.y -= 30;

            spawnedCharacter.name = character.name;



            CharacterStats characterstats = spawnedCharacter.GetComponent<CharacterStats>();
            CharacterStats playerCharacterStats = playerCharacter.GetComponent<CharacterStats>();

            AssignStats(characterstats, playerCharacterStats);

            Debug.Log(spawnedCharacter.GetComponent<CharacterStats>().classname.ToString());
            spawnedCharacter.GetComponent<CharacterIcon>().SetIcon();

        }


    }

    public void LoadCharacterPortrait()
    {
        SpriteRenderer portrait = characterPortrait.GetComponent<SpriteRenderer>();
        switch (activeCharacter.GetComponent<CharacterStats>().classname)
        {
            case (CharacterStats.Classes.DMG):
                portrait.sprite = portraitDPS;
                break;
            case (CharacterStats.Classes.TANK):
                portrait.sprite = portraitTank;
                break;
            case (CharacterStats.Classes.SUPPORT):
                portrait.sprite = portraitSupport;
                break;
        }
    }

    public void MainHubButton()
    {
        SceneManager.LoadScene(sceneName: "MainHub");
    }

}
