using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    /*
    public MissionSelectionHandler MissionSelectionHandler;
    public ShipUpgradeHandler ShipUpgradeHandler;
    public CrewManagementHandler CrewManagementHandler;
    public InventoryHandler InventoryHandler;
    public PlayerInfo playerInfo;
    */


    //MissionSelectionHandler
    public bool missionsGenerated;
    public List<GameObject> generatedMissions;
    //ShipUpgradeHandler
    public bool crewUpgrade_1;
    public bool crewUpgrade_2;
    public bool crewUpgrade_3;
    public bool teamUpgrade_1;
    public bool teamUpgrade_2;
    public bool recruitUpgrade_1;
    public bool recruitUpgrade_2;
    public bool recruitUpgrade_3;
    public bool betterRecruitsUpgrade;
    //CrewManagementHandler
    public List<GameObject> generatedRecruits;
    public bool recruitsAreGenerated;
    //InventoryHandler
    public List<GameObject> inventoryItems;
    public bool traderInventoryGenerated;
    public List<GameObject> traderItems;
    //PlayerInfo
    public List<GameObject> CharactersInActiveTeam;
    public List<GameObject> RecruitedCharacters;
    public int playerMoney;
    public int recruitslimit;
    public int crewlimit;
    public int teamlimit;
    public bool betterrecruits;
    public GameData()
    {

    }

}
