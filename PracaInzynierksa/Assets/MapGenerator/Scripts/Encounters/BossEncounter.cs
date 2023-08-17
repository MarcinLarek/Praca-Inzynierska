using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEncounter : MonoBehaviour
{
    public GameObject enmyInfoPrefab;
    private int enemyCounter;
    private BattleScreenHandler battleScreenHandler;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            battleScreenHandler.bossFight = true;
            MoveToBattleScene();
        }
    }

    private void Awake()
    {
        enemyCounter = Random.Range(1, 3);
        battleScreenHandler = BattleScreenHandler.GetInstance();
    }

    private void GenerateEnemies()
    {
        GameObject enemy;
        GameObject bossEnemy;
        bossEnemy = Instantiate(enmyInfoPrefab, Vector3.zero, Quaternion.identity);
        CharacterStats bossEnemyStats = bossEnemy.GetComponent<CharacterStats>();
        bossEnemyStats.maxHealth = 50;
        bossEnemyStats.health = 50;
        bossEnemyStats.endurance = 1;
        bossEnemyStats.experience = 100;
        bossEnemyStats.classname = CharacterStats.Classes.TANK;
        battleScreenHandler.enemyCharactersList.Add(bossEnemy);


        for (int i = 0; i < enemyCounter; i++)
        {
            enemy = Instantiate(enmyInfoPrefab, Vector3.zero, Quaternion.identity);
            battleScreenHandler.enemyCharactersList.Add(enemy);
        }

    }

    private void MoveToBattleScene()
    {
        //Pierw w Handlerze zapisujemy liste pokoi, encounterow i pozycje gracza aby przenies
        MapGeneratorHandler instance = MapGeneratorHandler.GetInstance();
        RoomTemplates templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        instance.rooms = templates.rooms;
        instance.encountersList = templates.encountersList;
        instance.player = GameObject.FindGameObjectWithTag("Player");
        //Teraz do wszystkich elementow ustawiamy wartosc SetActive na false aby nie wyswietlaly sie w nastepnej scenie.
        //Ustawiamy rowniez DontDestroyOnLoad() aby nie niszycz ich przy przechodzeniu do sceny walki

        DontDestroyOnLoad(instance.player);
        instance.player.SetActive(false);

        foreach (GameObject room in instance.rooms)
        {
            if (room != null)
            {
                DontDestroyOnLoad(room);
                room.SetActive(false);
            }
        }

        foreach (GameObject encounter in instance.encountersList)
        {
            if (encounter != null)
            {
                if (this.gameObject == encounter)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    DontDestroyOnLoad(encounter);
                    encounter.SetActive(false);
                }

            }
        }
        //Zmieniamy wartosc mapGenerated na true znajdujaca sie w naszym GameHandlerze aby scena BattleScreen wiedzia
        //ze powracamy do niej ze sceny walki a nie wchodzimy na czysto z misji.
        GenerateEnemies();
        instance.mapGenerated = true;
        SceneManager.LoadScene(sceneName: "BattleScreen");
    }

}


