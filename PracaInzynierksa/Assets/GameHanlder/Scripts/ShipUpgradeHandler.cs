using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUpgradeHandler : MonoBehaviour
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

}
