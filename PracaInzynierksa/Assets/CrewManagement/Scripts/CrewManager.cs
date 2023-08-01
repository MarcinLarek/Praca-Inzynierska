using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
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

}
