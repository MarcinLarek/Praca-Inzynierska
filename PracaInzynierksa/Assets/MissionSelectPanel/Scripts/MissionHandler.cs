using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class MissionHandler : MonoBehaviour
{
    private static MissionHandler instance;
    public static MissionHandler GetInstance()
    {
        return instance;
    }

    public List<GameObject> missionsList;
    public GameObject spawningSpace;
    public int numberToSpawn;
    private int numberOfSpawned;

    private void Awake()
    {
        instance = this; // Singleton
        spawnMissions();
    }

    private void spawnMissions()
    {
        //Poniewaz uzyto troche mentalnej gimnastyki, mozliwe jest zespawnowanie sie minimalnego wyniku - 1 misji.
        //Na przyklad jesli mamy minimum 2 misje i druga misja umiesci sie w tym samym miejscu co pierwsza
        //to druga misja zostanie usunieta.
        numberToSpawn = Random.Range(2, 5);
        Debug.Log("Spawned missions: " + numberToSpawn);
        for (int i = 0; i < numberToSpawn; i++)
        {
            spawnSingleMission();
        }
    }

    private void spawnSingleMission()
    {
        //Bierzemy liste mozliwych misji/typow misji do wylosowania
        GameObject missionToSpawn = missionsList[Random.Range(0, missionsList.Count)];
        //Obszar na jakim moze byc wygenerowany marker
        MeshCollider renderSpace = spawningSpace.GetComponent<MeshCollider>();
        float spawnPositionX, spawnPositionY;
        Vector2 spawnposition;

        spawnPositionX = Random.Range(renderSpace.bounds.min.x, renderSpace.bounds.max.x);
        spawnPositionY = Random.Range(renderSpace.bounds.min.y, renderSpace.bounds.max.y);
        spawnposition = new Vector2(spawnPositionX, spawnPositionY);

        GameObject spawnedMarker = Instantiate(missionToSpawn, spawnposition, missionToSpawn.transform.rotation);
        numberOfSpawned += 1;
        //Przypisujemy kolejny numer po to aby przy wyrkyciu kolizji uzyc go do usuniecia tylko jednego markera
        //Obencie usuwamy ten z wiekszym numerem
        spawnedMarker.GetComponent<MissionMarkerBehaviour>().spawnedNumber = numberOfSpawned;
    }

}
