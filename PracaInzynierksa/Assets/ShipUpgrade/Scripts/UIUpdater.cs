using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIUpdater : MonoBehaviour
{
    private TextMeshProUGUI moneydisplay;
    public List<GameObject> upgradeButtons;
    private void Awake()
    {
        moneydisplay = GameObject.Find("Canvas/MoneyPanel/MoneyCount").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        MoneyCounterUpdater();
        ButtonsUpdater();
    }

    private void MoneyCounterUpdater()
    {
        moneydisplay.text = PlayerInfo.GetInstance().playerMoney.ToString();
    }

    private void ButtonsUpdater()
    {
        ScriptUpgrader instance = ScriptUpgrader.GetInstance();
        foreach(GameObject upgradeButton in upgradeButtons)
        {
            switch (upgradeButton.name)
            {
                case "CrewUpgrade1":
                    UpdateSingleButton(instance.crewUpgrade_1_Cost, instance.crewUpgrade_1_Name, instance.crewUpgrade_1_Description);
                    break;
                case "CrewUpgrade2":
                    UpdateSingleButton(instance.crewUpgrade_2_Cost, instance.crewUpgrade_2_Name, instance.crewUpgrade_2_Description);
                    break;
                case "CrewUpgrade3":
                    UpdateSingleButton(instance.crewUpgrade_3_Cost, instance.crewUpgrade_3_Name, instance.crewUpgrade_3_Description);
                    break;
                case "RecruitUpgrade1":
                    UpdateSingleButton(instance.recruitUpgrade_1_Cost, instance.recruitUpgrade_1_Name, instance.recruitUpgrade_1_Description);
                    break;
                case "RecruitUpgrade2":
                    UpdateSingleButton(instance.recruitUpgrade_2_Cost, instance.recruitUpgrade_2_Name, instance.recruitUpgrade_2_Description);
                    break;
                case "RecruitUpgrade3":
                    UpdateSingleButton(instance.recruitUpgrade_3_Cost, instance.recruitUpgrade_3_Name, instance.recruitUpgrade_3_Description);
                    break;
                case "TeamUpgrade1":
                    UpdateSingleButton(instance.teamUpgrade_1_Cost, instance.teamUpgrade_1_Name, instance.teamUpgrade_1_Description);
                    break;
                case "TeamUpgrade2":
                    UpdateSingleButton(instance.teamUpgrade_2_Cost, instance.teamUpgrade_2_Name, instance.teamUpgrade_2_Description);
                    break;
                case "BetterRecruitsUpgrade":
                    UpdateSingleButton(instance.betterRecruitsUpgrade_Cost, instance.betterRecruitsUpgrade_Name, instance.betterRecruitsUpgrade_Description);
                    break;
                default:
                    Debug.Log("Wrong Button Name");
                    break;

            }
        }
    }
    private void UpdateSingleButton(int price, string name, string description)
    {

    }
}
