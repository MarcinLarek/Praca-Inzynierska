using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        instance = this;
    }

    private void Start()
    {
        CharacterSpawner();
        SetActiveCharacter();
    }

    private void Update()
    {

    }

    //Funkcja obslugujaca spawnowanie wszystkich postaci. Obsluguje od 2 do 10 postaci, po 5 na team.
    //Jesli damy wiecej niz 5 na team to beda respic "na sobie" 
    private void CharacterSpawner()
    {
        int positionPlayer = 0;
        int positionEnemy = 0;
        foreach (GameObject singlecharacter in charactersList)
        {
            CharacterStats character = singlecharacter.GetComponent<CharacterStats>();

            if (character.isplayerteam)
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
    //Glownie ustala sie tutaj jej pozycje na mapie, ewentualnie mozna dodac tutaj obslug skali
    private CharacterBattle SpawnCharacter(bool isPlayerTeam, LanePosition lanePosition, GameObject singlecharacter)
    {
        Vector3 position = new Vector3(0, 0); ;
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

            

        GameObject characterTransform = Instantiate(singlecharacter, position, Quaternion.identity);
        CharacterBattle characterBattle = characterTransform.GetComponent<CharacterBattle>();
        characterBattle.Setup(isPlayerTeam);
        charactersListinbattle.Add(characterTransform);

        return characterBattle;
    }

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

        if (playerAliveCounter == 0 || enemyAliveCounter == 0)
        {
            Debug.Log("#################/#########");
            Debug.Log("----------------/----------");
            Debug.Log("----------KONIEC-----------");
            Debug.Log("---------/-----------------");
            Debug.Log("########/##################");
            return;
        }
        ////////////////////////////////////////////////////////


        turn++;
        if (turn >= charactersListinbattle.Count)
        {
            turn = 0;
        }

        if (selectedCharacter != null)
        {
            selectedCharacter.GetComponent<CharacterBattle>().ToggleSelectedCharacter();
        }
        SetActiveCharacter();

        if(activeCharacter.GetComponent<CharacterStats>().isplayerteam == false)
        {
            Debug.Log("Komputer - " + activeCharacter.name);

            if (enemyAliveCounter > 0)
            {
                ComputerTurn();
            }
        }
    }

    public void ComputerTurn()
    {
        GameObject characterToAttack;
        int listId = 0;
        do
        {
            listId = Random.Range(0, charactersListinbattle.Count);
            if(charactersListinbattle[listId].GetComponent<CharacterStats>().isplayerteam == true && charactersListinbattle[listId].GetComponent<CharacterStats>().isalive == true)
            {
                break;
            }
        } while (true);
        characterToAttack = charactersListinbattle[listId];
        activeCharacter.GetComponent<CharacterBattle>().Attack(characterToAttack.GetComponent<CharacterBattle>());
        EndTurn();
    }

    public void AttackButton()
    {
        if(selectedCharacter!= null)
        {
            activeCharacter.GetComponent<CharacterBattle>().Attack(selectedCharacter.GetComponent<CharacterBattle>());
            EndTurn();

        }
        else
        {
            Debug.Log("Nie wybrano celu");
        }
    }

}