using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarkerUpdater : MonoBehaviour
{
    public GameObject dungType;
    private TextMeshPro dungRewardText;
    public GameObject missType;
    private TextMeshPro missRewardText;
    public GameObject moneyReward;
    private TextMeshPro moneyRewardText;
    public GameObject expReward;
    private TextMeshPro expRewardText;


    
    private void Awake()
    {
        dungRewardText = dungType.GetComponent<TextMeshPro>();
        missRewardText = missType.GetComponent<TextMeshPro>();
        moneyRewardText = moneyReward.GetComponent<TextMeshPro>();
        expRewardText = expReward.GetComponent<TextMeshPro>();
    }



    void Update()
    {
      dungRewardText.text = this.gameObject.GetComponent<MissionInfo>().dungeonType.ToString();
      missRewardText.text = this.gameObject.GetComponent<MissionInfo>().missionType.ToString();
      moneyRewardText.text = this.gameObject.GetComponent<MissionInfo>().rewardMoney.ToString() + " $";
      expRewardText.text = this.gameObject.GetComponent<MissionInfo>().rewardEXP.ToString() + " points";
    }
}
