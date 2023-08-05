using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static CharacterStats;
using Random = UnityEngine.Random;

public class CrewManager : MonoBehaviour
{
    public List<GameObject> RecruitableCharacters;
    public GameObject CharacterTemplate;
    private int recruitsAmmount = 5;
    private static CrewManager instance;
    public GameObject activeCharacter;

    public static CrewManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this; // Singleton
        GenerateCharatersToRecruit();
        GeneratePlayerCharacters();
    }
    private static string[] firstNames = {
        "John", "Jane", "Michael", "Emily", "William", "Olivia", "James", "Sophia", "Robert", "Emma",
        "David", "Isabella", "Joseph", "Ava", "Daniel", "Mia", "Matthew", "Abigail", "Christopher", "Charlotte"
    };

    private static string[] lastNames = {
        "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Martinez",
        "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Lee"
    };
    private string GenerateRandomName()
    {
        string firstName = firstNames[Random.Range(0, firstNames.Length)];
        string lastName = lastNames[Random.Range(0, lastNames.Length)];

        return firstName + " " + lastName;
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
    }

    private void GenerateCharatersToRecruit()
    {
        Vector3 position = new Vector3(-350, 200);
        for (int i = 0; i < recruitsAmmount; i++)
        {
            GameObject character = CharacterTemplate;
            CharacterStats characterstats = character.GetComponent<CharacterStats>();
            characterstats.charactername = GenerateRandomName();
            characterstats.classname = (Classes)Random.Range(0, System.Enum.GetValues(typeof(Classes)).Length);
            characterstats.isplayerteam = false;
            characterstats.isalive = true;
            characterstats.maxHealth = Random.Range(15,21);
            characterstats.health = characterstats.maxHealth;
            characterstats.maxActionPoints = Random.Range(4, 7);
            characterstats.actionPoints = characterstats.maxActionPoints;
            characterstats.strength = Random.Range(1, 7);
            characterstats.endurance = Random.Range(1, 7);
            characterstats.agility = Random.Range(1, 7);
            characterstats.luck = Random.Range(1, 7);
            characterstats.inteligence = Random.Range(1, 7);

            characterstats.price =
                ((characterstats.strength + characterstats.endurance + characterstats.agility +
                characterstats.luck + characterstats.inteligence + characterstats.actionPoints) * 10) +
                (characterstats.maxHealth * 10);

            GameObject spawnedCharacter = Instantiate(character, position,Quaternion.identity);
            position.y -= 100;
            spawnedCharacter.name = character.name;
            spawnedCharacter.GetComponent<CharacterIcon>().SetIcon();
            RecruitableCharacters.Add(spawnedCharacter);

        }

    }

    private void GeneratePlayerCharacters()
    {
        Vector3 position = new Vector3(350, 200);
        foreach(GameObject playerCharacter in PlayerInfo.GetInstance().RecruitedCharacters)
        {
            GameObject character = CharacterTemplate;
            GameObject spawnedCharacter = Instantiate(character, position, Quaternion.identity);
            position.y -= 100;

            spawnedCharacter.name = character.name;
            


            CharacterStats characterstats = spawnedCharacter.GetComponent<CharacterStats>();
            CharacterStats playerCharacterStats = playerCharacter.GetComponent<CharacterStats>();

            AssignStats(characterstats, playerCharacterStats);

            Debug.Log(spawnedCharacter.GetComponent<CharacterStats>().classname.ToString());
            spawnedCharacter.GetComponent<CharacterIcon>().SetIcon();

        }
    }

    public void PurchaseCharacterButton()
    {
        PlayerInfo instance = PlayerInfo.GetInstance();
        CharacterStats activeCharacterStats = activeCharacter.GetComponent<CharacterStats>();
        if (instance.playerMoney >= activeCharacterStats.price){
            instance.playerMoney -= activeCharacterStats.price;
            activeCharacterStats.isplayerteam = true;
            Vector3 position = new Vector3(350, 200);
            if (instance.RecruitedCharacters.Count != 0)
            {
                position = new Vector3(instance.RecruitedCharacters.Last().transform.position.x, instance.RecruitedCharacters.Last().transform.position.y - 100);
            }
            GameObject spawnedCharacter = Instantiate(instance.playerCharacterPreFab, position, Quaternion.identity);
            AssignStats(spawnedCharacter.GetComponent<CharacterStats>(), activeCharacterStats);
            RecruitableCharacters.Remove(activeCharacter);
            activeCharacter.transform.position = position;


            PlayerInfo.GetInstance().RecruitedCharacters.Add(spawnedCharacter);
        }
        else
        {
            Debug.Log("insufficient money");
        }
        
    }

    public void MainHubButton()
    {
        SceneManager.LoadScene(sceneName: "MainHub");
    }

}
