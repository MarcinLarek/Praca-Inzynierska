using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipUpgrader : MonoBehaviour
{
    private static ShipUpgrader instance;

    public static ShipUpgrader GetInstance()
    {
        return instance;
    }

    public int crewUpgrade_1_Cost;
    public string crewUpgrade_1_Name;
    public string crewUpgrade_1_Description;

    public int crewUpgrade_2_Cost;
    public string crewUpgrade_2_Name;
    public string crewUpgrade_2_Description;

    public int crewUpgrade_3_Cost;
    public string crewUpgrade_3_Name;
    public string crewUpgrade_3_Description;


    public int teamUpgrade_1_Cost;
    public string teamUpgrade_1_Name;
    public string teamUpgrade_1_Description;

    public int teamUpgrade_2_Cost;
    public string teamUpgrade_2_Name;
    public string teamUpgrade_2_Description;

    public int betterRecruitsUpgrade_Cost;
    public string betterRecruitsUpgrade_Name;
    public string betterRecruitsUpgrade_Description;


    public int recruitUpgrade_1_Cost;
    public string recruitUpgrade_1_Name;
    public string recruitUpgrade_1_Description;

    public int recruitUpgrade_2_Cost;
    public string recruitUpgrade_2_Name;
    public string recruitUpgrade_2_Description;

    public int recruitUpgrade_3_Cost;
    public string recruitUpgrade_3_Name;
    public string recruitUpgrade_3_Description;

    private void Awake()
    {
        instance = this; // Singleton

        //Todo: Zrobic nazwy dla ulepszen
        crewUpgrade_1_Cost = 1000;
        crewUpgrade_1_Name = "Crew Upgrade 1";
        crewUpgrade_1_Description = "Increase recruited charater limit to 10";

        crewUpgrade_2_Cost = 2000;
        crewUpgrade_2_Name = "Crew Upgrade 2";
        crewUpgrade_2_Description = "Increase recruited charater limit to 15";

        crewUpgrade_3_Cost = 3000;
        crewUpgrade_3_Name = "Crew Upgrade 3";
        crewUpgrade_3_Description = "Increase recruited charater limit to 20";


        teamUpgrade_1_Cost = 2000;
        teamUpgrade_1_Name = "Team Upgrade 1";
        teamUpgrade_1_Description = "You can now take 4 characters on a mission";

        teamUpgrade_2_Cost = 4000;
        teamUpgrade_2_Name = "Team Upgrade 2";
        teamUpgrade_2_Description = "You can now take 5 characters on a mission";

        betterRecruitsUpgrade_Cost = 5000;
        betterRecruitsUpgrade_Name = "Better Recruits";
        betterRecruitsUpgrade_Description = "New recruits will have better stats";


        recruitUpgrade_1_Cost = 500;
        recruitUpgrade_1_Name = "Recruit Upgrade 1";
        recruitUpgrade_1_Description = "Recruiting pool is now 5";

        recruitUpgrade_2_Cost = 1500;
        recruitUpgrade_2_Name = "Recruit Upgrade 2";
        recruitUpgrade_2_Description = "Recruiting pool is now 7";

        recruitUpgrade_3_Cost = 2500;
        recruitUpgrade_3_Name = "Recruit Upgrade 3";
        recruitUpgrade_3_Description = "Recruiting pool is now 10";

    }

    public void ButtonSelector()
    {
        PlayerInfo playerInfo = PlayerInfo.GetInstance();
        //Szukamy po nazwie GameObjcetu ktory zawiera nasz przycisk
        switch (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name)
        {
            case "CrewUpgrade1":
                if(!ShipUpgradeHandler.GetInstance().crewUpgrade_1 && BuyUpgrade(crewUpgrade_1_Cost))
                {
                    ShipUpgradeHandler.GetInstance().crewUpgrade_1 = true;

                }
                break;
            case "CrewUpgrade2":
                if (!ShipUpgradeHandler.GetInstance().crewUpgrade_2 && BuyUpgrade(crewUpgrade_2_Cost))
                {
                    ShipUpgradeHandler.GetInstance().crewUpgrade_2 = true;
                }
                break;
            case "CrewUpgrade3":
                if (!ShipUpgradeHandler.GetInstance().crewUpgrade_3 && BuyUpgrade(crewUpgrade_3_Cost))
                {
                    ShipUpgradeHandler.GetInstance().crewUpgrade_3 = true;
                }
                break;
            case "RecruitUpgrade1":
                if (!ShipUpgradeHandler.GetInstance().recruitUpgrade_1 && BuyUpgrade(recruitUpgrade_1_Cost))
                {
                    ShipUpgradeHandler.GetInstance().recruitUpgrade_1 = true;
                }
                break;
            case "RecruitUpgrade2":
                if (!ShipUpgradeHandler.GetInstance().recruitUpgrade_2 && BuyUpgrade(recruitUpgrade_2_Cost))
                {
                    ShipUpgradeHandler.GetInstance().recruitUpgrade_2 = true;
                }
                break;
            case "RecruitUpgrade3":
                if (!ShipUpgradeHandler.GetInstance().recruitUpgrade_3 && BuyUpgrade(recruitUpgrade_3_Cost))
                {
                    ShipUpgradeHandler.GetInstance().recruitUpgrade_3 = true;
                }
                break;
            case "TeamUpgrade1":
                if (!ShipUpgradeHandler.GetInstance().teamUpgrade_1 && BuyUpgrade(teamUpgrade_1_Cost))
                {
                    ShipUpgradeHandler.GetInstance().teamUpgrade_1 = true;
                }
                break;
            case "TeamUpgrade2":
                if (!ShipUpgradeHandler.GetInstance().teamUpgrade_2 && BuyUpgrade(teamUpgrade_2_Cost))
                {
                    ShipUpgradeHandler.GetInstance().teamUpgrade_2 = true;
                }
                break;
            case "BetterRecruitsUpgrade":
                if (!ShipUpgradeHandler.GetInstance().betterRecruitsUpgrade && BuyUpgrade(betterRecruitsUpgrade_Cost))
                {
                    ShipUpgradeHandler.GetInstance().betterRecruitsUpgrade = true;
                }
                break;
            default:
                Debug.Log("Wrong Button Name");
                break;

        }
    }

    private bool BuyUpgrade(int cost)
    {
        if(PlayerInfo.GetInstance().playerMoney >= cost)
        {
            PlayerInfo.GetInstance().playerMoney -= cost;
            return true;
        }
        else
        {
            Debug.Log("Insufficient money");
            return false;
        }
    }


}
