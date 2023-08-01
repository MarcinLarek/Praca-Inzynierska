using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    private static PlayerInfo instance;
    public static PlayerInfo GetInstance()
    {
        return instance;
    }

    public List<GameObject> CharactersInActiveTeam;
    public List<GameObject> RecruitedCharacters;
    public int playerMoney;

    private void Awake()
    {
        instance = this; // Singleton
    }
}
