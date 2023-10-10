using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUiManager : MonoBehaviour
{
    public void GoToMainMenuButton()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    public void SaveGameButton()
    {
        DataPersistanceManager.GetInstance().SaveGame();
    }

    public void LoadGameButton()
    {
        DataPersistanceManager.GetInstance().LoadGame();
    }

}
