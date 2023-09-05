using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MissionSelectionHandler : MonoBehaviour
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

}
