using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHubButtonsOperator : MonoBehaviour
{
    public void GoToMissionSelectionButton()
    {
        SceneManager.LoadScene(sceneName: "MissionSelection");
    }
    public void GoToTradeButton()
    {
        SceneManager.LoadScene(sceneName: "Trade");
    }
    public void GoToCrewManagementButton()
    {
        SceneManager.LoadScene(sceneName: "CrewManagement");
    }
}