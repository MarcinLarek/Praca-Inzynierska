using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    private int recruitsAmmount;
    private static CrewManager instance;
    public GameObject activeCharacter;
    private PlayerInfo playerInfo;
    public GameObject recruitsScrollableList;
    public GameObject crewScrollableList;
    private CrewManagementHandler crewManagementHandler;

    public TextMeshProUGUI header;
    public TextMeshProUGUI description;

    public static CrewManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this; // Singleton
        playerInfo = PlayerInfo.GetInstance();
        recruitsAmmount = playerInfo.recruitslimit;
        crewManagementHandler = CrewManagementHandler.GetInstance();
        if (crewManagementHandler.recruitsAreGenerated == true)
        {
            LoadRecruitsCharacters();
        }
        else
        {
            crewManagementHandler.recruitsAreGenerated = true;
            GenerateCharatersToRecruit(); // Tworzymy losowe postaci do rekrutacji
        }

        GeneratePlayerCharacters(); // Wczytujemy liste zrekrtutowanych postaci gracza
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

  
    private CharacterStats NormalRandomStats(CharacterStats characterstats)
    {
        characterstats.charactername = GenerateRandomName();
        characterstats.classname = (Classes)Random.Range(0, System.Enum.GetValues(typeof(Classes)).Length);
        characterstats.isplayerteam = false;
        characterstats.isalive = true;
        characterstats.maxHealth = Random.Range(10, 16);
        characterstats.health = characterstats.maxHealth;
        characterstats.maxActionPoints = Random.Range(3, 6);
        characterstats.actionPoints = characterstats.maxActionPoints;
        characterstats.strength = Random.Range(1, 5);
        characterstats.endurance = Random.Range(1, 5);
        characterstats.agility = Random.Range(1, 5);
        characterstats.luck = Random.Range(1, 5);
        characterstats.inteligence = Random.Range(1, 5);

        return characterstats;
    }

    private CharacterStats BetterRandomStats(CharacterStats characterstats)
    {
        characterstats.charactername = GenerateRandomName();
        characterstats.classname = (Classes)Random.Range(0, System.Enum.GetValues(typeof(Classes)).Length);
        characterstats.isplayerteam = false;
        characterstats.isalive = true;
        characterstats.maxHealth = Random.Range(15, 21);
        characterstats.health = characterstats.maxHealth;
        characterstats.maxActionPoints = Random.Range(4, 7);
        characterstats.actionPoints = characterstats.maxActionPoints;
        characterstats.strength = Random.Range(2, 8);
        characterstats.endurance = Random.Range(2, 8);
        characterstats.agility = Random.Range(2, 8);
        characterstats.luck = Random.Range(2, 7);
        characterstats.inteligence = Random.Range(2, 8);

        return characterstats;
    }
    private void GenerateCharatersToRecruit()
    {
        for (int i = 0; i < recruitsAmmount; i++)
        {
            //Dajemy losowe statystyki rekrutom
            GameObject character = CharacterTemplate;
            CharacterStats characterstats = character.GetComponent<CharacterStats>();
            if (playerInfo.betterrecruits)
            {
                characterstats = BetterRandomStats(characterstats);
            }
            else
            {
                characterstats = NormalRandomStats(characterstats);
            }

            //Cena kupna postaci zalezy od kombinacji jej statystyk. Mozna zrobic pozniej inny przelicznik
            characterstats.price =
                ((characterstats.strength + characterstats.endurance + characterstats.agility +
                characterstats.luck + characterstats.inteligence + characterstats.actionPoints) * 10) +
                (characterstats.maxHealth * 10);

            GameObject spawnedCharacter = Instantiate(character, new Vector3(0, 0), Quaternion.identity);

            //Start - HooverInfoThings
            //Robimy referencje aby wiedzialo ktore teksty zmieniac przy najechaniu myszka
            HooverEvent spawnedCharacterHooverInfo = spawnedCharacter.GetComponent<HooverEvent>();
            spawnedCharacterHooverInfo.header = header;
            spawnedCharacterHooverInfo.description = description;
            //End - HooverInfoThings

            //Dodajemy do listy przewijanej rekrutow
            spawnedCharacter.transform.SetParent(recruitsScrollableList.transform);

            spawnedCharacter.name = character.name;
            //Ustalamy grafike wyswietlanej ikony
            spawnedCharacter.GetComponent<CharacterIcon>().SetIcon();
            //Dodajemy zrespiona postac do listy postaci do rekrutacji
            //Pozniej wykorzystamy ta liste aby dostepne postacie sie nie resetowaly co wejscie do sceny
            RecruitableCharacters.Add(spawnedCharacter);


            GameObject gameHandlerCharacter = Instantiate(crewManagementHandler.playerCharacterPreFab, new Vector3(0, 0), Quaternion.identity);
            gameHandlerCharacter.name = crewManagementHandler.playerCharacterPreFab.name;
            gameHandlerCharacter.GetComponent<CharacterStats>().CopyStats(spawnedCharacter.GetComponent<CharacterStats>());
            crewManagementHandler.generatedRecruits.Add(gameHandlerCharacter);

        }

    }

    private void LoadRecruitsCharacters()
    {
        foreach (GameObject recruitCharacter in crewManagementHandler.generatedRecruits)
        {
            GameObject character = CharacterTemplate;
            //Respimy gaeombejct z ikona i danymi naszej postaci
            GameObject spawnedCharacter = Instantiate(character, new Vector3(0, 0), Quaternion.identity);

            //Start - HooverInfoThings
            //Robimy referencje aby wiedzialo ktore teksty zmieniac przy najechaniu myszka
            HooverEvent spawnedCharacterHooverInfo = spawnedCharacter.GetComponent<HooverEvent>();
            spawnedCharacterHooverInfo.header = header;
            spawnedCharacterHooverInfo.description = description;
            //End - HooverInfoThings

            spawnedCharacter.transform.SetParent(recruitsScrollableList.transform);

            spawnedCharacter.name = character.name;
            CharacterStats characterstats = spawnedCharacter.GetComponent<CharacterStats>();
            CharacterStats recruitCharacterStats = recruitCharacter.GetComponent<CharacterStats>();
            //Kopiujemy dane z obiekut w GameHandlerze do zrespionego obieku w Scenie, po czym ustawiamy odpowiednia
            //ikone
            characterstats.CopyStats(recruitCharacterStats);
            spawnedCharacter.GetComponent<CharacterIcon>().SetIcon();

        }
    }
    private void GeneratePlayerCharacters()
    {
        //Pobieramy liste naszych postaci z GameHandlera
        foreach(GameObject playerCharacter in PlayerInfo.GetInstance().RecruitedCharacters)
        {
            GameObject character = CharacterTemplate;
            //Respimy gaeombejct z ikona i danymi naszej postaci
            GameObject spawnedCharacter = Instantiate(character, new Vector3(0, 0), Quaternion.identity);

            //Start - HooverInfoThings
            //Robimy referencje aby wiedzialo ktore teksty zmieniac przy najechaniu myszka
            HooverEvent spawnedCharacterHooverInfo = spawnedCharacter.GetComponent<HooverEvent>();
            spawnedCharacterHooverInfo.header = header;
            spawnedCharacterHooverInfo.description = description;
            //End - HooverInfoThings

            spawnedCharacter.transform.SetParent(crewScrollableList.transform);

            spawnedCharacter.name = character.name;
            CharacterStats characterstats = spawnedCharacter.GetComponent<CharacterStats>();
            CharacterStats playerCharacterStats = playerCharacter.GetComponent<CharacterStats>();
            //Kopiujemy dane z obiekut w GameHandlerze do zrespionego obieku w Scenie, po czym ustawiamy odpowiednia
            //ikone
            characterstats.CopyStats(playerCharacterStats);
            spawnedCharacter.GetComponent<CharacterIcon>().SetIcon();

        }
    }

    public void PurchaseCharacterButton()
    {
        PlayerInfo instance = PlayerInfo.GetInstance();
        CharacterStats activeCharacterStats = activeCharacter.GetComponent<CharacterStats>();
        //Sprawdzamy czy nie przekrocilismy limut zrekrutowanych postaci
        if (instance.RecruitedCharacters.Count <= instance.crewlimit)
        {
            //Sprawdzamy czy nas stac
            if (instance.playerMoney >= activeCharacterStats.price)
            {
                instance.playerMoney -= activeCharacterStats.price;
                //Jesli tak przypisujemy go do naszej druzyny
                activeCharacterStats.isplayerteam = true;

                //Dodajemy do listy przewijanej zalogi
                activeCharacterStats.transform.SetParent(crewScrollableList.transform);

                // Szukamy odpowiednia postac po ID
                GameObject purchasedCharacterGameHandler = CrewManagementHandler.GetInstance().generatedRecruits.Find((x) => x.GetComponent<CharacterStats>().characterID == activeCharacter.GetComponent<CharacterStats>().characterID);
                purchasedCharacterGameHandler.GetComponent<CharacterStats>().CopyStats(activeCharacterStats);
                //Usuwamy kupiona postac z listy postaci mozliwych do kupienia i przypisujemy do listy zrekrutowanych.
                RecruitableCharacters.Remove(activeCharacter);
                CrewManagementHandler.GetInstance().generatedRecruits.Remove(purchasedCharacterGameHandler);
                PlayerInfo.GetInstance().RecruitedCharacters.Add(purchasedCharacterGameHandler);
            }
            else
            {
                //TODO: Dodac jakies fancy rzeczy jak nas nie stac na zakup
                Debug.Log("insufficient money");
            }
        }
        else
        {
            Debug.Log("Team Limit Reached");
        }
        
    }

    //Funkcja dodajaca zrekrutowana postac do listy postaci wybierajacyh sie na nastepna misje. 
    public void TeamSwitchButton()
    {
        List<GameObject> CharactersInActiveTeam = PlayerInfo.GetInstance().CharactersInActiveTeam;
        if (activeCharacter.GetComponent<CharacterStats>().inactiveteam == false)
        { //Po sprawdzeniu ze postaci nie ma jeszcze w liscie, dodajemy ja na liste.
            if (CharactersInActiveTeam.Count() >= playerInfo.teamlimit)
            {
                Debug.Log("Team Limit Reached");
            }
            else
            {
                // Szukamy postac po ID
                GameObject playerCharacter = PlayerInfo.GetInstance().RecruitedCharacters.Find((x) => x.GetComponent<CharacterStats>().characterID == activeCharacter.GetComponent<CharacterStats>().characterID);
                activeCharacter.GetComponent<CharacterStats>().inactiveteam = true;
                playerCharacter.GetComponent<CharacterStats>().inactiveteam = true;
                CharactersInActiveTeam.Add(playerCharacter);
                activeCharacter.GetComponent<CharacterIcon>().ToggleActiveTeamVisuals();
            }
        }
        else
        {// Po sprawdzeniu ze postac jest juz na liscie, usuwamy ja z listy.
         // Szukamy postac po ID
            GameObject playerCharacter = PlayerInfo.GetInstance().RecruitedCharacters.Find((x) => x.GetComponent<CharacterStats>().characterID == activeCharacter.GetComponent<CharacterStats>().characterID);

            activeCharacter.GetComponent<CharacterStats>().inactiveteam = false;
            playerCharacter.GetComponent<CharacterStats>().inactiveteam = false;
            CharactersInActiveTeam.Remove(playerCharacter);
            activeCharacter.GetComponent<CharacterIcon>().ToggleActiveTeamVisuals();
        }
    }

    public void MainHubButton()
    {
        SceneManager.LoadScene(sceneName: "MainHub");
    }

}
