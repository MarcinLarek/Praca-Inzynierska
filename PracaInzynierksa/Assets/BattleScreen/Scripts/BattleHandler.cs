using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Funckja obslugujaca spawnowanie wszystkich postaci. Obsluguje od 2 do 10 postaci, po 5 na team.
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
            activeCharacter = charactersListinbattle[turn];
            CharacterBattle activeCharacterBattle = activeCharacter.GetComponent<CharacterBattle>();
            activeCharacterBattle.ShowActiveCircle();
        }
    }

    public void EndTurn()
    {
        turn++;
        if (turn >= charactersListinbattle.Count)
        {
            turn = 0;
        }

        SetActiveCharacter();
    }

    public void AttackButton()
    {
        if(selectedCharacter!= null)
        {
            activeCharacter.GetComponent<CharacterBattle>().Attack(selectedCharacter.GetComponent<CharacterBattle>());

        }
        else
        {
            Debug.Log("Nie wybrano celu");
        }
        

    }
}
