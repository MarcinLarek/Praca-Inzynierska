using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainHubButtonOperator : MonoBehaviour
{
    public void GoToMainHubButton()
    {
        SceneManager.LoadScene(sceneName: "MainHub");
    }
}
