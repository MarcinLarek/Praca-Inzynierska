using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //MissionSelectionHandler
    public bool missionsGenerated;
    public List<GameDataMission> generatedMissions = new List<GameDataMission>();
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
    public List<GameDataCharacter> generatedRecruits = new List<GameDataCharacter>();
    public bool recruitsAreGenerated;
    //InventoryHandler
    public bool traderInventoryGenerated;
    public List<GameDataItem> listItems = new List<GameDataItem>();
    public List<GameDataWeapon> listWeapons = new List<GameDataWeapon>();
    public List<GameDataArmor> listArmors = new List<GameDataArmor>();
    public List<GameDataConsumable> listConsumables = new List<GameDataConsumable>();
    //PlayerInfo
    public List<GameDataCharacter> CharactersInActiveTeam = new List<GameDataCharacter>();
    public List<GameDataCharacter> RecruitedCharacters = new List<GameDataCharacter>();
    public int playerMoney;
    public int recruitslimit;
    public int crewlimit;
    public int teamlimit;
    public bool betterrecruits;
    public GameData()
    {

    }

}
