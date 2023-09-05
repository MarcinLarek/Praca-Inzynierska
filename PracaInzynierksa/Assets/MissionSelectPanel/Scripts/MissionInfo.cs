using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterStats;
using static MissionSelectionHandler;
using Random = UnityEngine.Random;

public class MissionInfo : MonoBehaviour
{
    public int missionID;
    MissionType missionType;
    DungeonType dungeonType;
    public int rewardEXP;
    public int rewardMoney;

    private void Awake()
    {
        missionType = (MissionType)Random.Range(0, System.Enum.GetValues(typeof(MissionType)).Length);
        dungeonType = (DungeonType)Random.Range(0, System.Enum.GetValues(typeof(DungeonType)).Length);
        rewardMoney = Random.Range(5, 15) * 100;
        rewardEXP = Random.Range(10, 20) * 10;
    }
}
