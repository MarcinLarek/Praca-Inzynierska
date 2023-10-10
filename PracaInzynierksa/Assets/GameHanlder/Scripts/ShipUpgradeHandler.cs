using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUpgradeHandler : MonoBehaviour, IDataPersistence
{
    private static ShipUpgradeHandler instance;
    public static ShipUpgradeHandler GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this; // Singleton
    }
    public bool crewUpgrade_1;
    public bool crewUpgrade_2;
    public bool crewUpgrade_3;

    public bool teamUpgrade_1;
    public bool teamUpgrade_2;

    public bool recruitUpgrade_1;
    public bool recruitUpgrade_2;
    public bool recruitUpgrade_3;

    public bool betterRecruitsUpgrade;

    public void LoadData(GameData data)
    {
        this.crewUpgrade_1 = data.crewUpgrade_1;
        this.crewUpgrade_2 = data.crewUpgrade_2;
        this.crewUpgrade_3 = data.crewUpgrade_3;
        this.teamUpgrade_1 = data.teamUpgrade_1;
        this.teamUpgrade_2 = data.teamUpgrade_2;
        this.recruitUpgrade_1 = data.recruitUpgrade_1;
        this.recruitUpgrade_2 = data.recruitUpgrade_2;
        this.recruitUpgrade_3 = data.recruitUpgrade_3;
        this.betterRecruitsUpgrade = data.betterRecruitsUpgrade;
    }

    public void SaveData(ref GameData data)
    {
        data.crewUpgrade_1 = this.crewUpgrade_1;
        data.crewUpgrade_2 = this.crewUpgrade_2;
        data.crewUpgrade_3 = this.crewUpgrade_3;
        data.teamUpgrade_1 = this.teamUpgrade_1;
        data.teamUpgrade_2 = this.teamUpgrade_2;
        data.recruitUpgrade_1 = this.recruitUpgrade_1;
        data.recruitUpgrade_2 = this.recruitUpgrade_2;
        data.recruitUpgrade_3 = this.recruitUpgrade_3;
        data.betterRecruitsUpgrade = this.betterRecruitsUpgrade;
    }

}
