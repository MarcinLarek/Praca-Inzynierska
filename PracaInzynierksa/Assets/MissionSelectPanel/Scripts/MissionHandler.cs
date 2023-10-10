using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Device;

public class MissionHandler : MonoBehaviour
{
    private static MissionHandler instance;
    public static MissionHandler GetInstance()
    {
        return instance;
    }

    public GameObject spawningSpace;
    public GameObject MissionMarker;
    public int numberToSpawn;
    private int numberOfSpawned;
    public List<GameObject> generatedMarkers;
    MissionSelectionHandler missionSelectionHandler;
    

    private void Awake()
    {
        instance = this; // Singleton
        missionSelectionHandler = MissionSelectionHandler.GetInstance();
        PrepareMissions();
    }
    public void PrepareForMission(MissionMarkerBehaviour chosenMission)
    {
        RemoveMarkers();
        SaveChosenMission(chosenMission);
    }

    public void RemoveMarkers()
    {
        foreach(GameObject marker in generatedMarkers)
        {
            Destroy(marker);
        }
        generatedMarkers.Clear();
    }


    private void SaveChosenMission(MissionMarkerBehaviour chosenMission)
    {
        foreach(GameObject mission in missionSelectionHandler.generatedMissions)
        {
            if (mission.GetComponent<MissionInfo>().missionID == chosenMission.spawnedNumber)
            {
                MissionSelectionHandler.GetInstance().ActiveMission = mission;
            }
            else
            {
                Destroy(mission);
            }
        }
    }

    private void PrepareMissions()
    {
        if (MissionSelectionHandler.GetInstance().missionsGenerated == false)
        {
            SpawnMissions();
            MissionSelectionHandler.GetInstance().missionsGenerated = true;
        }
        else
        {
            LoadMissions();
        }
    }

    private void LoadMissions()
    {
        foreach (GameObject missions in missionSelectionHandler.generatedMissions)
        {
            GameObject spawnedMarker = Instantiate(MissionMarker, missions.transform.position, missions.transform.rotation);
            spawnedMarker.GetComponent<MissionMarkerBehaviour>().spawnedNumber = missions.GetComponent<MissionInfo>().missionID;
            MissionInfo spawnedMarkerinfo = spawnedMarker.GetComponent<MissionInfo>();
            MissionInfo missionsInfo = missions.GetComponent<MissionInfo>();
            spawnedMarkerinfo.dungeonType = missionsInfo.dungeonType;
            spawnedMarkerinfo.missionType = missionsInfo.missionType;
            spawnedMarkerinfo.missionID = missionsInfo.missionID;
            spawnedMarkerinfo.rewardEXP = missionsInfo.rewardEXP    ;
            spawnedMarkerinfo.rewardMoney = missionsInfo.rewardMoney;
            generatedMarkers.Add(spawnedMarker);
        }
    }

    private void SpawnMissions()
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
        GameObject missionToSpawn = MissionSelectionHandler.GetInstance().MissionInfoPrefab;
        missionToSpawn.GetComponent<MissionInfo>().missionID = numberOfSpawned;

        GameObject markerToSpawn = MissionMarker;


        //Obszar na jakim moze byc wygenerowany marker
        MeshCollider renderSpace = spawningSpace.GetComponent<MeshCollider>();
        float spawnPositionX, spawnPositionY;
        Vector2 spawnposition;

        spawnPositionX = Random.Range(renderSpace.bounds.min.x, renderSpace.bounds.max.x);
        spawnPositionY = Random.Range(renderSpace.bounds.min.y, renderSpace.bounds.max.y);
        spawnposition = new Vector2(spawnPositionX, spawnPositionY);

        GameObject spawnedMarker = Instantiate(markerToSpawn, spawnposition, markerToSpawn.transform.rotation);
        generatedMarkers.Add(spawnedMarker);
        GameObject spawnedMission = Instantiate(missionToSpawn, spawnposition, missionToSpawn.transform.rotation);

        MissionInfo spawnedMarkerinfo = spawnedMarker.GetComponent<MissionInfo>();
        MissionInfo missionsInfo = spawnedMission.GetComponent<MissionInfo>();
        missionsInfo.dungeonType = spawnedMarkerinfo.dungeonType;
        missionsInfo.missionType = spawnedMarkerinfo.missionType;
        missionsInfo.missionID = spawnedMarkerinfo.missionID;
        missionsInfo.rewardEXP = spawnedMarkerinfo.rewardEXP;
        missionsInfo.rewardMoney = spawnedMarkerinfo.rewardMoney;

        missionSelectionHandler.generatedMissions.Add(spawnedMission);

        numberOfSpawned += 1;
        //Przypisujemy kolejny numer po to aby przy wyrkyciu kolizji uzyc go do usuniecia tylko jednego markera
        //Obencie usuwamy ten z wiekszym numerem
        spawnedMarker.GetComponent<MissionMarkerBehaviour>().spawnedNumber = numberOfSpawned;
        spawnedMission.GetComponent<MissionInfo>().missionID = spawnedMarker.GetComponent<MissionMarkerBehaviour>().spawnedNumber;
    }

}
