using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using Random = UnityEngine.Random;


public class BattleHandler : MonoBehaviour
{
    //Ten plik nadzoruje przebieg walki
    private static BattleHandler instance;
    public static BattleHandler GetInstance()
    {
        return instance;
    }

    [SerializeField] private Transform pfCharacterBattle;

    public List<GameObject> charactersList;
    public List<GameObject> charactersListinbattle;
    private GameObject activeCharacter;
    public GameObject selectedCharacter;
    public GameObject ActionPointsDisplay;
    private CharacterStats.Classes activecharacterclass;
    private BattleScreenHandler battleScreenHandler;
    public GameObject enemySpawnPrefab;
    public GameObject playerSpawnPrefab;

    private int turn = 0;

    public enum LanePosition
    {
        Middle,
        Up,
        Down,
        Top,
        Bottom,
    }

    private void Awake()
    {
        battleScreenHandler = BattleScreenHandler.GetInstance();
        instance = this; // Singleton
    }

    private void Start()
    {
        PrepareCharacterList();
        CharacterSpawner();
        SetActiveCharacter();
        activeCharacter.GetComponent<ClassAbilities>().PrepareButtons(activecharacterclass);
    }

    private void Update()
    {
        ActionPointsDisplay.GetComponent<TextMeshProUGUI>().text = $"Action Points: {activeCharacter.GetComponent<CharacterStats>().actionPoints}/{activeCharacter.GetComponent<CharacterStats>().maxActionPoints}";
    }

    //Funkcja obslugujaca spawnowanie wszystkich postaci. Obsluguje od 2 do 10 postaci, po 5 na team.
    //Jesli damy wiecej niz 5 na team to beda respic "na sobie" 
    private void PrepareCharacterList()
    {
        foreach (GameObject playerCharacter in battleScreenHandler.playerCharactersList)
        {
            charactersList.Add(playerCharacter);
        }

        foreach (GameObject enemyCharacter in battleScreenHandler.enemyCharactersList)
        {
            charactersList.Add(enemyCharacter);
        }

    }

    private void CharacterSpawner()
    {
        int positionPlayer = 0;
        int positionEnemy = 0;
        foreach (GameObject singlecharacter in charactersList)
        {
            CharacterStats character = singlecharacter.GetComponent<CharacterStats>();

            if (character.isplayerteam) 
                //Prosty warunek sprawdzajaczy druzyne. Pozniej w zaleznosci od ilosci postaci w druznie spawnujemy je kolejno
                //Zaczynajac od srodka
            {
                switch (positionPlayer)
                {
                    case 0:
                        SpawnCharacter(character.isplayerteam, LanePosition.Middle, singlecharacter);
                        break;
                    case 1:
                        SpawnCharacter(character.isplayerteam, LanePosition.Up, singlecharacter);
                        break;
                    case 2:
                        SpawnCharacter(character.isplayerteam, LanePosition.Down, singlecharacter);
                        break;
                    case 3:
                        SpawnCharacter(character.isplayerteam, LanePosition.Top, singlecharacter);
                        break;
                    case 4:
                        SpawnCharacter(character.isplayerteam, LanePosition.Bottom, singlecharacter);
                        break;
                }
                positionPlayer++;
            }
            else
            {
                switch (positionEnemy)
                {
                    case 0:
                        SpawnCharacter(character.isplayerteam, LanePosition.Middle, singlecharacter);
                        break;
                    case 1:
                        SpawnCharacter(character.isplayerteam, LanePosition.Up, singlecharacter);
                        break;
                    case 2:
                        SpawnCharacter(character.isplayerteam, LanePosition.Down, singlecharacter);
                        break;
                    case 3:
                        SpawnCharacter(character.isplayerteam, LanePosition.Top, singlecharacter);
                        break;
                    case 4:
                        SpawnCharacter(character.isplayerteam, LanePosition.Bottom, singlecharacter);
                        break;
                }
                positionEnemy++;
            }
        }
    }

    //Tutaj funkcja odpowiadajaca za respienie jednej, poszczegolnej postaci
    //Glownie ustala sie tutaj jej pozycje na mapie, ewentualnie mozna dodac tutaj obsluge skali
    private CharacterBattle SpawnCharacter(bool isPlayerTeam, LanePosition lanePosition, GameObject singlecharacter)
    {
        Vector3 position = new Vector3(0, 0);
        if (isPlayerTeam)
        {
            position = new Vector3(-100, -30);

            switch (lanePosition)
            {
                case LanePosition.Top:
                    position = new Vector3(position.x + 60, position.y + 5);
                    break;
                case LanePosition.Up:
                    position = new Vector3(position.x + 20, position.y + 5);
                    break;
                case LanePosition.Middle:
                    position = new Vector3(position.x, position.y);
                    break;
                case LanePosition.Down:
                    position = new Vector3(position.x + 40, position.y - 5);
                    break;
                case LanePosition.Bottom:
                    position = new Vector3(position.x + 80, position.y - 5);
                    break;
            }
        }
        else
        {
            position = new Vector3(+100, -30);

            switch (lanePosition)
            {
                case LanePosition.Top:
                    position = new Vector3(position.x - 60, position.y + 5);
                    break;
                case LanePosition.Up:
                    position = new Vector3(position.x - 20, position.y + 5);
                    break;
                case LanePosition.Middle:
                    position = new Vector3(position.x, position.y);
                    break;
                case LanePosition.Down:
                    position = new Vector3(position.x - 40, position.y - 5);
                    break;
                case LanePosition.Bottom:
                    position = new Vector3(position.x - 80, position.y - 5);
                    break;
            }
        }

        GameObject spawnedCharacter;


        //Spawnimy odpowiedni prefab gaemobjectu postaci do walki w zaleznosci od przeciwnik/swoj
        //Mozna to zas ogarnac na jednym prefabie ale to pozniej
        if (isPlayerTeam)
        {
            spawnedCharacter = Instantiate(playerSpawnPrefab, position, Quaternion.identity);
        }
        else
        {
            spawnedCharacter = Instantiate(enemySpawnPrefab, position, Quaternion.identity);
        }

        //Kopiujemy statystyki naszej postaci przyciagnietej z GameHandlera do tej na planszy
        AssignStats(spawnedCharacter.GetComponent<CharacterStats>(), singlecharacter.GetComponent<CharacterStats>());
        CharacterBattle characterBattle = spawnedCharacter.GetComponent<CharacterBattle>();
        characterBattle.Setup(isPlayerTeam);
        charactersListinbattle.Add(spawnedCharacter);
        return characterBattle;
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
        toCharacter.agility = fromCharacter.agility;
    }

    //Ustalamy ktora postac ma teraz ture
    private void SetActiveCharacter()
    {
        if (charactersList.Count > 0)
        {
            if(activeCharacter != null)
            {
                activeCharacter.GetComponent<CharacterBattle>().HideActiveCircle();
            }
            // Ok tutaj sprawdzamy czy czasem postac ktora ma grac w nastepnej turze nie jest czasem martwa
            // jesli jest to tak jakby skipujemy jej ture
            // To jest glupie rozwiazanie i powinno byc w funkcji EndTurn() a nie tutaj
            // Ale tam jakos nie moge tego zrobic + jest prawie 12 i umieram psychicznie
            // Wiec poki co niech bedzie tutaj
            bool stupidalivecheck = true;
            while(stupidalivecheck)
            {
                if(charactersListinbattle[turn].GetComponent<CharacterStats>().isalive == false)
                {
                    turn++;
                    if (turn >= charactersListinbattle.Count)
                    {
                        turn = 0;
                    }
                }
                else
                {
                    stupidalivecheck = false;
                    activeCharacter = charactersListinbattle[turn];
                    activecharacterclass = activeCharacter.GetComponent<CharacterStats>().classname;
                }
            }
            //Tutaj koniec tego sprawdzamia. Wszystko co do gory trzeba jakos przeniesc do EndTurn()
            CharacterBattle activeCharacterBattle = activeCharacter.GetComponent<CharacterBattle>();
            activeCharacterBattle.ShowActiveCircle();
        }
    }

    public void EndTurn()
    {
        //Sprawdzamy kiedy zakonczyc gre
        int playerAliveCounter = 0;
        int enemyAliveCounter = 0;
        foreach (GameObject singlecharacter in charactersListinbattle)
        {
            if (singlecharacter.GetComponent<CharacterStats>().isplayerteam == true && singlecharacter.GetComponent<CharacterStats>().isalive == true)
            {
                playerAliveCounter++;
            }
            if (singlecharacter.GetComponent<CharacterStats>().isplayerteam == false && singlecharacter.GetComponent<CharacterStats>().isalive == true)
            {
                enemyAliveCounter++;
            }
        }
        Debug.Log("Alive Players: " + playerAliveCounter);
        Debug.Log("Alive Enemies: " + enemyAliveCounter);

        if (playerAliveCounter == 0)
        {
            BattleEnd(false);
            return;
        }
        if (enemyAliveCounter == 0)
        {
            BattleEnd(true);
            return;
        }

        ////////////////////////////////////////////////////////

        //Podnosimy ture. Jesli numer tury jest wiekszy niz ilosc postaci to zerujemy i zaczynamy nowa kolejke
        turn++;
        if (turn >= charactersListinbattle.Count)
        {
            turn = 0;
        }

        //Jesli jakas postac byla zaznaczona przez gracza na koniec tury to ja odznaczamy
        if (selectedCharacter != null)
        {
            selectedCharacter.GetComponent<CharacterBattle>().ToggleSelectedCharacter();
        }
        SetActiveCharacter();

        //Jesli aktywna postacia jest komputer odpalamy AI komputera
        if(activeCharacter.GetComponent<CharacterStats>().isplayerteam == false)
        {
            Debug.Log("Komputer - " + activeCharacter.name);

            if (enemyAliveCounter > 0)
            {
                ComputerTurn();
            }
        }
        else // Jesli nie to updatujemy UI
        {
            activeCharacter.GetComponent<ClassAbilities>().PrepareButtons(activeCharacter.GetComponent<CharacterStats>().classname);
        }
    }

    public void ComputerTurn()
    {
        GameObject characterToAttack;
        int listId = 0;
        do
        {
            // Losujemy losowa liczbe od 0 do ilosci postaci w bitwie
            // Jesli postac jest martwa, albo jest to postac z druzyny komputera to losujemy ponownie
            listId = Random.Range(0, charactersListinbattle.Count);
            if(charactersListinbattle[listId].GetComponent<CharacterStats>().isplayerteam == true && charactersListinbattle[listId].GetComponent<CharacterStats>().isalive == true)
            {
                break;
            }
        } while (true);
        // Jesli trafiamy na zywa postac gracza wykonujemy akcje komputera
        // Obecnie po prostu zwykly atak, pozniej mozna tutaj dac jakies bardzeij skomplikowane AI 
        characterToAttack = charactersListinbattle[listId];
        activeCharacter.GetComponent<CharacterBattle>().Attack(characterToAttack.GetComponent<CharacterBattle>());
        EndTurn();
    }

    private void BattleEnd(bool playerWin)
    {
        //Przenosimy dane z walki (HP i inne statystyki) do statysytk postaci w gameHandlerze
        foreach (GameObject enemy in battleScreenHandler.enemyCharactersList)
        {
            Destroy(enemy);
        }
        battleScreenHandler.enemyCharactersList.Clear();
        foreach (GameObject playerCharacter in battleScreenHandler.playerCharactersList)
        {
            // U W A G A
            // Poki co szukamy po imieniu. Bedzie problem jesli 2 postaci beda mialy takie samo imie.
            // Pozniej trzeba dodac jaki unikalny identyfikator.
            GameObject stats = charactersListinbattle.Find((x) => x.GetComponent<CharacterStats>().charactername == playerCharacter.GetComponent<CharacterStats>().charactername);
            AssignStats(playerCharacter.GetComponent<CharacterStats>(), stats.GetComponent<CharacterStats>());
        }
        if (playerWin)
        {
            SceneManager.LoadScene(sceneName: "MapGenerator");
        }
        else
        {
            MapGeneratorHandler.GetInstance().RemoveGeneratedScene();
            Destroy(MapGeneratorHandler.GetInstance().player);
            SceneManager.LoadScene(sceneName: "MainHub");
        }
    }

    public void AttackButton()
    {
        if(selectedCharacter!= null)
        {
            if (activeCharacter.GetComponent<CharacterStats>().actionPoints < 3)
            {
                Debug.Log("You don't have at least 3 action points");
            }
            else
            {
                if (selectedCharacter.GetComponent<CharacterStats>().isplayerteam && activeCharacter.GetComponent<CharacterStats>().isplayerteam)
                {
                    Debug.Log("Cant shoot teammates");
                }
                else
                {
                    activeCharacter.GetComponent<CharacterBattle>().Attack(selectedCharacter.GetComponent<CharacterBattle>());
                    activeCharacter.GetComponent<CharacterStats>().actionPoints -= 3;
                }
            }
        }
        else
        {
            Debug.Log("Nie wybrano celu");
        }
    }

    public void EndTurnButton()
    {
        activeCharacter.GetComponent<CharacterStats>().actionPoints = activeCharacter.GetComponent<CharacterStats>().maxActionPoints;
        EndTurn();
    }

    public void GiveActionPointButton()
    {
        if (activeCharacter.GetComponent<CharacterStats>().actionPoints < 3)
        {
            Debug.Log("You don't have at least 3 action points");
        }
        else
        {
            activeCharacter.GetComponent<CharacterStats>().actionPoints -= 3;
            selectedCharacter.GetComponent<CharacterStats>().actionPoints += 1;
        }
            
    }

    public void AbilitiOneButton()
    {
        activeCharacter.GetComponent<ClassAbilities>().AbilityDistributor(activecharacterclass, 1);
    }

    public void AbilitTwoButton()
    {
        activeCharacter.GetComponent<ClassAbilities>().AbilityDistributor(activecharacterclass, 2);
    }

    public void AbilitiThreeButton()
    {
        activeCharacter.GetComponent<ClassAbilities>().AbilityDistributor(activecharacterclass, 3);
    }
}
