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
}
