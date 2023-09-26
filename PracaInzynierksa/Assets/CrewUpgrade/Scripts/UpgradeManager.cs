using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UpgradeManager : MonoBehaviour
{

    public GameObject CharacterTemplate;
    public GameObject activeCharacter;
    public GameObject characterPortrait;
    public List<GameObject> playerTeamIconList;
    public Sprite portraitDPS;
    public Sprite portraitTank;
    public Sprite portraitSupport;
    private static UpgradeManager instance;
    public GameObject crewScrollableList;

    public static UpgradeManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        GeneratePlayerCharacters();
    }

    private void GeneratePlayerCharacters()
    {
        foreach (GameObject playerCharacter in PlayerInfo.GetInstance().RecruitedCharacters)
        {
            GameObject character = CharacterTemplate;
            GameObject spawnedCharacter = Instantiate(character, new Vector3(0, 0), Quaternion.identity);
            spawnedCharacter.transform.SetParent(crewScrollableList.transform);

            spawnedCharacter.name = character.name;



            CharacterStats characterstats = spawnedCharacter.GetComponent<CharacterStats>();
            CharacterStats playerCharacterStats = playerCharacter.GetComponent<CharacterStats>();

            characterstats.CopyStats(playerCharacterStats);

            Debug.Log(spawnedCharacter.GetComponent<CharacterStats>().classname.ToString());
            spawnedCharacter.GetComponent<CharacterIcon>().SetIcon();
            playerTeamIconList.Add(spawnedCharacter);

        }


    }

    public void LoadCharacterPortrait()
    {
        Image portrait = characterPortrait.GetComponent<Image>();
        switch (activeCharacter.GetComponent<CharacterStats>().classname)
        {
            case (CharacterStats.Classes.DMG):
                portrait.sprite = portraitDPS;
                break;
            case (CharacterStats.Classes.TANK):
                portrait.sprite = portraitTank;
                break;
            case (CharacterStats.Classes.SUPPORT):
                portrait.sprite = portraitSupport;
                break;
        }
        portrait.color = Color.white;
    }

    public void SaveCharacterStats()
    {
        foreach (GameObject playerCharacter in playerTeamIconList)
        {

            GameObject stats = PlayerInfo.GetInstance().RecruitedCharacters.Find((x) => x.GetComponent<CharacterStats>().characterID == playerCharacter.GetComponent<CharacterStats>().characterID);
            stats.GetComponent<CharacterStats>().CopyStats(playerCharacter.GetComponent<CharacterStats>());
        }
        GameObject.Find("Canvas/StatsPanel/AcceptChangesButton").SetActive(false);
    } 

    public void MainHubButton()
    {
        SceneManager.LoadScene(sceneName: "MainHub");
    }

}
