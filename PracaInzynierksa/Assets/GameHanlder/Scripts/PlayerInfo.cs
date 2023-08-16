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
    //Prefab GameObjectu przechowywujacego informacje miedzy scenami o pojedynczej postaci
    public GameObject playerCharacterPreFab;
    //Lista wszystkich zrekrutowanych postaci
    public List<GameObject> CharactersInActiveTeam;
    //Lista postaci ktore bierzemy na misje
    public List<GameObject> RecruitedCharacters;
    //Waluta i statysytki zmieniajace sie po ulepszeniu statku
    public int playerMoney;
    public int recruitslimit = 3;
    public int crewlimit = 5;
    public int teamlimit = 3;
    public bool betterrecruits = false;

    private void Awake()
    {
        instance = this; // Singleton
    }
}
