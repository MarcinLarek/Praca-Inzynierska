using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Vector3 position = new Vector3(-140, 70);
        foreach (GameObject playerCharacter in PlayerInfo.GetInstance().RecruitedCharacters)
        {
            GameObject character = CharacterTemplate;
            GameObject spawnedCharacter = Instantiate(character, position, Quaternion.identity);
            position.y -= 30;

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
        SpriteRenderer portrait = characterPortrait.GetComponent<SpriteRenderer>();
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
    }

    public void SaveCharacterStats()
    {
        foreach (GameObject playerCharacter in playerTeamIconList)
        {

            GameObject stats = PlayerInfo.GetInstance().RecruitedCharacters.Find((x) => x.GetComponent<CharacterStats>().charactername == playerCharacter.GetComponent<CharacterStats>().charactername);
            stats.GetComponent<CharacterStats>().CopyStats(playerCharacter.GetComponent<CharacterStats>());
        }
        GameObject.Find("Canvas/StatsPanel/AcceptChangesButton").SetActive(false);
    } 

    public void MainHubButton()
    {
        SceneManager.LoadScene(sceneName: "MainHub");
    }

}
