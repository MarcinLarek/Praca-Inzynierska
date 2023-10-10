using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIUpdater : MonoBehaviour
{
    public List<GameObject> upgradeButtons;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ButtonsUpdater();
    }

    private void ButtonsUpdater()
    {
        ShipUpgrader shipUpgrader = ShipUpgrader.GetInstance();
        ShipUpgradeHandler shipUpgradeHandler = ShipUpgradeHandler.GetInstance();
        foreach (GameObject upgradeButton in upgradeButtons)
        {
            switch (upgradeButton.name)
            {
                case "CrewUpgrade1":
                    if(shipUpgradeHandler.crewUpgrade_1)
                    {
                        //Zmienic tutaj pozniej na to, zeby przycisk mial jakas inna grafike po zakupie ulepszenia
                        //Obecnie po prostu sie wylacza
                        upgradeButton.SetActive(false);
                    }
                    else
                    {
                        UpdateSingleButton(upgradeButton, shipUpgrader.crewUpgrade_1_Cost, shipUpgrader.crewUpgrade_1_Name, shipUpgrader.crewUpgrade_1_Description);
                    }
                    break;
                case "CrewUpgrade2":
                    if(shipUpgradeHandler.crewUpgrade_2)
                    {
                        //Zmienic tutaj pozniej na to, zeby przycisk mial jakas inna grafike po zakupie ulepszenia
                        //Obecnie po prostu sie wylacza
                        upgradeButton.SetActive(false);
                    }
                    else
                    {
                        UpdateSingleButton(upgradeButton, shipUpgrader.crewUpgrade_2_Cost, shipUpgrader.crewUpgrade_2_Name, shipUpgrader.crewUpgrade_2_Description);
                    }
                    break;
                case "CrewUpgrade3":
                    if (shipUpgradeHandler.crewUpgrade_3)
                    {
                        //Zmienic tutaj pozniej na to, zeby przycisk mial jakas inna grafike po zakupie ulepszenia
                        //Obecnie po prostu sie wylacza
                        upgradeButton.SetActive(false);
                    }
                    else
                    {
                        UpdateSingleButton(upgradeButton, shipUpgrader.crewUpgrade_3_Cost, shipUpgrader.crewUpgrade_3_Name, shipUpgrader.crewUpgrade_3_Description);
                    }
                    break;
                case "RecruitUpgrade1":
                    if (shipUpgradeHandler.recruitUpgrade_1)
                    {
                        //Zmienic tutaj pozniej na to, zeby przycisk mial jakas inna grafike po zakupie ulepszenia
                        //Obecnie po prostu sie wylacza
                        upgradeButton.SetActive(false);
                    }
                    else
                    {
                        UpdateSingleButton(upgradeButton, shipUpgrader.recruitUpgrade_1_Cost, shipUpgrader.recruitUpgrade_1_Name, shipUpgrader.recruitUpgrade_1_Description);
                    }
                    break;
                case "RecruitUpgrade2":
                    if (shipUpgradeHandler.recruitUpgrade_2)
                    {
                        //Zmienic tutaj pozniej na to, zeby przycisk mial jakas inna grafike po zakupie ulepszenia
                        //Obecnie po prostu sie wylacza
                        upgradeButton.SetActive(false);
                    }
                    else
                    {
                        UpdateSingleButton(upgradeButton, shipUpgrader.recruitUpgrade_2_Cost, shipUpgrader.recruitUpgrade_2_Name, shipUpgrader.recruitUpgrade_2_Description);
                    }
                    break;
                case "RecruitUpgrade3":
                    if (shipUpgradeHandler.recruitUpgrade_3)
                    {
                        //Zmienic tutaj pozniej na to, zeby przycisk mial jakas inna grafike po zakupie ulepszenia
                        //Obecnie po prostu sie wylacza
                        upgradeButton.SetActive(false);
                    }
                    else
                    {
                        UpdateSingleButton(upgradeButton, shipUpgrader.recruitUpgrade_3_Cost, shipUpgrader.recruitUpgrade_3_Name, shipUpgrader.recruitUpgrade_3_Description);
                    }
                    break;
                case "TeamUpgrade1":
                    if (shipUpgradeHandler.teamUpgrade_1)
                    {
                        //Zmienic tutaj pozniej na to, zeby przycisk mial jakas inna grafike po zakupie ulepszenia
                        //Obecnie po prostu sie wylacza
                        upgradeButton.SetActive(false);
                    }
                    else
                    {
                        UpdateSingleButton(upgradeButton, shipUpgrader.teamUpgrade_1_Cost, shipUpgrader.teamUpgrade_1_Name, shipUpgrader.teamUpgrade_1_Description);
                    }
                    break;
                case "TeamUpgrade2":
                    if (shipUpgradeHandler.teamUpgrade_2)
                    {
                        //Zmienic tutaj pozniej na to, zeby przycisk mial jakas inna grafike po zakupie ulepszenia
                        //Obecnie po prostu sie wylacza
                        upgradeButton.SetActive(false);
                    }
                    else
                    {
                        UpdateSingleButton(upgradeButton, shipUpgrader.teamUpgrade_2_Cost, shipUpgrader.teamUpgrade_2_Name, shipUpgrader.teamUpgrade_2_Description);
                    }
                    break;
                case "BetterRecruitsUpgrade":
                    if (shipUpgradeHandler.betterRecruitsUpgrade)
                    {
                        //Zmienic tutaj pozniej na to, zeby przycisk mial jakas inna grafike po zakupie ulepszenia
                        //Obecnie po prostu sie wylacza
                        upgradeButton.SetActive(false);
                    }
                    else
                    {
                        UpdateSingleButton(upgradeButton, shipUpgrader.betterRecruitsUpgrade_Cost, shipUpgrader.betterRecruitsUpgrade_Name, shipUpgrader.betterRecruitsUpgrade_Description);
                    }
                    break;
                default:
                    Debug.Log("Wrong Button Name");
                    break;

            }
        }
    }
    private void UpdateSingleButton(GameObject upgradeButton, int price, string name, string description)
    {
        upgradeButton.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = price.ToString();
        upgradeButton.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = description;
        upgradeButton.transform.Find("Button").GetComponentInChildren<TextMeshProUGUI>().text = name;
    }
}
