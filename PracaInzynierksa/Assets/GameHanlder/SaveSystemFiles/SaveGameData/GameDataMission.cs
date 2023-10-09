using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MissionSelectionHandler;

[System.Serializable]
public class GameDataMission
{

    public int missionID;
    public MissionType missionType;
    public DungeonType dungeonType;
    public int rewardEXP;
    public int rewardMoney;

    public float markerXPosition;
    public float markerYPosition;

    public GameDataMission(int missionID, MissionType missionType, DungeonType dungeonType, int rewardEXP, int rewardMoney, float markerXPosition, float markerYPosition)
    {
        this.missionID = missionID;
        this.missionType = missionType;
        this.dungeonType = dungeonType;
        this.rewardEXP = rewardEXP;
        this.rewardMoney = rewardMoney;
        this.markerXPosition = markerXPosition;
        this.markerYPosition = markerYPosition;
    }
    public GameDataMission()
    {

    }
}
