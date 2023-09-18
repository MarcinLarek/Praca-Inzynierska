using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUiStatsUpdater : MonoBehaviour
{
    public GameObject playerMoney;
    private TextMeshProUGUI playerMoneyText;

    public GameObject crew;
    private TextMeshProUGUI crewText;

    public GameObject team;
    private TextMeshProUGUI teamText;

    private PlayerInfo instance;

    private void Awake()
    {
        playerMoneyText = playerMoney.GetComponent<TextMeshProUGUI>();
        crewText = crew.GetComponent<TextMeshProUGUI>();
        teamText = team.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        instance = PlayerInfo.GetInstance();
    }

    private void Update()
    {

        playerMoneyText.text = instance.playerMoney.ToString();
        crewText.text = $"{PlayerInfo.GetInstance().RecruitedCharacters.Count.ToString()}/{PlayerInfo.GetInstance().crewlimit.ToString()}";
        teamText.text = $"{instance.CharactersInActiveTeam.Count.ToString()} / {instance.teamlimit}";
    }

}
