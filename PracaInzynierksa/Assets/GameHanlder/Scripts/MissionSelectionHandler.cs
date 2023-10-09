using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using UnityEngine;

public class MissionSelectionHandler : MonoBehaviour, IDataPersistence
{
    private static MissionSelectionHandler instance;
    public static MissionSelectionHandler GetInstance()
    {
        return instance;
    }

    public enum MissionType
    {
        Cleaning,
        Recovery,
        Destroy,
    }
    public enum DungeonType
    {
        MineTunels,
        ShipWreck,
        ScavengerBase
    }

    private void Awake()
    {
        instance = this; // Singleton
    }

    public bool missionsGenerated;
    public List<GameObject> generatedMissions;
    public GameObject MissionInfoPrefab;
    public GameObject ActiveMission;

    public void CompleteMission(bool playerwin)
    {
        missionsGenerated = false;
        if (playerwin)
        {

            foreach (GameObject character in PlayerInfo.GetInstance().CharactersInActiveTeam)
            {
                character.GetComponent<CharacterStats>().experience += ActiveMission.GetComponent<MissionInfo>().rewardEXP;
            }
            PlayerInfo.GetInstance().playerMoney += ActiveMission.GetComponent<MissionInfo>().rewardMoney;
        }

        Destroy(ActiveMission);

        generatedMissions.Clear();
    }

    public void LoadData(GameData data)
    {
        this.missionsGenerated = data.missionsGenerated;
        //List
        foreach(GameDataMission datamission in data.generatedMissions)
        {
            GameObject mission = Instantiate(MissionInfoPrefab);
            MissionInfo missioninfo = mission.GetComponent<MissionInfo>();
            missioninfo.missionID = datamission.missionID;
            missioninfo.missionType = datamission.missionType;
            missioninfo.dungeonType = datamission.dungeonType;
            missioninfo.rewardEXP = datamission.rewardEXP;
            missioninfo.rewardMoney = datamission.rewardMoney;
            Vector3 position = new Vector3(datamission.markerXPosition, datamission.markerYPosition);
            mission.transform.position = position;

            this.generatedMissions.Add(mission);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.missionsGenerated = this.missionsGenerated;
        //List
        foreach(GameObject mission in generatedMissions)
        {
            MissionInfo missioninfo = mission.GetComponent<MissionInfo>();
            data.generatedMissions.Add(new GameDataMission(
                missioninfo.missionID,
                missioninfo.missionType,
                missioninfo.dungeonType,
                missioninfo.rewardEXP,
                missioninfo.rewardMoney,
                mission.transform.position.x,
                mission.transform.position.y
                ));
        }
    }
}
