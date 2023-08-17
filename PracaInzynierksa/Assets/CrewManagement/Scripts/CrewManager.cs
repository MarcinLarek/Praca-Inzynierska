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
    private int recruitsAmmount;
    private static CrewManager instance;
    public GameObject activeCharacter;
    private PlayerInfo playerInfo; 

    public static CrewManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this; // Singleton
        playerInfo = PlayerInfo.GetInstance();
        recruitsAmmount = playerInfo.recruitslimit;
        GenerateCharatersToRecruit(); // Tworzymy losowe postaci do rekrutacji
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
        //Pozycja od ktorej zaczynamy respic ikony postaci
        Vector3 position = new Vector3(-350, 200);
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

            GameObject spawnedCharacter = Instantiate(character, position,Quaternion.identity);
            //Z kazda respiona ikona postaci zmieniamy pozycje do nastepnego respa na osi Y o 100
            position.y -= 100;
            spawnedCharacter.name = character.name;
            //Ustalamy grafike wyswietlanej ikony
            spawnedCharacter.GetComponent<CharacterIcon>().SetIcon();
            //Dodajemy zrespiona postac do listy postaci do rekrutacji
            //Pozniej wykorzystamy ta liste aby dostepne postacie sie nie resetowaly co wejscie do sceny
            RecruitableCharacters.Add(spawnedCharacter);

        }

    }


    private void GeneratePlayerCharacters()
    {
        //Pozycja od ktorej zaczynamy respic ikony postaci
        Vector3 position = new Vector3(350, 200);
        //Pobieramy liste naszych postaci z GameHandlera
        foreach(GameObject playerCharacter in PlayerInfo.GetInstance().RecruitedCharacters)
        {
            GameObject character = CharacterTemplate;
            //Respimy gaeombejct z ikona i danymi naszej postaci
            GameObject spawnedCharacter = Instantiate(character, position, Quaternion.identity);
            position.y -= 100;

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
                //I zmieniamy poozycje ikony. Jesli jest to pierwsza psotac w naszej druzynie to respimy ja na wskazanych
                //koordynatach. Jesli mamy juz kogos to respimy go na x i y-100 ostatniej postaci w naszej liscie
                Vector3 position = new Vector3(350, 200);
                if (instance.RecruitedCharacters.Count != 0)
                {
                    position = new Vector3(instance.RecruitedCharacters.Last().transform.position.x, instance.RecruitedCharacters.Last().transform.position.y - 100);
                }
                GameObject spawnedCharacter = Instantiate(instance.playerCharacterPreFab, position, Quaternion.identity);
                spawnedCharacter.GetComponent<CharacterStats>().CopyStats(activeCharacterStats);
                activeCharacter.transform.position = position;
                //Usuwamy kupiona postac z listy postaci mozliwych do kupienia i przypisujemy do listy zrekrutowanych.
                RecruitableCharacters.Remove(activeCharacter);
                PlayerInfo.GetInstance().RecruitedCharacters.Add(spawnedCharacter);
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
                // U W A G A
                // Poki co szukamy po imieniu. Bedzie problem jesli 2 postaci beda mialy takie samo imie.
                // Pozniej trzeba dodac jaki unikalny identyfikator.
                GameObject playerCharacter = PlayerInfo.GetInstance().RecruitedCharacters.Find((x) => x.GetComponent<CharacterStats>().charactername == activeCharacter.GetComponent<CharacterStats>().charactername);
                activeCharacter.GetComponent<CharacterStats>().inactiveteam = true;
                playerCharacter.GetComponent<CharacterStats>().inactiveteam = true;
                CharactersInActiveTeam.Add(playerCharacter);
                activeCharacter.GetComponent<CharacterIcon>().ToggleActiveTeamVisuals();
            }
        }
        else
        {// Po sprawdzeniu ze postac jest juz na liscie, usuwamy ja z listy.
         // U W A G A
         // Poki co szukamy po imieniu. Bedzie problem jesli 2 postaci beda mialy takie samo imie.
         // Pozniej trzeba dodac jaki unikalny identyfikator.
            GameObject playerCharacter = PlayerInfo.GetInstance().RecruitedCharacters.Find((x) => x.GetComponent<CharacterStats>().charactername == activeCharacter.GetComponent<CharacterStats>().charactername);

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
